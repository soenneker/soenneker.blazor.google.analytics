using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Google.Analytics;

/// <inheritdoc cref="IGoogleAnalyticsInterop"/>
public sealed class GoogleAnalyticsInterop : IGoogleAnalyticsInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<GoogleAnalyticsInterop> _logger;
    private readonly IResourceLoader _resourceLoader;

    private readonly AsyncSingleton _scriptInitializer;

    private const string _modulePath = "Soenneker.Blazor.Google.Analytics/js/googleanalyticsinterop.js";
    private const string _moduleName = "GoogleAnalyticsInterop";

    private readonly CancellationScope _cancellationScope = new();

    public GoogleAnalyticsInterop(IJSRuntime jSRuntime, ILogger<GoogleAnalyticsInterop> logger, IResourceLoader resourceLoader)
    {
        _jsRuntime = jSRuntime;
        _logger = logger;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton(async (token, _) =>
        {
            await _resourceLoader.ImportModuleAndWaitUntilAvailable(_modulePath, _moduleName, 100, token).NoSync();
            return new object();
        });
    }

    public async ValueTask Init(string tagId, bool log = false, CancellationToken cancellationToken = default)
    {
        if (log)
            _logger.LogDebug("Initializing Google Analytics...");

        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _scriptInitializer.Init(linked).NoSync();

            await _jsRuntime.InvokeVoidAsync($"{_moduleName}.init", linked, tagId).NoSync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_modulePath).NoSync();

        await _scriptInitializer.DisposeAsync().NoSync();

        await _cancellationScope.DisposeAsync().NoSync();
    }
}
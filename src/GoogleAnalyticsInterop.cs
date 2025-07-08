using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;
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

        await _scriptInitializer.Init(cancellationToken).NoSync();

        await _jsRuntime.InvokeVoidAsync($"{_moduleName}.init", cancellationToken, tagId).NoSync();
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_modulePath).NoSync();

        await _scriptInitializer.DisposeAsync().NoSync();
    }
}
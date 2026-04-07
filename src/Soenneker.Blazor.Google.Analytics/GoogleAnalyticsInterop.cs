using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Google.Analytics;

/// <inheritdoc cref="IGoogleAnalyticsInterop"/>
public sealed class GoogleAnalyticsInterop : IGoogleAnalyticsInterop
{
    private readonly ILogger<GoogleAnalyticsInterop> _logger;
    private readonly IModuleImportUtil _moduleImportUtil;

    private const string _modulePath = "/_content/Soenneker.Blazor.Google.Analytics/js/googleanalyticsinterop.js";

    private readonly CancellationScope _cancellationScope = new();

    public GoogleAnalyticsInterop(ILogger<GoogleAnalyticsInterop> logger, IModuleImportUtil moduleImportUtil)
    {
        _logger = logger;
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Init(string tagId, bool log = false, CancellationToken cancellationToken = default)
    {
        if (log)
            _logger.LogDebug("Initializing Google Analytics...");

        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("init", linked, tagId);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);

        await _cancellationScope.DisposeAsync();
    }
}

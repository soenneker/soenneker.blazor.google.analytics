using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Blazor.Google.Analytics.Models;
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

    private const string _modulePath = "_content/Soenneker.Blazor.Google.Analytics/js/googleanalyticsinterop.js";

    private readonly CancellationScope _cancellationScope = new();

    public GoogleAnalyticsInterop(ILogger<GoogleAnalyticsInterop> logger, IModuleImportUtil moduleImportUtil)
    {
        _logger = logger;
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask Init(string tagId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Initializing Google Analytics...");

        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("init", linked, tagId);
        }
    }

    public async ValueTask SetDefaultConsent(GoogleAnalyticsConsentSettings settings, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setDefaultConsent", linked, settings);
        }
    }

    public async ValueTask UpdateConsent(GoogleAnalyticsConsentSettings settings, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("updateConsent", linked, settings);
        }
    }

    public async ValueTask Config(string tagId, object? parameters = null, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("config", linked, tagId, parameters);
        }
    }

    public async ValueTask Event(string name, object? parameters = null, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("event", linked, name, parameters);
        }
    }

    public async ValueTask PageView(string? pageLocation = null, string? pageTitle = null, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("pageView", linked, pageLocation, pageTitle);
        }
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);

        await _cancellationScope.DisposeAsync();
    }
}

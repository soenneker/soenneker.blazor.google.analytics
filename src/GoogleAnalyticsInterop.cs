using Soenneker.Blazor.Google.Analytics.Abstract;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Soenneker.Blazor.Google.Analytics;

/// <inheritdoc cref="IGoogleAnalyticsInterop"/>
public class GoogleAnalyticsInterop: IGoogleAnalyticsInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger<GoogleAnalyticsInterop> _logger;

    public GoogleAnalyticsInterop(IJSRuntime jSRuntime, ILogger<GoogleAnalyticsInterop> logger)
    {
        _jsRuntime = jSRuntime;
        _logger = logger;
    }

    public ValueTask Init(string tagId, bool log = false, CancellationToken cancellationToken = default)
    {
        if (log)
            _logger.LogDebug("Initializing Google Analytics...");

        // Does not import modules, load external scripts, etc for maximum injection speed

        var script = $@"
            var script = document.createElement('script');
            script.src = 'https://www.googletagmanager.com/gtag/js?id={tagId}';
            script.async = true;
            script.onload = function () {{
                window.dataLayer = window.dataLayer || [];
                function gtag() {{ dataLayer.push(arguments); }}
                gtag('js', new Date());
                gtag('config', '{tagId}');
            }};
            document.head.appendChild(script);
        ";

        return _jsRuntime.InvokeVoidAsync("eval", cancellationToken, script);
    }
}

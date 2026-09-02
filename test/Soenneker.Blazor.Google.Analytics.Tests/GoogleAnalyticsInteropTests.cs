using System.Threading;
using Microsoft.JSInterop;
using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Blazor.Google.Analytics.Models;
using Soenneker.Blazor.MockJsRuntime.Abstract;
using Soenneker.Tests.HostedUnit;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Google.Analytics.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class GoogleAnalyticsInteropTests : HostedUnitTest
{
    private readonly IGoogleAnalyticsInterop _util;

    public GoogleAnalyticsInteropTests(Host host) : base(host)
    {
        var jsRuntime = (IMockJsRuntime) Resolve<IJSRuntime>(true);
        jsRuntime.SetupMockResult<IJSObjectReference>("import", new TestJsObjectReference());
        _util = Resolve<IGoogleAnalyticsInterop>(true);
    }

    [Test]
    public async Task Analytics_commands_can_be_invoked(CancellationToken cancellationToken)
    {
        var settings = new GoogleAnalyticsConsentSettings
        {
            AdStorage = false,
            AnalyticsStorage = false,
            AdUserData = false,
            AdPersonalization = false,
            WaitForUpdateMilliseconds = 500
        };

        await _util.SetDefaultConsent(settings, cancellationToken: cancellationToken);
        await _util.Init("G-TEST", cancellationToken: cancellationToken);
        await _util.Init("G-TEST-CONFIGURED", new { send_page_view = false }, cancellationToken: cancellationToken);
        await _util.UpdateConsent(settings, cancellationToken: cancellationToken);
        await _util.Config("G-TEST", new { send_page_view = false }, cancellationToken: cancellationToken);
        await _util.Event("test_event", new { value = 1 }, cancellationToken: cancellationToken);
        await _util.PageView("https://example.com/test", "Test", cancellationToken: cancellationToken);
    }
}

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
    public async Task Analytics_commands_can_be_invoked()
    {
        var settings = new GoogleAnalyticsConsentSettings
        {
            AdStorage = false,
            AnalyticsStorage = false,
            AdUserData = false,
            AdPersonalization = false,
            WaitForUpdateMilliseconds = 500
        };

        await _util.SetDefaultConsent(settings);
        await _util.Init("G-TEST");
        await _util.Init("G-TEST-CONFIGURED", new { send_page_view = false });
        await _util.UpdateConsent(settings);
        await _util.Config("G-TEST", new { send_page_view = false });
        await _util.Event("test_event", new { value = 1 });
        await _util.PageView("https://example.com/test", "Test");
    }
}

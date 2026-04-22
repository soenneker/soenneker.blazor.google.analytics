using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Google.Analytics.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class GoogleAnalyticsInteropTests : HostedUnitTest
{
    private readonly IGoogleAnalyticsInterop _util;

    public GoogleAnalyticsInteropTests(Host host) : base(host)
    {
        _util = Resolve<IGoogleAnalyticsInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}

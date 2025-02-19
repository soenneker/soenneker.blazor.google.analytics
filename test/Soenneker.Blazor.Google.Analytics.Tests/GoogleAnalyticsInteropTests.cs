using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blazor.Google.Analytics.Tests;

[Collection("Collection")]
public sealed class GoogleAnalyticsInteropTests : FixturedUnitTest
{
    private readonly IGoogleAnalyticsInterop _util;

    public GoogleAnalyticsInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IGoogleAnalyticsInterop>(true);
    }

    [Fact]
    public void Default()
    {

    }
}

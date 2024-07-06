using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Blazor.Google.Analytics.Tests;

[Collection("Collection")]
public class GoogleAnalyticsInteropTests : FixturedUnitTest
{
    private readonly IGoogleAnalyticsInterop _interop;

    public GoogleAnalyticsInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _interop = Resolve<IGoogleAnalyticsInterop>(true);
    }
}

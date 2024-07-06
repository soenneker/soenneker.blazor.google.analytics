using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Blazor.Google.Analytics.Abstract;

/// <summary>
/// A Blazor interop library for Google Analytics
/// </summary>
public interface IGoogleAnalyticsInterop
{
    ValueTask Init(string tagId, bool log = false, CancellationToken cancellationToken = default);
}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Google.Analytics.Abstract;

/// <summary>
/// A Blazor interop library for Google Analytics
/// </summary>
public interface IGoogleAnalyticsInterop : IAsyncDisposable
{
    ValueTask Init(string tagId, bool log = false, CancellationToken cancellationToken = default);
}

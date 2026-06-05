using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Google.Analytics.Abstract;

/// <summary>
/// A Blazor interop library for Google Analytics
/// </summary>
public interface IGoogleAnalyticsInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the instance.
    /// </summary>
    /// <param name="tagId">The tag id.</param>
    /// <param name="log">The log.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Init(string tagId, bool log = false, CancellationToken cancellationToken = default);
}

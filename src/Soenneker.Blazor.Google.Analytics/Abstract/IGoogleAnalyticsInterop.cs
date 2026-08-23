using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Google.Analytics.Models;

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
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Init(string tagId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queues default Consent Mode V2 settings. Call before <see cref="Init"/>.
    /// </summary>
    ValueTask SetDefaultConsent(GoogleAnalyticsConsentSettings settings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queues a Consent Mode V2 update after the visitor changes their consent choice.
    /// </summary>
    ValueTask UpdateConsent(GoogleAnalyticsConsentSettings settings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a Google tag configuration command.
    /// </summary>
    ValueTask Config(string tagId, object? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an event through the Google tag.
    /// </summary>
    ValueTask Event(string name, object? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a page-view event, suitable for client-side navigation.
    /// </summary>
    ValueTask PageView(string? pageLocation = null, string? pageTitle = null, CancellationToken cancellationToken = default);
}

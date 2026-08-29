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
    /// <param name="tagId">Identifier of the tag to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the google analytics is ready for use.</returns>
    ValueTask Init(string tagId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes the Google tag with configuration parameters applied to its initial config command.
    /// </summary>
    /// <param name="tagId">Identifier of the tag to target.</param>
    /// <param name="parameters">Parameters for the initial config command.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when Google Analytics is ready for use.</returns>
    ValueTask Init(string tagId, object parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queues default Consent Mode V2 settings. Call before <see cref="Init(string, CancellationToken)"/>.
    /// </summary>
    /// <param name="settings">Settings to apply.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the default consent has been stored.</returns>
    ValueTask SetDefaultConsent(GoogleAnalyticsConsentSettings settings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queues a Consent Mode V2 update after the visitor changes their consent choice.
    /// </summary>
    /// <param name="settings">Settings to apply.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the consent update is complete.</returns>
    ValueTask UpdateConsent(GoogleAnalyticsConsentSettings settings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a Google tag configuration command.
    /// </summary>
    /// <param name="tagId">Identifier of the tag to target.</param>
    /// <param name="parameters">Parameters supplied to the operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the config operation is complete.</returns>
    ValueTask Config(string tagId, object? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an event through the Google tag.
    /// </summary>
    /// <param name="name">Name of the Google Analytics value to target.</param>
    /// <param name="parameters">Parameters supplied to the operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the event operation is complete.</returns>
    ValueTask Event(string name, object? parameters = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a page-view event, suitable for client-side navigation.
    /// </summary>
    /// <param name="pageLocation">Page Location for the page view operation.</param>
    /// <param name="pageTitle">Page Title for the page view operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the page view operation is complete.</returns>
    ValueTask PageView(string? pageLocation = null, string? pageTitle = null, CancellationToken cancellationToken = default);
}

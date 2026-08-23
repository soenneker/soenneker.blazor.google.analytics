namespace Soenneker.Blazor.Google.Analytics.Models;

/// <summary>
/// Consent Mode V2 settings for the Google tag.
/// </summary>
public sealed class GoogleAnalyticsConsentSettings
{
    /// <summary>
    /// Whether storage related to advertising is permitted.
    /// </summary>
    public bool AdStorage { get; set; }

    /// <summary>
    /// Whether storage related to analytics is permitted.
    /// </summary>
    public bool AnalyticsStorage { get; set; }

    /// <summary>
    /// Whether user data may be sent to Google for advertising purposes.
    /// </summary>
    public bool AdUserData { get; set; }

    /// <summary>
    /// Whether personalized advertising is permitted.
    /// </summary>
    public bool AdPersonalization { get; set; }

    /// <summary>
    /// Optional time for the Google tag to wait for a consent update. Applies only to default consent.
    /// </summary>
    public int? WaitForUpdateMilliseconds { get; set; }
}

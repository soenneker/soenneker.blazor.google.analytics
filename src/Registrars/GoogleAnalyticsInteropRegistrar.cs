using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Google.Analytics.Abstract;

namespace Soenneker.Blazor.Google.Analytics.Registrars;

/// <summary>
/// A Blazor interop library for Google Analytics
/// </summary>
public static class GoogleAnalyticsInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="IGoogleAnalyticsInterop"/> as a scoped service. <para/>
    /// </summary>
    public static void AddGoogleAnalyticsInteropAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IGoogleAnalyticsInterop, GoogleAnalyticsInterop>();
    }
}

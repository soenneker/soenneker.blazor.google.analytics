using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.Google.Analytics.Registrars;

/// <summary>
/// A Blazor interop library for Google Analytics
/// </summary>
public static class GoogleAnalyticsInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="IGoogleAnalyticsInterop"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddGoogleAnalyticsInteropAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IGoogleAnalyticsInterop, GoogleAnalyticsInterop>();
        return services;
    }
}

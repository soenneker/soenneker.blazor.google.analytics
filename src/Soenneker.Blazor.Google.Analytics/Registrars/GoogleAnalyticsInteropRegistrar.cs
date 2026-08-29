using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Google.Analytics.Abstract;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Blazor.Google.Analytics.Registrars;

/// <summary>
/// A Blazor interop library for Google Analytics
/// </summary>
public static class GoogleAnalyticsInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="IGoogleAnalyticsInterop"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddGoogleAnalyticsInteropAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IGoogleAnalyticsInterop, GoogleAnalyticsInterop>();
        return services;
    }
}

[![](https://img.shields.io/nuget/v/soenneker.blazor.google.analytics.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.google.analytics/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.google.analytics/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.google.analytics/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.google.analytics.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.google.analytics/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.google.analytics/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.google.analytics/actions/workflows/codeql.yml)

# Soenneker.Blazor.Google.Analytics

A Blazor interop library for the Google tag and Google Analytics.

## Installation

```bash
dotnet add package Soenneker.Blazor.Google.Analytics
```

## Usage

Register and inject the interop:

```csharp
services.AddGoogleAnalyticsInteropAsScoped();
```

```razor
@using Soenneker.Blazor.Google.Analytics.Abstract
@using Soenneker.Blazor.Google.Analytics.Models
@inject IGoogleAnalyticsInterop GoogleAnalyticsInterop
```

Initialize the Google tag after the page becomes interactive:

```csharp
await GoogleAnalyticsInterop.Init("G-XXXXXXXXXX");
```

Send events and client-side page views:

```csharp
await GoogleAnalyticsInterop.Event("purchase", new
{
    currency = "USD",
    value = 42.00
});

await GoogleAnalyticsInterop.PageView(
    pageLocation: Navigation.Uri,
    pageTitle: "Settings");
```

Use `Config` when a destination needs additional configuration after initialization.

## Consent Mode V2

Queue default consent before calling `Init`:

```csharp
var consent = new GoogleAnalyticsConsentSettings
{
    AdStorage = false,
    AnalyticsStorage = false,
    AdUserData = false,
    AdPersonalization = false,
    WaitForUpdateMilliseconds = 500
};

await GoogleAnalyticsInterop.SetDefaultConsent(consent);
await GoogleAnalyticsInterop.Init("G-XXXXXXXXXX");
```

Call `UpdateConsent` as soon as the visitor makes or changes their choice.

Use this package for a direct `gtag.js` integration. If the application deploys Google Analytics through Google Tag Manager, initialize the GTM package instead of independently initializing both loaders.

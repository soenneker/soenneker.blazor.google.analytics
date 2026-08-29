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
using Soenneker.Blazor.Google.Analytics.Registrars;

builder.Services.AddGoogleAnalyticsInteropAsScoped();
```

```razor
@using Soenneker.Blazor.Google.Analytics.Abstract
@using Soenneker.Blazor.Google.Analytics.Models
@inject IGoogleAnalyticsInterop GoogleAnalyticsInterop
```

Initialize the Google tag after the page becomes interactive. In a component, that normally means the first `OnAfterRenderAsync` call:

```csharp
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (!firstRender)
        return;

    await GoogleAnalyticsInterop.Init("G-XXXXXXXXXX");
}
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

## Client-side navigation

Google's initial `config` command sends a page view by default. If the application also reports every Blazor navigation, disable that automatic view and send each view deliberately:

```csharp
await GoogleAnalyticsInterop.Init("G-XXXXXXXXXX", new
{
    send_page_view = false
});

await GoogleAnalyticsInterop.PageView(
    pageLocation: Navigation.Uri,
    pageTitle: "Orders");
```

Subscribe to `NavigationManager.LocationChanged` in a long-lived component and unsubscribe when that component is disposed. Avoid reporting both automatic and manual initial views, or the first page will be counted twice.

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

Consent defaults must be queued before `Init`; otherwise the Google script and initial configuration are added first. Persist the visitor's choice according to the requirements that apply to your application, and do not represent denied consent as granted until the visitor opts in.

## Data handling

Event names and parameters are forwarded to Google's `gtag` queue. Do not send email addresses, names, access tokens, raw URLs containing secrets, or other personally identifiable/sensitive values. Treat analytics configuration as part of the application's privacy and content-security-policy design; loading the tag requires allowing the relevant Google origins.

Use this package for a direct `gtag.js` integration. If the application deploys Google Analytics through Google Tag Manager, initialize the GTM package instead of independently initializing both loaders.

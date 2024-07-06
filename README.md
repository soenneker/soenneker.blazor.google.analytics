[![](https://img.shields.io/nuget/v/soenneker.blazor.google.analytics.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.google.analytics/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.google.analytics/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.google.analytics/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.google.analytics.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.google.analytics/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Google.Analytics
### A Blazor interop library for Google Analytics

## Installation

```
dotnet add package Soenneker.Blazor.Google.Analytics
```

## Usage

1. Register the interop within DI (`Program.cs`)

```csharp
public static async Task Main(string[] args)
{
    ...
    builder.Services.AddGoogleAnalyticsInterop();
}
```

2. Inject `IClarityInterop` within your `App.Razor` file

```csharp
@using Soenneker.Blazor.Google.Analytics.Abstract
@inject IGoogleAnalyticsInterop GoogleAnalyticsInterop
```

3. Initialize the interop in `OnInitializedAsync` within `App.Razor` using your Google Analytics tag id

```csharp
protected override async Task OnInitializedAsync()
{
    await GoogleAnalyticsInterop.Init("your-key-here");
    ...
}
```
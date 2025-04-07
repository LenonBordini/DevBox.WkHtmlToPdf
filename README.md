# DevBox.WkHtmlToPdf

![NuGet Version](https://img.shields.io/nuget/v/DevBox.WkHtmlToPdf?style=for-the-badge)

A simple wrapper for [wkhtmltopdf](https://wkhtmltopdf.org/), enabling conversion of **HTML** or **Razor Views** directly into **PDF files** within [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) applications.

> This is a refactor from [Wkhtmltopdf.NetCore](https://github.com/fpanaccia/Wkhtmltopdf.NetCore-deprecated) since is deprecated.\
> It's also important to read this [recommendations](https://wkhtmltopdf.org/status.html#recommendations).

---

## Installation

1. Add **DevBox.WkHtmlToPdf** package to your project:

```bash
dotnet add package DevBox.WkHtmlToPdf
```

2. Configure your `IServiceCollection`:

```csharp
services.AddHttpContextAccessor();
services.AddWkHtmlToPdf(options =>
{
    options.Orientation = PdfOrientation.Landscape;
    // ...other global options
});
```

> You can find all possible options [here](./Configurations/Options/PdfOptions.cs).

## Usage

Inject `IPdfConverterService` in any class you need.

1. Converting a **HTML**:

```csharp
var buffer = await _pdfConverterService.FromHtmlAsync("<html>...</html>");

// Customizing global options
var buffer = await _pdfConverterService.FromHtmlAsync("<html>...</html>", options =>
{
    // ...
});
```

2. Converting a **Razor View**:

```csharp
// No model
var buffer = await _pdfConverterService.FromViewAsync("PathToView/ViewName");

// With model
var buffer = await _pdfConverterService.FromViewAsync("PathToView/ViewName", model);

// Customizing global options
var buffer = await _pdfConverterService.FromViewAsync("PathToView/ViewName", model, options =>
{
    // ...
});
```

If you have a `BaseController`, you can do like below if you want (no need to inject `IPdfConverterService` everytime):

```csharp
public abstract class BaseController
{
    protected async Task<byte[]> ConvertViewToPdfAsync<T>(string viewName, T model = null, Action<PdfOptions> configureOptions = null)
        where T : class
    {
        var pdfConverter = HttpContext.RequestServices.GetRequiredService<IPdfConverterService>();
        return await pdfConverter.FromViewAsync(viewName, model, configureOptions);
    }
}

public class ExampleController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        var buffer = await ConvertViewToPdfAsync("PathToView/ViewName");
        return File(buffer, "application/pdf", "report.pdf");
    }
}
```

## Docker

To deploy using docker, we need to install **libgdiplus** and **libc6-dev**. You can configure [Dockerfile](https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-9-0#create-the-dockerfile) like below:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0
# ...
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
```

> You'll also need **libgdiplus** and **libc6-dev** on **linux**.

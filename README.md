# DevBox.WkHtmlToPdf

A simple wrapper for [wkhtmltopdf](https://wkhtmltopdf.org/), enabling conversion of **HTML** or **Razor Views** directly into **PDF files** within [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) applications.

> This is a refactor from [Wkhtmltopdf.NetCore](https://github.com/fpanaccia/Wkhtmltopdf.NetCore-deprecated) since is deprecated.

---

## Instalation

1. Add package to your project:

```bash
dotnet add package DevBox.WkHtmlToPdf
```

2. Configure your `IServiceCollection`:

```csharp
services.AddHttpContextAccessor();
services.AddWkhtmltopdf(options =>
{
    options.Orientation = PdfOrientation.Landscape;
    // ...other global options
});
```

> You can find all possible options [here](./Configurations/Options/PdfOptions.cs).

## Usage

Inject `IPdfConverterService` in any class you need.

1. Converting a `HTML`:

```csharp
var buffer = await _pdfConverterService.FromHtmlAsync("<html>...</html>");
```

```csharp
// Customizing global options
var buffer = await _pdfConverterService.FromHtmlAsync("<html>...</html>", options =>
{
    // ...
});
```

2. Converting a `View`:

```csharp
// No model
var buffer = await _pdfConverterService.FromViewAsync("PathToView/ViewName");
```

```csharp
// With model
var buffer = await _pdfConverterService.FromViewAsync("PathToView/ViewName", model);
```

```csharp
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
    protected async Task<byte[]> ConvertViewToPdfAsync<T>(string viewName, T model = null)
    {
        var pdfConverter = HttpContext.RequestServices.GetRequiredService<IPdfConverterService>();
        return await pdfConverter.FromViewAsync(viewName, model);
    }
}
```

```csharp

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

## Recommendations

- **Do not use wkhtmltopdf with any untrusted HTML** – be sure to sanitize any user-supplied HTML/JS, otherwise it can lead to complete takeover of the server it is running on! Please consider using a Mandatory Access Control system like AppArmor or SELinux, see [recommended AppArmor policy](https://wkhtmltopdf.org/apparmor.html).
- If you’re using it for report generation (i.e. with HTML you control), also consider using [WeasyPrint](https://weasyprint.org/) or the [commercial tool Prince](https://www.princexml.com/) – note that I’m not affiliated with either project, and do your diligence.
- If you’re using it to convert a site which uses dynamic JS, consider using [puppeteer](https://pptr.dev/) or one of the many wrappers it has.

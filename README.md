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

2. Download the desirable version of [wkhtmltopdf](https://wkhtmltopdf.org/downloads.html#stable) and add to your project:

```bash
.
├── MyProject
│   ├── WkHtmlToPdf
│   │   ├── Windows
│   │   │   └── wkhtmltopdf.exe
│   │   └── Linux
│   │       └── wkhtmltopdf
│   └── MyProject.csproj
└── MyProject.sln
```

| OS/Distribution | Supported on | Architectures |
| :--             | :--          | :--           |
| Windows         |              | [64-bit](./WkHtmlToPdf/Windows/wkhtmltopdf-64.exe) |
| Debian          | 11 bullseye  | [64-bit](./WkHtmlToPdf/Linux/wkhtmltopdf-debian-11-bullseye-amd64) |

> The package could copy the binaries automatically and verify the OS, but it would be large. This way the executable becomes configurable.

- Edit your `.csproj`:

```xml
<ItemGroup>
    <Content Include="WkHtmlToPdf\**\*.*">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
</ItemGroup>
```

> If you have wkhtmltopdf installed in your server, you can skip item 2 configuring through `SetExecutableFilePath` using an absolute path.

3. Configure your `IServiceCollection`:

```csharp
services.AddHttpContextAccessor();
services.AddWkHtmlToPdf(options =>
{
    options.SetExecutableFilePath("custom-absolute-path/wkhtmltopdf.exe");

    // Customize the default options
    options.Title = "My PDF";
    options.PageSize = PageSize.A4;
    options.Orientation = PdfOrientation.Landscape;
    // ...
});
```

- Default executable file path:
  - Windows: `WkHtmlToPdf/Windows/wkhtmltopdf.exe`
  - Linux: `WkHtmlToPdf/Linux/wkhtmltopdf`
  - MAC: `WkHtmlToPdf/Mac/wkhtmltopdf`

4. If you work with **Linux** or **Docker**, you need to install `libgdiplus` and `libc6-dev`.

```bash
apt-get update
apt-get -y install libgdiplus libc6-dev
```

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0
# ...
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
```

- If you have problems with differents fonts in **Windows** vs **Docker**, you can try:

```dockerfile
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev fontconfig fonts-liberation
RUN fc-cache -f -v
```

## Usage

Inject `IPdfConverterService` in any class you need.

1. Converting a **HTML**:

```csharp
var buffer = await _pdfConverterService.FromHtmlAsync("<html>...</html>", options =>
{
    // Overrides the default options
    options.Title = "My Report";
});
```

2. Converting a **Razor View**:

```csharp
var buffer = await _pdfConverterService.FromViewAsync("PathToView/ViewName", model, options =>
{
    // Overrides the default options
    options.HeaderFooter.HeaderHtml = "PathToView/HeaderViewName";
});
```

using DevBox.WkHtmlToPdf.Configurations;
using DevBox.WkHtmlToPdf.Configurations.Options;
using DevBox.WkHtmlToPdf.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace DevBox.WkHtmlToPdf.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Setup WkHtmlToPdf library
    /// </summary>
    /// <param name="services">The IServiceCollection object</param>
    /// <param name="configureGlobalPdfOptions">Action to configure a global PdfOptions used for every PDF</param>
    public static IServiceCollection AddWkHtmlToPdf(this IServiceCollection services, Action<PdfOptions> configureGlobalPdfOptions = null)
    {
        var pdfOptions = new PdfOptions();
        configureGlobalPdfOptions?.Invoke(pdfOptions);
        services.AddDependencies(pdfOptions);

        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? string.Empty, "WkHtmlToPdf");

        if (string.IsNullOrEmpty(WkHtmlToPdfDriver.ExecutableFilePath))
        {
            var executableFilePath = OSPlatformFactory.OsPlatform switch
            {
                OsPlatform.Windows => Path.Combine(basePath, "Windows/wkhtmltopdf.exe"),
                OsPlatform.Linux => Path.Combine(basePath, "Linux/wkhtmltopdf"),
                OsPlatform.Mac => Path.Combine(basePath, "Mac/wkhtmltopdf"),
                _ => throw new Exception("Could not determine wkhtmltopdf executable path")
            };

            pdfOptions.SetExecutableFilePath(executableFilePath);
        }

        if (string.IsNullOrEmpty(WkHtmlToPdfDriver.TempPath))
            pdfOptions.SetTempPath(Path.Combine(basePath, "Temp"));

        return services;
    }
}

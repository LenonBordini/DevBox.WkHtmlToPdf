using DevBox.WkHtmlToPdf.Configurations;
using DevBox.WkHtmlToPdf.Configurations.Options;
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

        services.AddDependencies(pdfOptions);

        configureGlobalPdfOptions?.Invoke(pdfOptions);

        return services;
    }
}

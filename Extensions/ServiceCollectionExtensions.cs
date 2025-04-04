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
    /// <param name="configureGlobalOptions">Action to configure a global PdfOptions used for every PDF</param>
    public static void AddWkHtmlToPdf(this IServiceCollection services, Action<PdfOptions> configureGlobalOptions = null)
    {
        var pdfOptions = new PdfOptions();

        services.AddDependencies(pdfOptions);

        configureGlobalOptions?.Invoke(pdfOptions);
    }
}

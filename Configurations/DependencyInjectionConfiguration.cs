using DevBox.WkHtmlToPdf.Configurations.Options;
using DevBox.WkHtmlToPdf.Drivers;
using DevBox.WkHtmlToPdf.Interfaces.Driver;
using DevBox.WkHtmlToPdf.Interfaces.Services;
using DevBox.WkHtmlToPdf.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace DevBox.WkHtmlToPdf.Configurations;

internal static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, PdfOptions pdfOptions)
    {
        services.AddSingleton(pdfOptions);

        services.AddSingleton<IRazorViewEngine, RazorViewEngine>();
        services.AddTransient<ITempDataProvider, SessionStateTempDataProvider>();

        services.AddSingleton<IWkHtmlToPdfDriver, WkHtmlToPdfDriver>();
        services.AddTransient<IViewRenderService, ViewRenderService>();
        services.AddTransient<IPdfConverterService, PdfConverterService>();

        return services;
    }
}

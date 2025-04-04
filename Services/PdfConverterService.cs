using DevBox.WkHtmlToPdf.Configurations.Options;
using DevBox.WkHtmlToPdf.Drivers;
using DevBox.WkHtmlToPdf.Extensions;
using DevBox.WkHtmlToPdf.Interfaces.Services;

namespace DevBox.WkHtmlToPdf.Services;

public class PdfConverterService : IPdfConverterService
{
    private readonly IViewRenderService _viewRenderService;
    private readonly PdfOptions _pdfOptions;

    public PdfConverterService(IViewRenderService viewRenderService, PdfOptions pdfOptions)
    {
        _viewRenderService = viewRenderService;
        _pdfOptions = pdfOptions;
    }

    /// <summary>
    /// Convert HTML to PDF using global PdfOptions
    /// </summary>
    /// <param name="html">HTML to convert</param>
    /// <returns>PDF buffer</returns>
    public async Task<byte[]> FromHtmlAsync(string html)
    {
        return await WkHtmlToPdfDriver.ConvertHtmlAsync(html, _pdfOptions);
    }

    /// <summary>
    /// Convert HTML to PDF changing global PdfOptions
    /// </summary>
    /// <param name="html">HTML to convert</param>
    /// <param name="configureOptions">Action to customize global PdfOtpions</param>
    /// <returns>PDF buffer</returns>
    public async Task<byte[]> FromHtmlAsync(string html, Action<PdfOptions> configureOptions)
    {
        var pdfOptionsClone = _pdfOptions.Clone();

        configureOptions.Invoke(pdfOptionsClone);

        return await WkHtmlToPdfDriver.ConvertHtmlAsync(html, pdfOptionsClone);
    }

    /// <summary>
    /// Render the view as HTML and convert to PDF using global PdfOptions
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <param name="model">The view model</param>
    /// <returns>PDF buffer</returns>
    public async Task<byte[]> FromViewAsync(string viewName, object model)
    {
        var html = await _viewRenderService.RenderToStringAsync(viewName, model);
        return await FromHtmlAsync(html);
    }

    /// <summary>
    /// Render the view as HTML and convert to PDF using global PdfOptions
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <returns>PDF buffer</returns>
    public async Task<byte[]> FromViewAsync(string viewName)
    {
        return await FromViewAsync(viewName, null);
    }

    /// <summary>
    /// Render the view as HTML and convert to PDF changing global PdfOptions
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <param name="model">The view model</param>
    /// <param name="configureOptions">Action to customize global PdfOtpions</param>
    /// <returns>PDF buffer</returns>
    public async Task<byte[]> FromViewAsync(string viewName, object model, Action<PdfOptions> configureOptions)
    {
        var html = await _viewRenderService.RenderToStringAsync(viewName, model);
        return await FromHtmlAsync(html, configureOptions);
    }

    /// <summary>
    /// Render the view as HTML and convert to PDF changing global PdfOptions
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <param name="configureOptions">Action to customize global PdfOtpions</param>
    /// <returns>PDF buffer</returns>
    public async Task<byte[]> FromViewAsync(string viewName, Action<PdfOptions> configureOptions)
    {
        return await FromViewAsync(viewName, null, configureOptions);
    }
}

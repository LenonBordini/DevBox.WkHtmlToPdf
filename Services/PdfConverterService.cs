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

    public async Task<byte[]> FromHtmlAsync(string html)
    {
        return await WkHtmlToPdfDriver.ConvertHtmlAsync(html, _pdfOptions);
    }

    public async Task<byte[]> FromHtmlAsync(string html, Action<PdfOptions> configureOptions)
    {
        PdfOptions options;
        if (configureOptions == null)
            options = _pdfOptions;
        else
        {
            options = _pdfOptions.Clone();
            configureOptions.Invoke(options);
        }

        return await WkHtmlToPdfDriver.ConvertHtmlAsync(html, options);
    }

    public async Task<byte[]> FromViewAsync(string viewName, object model)
    {
        var html = await _viewRenderService.RenderToStringAsync(viewName, model);
        return await FromHtmlAsync(html);
    }

    public async Task<byte[]> FromViewAsync(string viewName)
    {
        return await FromViewAsync(viewName, null);
    }

    public async Task<byte[]> FromViewAsync(string viewName, object model, Action<PdfOptions> configureOptions)
    {
        var html = await _viewRenderService.RenderToStringAsync(viewName, model);
        return await FromHtmlAsync(html, configureOptions);
    }

    public async Task<byte[]> FromViewAsync(string viewName, Action<PdfOptions> configureOptions)
    {
        return await FromViewAsync(viewName, null, configureOptions);
    }
}

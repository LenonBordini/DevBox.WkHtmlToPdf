using DevBox.WkHtmlToPdf.Configurations.Options;
using DevBox.WkHtmlToPdf.Extensions;
using DevBox.WkHtmlToPdf.Helpers;
using DevBox.WkHtmlToPdf.Interfaces.Driver;
using DevBox.WkHtmlToPdf.Interfaces.Services;

namespace DevBox.WkHtmlToPdf.Services;

public class PdfConverterService : IPdfConverterService
{
    private readonly IWkHtmlToPdfDriver _wkHtmlToPdfDriver;
    private readonly IViewRenderService _viewRenderService;
    private readonly PdfOptions _pdfOptions;

    public PdfConverterService(IWkHtmlToPdfDriver wkHtmlToPdfDriver, IViewRenderService viewRenderService, PdfOptions pdfOptions)
    {
        _wkHtmlToPdfDriver = wkHtmlToPdfDriver;
        _viewRenderService = viewRenderService;
        _pdfOptions = pdfOptions;
    }

    public async Task<byte[]> FromHtmlAsync(string html, Action<PdfOptions> configurePdfOptions)
    {
        var options = GetOptions(configurePdfOptions);
        return await _wkHtmlToPdfDriver.ConvertHtmlAsync(html, options);
    }

    public async Task<byte[]> FromHtmlAsync(string html)
    {
        return await FromHtmlAsync(html, null);
    }

    public async Task<byte[]> FromViewAsync(string viewName, object model, Action<PdfOptions> configurePdfOptions)
    {
        var html = await _viewRenderService.RenderToStringAsync(viewName, model);
        var options = GetOptions(configurePdfOptions);

        if (!string.IsNullOrEmpty(options.HeaderFooter?.HeaderHtml) && !FileHelper.IsHtml(options.HeaderFooter.HeaderHtml))
            options.HeaderFooter.HeaderHtml = await _viewRenderService.RenderToStringAsync(options.HeaderFooter.HeaderHtml, model);

        if (!string.IsNullOrEmpty(options.HeaderFooter?.FooterHtml) && !FileHelper.IsHtml(options.HeaderFooter.FooterHtml))
            options.HeaderFooter.FooterHtml = await _viewRenderService.RenderToStringAsync(options.HeaderFooter.FooterHtml, model);

        return await _wkHtmlToPdfDriver.ConvertHtmlAsync(html, options);
    }

    public async Task<byte[]> FromViewAsync(string viewName, object model)
    {
        return await FromViewAsync(viewName, model, null);
    }

    public async Task<byte[]> FromViewAsync(string viewName, Action<PdfOptions> configurePdfOptions)
    {
        return await FromViewAsync(viewName, null, configurePdfOptions);
    }

    public async Task<byte[]> FromViewAsync(string viewName)
    {
        return await FromViewAsync(viewName, null, null);
    }

    private PdfOptions GetOptions(Action<PdfOptions> configurePdfOptions)
    {
        if (configurePdfOptions == null)
            return _pdfOptions;

        var options = _pdfOptions.Clone();
        configurePdfOptions.Invoke(options);

        return options;
    }
}

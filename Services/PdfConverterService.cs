using DevBox.WkHtmlToPdf.Configurations.Options;
using DevBox.WkHtmlToPdf.Extensions;
using DevBox.WkHtmlToPdf.Helpers;
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

    public async Task<byte[]> FromHtmlAsync(string html, Action<PdfOptions> configurePdfOptions, CancellationToken cancellationToken = default)
    {
        var options = GetOptions(configurePdfOptions);
        return await WkHtmlToPdfDriver.ConvertHtmlAsync(html, options, cancellationToken);
    }

    public async Task<byte[]> FromHtmlAsync(string html, CancellationToken cancellationToken = default)
    {
        return await FromHtmlAsync(html, null, cancellationToken);
    }

    public async Task<byte[]> FromViewAsync(string viewName, object model, Action<PdfOptions> configurePdfOptions, CancellationToken cancellationToken = default)
    {
        var html = await _viewRenderService.RenderToStringAsync(viewName, model);
        var options = GetOptions(configurePdfOptions);

        if (!string.IsNullOrEmpty(options.HeaderFooter?.HeaderHtml) && !FileHelper.IsHtml(options.HeaderFooter.HeaderHtml))
            options.HeaderFooter.HeaderHtml = await _viewRenderService.RenderToStringAsync(options.HeaderFooter.HeaderHtml, model);

        if (!string.IsNullOrEmpty(options.HeaderFooter?.FooterHtml) && !FileHelper.IsHtml(options.HeaderFooter.FooterHtml))
            options.HeaderFooter.FooterHtml = await _viewRenderService.RenderToStringAsync(options.HeaderFooter.FooterHtml, model);

        return await WkHtmlToPdfDriver.ConvertHtmlAsync(html, options, cancellationToken);
    }

    public async Task<byte[]> FromViewAsync(string viewName, object model, CancellationToken cancellationToken = default)
    {
        return await FromViewAsync(viewName, model, null, cancellationToken);
    }

    public async Task<byte[]> FromViewAsync(string viewName, Action<PdfOptions> configurePdfOptions, CancellationToken cancellationToken = default)
    {
        return await FromViewAsync(viewName, null, configurePdfOptions, cancellationToken);
    }

    public async Task<byte[]> FromViewAsync(string viewName, CancellationToken cancellationToken = default)
    {
        return await FromViewAsync(viewName, null, null, cancellationToken);
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

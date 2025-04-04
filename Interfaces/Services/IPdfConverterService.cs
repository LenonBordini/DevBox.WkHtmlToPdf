using DevBox.WkHtmlToPdf.Configurations.Options;

namespace DevBox.WkHtmlToPdf.Interfaces.Services;

public interface IPdfConverterService
{
    Task<byte[]> FromHtmlAsync(string html);
    Task<byte[]> FromHtmlAsync(string html, Action<PdfOptions> configureOptions);
    Task<byte[]> FromViewAsync(string viewName, object model);
    Task<byte[]> FromViewAsync(string viewName);
    Task<byte[]> FromViewAsync(string viewName, object model, Action<PdfOptions> configureOptions);
    Task<byte[]> FromViewAsync(string viewName, Action<PdfOptions> configureOptions);
}

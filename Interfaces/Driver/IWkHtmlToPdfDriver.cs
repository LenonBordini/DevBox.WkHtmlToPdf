using DevBox.WkHtmlToPdf.Configurations.Options;

namespace DevBox.WkHtmlToPdf.Interfaces.Driver;

public interface IWkHtmlToPdfDriver
{
    Task<byte[]> ConvertHtmlAsync(string html, PdfOptions pdfOptions);
}

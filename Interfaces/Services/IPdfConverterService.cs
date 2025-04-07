using DevBox.WkHtmlToPdf.Configurations.Options;

namespace DevBox.WkHtmlToPdf.Interfaces.Services;

public interface IPdfConverterService
{
    /// <summary>
    /// Convert HTML to PDF changing global PdfOptions
    /// </summary>
    /// <param name="html">HTML to convert</param>
    /// <param name="configureOptions">Action to customize global PdfOtpions</param>
    /// <returns>PDF buffer</returns>
    Task<byte[]> FromHtmlAsync(string html, Action<PdfOptions> configurePdfOptions);

    /// <summary>
    /// Convert HTML to PDF using global PdfOptions
    /// </summary>
    /// <param name="html">HTML to convert</param>
    /// <returns>PDF buffer</returns>
    Task<byte[]> FromHtmlAsync(string html);

    /// <summary>
    /// Render the view as HTML and convert to PDF changing global PdfOptions
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <param name="model">The view model</param>
    /// <param name="configureOptions">Action to customize global PdfOtpions</param>
    /// <returns>PDF buffer</returns>
    Task<byte[]> FromViewAsync(string viewName, object model, Action<PdfOptions> configurePdfOptions);

    /// <summary>
    /// Render the view as HTML and convert to PDF using global PdfOptions
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <param name="model">The view model</param>
    /// <returns>PDF buffer</returns>
    Task<byte[]> FromViewAsync(string viewName, object model);

    /// <summary>
    /// Render the view as HTML and convert to PDF changing global PdfOptions
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <param name="configureOptions">Action to customize global PdfOtpions</param>
    /// <returns>PDF buffer</returns>
    Task<byte[]> FromViewAsync(string viewName, Action<PdfOptions> configurePdfOptions);

    /// <summary>
    /// Render the view as HTML and convert to PDF using global PdfOptions
    /// </summary>
    /// <param name="viewName">The view name</param>
    /// <returns>PDF buffer</returns>
    Task<byte[]> FromViewAsync(string viewName);
}

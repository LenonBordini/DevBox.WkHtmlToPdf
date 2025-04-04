using DevBox.WkHtmlToPdf.Configurations.Attributes;
using DevBox.WkHtmlToPdf.Enums;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

/// <summary>
/// See https://wkhtmltopdf.org/usage/wkhtmltopdf.txt.
/// </summary>
public class PdfOptions
{
    /// <summary>
    /// Doesn't delete temp files (.html + .pdf), and saves the wkhtmltopdf command inside a .txt (default false)
    /// <para>Useful when debugging</para>
    /// </summary>
    public bool KeepTempFiles { get; set; }

    /// <summary>
    /// Collate when printing multiple copies (default true)
    /// </summary>
    [BooleanCommandFlag("--collate", falseFlag: "--no-collate")]
    public bool? Collate { get; set; }

    /// <summary>
    /// Number of copies to print into the pdf file (default 1)
    /// </summary>
    [CommandFlag("--copies")]
    public int? Copies { get; set; }

    /// <summary>
    /// PDF will be generated in grayscale
    /// </summary>
    [BooleanCommandFlag("--grayscale", trueAlias: "-g")]
    public bool? GrayScale { get; set; }

    /// <summary>
    /// Generates lower quality pdf/ps. Useful to shrink the result document space
    /// </summary>
    [BooleanCommandFlag("--lowquality", trueAlias: "-l")]
    public bool? LowQuality { get; set; }

    /// <summary>
    /// Sets the page margin
    /// </summary>
    public PdfMargin Margin { get; set; }

    /// <summary>
    /// Set orientation to Landscape or Portrait (default Portrait)
    /// </summary>
    [CommandFlag("--orientation", alias: "-O")]
    public PdfOrientation? Orientation { get; set; }

    /// <summary>
    /// Set paper size to: A4, Letter, etc. (default A4)
    /// </summary>
    [CommandFlag("--page-size", alias: "-s")]
    public PageSize? PageSize { get; set; }

    /// <summary>
    /// Sets a custom page size
    /// </summary>
    /// <remarks>Used when <see cref="PageSize"/> is set to Custom</remarks>
    public CustomPageSize CustomPageSize { get; set; }

    /// <summary>
    /// Sets page options
    /// </summary>
    public PageOptions PageOptions { get; set; }

    /// <summary>
    /// Sets header and footer options
    /// </summary>
    public HeaderFooterOptions HeaderFooterOptions { get; set; }

    /// <summary>
    /// Sets TOC options
    /// </summary>
    public TocOptions TocOptions { get; set; }
}

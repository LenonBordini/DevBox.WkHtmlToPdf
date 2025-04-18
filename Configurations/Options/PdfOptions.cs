using DevBox.WkHtmlToPdf.Configurations.Attributes;
using DevBox.WkHtmlToPdf.Enums;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

/// <summary>
/// See <see href="https://wkhtmltopdf.org/usage/wkhtmltopdf.txt">wkhtmltopdf documentation</see>.
/// </summary>
public class PdfOptions
{
    public PdfOptions()
    {
        Margin = new PdfMargin();
        Page = new PageOptions();
        CustomPageSize = new CustomPageSizeOptions();
        HeaderFooter = new HeaderFooterOptions();
    }

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
    /// Change the dpi explicitly (this has no effect on X11 based systems) (default 96)
    /// </summary>
    [CommandFlag("--dpi", alias: "-d")]
    public int? Dpi { get; set; }

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
    public CustomPageSizeOptions CustomPageSize { get; set; }

    /// <summary>
    /// The title of the generated pdf file (The title of the first document is used if not specified)
    /// </summary>
    [CommandFlag("--title")]
    public string Title { get; set; }

    /// <summary>
    /// Sets page options
    /// </summary>
    public PageOptions Page { get; set; }

    /// <summary>
    /// Sets header and footer options
    /// </summary>
    public HeaderFooterOptions HeaderFooter { get; set; }

    /// <summary>
    /// Sets the path to the wkhtmltopdf executable file
    /// </summary>
    /// <param name="executableFilePath">The path to the wkhtmltopdf executable file</param>
    public void SetExecutableFilePath(string executableFilePath)
    {
        if (!File.Exists(executableFilePath))
            throw new Exception($"Could not find executable file path for wkhtmltopdf in \"{executableFilePath}\"");

        WkHtmlToPdfDriver.ExecutableFilePath = executableFilePath;
    }

    /// <summary>
    /// Sets the path to the temp files used/created by wkhtmltopdf
    /// </summary>
    /// <param name="tempPath">The path to the temp files used/created by wkhtmltopdf</param>
    public void SetTempPath(string tempPath)
    {
        if (!Directory.Exists(tempPath))
            Directory.CreateDirectory(tempPath);

        WkHtmlToPdfDriver.TempPath = tempPath;
    }
}

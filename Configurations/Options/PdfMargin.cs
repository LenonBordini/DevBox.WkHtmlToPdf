using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class PdfMargin
{
    public PdfMargin()
    {

    }

    public PdfMargin(string pdfMargin)
    {
        Top = Right = Bottom = Left = pdfMargin;
    }

    public PdfMargin(string topBottom, string leftRight)
    {
        Top = Bottom = topBottom;
        Left = Right = leftRight;
    }

    public PdfMargin(string top, string right, string bottom, string left)
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }

    /// <summary>
    /// Set the page top margin (in mm)
    /// </summary>
    [CommandFlag("--margin-top", alias: "-T")]
    public string Top { get; set; }

    /// <summary>
    /// Set the page right margin (in mm, default 10)
    /// </summary>
    [CommandFlag("--margin-right", alias: "-R")]
    public string Right { get; set; }

    /// <summary>
    /// Set the page bottom margin (in mm)
    /// </summary>
    [CommandFlag("--margin-bottom", alias: "-B")]
    public string Bottom { get; set; }

    /// <summary>
    /// Set the page left margin (in mm, default 10)
    /// </summary>
    [CommandFlag("--margin-left", alias: "-L")]
    public string Left { get; set; }
}

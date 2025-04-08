using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class PdfMargin
{
    public PdfMargin()
    {

    }

    public PdfMargin(object pdfMargin)
    {
        Top = Right = Bottom = Left = pdfMargin;
    }

    public PdfMargin(object topBottom, object leftRight)
    {
        Top = Bottom = topBottom;
        Left = Right = leftRight;
    }

    public PdfMargin(object top, object right, object bottom, object left)
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
    public object Top { get; set; }

    /// <summary>
    /// Set the page right margin (in mm, default 10)
    /// </summary>
    [CommandFlag("--margin-right", alias: "-R")]
    public object Right { get; set; }

    /// <summary>
    /// Set the page bottom margin (in mm)
    /// </summary>
    [CommandFlag("--margin-bottom", alias: "-B")]
    public object Bottom { get; set; }

    /// <summary>
    /// Set the page left margin (in mm, default 10)
    /// </summary>
    [CommandFlag("--margin-left", alias: "-L")]
    public object Left { get; set; }
}

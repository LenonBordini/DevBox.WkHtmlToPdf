using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class PdfMargin
{
    public PdfMargin()
    {

    }

    public PdfMargin(int pdfMargin)
    {
        Top = Right = Bottom = Left = pdfMargin;
    }

    public PdfMargin(int? topBottom, int? leftRight)
    {
        Top = Bottom = topBottom;
        Left = Right = leftRight;
    }

    public PdfMargin(int? top, int? right, int? bottom, int? left)
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
    public int? Top { get; }

    /// <summary>
    /// Set the page right margin (in mm, default 10)
    /// </summary>
    [CommandFlag("--margin-right", alias: "-R")]
    public int? Right { get; }

    /// <summary>
    /// Set the page bottom margin (in mm)
    /// </summary>
    [CommandFlag("--margin-bottom", alias: "-B")]
    public int? Bottom { get; }

    /// <summary>
    /// Set the page left margin (in mm, default 10)
    /// </summary>
    [CommandFlag("--margin-left", alias: "-L")]
    public int? Left { get; }
}

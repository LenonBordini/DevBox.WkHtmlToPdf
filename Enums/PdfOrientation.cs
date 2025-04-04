namespace DevBox.WkHtmlToPdf.Enums;

/// <summary>
/// Page orientation, see: https://doc.qt.io/archives/qt-4.8/qprinter.html#Orientation-enum.
/// </summary>
public enum PdfOrientation
{
    /// <summary>
    /// the page's height is greater than its width.
    /// </summary>
    Portrait = 0,

    /// <summary>
    /// the page's width is greater than its height.
    /// </summary>
    Landscape = 1
}

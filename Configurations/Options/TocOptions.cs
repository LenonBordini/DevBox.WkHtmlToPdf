using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class TocOptions
{
    /// <summary>
    /// Do not use dotted lines in the toc
    /// </summary>
    [BooleanCommandFlag("--disable-dotted-lines")]
    public bool? DisableDottedLines { get; set; }

    /// <summary>
    /// The header text of the toc (default Table of Contents)
    /// </summary>
    [CommandFlag("--toc-header-text")]
    public string TocHeaderText { get; set; }

    /// <summary>
    /// For each level of headings in the toc indent by this length (default 1em)
    /// </summary>
    [CommandFlag("--toc-level-indentation")]
    public int? TocLevelIndentation { get; set; }

    /// <summary>
    /// Do not link from toc to sections
    /// </summary>
    [BooleanCommandFlag("--disable-toc-links")]
    public bool? DisableTocLinks { get; set; }

    /// <summary>
    /// For each level of headings in the toc the font is scaled by this factor (default 0.8)
    /// </summary>
    [CommandFlag("--toc-text-size-shrink")]
    public decimal? TocTextSizeShrink { get; set; }

    /// <summary>
    /// Use the supplied xsl style sheet for printing the table of contents
    /// </summary>
    [CommandFlag("--xsl-style-sheet")]
    public string XslStyleSheet { get; set; }
}

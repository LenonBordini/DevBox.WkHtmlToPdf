using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class HeaderFooterOptions
{
    /// <summary>
    /// Centered footer text
    /// </summary>
    [CommandFlag("--footer-center")]
    public string FooterCenter { get; set; }

    /// <summary>
    /// Set footer font name (default Arial)
    /// </summary>
    [CommandFlag("--footer-font-name")]
    public string FooterFontName { get; set; }

    /// <summary>
    /// Set footer font size (default 12)
    /// </summary>
    [CommandFlag("--footer-font-size")]
    public int? FooterFontSize { get; set; }

    /// <summary>
    /// Adds a html footer
    /// </summary>
    [CommandFlag("--footer-html")]
    public string FooterHtml { get; set; }

    /// <summary>
    /// Left aligned footer text
    /// </summary>
    [CommandFlag("--footer-left")]
    public string FooterLeft { get; set; }

    /// <summary>
    /// Display line above the footer (default false)
    /// </summary>
    [BooleanCommandFlag("--footer-line", falseFlag: "--no-footer-line")]
    public bool? FooterLine { get; set; }

    /// <summary>
    /// Right aligned footer text
    /// </summary>
    [CommandFlag("--footer-right")]
    public string FooterRight { get; set; }

    /// <summary>
    /// Spacing between footer and content in mm (default 0)
    /// </summary>
    [CommandFlag("--footer-spacing")]
    public decimal? FooterSpacing { get; set; }

    /// <summary>
    /// Centered header text
    /// </summary>
    [CommandFlag("--header-center")]
    public string HeaderCenter { get; set; }

    /// <summary>
    /// Set header font name (default Arial)
    /// </summary>
    [CommandFlag("--header-font-name")]
    public string HeaderFontName { get; set; }

    /// <summary>
    /// Set header font size (default 12)
    /// </summary>
    [CommandFlag("--header-font-size")]
    public int? HeaderFontSize { get; set; }

    /// <summary>
    /// Adds a html header
    /// </summary>
    [CommandFlag("--header-html")]
    public string HeaderHtml { get; set; }

    /// <summary>
    /// Left aligned header text
    /// </summary>
    [CommandFlag("--header-left")]
    public string HeaderLeft { get; set; }

    /// <summary>
    /// Display line below the header (default false)
    /// </summary>
    [BooleanCommandFlag("--header-line", falseFlag: "--no-header-line")]
    public bool? HeaderLine { get; set; }

    /// <summary>
    /// Right aligned header text
    /// </summary>
    [CommandFlag("--header-right")]
    public string HeaderRight { get; set; }

    /// <summary>
    /// Spacing between header and content in mm (default 0)
    /// </summary>
    [CommandFlag("--header-spacing")]
    public decimal? HeaderSpacing { get; set; }

    /// <summary>
    /// Replace [name] with value in header and footer
    /// </summary>
    [CommandFlag("--replace")]
    public Dictionary<string, string> Replacements { get; set; }
}

using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class PageOptions
{
    /// <summary>
    /// Allow the file or files from the specified folder to be loaded
    /// </summary>
    [CommandFlag("--allow")]
    public IEnumerable<string> Allow { get; set; }

    /// <summary>
    /// Print background (default true)
    /// </summary>
    [BooleanCommandFlag("--background", falseFlag: "--no-background")]
    public bool? Background { get; set; }

    /// <summary>
    /// Bypass proxy for host
    /// </summary>
    [CommandFlag("--bypass-proxy-for")]
    public IEnumerable<string> BypassProxyFor { get; set; }

    /// <summary>
    /// Web cache directory
    /// </summary>
    [CommandFlag("--cache-dir")]
    public string CacheDir { get; set; }

    /// <summary>
    /// Use this SVG file when rendering checked checkboxes
    /// </summary>
    [CommandFlag("--checkbox-checked-svg")]
    public string CheckboxCheckedSvg { get; set; }

    /// <summary>
    /// Use this SVG file when rendering unchecked checkboxes
    /// </summary>
    [CommandFlag("--checkbox-svg")]
    public string CheckboxSvg { get; set; }

    /// <summary>
    /// Set additional cookies, value should be url encoded
    /// </summary>
    [CommandFlag("--cookie")]
    public Dictionary<string, string> Cookies { get; set; }

    /// <summary>
    /// Set additional HTTP headers
    /// </summary>
    [CommandFlag("--custom-header")]
    public Dictionary<string, string> CustomHeaders { get; set; }

    /// <summary>
    /// Add HTTP headers specified by <see cref="CustomHeaders"/> for each resource request.
    /// </summary>
    [BooleanCommandFlag("--custom-header-propagation")]
    public bool? CustomHeaderPropagation { get; set; }

    /// <summary>
    /// Show javascript debugging output (default false)
    /// </summary>
    [BooleanCommandFlag("--debug-javascript", falseFlag: "--no-debug-javascript")]
    public bool? DebugJavascript { get; set; }

    /// <summary>
    /// Set the default text encoding, for input
    /// </summary>
    [CommandFlag("--encoding")]
    public string Encoding { get; set; }

    /// <summary>
    /// Make links to remote web pages (default true)
    /// </summary>
    [BooleanCommandFlag("--enable-external-links", falseFlag: "--disable-external-links")]
    public bool? EnableExternalLinks { get; set; }

    /// <summary>
    /// Turn HTML form fields into pdf form fields (default false)
    /// </summary>
    [BooleanCommandFlag("--enable-forms", falseFlag: "--disable-forms")]
    public bool? EnableForms { get; set; }

    /// <summary>
    /// Load or print images (default true)
    /// </summary>
    [BooleanCommandFlag("--images", falseFlag: "--no-images")]
    public bool? Images { get; set; }

    /// <summary>
    /// Make local links (default true)
    /// </summary>
    [BooleanCommandFlag("--enable-internal-links", falseFlag: "--disable-internal-links")]
    public bool? EnableInternalLinks { get; set; }

    /// <summary>
    /// Allow web pages to run javascript (default true)
    /// </summary>
    [BooleanCommandFlag("--enable-javascript", falseFlag: "--disable-javascript", falseAlias: "-n")]
    public bool? EnableJavascript { get; set; }

    /// <summary>
    /// Wait some time (in milliseconds) for javascript to finish (default 200)
    /// </summary>
    [CommandFlag("--javascript-delay")]
    public int? JavascriptDelay { get; set; }

    /// <summary>
    /// Keep relative external links as relative external links
    /// </summary>
    [BooleanCommandFlag("--keep-relative-links")]
    public bool? KeepRelativeLinks { get; set; }

    /// <summary>
    /// Allowed conversion of a local file to read in other local files (default false)
    /// </summary>
    [BooleanCommandFlag("--enable-local-file-access", falseFlag: "--disable-local-file-access")]
    public bool? EnableLocalFileAccess { get; set; }

    /// <summary>
    /// Minimum font size
    /// </summary>
    [CommandFlag("--minimum-font-size")]
    public int? MinimumFontSize { get; set; }

    /// <summary>
    /// Set the starting page number (default 0)
    /// </summary>
    [CommandFlag("--page-offset")]
    public int? PageOffset { get; set; }

    /// <summary>
    /// HTTP Authentication password
    /// </summary>
    [CommandFlag("--password")]
    public string Password { get; set; }

    /// <summary>
    /// Use print media-type instead of screen (default false)
    /// </summary>
    [BooleanCommandFlag("--print-media-type", falseFlag: "--no-print-media-type")]
    public bool? PrintMediaType { get; set; }

    /// <summary>
    /// Use a proxy
    /// </summary>
    [CommandFlag("--proxy", alias: "-p")]
    public string Proxy { get; set; }

    /// <summary>
    /// Use the proxy for resolving hostnames
    /// </summary>
    [BooleanCommandFlag("--proxy-hostname-lookup")]
    public bool? ProxyHostnameLookup { get; set; }

    /// <summary>
    /// Use this SVG file when rendering checked radiobuttons
    /// </summary>
    [CommandFlag("--radiobutton-checked-svg")]
    public string RadioButtonCheckedSvg { get; set; }

    /// <summary>
    /// Use this SVG file when rendering unchecked radiobuttons
    /// </summary>
    [CommandFlag("--radiobutton-svg")]
    public string RadioButtonSvg { get; set; }

    /// <summary>
    /// Resolve relative external links into absolute links (default true)
    /// </summary>
    [BooleanCommandFlag("--resolve-relative-links")]
    public bool? ResolveRelativeLinks { get; set; }

    /// <summary>
    /// Run additional javascripts after the page is done loading
    /// </summary>
    [CommandFlag("--run-script")]
    public IEnumerable<string> RunScripts { get; set; }

    /// <summary>
    /// Enable the intelligent shrinking strategy used by WebKit that makes the pixel/dpi ratio non-constant (default true)
    /// </summary>
    [BooleanCommandFlag("--enable-smart-shrinking", falseFlag: "--disable-smart-shrinking")]
    public bool? EnableSmartShrinking { get; set; }

    /// <summary>
    /// Path to the ssl client cert public key in OpenSSL PEM format, optionally followed by intermediate ca and trusted certs
    /// </summary>
    [CommandFlag("--ssl-crt-path")]
    public string SslCrtPath { get; set; }

    /// <summary>
    /// Password to ssl client cert private key
    /// </summary>
    [CommandFlag("--ssl-key-password")]
    public string SslKeyPassword { get; set; }

    /// <summary>
    /// Path to ssl client cert private key in OpenSSL PEM format
    /// </summary>
    [CommandFlag("--ssl-key-path")]
    public string SslKeyPath { get; set; }

    /// <summary>
    /// Stop slow running javascripts (default true)
    /// </summary>
    [BooleanCommandFlag("--stop-slow-scripts", falseFlag: "--no-stop-slow-scripts")]
    public bool? StopSlowScripts { get; set; }

    /// <summary>
    /// Link from section header to toc (default false)
    /// </summary>
    [BooleanCommandFlag("--enable-toc-back-links", falseFlag: "--disable-toc-back-links")]
    public bool? EnableTocBackLinks { get; set; }

    /// <summary>
    /// Specify a user style sheet, to load with every page
    /// </summary>
    [CommandFlag("--user-style-sheet")]
    public string UserStyleSheet { get; set; }

    /// <summary>
    /// HTTP Authentication username
    /// </summary>
    [CommandFlag("--username")]
    public string Username { get; set; }

    /// <summary>
    /// Set viewport size if you have custom scrollbars or css attribute overflow to emulate window size
    /// </summary>
    [CommandFlag("--viewport-size")]
    public object ViewPortSize { get; set; }

    /// <summary>
    /// Wait until window.status is equal to this string before rendering page
    /// </summary>
    [CommandFlag("--window-status")]
    public string WindowStatus { get; set; }

    /// <summary>
    /// Use this zoom factor (default 1)
    /// </summary>
    [CommandFlag("--zoom")]
    public float? Zoom { get; set; }
}

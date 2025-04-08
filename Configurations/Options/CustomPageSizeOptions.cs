using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class CustomPageSizeOptions
{
    public CustomPageSizeOptions()
    {

    }

    public CustomPageSizeOptions(string height, string width)
    {
        Height = height;
        Width = width;
    }

    /// <summary>
    /// Page height
    /// </summary>
    [CommandFlag("--page-height")]
    public string Height { get; set; }

    /// <summary>
    /// Page width
    /// </summary>
    [CommandFlag("--page-width")]
    public string Width { get; set; }
}

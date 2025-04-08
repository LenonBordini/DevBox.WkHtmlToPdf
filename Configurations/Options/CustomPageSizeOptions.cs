using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class CustomPageSizeOptions
{
    public CustomPageSizeOptions()
    {

    }

    public CustomPageSizeOptions(object height, object width)
    {
        Height = height;
        Width = width;
    }

    /// <summary>
    /// Page height
    /// </summary>
    [CommandFlag("--page-height")]
    public object Height { get; set; }

    /// <summary>
    /// Page width
    /// </summary>
    [CommandFlag("--page-width")]
    public object Width { get; set; }
}

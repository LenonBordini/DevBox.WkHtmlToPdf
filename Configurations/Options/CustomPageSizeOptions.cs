using DevBox.WkHtmlToPdf.Configurations.Attributes;

namespace DevBox.WkHtmlToPdf.Configurations.Options;

public class CustomPageSizeOptions
{
    public CustomPageSizeOptions()
    {

    }

    public CustomPageSizeOptions(int height, int width)
    {
        Height = height;
        Width = width;
    }

    /// <summary>
    /// Page height (in mm)
    /// </summary>
    [CommandFlag("--page-height")]
    public int? Height { get; }

    /// <summary>
    /// Page width (in mm)
    /// </summary>
    [CommandFlag("--page-width")]
    public int? Width { get; }
}

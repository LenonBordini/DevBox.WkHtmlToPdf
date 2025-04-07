namespace DevBox.WkHtmlToPdf.Helpers;

internal static class FileHelper
{
    internal static bool IsHtml(string fileName)
    {
        return fileName.EndsWith(".html", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".htm", StringComparison.OrdinalIgnoreCase);
    }
}

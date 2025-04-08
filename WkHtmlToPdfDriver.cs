using System.Diagnostics;
using System.Runtime.InteropServices;
using DevBox.WkHtmlToPdf.Configurations.Options;
using DevBox.WkHtmlToPdf.Extensions;
using DevBox.WkHtmlToPdf.Factories;
using DevBox.WkHtmlToPdf.Helpers;

namespace DevBox.WkHtmlToPdf;

internal static class WkHtmlToPdfDriver
{
    public static string ExecutableFilePath;
    public static string TempPath;

    /// <summary>
    /// Converts given URL or HTML string to PDF.
    /// </summary>
    /// <param name="switches">Switches that will be passed to wkhtmltopdf binary.</param>
    /// <param name="html">String containing HTML code that should be converted to PDF.</param>
    /// <returns>PDF as byte array.</returns>
    public static async Task<byte[]> ConvertHtmlAsync(string html, PdfOptions pdfOptions)
    {
        var tempFileNameWithoutExtension = (string)null;
        try
        {
            tempFileNameWithoutExtension = Path.Combine(TempPath, $"{DateTime.Now:yyMMddHHmmss}-{Guid.NewGuid():N}");
            await File.WriteAllTextAsync($"{tempFileNameWithoutExtension}.html", html);

            if (!string.IsNullOrEmpty(pdfOptions?.HeaderFooter?.HeaderHtml))
                pdfOptions.HeaderFooter.HeaderHtml = await SaveTempHtmlAsync(pdfOptions.HeaderFooter.HeaderHtml, tempFileNameWithoutExtension, "header");
            if (!string.IsNullOrEmpty(pdfOptions?.HeaderFooter?.FooterHtml))
                pdfOptions.HeaderFooter.FooterHtml = await SaveTempHtmlAsync(pdfOptions.HeaderFooter.FooterHtml, tempFileNameWithoutExtension, "footer");

            var arguments = $"{pdfOptions?.GetCommandFlags()} \"{tempFileNameWithoutExtension}.html\" \"{tempFileNameWithoutExtension}.pdf\"".Trim();

            using var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = ExecutableFilePath,
                Arguments = $"-q {arguments}",
                UseShellExecute = false,
                RedirectStandardOutput = OSPlatformFactory.OsPlatform != OsPlatform.Linux,
                RedirectStandardError = true,
                RedirectStandardInput = OSPlatformFactory.OsPlatform != OsPlatform.Linux,
                CreateNoWindow = true
            };

            if (pdfOptions?.KeepTempFiles == true)
                await File.WriteAllTextAsync($"{tempFileNameWithoutExtension}-command.txt", $"\"{process.StartInfo.FileName}\" {process.StartInfo.Arguments}");

            process.Start();

            var error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (!File.Exists($"{tempFileNameWithoutExtension}.pdf"))
                throw new Exception(error);

            using var ms = new MemoryStream();
            using (var fileStream = new FileStream($"{tempFileNameWithoutExtension}.pdf", FileMode.Open, FileAccess.Read))
                fileStream.CopyTo(ms);

            return ms.ToArray();
        }
        finally
        {
            if (pdfOptions?.KeepTempFiles != true)
            {
                if (File.Exists($"{tempFileNameWithoutExtension}.html"))
                    File.Delete($"{tempFileNameWithoutExtension}.html");
                if (File.Exists($"{tempFileNameWithoutExtension}.pdf"))
                    File.Delete($"{tempFileNameWithoutExtension}.pdf");
            }
        }
    }

    /// <summary>
    /// Checks if the content of the "html" parameter is HTML, otherwise saves the content to an HTML file
    /// </summary>
    /// <param name="fileName">The file name</param>
    /// <param name="sufix">The file name sufix</param>
    /// <param name="html">The html to check</param>
    /// <returns>The path to the temp file</returns>
    private static async Task<string> SaveTempHtmlAsync(string html, string fileName, string sufix)
    {
        if (FileHelper.IsHtml(html))
            return html;

        var htmlFile = $"{fileName}-{sufix}.html";
        await File.WriteAllTextAsync(htmlFile, html);

        return htmlFile;
    }
}

using System.Diagnostics;
using System.Runtime.InteropServices;
using DevBox.WkHtmlToPdf.Configurations.Options;
using DevBox.WkHtmlToPdf.Extensions;
using DevBox.WkHtmlToPdf.Factories;
using DevBox.WkHtmlToPdf.Helpers;
using DevBox.WkHtmlToPdf.Interfaces.Driver;

namespace DevBox.WkHtmlToPdf.Drivers;

internal class WkHtmlToPdfDriver : IWkHtmlToPdfDriver
{
    private readonly OSPlatform _osPlatform;
    private readonly string _executablePath;
    private readonly string _tempPath;

    public WkHtmlToPdfDriver()
    {
        _osPlatform = OSPlatformFactory.GetOSPlatform();

        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? string.Empty, "wkhtmltopdf");

        if (_osPlatform == OSPlatform.Windows)
            _executablePath = Path.Combine(basePath, @"Executables\Windows\wkhtmltopdf.exe");
        else if (_osPlatform == OSPlatform.Linux)
            _executablePath = Path.Combine(basePath, @"Executables\Linux\wkhtmltopdf");
        else if (_osPlatform == OSPlatform.OSX)
            _executablePath = Path.Combine(basePath, @"Executables\Mac\wkhtmltopdf");
        else
            throw new Exception("Could not determine wkhtmltopdf executable path");

        _tempPath = Path.Combine(basePath, "Temp");
        if (!Directory.Exists(_tempPath))
            Directory.CreateDirectory(_tempPath);
    }

    /// <summary>
    /// Converts given URL or HTML string to PDF.
    /// </summary>
    /// <param name="switches">Switches that will be passed to wkhtmltopdf binary.</param>
    /// <param name="html">String containing HTML code that should be converted to PDF.</param>
    /// <returns>PDF as byte array.</returns>
    public async Task<byte[]> ConvertHtmlAsync(string html, PdfOptions pdfOptions)
    {
        var tempFileNameWithoutExtension = (string)null;
        try
        {
            tempFileNameWithoutExtension = Path.Combine(_tempPath, $"{DateTime.Now:yyMMddHHmmss}-{Guid.NewGuid():N}");
            await File.WriteAllTextAsync($"{tempFileNameWithoutExtension}.html", html);

            if (!string.IsNullOrEmpty(pdfOptions.HeaderFooterOptions?.HeaderHtml))
                pdfOptions.HeaderFooterOptions.HeaderHtml = await SaveTempHtmlAsync(pdfOptions.HeaderFooterOptions.HeaderHtml, tempFileNameWithoutExtension, "header");
            if (!string.IsNullOrEmpty(pdfOptions.HeaderFooterOptions?.FooterHtml))
                pdfOptions.HeaderFooterOptions.FooterHtml = await SaveTempHtmlAsync(pdfOptions.HeaderFooterOptions.FooterHtml, tempFileNameWithoutExtension, "footer");

            var arguments = "-q";
            if (pdfOptions != null)
            {
                var optionsFlags = pdfOptions.GetCommandFlags();
                if (!string.IsNullOrEmpty(optionsFlags))
                    arguments += $" {optionsFlags}";
            }

            arguments += $" \"{tempFileNameWithoutExtension}.html\" \"{tempFileNameWithoutExtension}.pdf\"";

            using var process = new Process();

            process.StartInfo = new ProcessStartInfo
            {
                FileName = _executablePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = _osPlatform != OSPlatform.Linux,
                RedirectStandardError = true,
                RedirectStandardInput = _osPlatform != OSPlatform.Linux,
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

        await File.WriteAllTextAsync($"{fileName}-{sufix}.html", html);
        return fileName;
    }
}

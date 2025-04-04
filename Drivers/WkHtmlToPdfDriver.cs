using System.Diagnostics;
using System.Runtime.InteropServices;
using DevBox.WkHtmlToPdf.Configurations.Options;
using DevBox.WkHtmlToPdf.Extensions;
using DevBox.WkHtmlToPdf.Factories;

namespace DevBox.WkHtmlToPdf.Drivers;

internal static class WkHtmlToPdfDriver
{
    private static OSPlatform? _osPlatform;
    private static string _executablePath;

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
            if (_osPlatform == null)
                _osPlatform = OSPlatformFactory.GetOSPlatform();

            var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? string.Empty, "wkhtmltopdf");
            var tempPath = Path.Combine(basePath, "Temp");
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            if (string.IsNullOrEmpty(_executablePath))
            {
                if (_osPlatform == OSPlatform.Windows)
                    _executablePath = Path.Combine(basePath, @"Executables\Windows\wkhtmltopdf.exe");
                else if (_osPlatform == OSPlatform.Linux)
                    _executablePath = Path.Combine(basePath, @"Executables\Linux\wkhtmltopdf");
                else if (_osPlatform == OSPlatform.OSX)
                    _executablePath = Path.Combine(basePath, @"Executables\Mac\wkhtmltopdf");
                else
                    throw new Exception("Could not determine wkhtmltopdf executable path");
            }

            var arguments = "-q";
            if (pdfOptions != null)
            {
                var optionsFlags = pdfOptions.GetCommandFlags();
                if (!string.IsNullOrEmpty(optionsFlags))
                    arguments += $" {optionsFlags}";
            }

            tempFileNameWithoutExtension = Path.Combine(tempPath, $"{DateTime.Now:yyMMddHHmmss}-{Guid.NewGuid().ToString().Replace("-", string.Empty).ToUpper()}");
            await File.WriteAllTextAsync($"{tempFileNameWithoutExtension}.html", html);

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
}

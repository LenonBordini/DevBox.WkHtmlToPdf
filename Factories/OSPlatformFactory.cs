using System.Runtime.InteropServices;

namespace DevBox.WkHtmlToPdf.Factories;

public class OSPlatformFactory
{
    private static OSPlatform? _osPlatform = null;

    public static OSPlatform GetOSPlatform()
    {
        if (_osPlatform != null)
            return (OSPlatform)_osPlatform;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            _osPlatform = OSPlatform.Windows;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            _osPlatform = OSPlatform.Linux;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            _osPlatform = OSPlatform.OSX;

        if (_osPlatform == null)
            throw new Exception("Unable to determine operating system platform");

        return (OSPlatform)_osPlatform;
    }
}

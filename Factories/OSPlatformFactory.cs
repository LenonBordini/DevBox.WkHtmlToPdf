using System.Runtime.InteropServices;

namespace DevBox.WkHtmlToPdf.Factories;

internal class OSPlatformFactory
{
    private static OsPlatform? _osPlatform;
    public static OsPlatform OsPlatform
    {
        get
        {
            if (_osPlatform != null)
                return (OsPlatform)_osPlatform;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                _osPlatform = OsPlatform.Windows;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                _osPlatform = OsPlatform.Linux;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                _osPlatform = OsPlatform.Mac;
            else
                _osPlatform = OsPlatform.Unknown;

            return (OsPlatform)_osPlatform;
        }
    }
}

internal enum OsPlatform
{
    Unknown = 0,
    Windows = 1,
    Linux = 2,
    Mac = 3
}

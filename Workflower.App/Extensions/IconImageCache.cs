using System.Drawing;

#pragma warning disable CA1416 // Validate platform compatibility

namespace Workflower.App.Extensions;

public static class IconImageCache
{
    private static readonly Dictionary<string, byte[]> _imageCache = new();

    public static byte[] GetBytes(string executableName)
    {
        if (!_imageCache.ContainsKey(executableName))
        {
            var icon = System.Drawing.Icon.ExtractAssociatedIcon(executableName);
            var bytes = new ImageConverter().ConvertTo(icon.ToBitmap(), typeof(byte[]));

            _imageCache.Add(executableName, bytes as byte[] ?? Array.Empty<byte>());
        }

        return _imageCache[executableName];
    }
}
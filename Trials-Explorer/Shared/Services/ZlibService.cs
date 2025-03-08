using System.IO;
using System.IO.Compression;

namespace Trials_Explorer.Shared.Services
{
    public static class ZlibService
    {
        public static byte[] Compress(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                return new byte[0];
            }

            using (var outputStream = new MemoryStream())
            {
                using (var zlibStream = new ZLibStream(outputStream, CompressionLevel.Optimal, true))
                {
                    zlibStream.Write(buffer, 0, buffer.Length);
                }

                return outputStream.ToArray();
            }
        }

        public static byte[] Decompress(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                return new byte[0];
            }

            using (var inputStream = new MemoryStream(buffer))
            using (var zlibStream = new ZLibStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                zlibStream.CopyTo(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}

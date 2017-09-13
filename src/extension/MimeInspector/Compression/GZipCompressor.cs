using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace MimeInspector.Compression
{
    internal class GZipCompressor
    {
        /// <summary>
        /// Decompresses the specified compressed stream.
        /// </summary>
        /// <param name="inputStream">The compressed stream.</param>
        /// <returns></returns>
        public static Stream Decompress(Stream inputStream)
        {
            var memoryStream = new MemoryStream();

            try
            {
                using (var gzip = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    gzip.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                    return memoryStream;
                }
            }
            catch (Exception)
            {
                memoryStream.Dispose();
                throw;
            }
        }

        public static async Task<Stream> DecompressAsync(Stream inputStream)
        {
            var memoryStream = new MemoryStream();

            try
            {
                using (var gzip = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    await gzip.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    return memoryStream;
                }
            }
            catch (Exception)
            {
                memoryStream.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Compresses the specified original stream.
        /// </summary>
        /// <param name="inputStream">The original stream.</param>
        /// <returns>A new Stream containing the compressed contents.</returns>
        public static Stream Compress(Stream inputStream)
        {
            var outputStream = new MemoryStream();

            using (var gzip = new GZipStream(outputStream, CompressionMode.Compress, true))
            {
                inputStream.CopyTo(gzip);
            }

            outputStream.Position = 0;

            return outputStream;
        }

        public static async Task<Stream> CompressAsync(Stream inputStream)
        {
            var outputStream = new MemoryStream();

            using (var gzip = new GZipStream(outputStream, CompressionMode.Compress, true))
            {
                await inputStream.CopyToAsync(gzip);
            }

            outputStream.Position = 0;

            return outputStream;
        }
    }
}

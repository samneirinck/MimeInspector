using System;
using System.IO;
using System.IO.Compression;

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

        /// <summary>
        /// Compresses the specified original stream.
        /// </summary>
        /// <param name="inputStream">The original stream.</param>
        /// <param name="outputStream"></param>
        /// <returns></returns>
        public static void Compress(Stream inputStream, Stream outputStream)
        {
            using (inputStream)
            using (var gzip = new GZipStream(outputStream, CompressionMode.Compress, true))
            {
                inputStream.CopyTo(gzip);
            }

            outputStream.Position = 0;

        }
    }
}

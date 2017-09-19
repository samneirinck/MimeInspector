using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fiddler;
using MimeKit.IO;

namespace MimeInspector.Utilities
{
    public static class MimeParser
    {
        public static Task<MimeMessage> ParseMessageAsync(byte[] message, HTTPHeaders headers, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() =>
            {
                return ParseMessage(message, headers, cancellationToken);
            }, cancellationToken);
        }

        public static Task<MimeMessage> ParseMessageAsync(Stream inputStream, HTTPHeaders headers, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() =>
            {
                return ParseMessage(inputStream, headers, cancellationToken);
            }, cancellationToken);
        }

        public static MimeMessage ParseMessage(byte[] message, HTTPHeaders headers, CancellationToken cancellationToken)
        {
            using (var ms = new MemoryStream(message))
            {
                return ParseMessage(ms, headers, cancellationToken);
            }
        }

        public static MimeMessage ParseMessage(Stream inputStream, HTTPHeaders headers, CancellationToken cancellationToken)
        {
            if (inputStream == null || inputStream == Stream.Null)
            {
                return null;
            }

            var headerArray = headers.ToArray();

            if(IsMimeMessage(headerArray) == false)
            {
                return null;
            }

            string headerString = String.Join("\r\n", headerArray.Select(h => $"{h.Name}:{h.Value}"));

            try
            {
                using (ChainedStream streamWithHeaders = new ChainedStream())
                {
                    streamWithHeaders.Add(new MemoryStream(Encoding.UTF8.GetBytes(headerString)), false);
                    streamWithHeaders.Add(inputStream, false);

                    var parser = new MimeKit.MimeParser(streamWithHeaders);

                    return parser.ParseMessage(cancellationToken);
                }
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private static bool IsMimeMessage(HTTPHeaderItem[] headers)
        {
            var contentType = headers.FirstOrDefault(h => StringComparer.OrdinalIgnoreCase.Equals(h.Name, "content-type"));

            if (contentType != null &&
                contentType.Value.IndexOf("multipart/related", StringComparison.OrdinalIgnoreCase) > -1 )
            {
                return true;
            }

            return false;
        }
    }
}

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
    public static class AsyncMimeParser
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

            string headerString = String.Join("\r\n", headers.ToArray().Select(h => $"{h.Name}:{h.Value}"));

            using (ChainedStream streamWithHeaders = new ChainedStream())
            {
                streamWithHeaders.Add(new MemoryStream(Encoding.UTF8.GetBytes(headerString)), false);
                streamWithHeaders.Add(inputStream, false);

                var parser = new MimeParser(streamWithHeaders);

                return parser.ParseMessage(cancellationToken);
            }
        }
    }
}

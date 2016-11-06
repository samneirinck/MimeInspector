using MimeKit;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MimeInspector.Utilities
{
    public static class AsyncMimeParser
    {
        public static Task<MimeMessage> ParseMessageAsync(Stream inputStream, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            return Task.Run(() =>
            {
                MimeMessage parsedMessage = null;

                var parser = new MimeParser(inputStream);
                parsedMessage = parser.ParseMessage(cancellationToken);

                return parsedMessage;
            }, cancellationToken);
        }
    }
}

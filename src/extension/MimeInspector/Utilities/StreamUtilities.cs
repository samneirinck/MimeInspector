using Fiddler;
using MimeKit.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MimeInspector.Utilities
{
    internal class StreamUtilities
    {
        public static Stream CreateStreamFromBodyAndHttpHeaders(byte[] body, HTTPHeaders headers)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            ChainedStream resultStream = new ChainedStream();

            string headerString = string.Join("\r\n", headers.ToArray().Select(x => $"{x.Name}: {x.Value}"));
            resultStream.Add(new MemoryStream(Encoding.UTF8.GetBytes(headerString)));
            resultStream.Add(new MemoryStream(body));

            return resultStream;
        }
    }
}
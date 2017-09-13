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
        /// <summary>
        /// Reads the <paramref name="input"/> Stream and return its contents as a byte array.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
}
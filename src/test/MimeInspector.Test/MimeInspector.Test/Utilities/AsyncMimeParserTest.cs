using MimeInspector.Test.Properties;
using MimeInspector.Utilities;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MimeInspector.Test.Utilities
{
    public class AsyncMimeParserTest
    {
        [Fact]
        public async Task ParseMessageAsync_EmptyMessageStream_FormatException()
        {
            // Arrange
            using (var inputStream = new MemoryStream())
            {
                // Act & Assert
                await Assert.ThrowsAsync<FormatException>(() => AsyncMimeParser.ParseMessageAsync(inputStream));
            }
        }

        [Fact]
        public async Task ParseMessageAsync_NonMimeStream_FormatException()
        {
            // Arrange
            using (var inputStream = new MemoryStream(Encoding.UTF8.GetBytes("Hello world")))
            {
                // Act & Assert
                await Assert.ThrowsAsync<FormatException>(() => AsyncMimeParser.ParseMessageAsync(inputStream));
            }
        }

        [Fact]
        public async Task ParseMessageAsync_ValidMimeStream_MessageReturned()
        {
            // Arrange
            using (var inputStream = new MemoryStream(Resources.As4Message))
            {
                // Act
                var message = await AsyncMimeParser.ParseMessageAsync(inputStream);

                // Assert
                Assert.NotNull(message);
            }
        }

        [Fact]
        public async Task ParseMessageAsync_ValidMimeStreamButWithCanceledCancellationToken_OperationCanceledException()
        {
            // Arrange
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            using (var inputStream = new MemoryStream(Resources.As4Message))
            {
                // Act & Assert
                await Assert.ThrowsAsync<OperationCanceledException>(() => AsyncMimeParser.ParseMessageAsync(inputStream, cancellationTokenSource.Token));
            }
        }

    }
}

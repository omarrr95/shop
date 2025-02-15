using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace eCommerce.Shared.Attributes
{
    /// <summary>
    /// Attribute that compresses response content if the client supports it.
    /// </summary>
    public class CompressContentAttribute : ActionFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var response = context.HttpContext.Response;
            var request = context.HttpContext.Request;

            if (!IsCompressionSupported(request))
            {
                await next();
                return;
            }

            var encoding = request.Headers["Accept-Encoding"].ToString();
            var originalBodyStream = response.Body;

            try
            {
                using var compressedStream = new MemoryStream();
                Stream compressionStream;

                if (encoding.Contains("gzip"))
                {
                    compressionStream = new GZipStream(compressedStream, CompressionMode.Compress, true);
                    response.Headers.Add("Content-Encoding", "gzip");
                }
                else
                {
                    compressionStream = new DeflateStream(compressedStream, CompressionMode.Compress, true);
                    response.Headers.Add("Content-Encoding", "deflate");
                }

                response.Headers.Add("Vary", "Content-Encoding");

                using (compressionStream)
                {
                    response.Body = compressionStream;
                    await next(); // Execute action and write response

                    await compressionStream.FlushAsync();
                }

                // Write the compressed data back to the original response stream
                compressedStream.Seek(0, SeekOrigin.Begin);
                await compressedStream.CopyToAsync(originalBodyStream);
            }
            finally
            {
                response.Body = originalBodyStream; // Restore original response stream
            }
        }

        /// <summary>
        /// Checks if the client supports GZip or Deflate compression.
        /// </summary>
        private static bool IsCompressionSupported(HttpRequest request)
        {
            return request.Headers.ContainsKey("Accept-Encoding") &&
                   (request.Headers["Accept-Encoding"].ToString().Contains("gzip") ||
                    request.Headers["Accept-Encoding"].ToString().Contains("deflate"));
        }
    }
}

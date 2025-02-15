using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Shared.Attributes
{
    public abstract class OutputProcessorActionFilterAttribute : ActionFilterAttribute
    {
        public OutputProcessorActionFilterAttribute()
        {
            InputEncoding = Encoding.UTF8;
            OutputEncoding = Encoding.UTF8;
        }

        /// <summary>Gets or sets the input encoding.</summary>
        public Encoding InputEncoding { get; set; }

        /// <summary>Gets or sets the output encoding.</summary>
        public Encoding OutputEncoding { get; set; }

        /// <summary>Processes the output data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The processed data.</returns>
        protected abstract string Process(string data);

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var response = context.HttpContext.Response;

            // Store the original response body
            var originalBodyStream = response.Body;

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    response.Body = memoryStream;

                    await next(); // Continue processing the response

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(memoryStream, InputEncoding))
                    {
                        var body = await reader.ReadToEndAsync();
                        var processedBody = Process(body);

                        var outputBytes = OutputEncoding.GetBytes(processedBody);
                        response.ContentLength = outputBytes.Length;

                        // Reset the stream to write processed output
                        memoryStream.SetLength(0);
                        await memoryStream.WriteAsync(outputBytes, 0, outputBytes.Length);
                        await memoryStream.FlushAsync();
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        await memoryStream.CopyToAsync(originalBodyStream);
                    }
                }
            }
            finally
            {
                response.Body = originalBodyStream; // Restore original response body
            }
        }
    }
}

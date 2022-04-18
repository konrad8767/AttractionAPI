using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AttractionAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopwatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            this._logger = logger;
            this._stopwatch = new Stopwatch();

        }
        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            if (_stopwatch.ElapsedMilliseconds > 4000)
            {
                _logger.LogInformation($"Request [{context.Request.Method}] at {context.Request.Path} took {_stopwatch.ElapsedMilliseconds} ms.");
            }

        }
    }
}

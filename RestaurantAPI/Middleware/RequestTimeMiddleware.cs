using System.Diagnostics;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {           
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next.Invoke(context);
            stopWatch.Stop();

            if (stopWatch.ElapsedMilliseconds > 4000)
                _logger.LogInformation($"Request {context.Request.Method} with path: {context.Request.Path} took longer than 4 seconds," +
                    $" actual request time {stopWatch.ElapsedMilliseconds} ms");
        }
    }
}

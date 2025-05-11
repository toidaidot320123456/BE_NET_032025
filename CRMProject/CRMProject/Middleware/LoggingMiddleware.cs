namespace CRMProject.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            string request = string.Format("Request Method {0}, Path {1}", httpContext.Request.Method.ToString(), httpContext.Request.Path.ToString());
            _logger.LogError(request);
            await _next(httpContext);
            string response = string.Format("Response StatusCode {0}", httpContext.Response.StatusCode.ToString());
            _logger.LogError(response);
        }
    }
}

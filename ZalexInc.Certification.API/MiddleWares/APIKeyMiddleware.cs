namespace ZalexInc.Certification.API.MiddleWares
{
    public class APIKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private const string APIKeyHeader = "X-API-Key";

        public APIKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var providedAPIKey = context.Request.Headers[APIKeyHeader];
            var expectedAPIKey = _configuration["APIKey"];

            if (string.IsNullOrEmpty(providedAPIKey) || providedAPIKey != expectedAPIKey)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API key");
                return;
            }

            await _next(context);
        }
    }
}

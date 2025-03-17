namespace BloodsyncAPI.Helpers
{
    public class ApiKeyAuth
    {
        public const string ApiKeySectionName = "Authorization:ApiKey";
        public const string ApiKeyHeaderName = "X-Api-Key";

        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyAuth(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key Missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(ApiKeySectionName);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }
    }
}

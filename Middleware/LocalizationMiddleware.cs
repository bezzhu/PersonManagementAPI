using System.Globalization;

namespace PersonManagementAPI.Middleware
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var supportedCultures = new[] { "en-US", "ka-GE" }; 
            var defaultCulture = "en-US";

            var requestCulture = context.Request.Headers["Accept-Language"].ToString();
            var culture = supportedCultures.Contains(requestCulture) ? requestCulture : defaultCulture;

            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            await _next(context);
        }
    }
}


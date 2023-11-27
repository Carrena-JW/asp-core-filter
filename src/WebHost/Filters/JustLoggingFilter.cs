

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace WebHost.Filters
{

    public class JustLoggingFilter : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<JustLoggingFilter> _logger;

        public JustLoggingFilter(ILogger<JustLoggingFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation($"Start Method : {context.ActionDescriptor.DisplayName}");
            await next();
            sw.Stop();
            _logger.LogInformation($"End Method : {context.ActionDescriptor.DisplayName}, {sw.ElapsedMilliseconds}ms");

        }
         
    }
     


}

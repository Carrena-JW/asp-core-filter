

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace WebHost.Filters
{

    public class DurationLoggingFilter : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<DurationLoggingFilter> _logger;
        private readonly IMemoryCache _memoryCache;

        public DurationLoggingFilter(ILogger<DurationLoggingFilter> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var param = context.ActionArguments.Count == 0 ? null : context.ActionArguments;

            var key = new MemoryCacheKey(context.ActionDescriptor.DisplayName!, param);
            
            
            var memoryResult = _memoryCache.Get(key);

            if(memoryResult != null)
            {
                _logger.LogInformation("From cached data");
                context.Result = new ObjectResult(memoryResult);
            }
            else
            {
                _logger.LogInformation("From repository data");
                var resultContext = await next();
                if (resultContext.Result is ObjectResult objectResult)
                {
                    var resultValue = objectResult.Value;
                    _memoryCache.Set(key, resultValue,TimeSpan.FromSeconds(10));
                }
            }
        }
         
    }


    public record MemoryCacheKey(string from, object? param);




}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace StudentManagement.API.Filters
{
    public class CustomCacheResourceFilter : IResourceFilter
    {
        private readonly IMemoryCache _cache;
        public CustomCacheResourceFilter(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var cacheKey = context.HttpContext.Request.Path.ToString();
            if (_cache.TryGetValue(cacheKey, out IActionResult cachedResult))
            {
                context.Result = cachedResult; // Short-circuit the pipeline
            }
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var cacheKey = context.HttpContext.Request.Path.ToString();
            if (!_cache.TryGetValue(cacheKey, out _))
            {
                _cache.Set(cacheKey, context.Result, TimeSpan.FromMinutes(5));
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Options;
using OrderApi.Services;

namespace OrderApi.Filters
{
    internal class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly TimeSpan _timeToLive;

        public CacheAttribute(int timeToLive) => _timeToLive = new TimeSpan(timeToLive);

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(cacheResponse))
            {
                var contextResponse = new OkObjectResult(cacheResponse)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    ContentTypes = new Collection<string> { "application/json" } as MediaTypeCollection
                };

                context.Result = contextResponse;

                await next();
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, _timeToLive);
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}

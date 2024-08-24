using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace twitchbot.Middleware;

public class PointsMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new System.NotImplementedException();
    }
}
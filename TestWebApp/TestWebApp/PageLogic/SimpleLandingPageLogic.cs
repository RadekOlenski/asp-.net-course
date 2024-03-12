using TestWebApp.PageLogic.Utilities;

namespace TestWebApp.PageLogic;

internal sealed class SimpleLandingPageLogic : ILandingPageLogic
{
    private SimpleLandingPageLogic() { }

    public static ILandingPageLogic CreateInstance() => new SimpleLandingPageLogic();

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!await ProcessLandingPage(context))
        {
            await next(context);
        }
    }

    public async Task<bool> ProcessLandingPage(HttpContext context)
    {
        if (context.Request.Path != Pages.LandingPagePath)
        {
            return false;
        }

        //Awaiting async responses with HTML content
        await context.Response.WriteAsync("<h1>Welcome</h1>");
        await context.Response.WriteAsync("<h2>This is simple ASP .NET site</h2>");
        await context.Response.WriteAsync("<h3>Currently available content:</h3>");
        await context.Response.WriteAsync("<h3> - Home</h3>");
        await context.Response.WriteAsync("<h3> - Exit</h3>");
        return true;
    }
}

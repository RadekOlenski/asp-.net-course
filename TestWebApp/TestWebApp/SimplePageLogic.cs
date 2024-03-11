namespace TestWebApp;

internal static class SimplePageLogic
{
    private const string ExitPagePath = "/exit";
    private const string LandingPagePath = "/";

    internal static Task ProcessExitPage(HttpContext context) =>
        context.Request.Path != ExitPagePath
            ? Task.CompletedTask
            : context.Response.WriteAsync("<h1> Exiting page </h1>");

    internal static async Task ProcessLandingPage(HttpContext context)
    {
        if (context.Request.Path != LandingPagePath)
        {
            return;
        }

        //Awaiting async responses with HTML content
        await context.Response.WriteAsync("<h1>Welcome</h1>");
        await context.Response.WriteAsync("<h2>This is simple ASP .NET site</h2>");
        await context.Response.WriteAsync("<h3>Currently available content:</h3>");
        await context.Response.WriteAsync("<h3> - Home</h3>");
        await context.Response.WriteAsync("<h3> - Exit</h3>");
    }

    internal static async Task ProcessWrongPage(HttpContext context)
    {
        if (context.Request.Path == LandingPagePath || context.Request.Path == ExitPagePath)
        {
            return;
        }

        //Awaiting async responses with HTML content
        await context.Response.WriteAsync("<h1>Wrong site</h1>");
        await context.Response.WriteAsync("<h2>Please try again</h2>");
    }
}

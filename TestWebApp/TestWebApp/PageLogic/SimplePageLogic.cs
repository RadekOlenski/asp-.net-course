namespace TestWebApp.PageLogic;

internal sealed class SimplePageLogic : IPageLogic
{
    private const string ExitPagePath = "/exit";
    private const string LandingPagePath = "/";

    private string[] _availablePages = Array.Empty<string>();

    private SimplePageLogic()
    {
        ConstructPagesArray(LandingPagePath, ExitPagePath);
    }

    public static IPageLogic CreateInstance() => new SimplePageLogic();

    public async Task ProcessExitPage(HttpContext context)
    {
        if (context.Request.Path != ExitPagePath)
        {
            return;
        }

        await context.Response.WriteAsync("<h1> Exiting page </h1>");
        await LookForUserID(context);
        await LookForUserBrowser(context);
    }

    public async Task ProcessLandingPage(HttpContext context)
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

    public async Task ProcessWrongPage(HttpContext context)
    {
        var requestedPath = context.Request.Path;
        if (Array.Exists(_availablePages, pageName => pageName == requestedPath))
        {
            return;
        }

        //Awaiting async responses with HTML content
        await context.Response.WriteAsync("<h1>Wrong site!</h1>");
        await context.Response.WriteAsync($"<h2>Site {requestedPath} does not exist.</h2>");
        await context.Response.WriteAsync("<h2>Please try again.</h2>");
    }

    private static async Task LookForUserBrowser(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("User-Agent", out var userBrowser))
        {
            await context.Response.WriteAsync($"<h2> User using browser: {userBrowser} </h2>");
        }
        else
        {
            await context.Response.WriteAsync("<h2> Browser could not be determined. </h2>");
        }
    }

    private static async Task LookForUserID(HttpContext context)
    {
        if (context.Request.Query.TryGetValue("id", out var idValue))
        {
            await context.Response.WriteAsync($"<h2> Logging out user with id: {idValue} </h2>");
        }
        else
        {
            await context.Response.WriteAsync("<h2> No user found. </h2>");
        }
    }

    private void ConstructPagesArray(params string[] availablePages)
    {
        _availablePages = availablePages;
    }
}

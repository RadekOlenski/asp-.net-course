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

    public Task ProcessExitPage(HttpContext context) =>
        context.Request.Path != ExitPagePath
            ? Task.CompletedTask
            : context.Response.WriteAsync("<h1> Exiting page </h1>");

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
        if (Array.Exists(_availablePages, pageName => pageName == context.Request.Path))
        {
            return;
        }

        //Awaiting async responses with HTML content
        await context.Response.WriteAsync("<h1>Wrong site</h1>");
        await context.Response.WriteAsync("<h2>Please try again</h2>");
    }

    private void ConstructPagesArray(params string[] availablePages)
    {
        _availablePages = availablePages;
    }
}

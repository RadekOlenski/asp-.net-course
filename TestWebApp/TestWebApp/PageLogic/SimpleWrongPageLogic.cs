using TestWebApp.PageLogic.Utilities;

namespace TestWebApp.PageLogic;

internal sealed class SimpleWrongPageLogic : IWrongPageLogic
{
    private string[] _availablePages = Array.Empty<string>();

    private SimpleWrongPageLogic()
    {
        ConstructPagesArray(Pages.LandingPagePath, Pages.ExitPagePath);
    }

    public static IWrongPageLogic CreateInstance() => new SimpleWrongPageLogic();

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!await ProcessWrongPage(context))
        {
            await next(context);
        }
    }

    public async Task<bool> ProcessWrongPage(HttpContext context)
    {
        var requestedPath = context.Request.Path;
        if (Array.Exists(_availablePages, pageName => pageName == requestedPath))
        {
            return false;
        }

        //Awaiting async responses with HTML content
        await context.Response.WriteAsync("<h1>Wrong site!</h1>");
        await context.Response.WriteAsync($"<h2>Site {requestedPath} does not exist.</h2>");
        await context.Response.WriteAsync("<h2>Please try again.</h2>");
        return true;
    }

    private void ConstructPagesArray(params string[] availablePages)
    {
        _availablePages = availablePages;
    }
}

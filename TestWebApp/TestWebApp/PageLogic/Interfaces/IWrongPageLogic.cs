namespace TestWebApp.PageLogic;

internal interface IWrongPageLogic : IMiddleware
{
    Task<bool> ProcessWrongPage(HttpContext context);
}

public static class WrongPageExtension
{
    public static IApplicationBuilder UseWrongPage(this IApplicationBuilder app) =>
        app.UseMiddleware<IWrongPageLogic>();
}

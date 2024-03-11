namespace TestWebApp.PageLogic;

internal interface IPageLogic
{
    Task ProcessExitPage(HttpContext context);
    Task ProcessLandingPage(HttpContext context);
    Task ProcessWrongPage(HttpContext context);
}

using TestWebApp.PageLogic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var pageLogic = SimplePageLogic.CreateInstance();

//Simple response map for GET request sent from home page sending over a text
app.MapGet("/", static () => "Hello World!");

app.Run(
    async context =>
    {
        //Putting own key/value pair into response headers
        context.Response.Headers["Test-Key"] = "Test Value";

        //Setting response content type
        context.Response.Headers.ContentType = "text/html";

        await pageLogic.ProcessLandingPage(context);
        await pageLogic.ProcessWrongPage(context);
        await pageLogic.ProcessExitPage(context);
    });

app.Run();

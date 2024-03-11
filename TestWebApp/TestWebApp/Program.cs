using TestWebApp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Simple response map for GET request sent from home page sending over a text
app.MapGet("/", static () => "Hello World!");

app.Run(
    static async context =>
    {
        //Putting own key/value pair into response headers
        context.Response.Headers["Test-Key"] = "Test Value";

        //Setting response content type
        context.Response.Headers.ContentType = "text/html";

        await SimplePageLogic.ProcessLandingPage(context);
        await SimplePageLogic.ProcessWrongPage(context);
        await SimplePageLogic.ProcessExitPage(context);
    });

app.Run();

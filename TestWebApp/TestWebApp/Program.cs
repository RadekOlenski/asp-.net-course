var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Simple response map for GET request sent from home page sending over a text
app.MapGet("/", static () => "Hello World!");

app.Run(
    async static context =>
    {
        //Putting own key/value pair into response headers
        context.Response.Headers["Test-Key"] = "Test Value";

        //Setting response content type
        context.Response.Headers.ContentType = "text/html";

        //Awaiting async responses with HTML content
        await context.Response.WriteAsync("<h1>Wrong site</h1>");
        await context.Response.WriteAsync("<h2>Please try again</h2>");
    });

app.Run();

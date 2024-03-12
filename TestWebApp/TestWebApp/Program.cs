using TestWebApp.PageLogic;

var builder = WebApplication.CreateBuilder(args);

//Building middlewares as DI services
builder.Services.AddTransient<ILandingPageLogic>(static _ => SimpleLandingPageLogic.CreateInstance());
builder.Services.AddTransient<IWrongPageLogic>(static _ => SimpleWrongPageLogic.CreateInstance());
builder.Services.AddTransient<IExitPageLogic>(static _ => SimpleExitPageLogic.CreateInstance());

var app = builder.Build();

//Basic add of middlewares by calling UseMiddlewares multiple times
// app.UseMiddleware<ILandingPageLogic>();
// app.UseMiddleware<IWrongPageLogic>();
// app.UseMiddleware<IExitPageLogic>();

//Chaining middleware by extension methods
app.UseLandingPage().UseWrongPage().UseExitPage();

//Simple response map for GET request sent from home page sending over a text
//app.MapGet("/", static () => "Hello World!");

//---------------------- MIDDLEWARE SHORT_CIRCUIT WITH RUN -----------------------//
//Test response short-circuit middleware
// app.Run(
//     static context =>
//     {
//         //Putting own key/value pair into response headers
//         context.Response.Headers["Test-Key"] = "Test Value";
//
//         //Setting response content type
//         context.Response.Headers.ContentType = "text/html";
//         return Task.CompletedTask;
//     });

//---------------------- MIDDLEWARE WITH USE -----------------------//
//Test middleware chain with Use method
//var pageLogic = SimpleWrongPageLogic.CreateInstance();
// app.Use(
//     async (context, next) =>
//     {
//         if (!await pageLogic.ProcessLandingPage(context))
//         {
//             await next(context);
//         }
//     });
//
// app.Use(
//     async (context, next) =>
//     {
//         if (!await pageLogic.ProcessWrongPage(context))
//         {
//             await next(context);
//         }
//     });
//
// app.Use(
//     async (context, next) =>
//     {
//         if (!await pageLogic.ProcessExitPage(context))
//         {
//             await next(context);
//         }
//     });

app.Run();

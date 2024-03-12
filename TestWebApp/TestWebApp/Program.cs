// <copyright file="Program.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

using TestWebApp.PageLogic;
using TestWebApp.PageLogic.Utilities;

var builder = WebApplication.CreateBuilder(args);

//Building middlewares as DI services
builder.Services.AddTransient<ILandingPageLogic>(static _ => SimpleLandingPageLogic.CreateInstance());
builder.Services.AddTransient<IWrongPageLogic>(static _ => SimpleWrongPageLogic.CreateInstance());
builder.Services.AddTransient<IExitPageLogic>(static _ => SimpleExitPageLogic.CreateInstance());

var app = builder.Build();

//Recommended order of predefined middleware
// app.UseExceptionHandler("/Error");
// app.UseHsts();
// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
// app.UseCors();
// app.UseAuthentication();
// app.UseAuthorization();
// app.UseSession();
// app.MapControllers();

//Basic add of middlewares by calling UseMiddlewares multiple times
// app.UseMiddleware<ILandingPageLogic>();
// app.UseMiddleware<IWrongPageLogic>();
// app.UseMiddleware<IExitPageLogic>();

string[] availablePages = [Pages.LandingPagePath, Pages.ExitPagePath];

//Chaining middleware by extension methods
app.UseWhen(
    context => !Array.Exists(availablePages, pageName => pageName == context.Request.Path),
    static applicationBuilder => applicationBuilder.UseWrongPage());

app.UseLandingPage().UseExitPage();

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

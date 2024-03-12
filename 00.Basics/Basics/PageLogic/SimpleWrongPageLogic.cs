// <copyright file="SimpleWrongPageLogic.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

namespace Basics.PageLogic;

public sealed class SimpleWrongPageLogic : IWrongPageLogic
{
    private SimpleWrongPageLogic() { }

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

        //Awaiting async responses with HTML content
        await context.Response.WriteAsync("<h1>Wrong site!</h1>");
        await context.Response.WriteAsync($"<h2>Site {requestedPath} does not exist.</h2>");
        await context.Response.WriteAsync("<h2>Please try again.</h2>");
        return true;
    }
}

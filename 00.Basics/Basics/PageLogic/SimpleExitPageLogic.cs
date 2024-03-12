// <copyright file="SimpleExitPageLogic.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

using Basics.PageLogic.Utilities;

namespace Basics.PageLogic;

public sealed class SimpleExitPageLogic : IExitPageLogic
{
    private SimpleExitPageLogic() { }

    public static IExitPageLogic CreateInstance() => new SimpleExitPageLogic();

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!await ProcessExitPage(context))
        {
            await next(context);
        }
    }

    public async Task<bool> ProcessExitPage(HttpContext context)
    {
        if (context.Request.Path != Pages.ExitPagePath)
        {
            return false;
        }

        await context.Response.WriteAsync("<h1> Exiting page </h1>");
        await LookForUserID(context);
        await LookForUserBrowser(context);
        return true;
    }

    private static async Task LookForUserBrowser(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("User-Agent", out var userBrowser))
        {
            await context.Response.WriteAsync($"<h2> User using browser: {userBrowser} </h2>");
        }
        else
        {
            await context.Response.WriteAsync("<h2> Browser could not be determined. </h2>");
        }
    }

    private static async Task LookForUserID(HttpContext context)
    {
        if (context.Request.Query.TryGetValue("id", out var idValue))
        {
            await context.Response.WriteAsync($"<h2> Logging out user with id: {idValue} </h2>");
        }
        else
        {
            await context.Response.WriteAsync("<h2> No user found. </h2>");
        }
    }
}

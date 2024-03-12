// <copyright file="IWrongPageLogic.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

namespace TestWebApp.PageLogic;

public interface IWrongPageLogic : IMiddleware
{
    Task<bool> ProcessWrongPage(HttpContext context);
}

public static class WrongPageExtension
{
    public static IApplicationBuilder UseWrongPage(this IApplicationBuilder app) =>
        app.UseMiddleware<IWrongPageLogic>();
}

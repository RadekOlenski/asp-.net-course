// <copyright file="IExitPageLogic.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

namespace TestWebApp.PageLogic;

public interface IExitPageLogic : IMiddleware
{
    Task<bool> ProcessExitPage(HttpContext context);
}

public static class ExitPageExtension
{
    public static IApplicationBuilder UseExitPage(this IApplicationBuilder app) => app.UseMiddleware<IExitPageLogic>();
}

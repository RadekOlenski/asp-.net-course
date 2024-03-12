// <copyright file="IExitPageLogic.cs" company="Something Random S.A.">
// Copyright (c) Something Random S.A. All rights reserved
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

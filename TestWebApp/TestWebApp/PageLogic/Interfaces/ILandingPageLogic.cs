// <copyright file="ILandingPageLogic.cs" company="Something Random S.A.">
// Copyright (c) Something Random S.A. All rights reserved
// </copyright>

namespace TestWebApp.PageLogic;

public interface ILandingPageLogic : IMiddleware
{
    Task<bool> ProcessLandingPage(HttpContext context);
}

public static class LandingPageExtension
{
    public static IApplicationBuilder UseLandingPage(this IApplicationBuilder app) =>
        app.UseMiddleware<ILandingPageLogic>();
}

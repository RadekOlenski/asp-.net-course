// <copyright file="SendDefaultGetResponseMiddleware.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

namespace MiddlewareLoginPage.Middlewares;

public sealed class SendDefaultGetResponseMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.StatusCode = 200;
        await next(context);
    }
}

public static class SendDefaultGetResponseMiddlewareExtension
{
    public static IApplicationBuilder UseSendDefaultGetResponseMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<SendDefaultGetResponseMiddleware>();
}

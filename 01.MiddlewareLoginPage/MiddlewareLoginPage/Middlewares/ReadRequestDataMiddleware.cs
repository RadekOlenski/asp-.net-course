// <copyright file="ReadRequestDataMiddleware.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

using Microsoft.AspNetCore.WebUtilities;
using MiddlewareLoginPage.Data;

namespace MiddlewareLoginPage.Middlewares;

public sealed class ReadRequestDataMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        using var reader = new StreamReader(context.Request.Body);
        var requestBody = await reader.ReadToEndAsync();
        var query = QueryHelpers.ParseQuery(requestBody);

        context.Items[MiddlewareObjects.Email] = query.TryGetValue("email", out var email) ? email[0] : string.Empty;
        context.Items[MiddlewareObjects.Password] =
            query.TryGetValue("password", out var password) ? password[0] : string.Empty;

        await next(context);
    }
}

public static class ReadRequestDataMiddlewareExtension
{
    public static IApplicationBuilder UseReadRequestData(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ReadRequestDataMiddleware>();
}

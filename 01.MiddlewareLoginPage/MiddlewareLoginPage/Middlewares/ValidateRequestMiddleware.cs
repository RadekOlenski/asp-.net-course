// <copyright file="ValidateRequestMiddleware.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

using MiddlewareLoginPage.Data;

namespace MiddlewareLoginPage.Middlewares;

public sealed class ValidateRequestMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var (passwordEmpty, password) = GetDataFromItems(context, MiddlewareObjects.Password);
        var (emailEmpty, email) = GetDataFromItems(context, MiddlewareObjects.Email);

        if (passwordEmpty || emailEmpty)
        {
            await PrepareInvalidDataResponse(context, passwordEmpty, emailEmpty);
            return;
        }

        if (password != UserPasses.Password || email != UserPasses.Email)
        {
            await PrepareLoginResponse(context, 400, Texts.InvalidLoginText);
            return;
        }

        await PrepareLoginResponse(context, 200, Texts.SuccessfulLoginText);
        await next(context);
    }

    private static (bool empty, string? value) GetDataFromItems(HttpContext context, object dataKey)
    {
        var emptyValue = (true, string.Empty);
        if (!context.Items.TryGetValue(dataKey, out var value))
        {
            return emptyValue;
        }

        if (value is null)
        {
            return emptyValue;
        }

        var stringValue = value.ToString();
        return stringValue == string.Empty ? emptyValue : (false, stringValue);
    }

    private static async Task PrepareInvalidDataResponse(HttpContext context, bool passwordEmpty, bool emailEmpty)
    {
        if (passwordEmpty)
        {
            await PrepareLoginResponse(context, 400, Texts.MissingPasswordText);
        }

        if (emailEmpty)
        {
            await PrepareLoginResponse(context, 400, Texts.MissingEmailText);
        }
    }

    private static async Task PrepareLoginResponse(HttpContext context, int statusCode, string infoText)
    {
        context.Response.StatusCode = statusCode;
        await using var stream = new StreamWriter(context.Response.Body);
        await stream.WriteAsync(infoText);
    }
}

public static class ValidateRequestMiddlewareExtension
{
    public static IApplicationBuilder UseValidateRequest(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ValidateRequestMiddleware>();
}

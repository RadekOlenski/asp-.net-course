// <copyright file="Program.cs" company="Radosław Oleński">
// Copyright (c) Radosław Oleński. All rights reserved
// </copyright>

using MiddlewareLoginPage.Data;
using MiddlewareLoginPage.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(
    static context => context.Request.Method == RequestMethod.Get && context.Request.Path == "/",
    static applicationBuilder => applicationBuilder.UseSendDefaultGetResponseMiddleware());

app.UseWhen(
    static context => context.Request.Method == RequestMethod.Post && context.Request.Path == "/",
    static applicationBuilder => applicationBuilder.UseReadRequestData().UseValidateRequest());

app.Run(static context => context.Response.WriteAsync("awaiting input..."));
app.Run();

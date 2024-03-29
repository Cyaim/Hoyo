﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Hoyo.WebCore;
/// <summary>
/// API耗时监控中间件
/// </summary>
public class ResponseTimeMiddleware
{
    private const string RESPONSE_HEADER_RESPONSE_TIME = "Hoyo-Response-Time";
    private readonly RequestDelegate next;
    public ResponseTimeMiddleware(RequestDelegate next) => this.next = next;
    public async Task Invoke(HttpContext context)
    {
        var watch = new Stopwatch();
        watch.Start();
        context.Response.OnStarting(() =>
        {
            watch.Stop();
            context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = $"{watch.ElapsedMilliseconds} ms";
            return Task.CompletedTask;
        });
        await next(context);
    }
}
/// <summary>
/// 全局API耗时监控中间件
/// </summary>
public static class ResponseTimeMiddlewareExtensions
{
    public static IApplicationBuilder UseHoyoResponseTime(this IApplicationBuilder builder) => builder.UseMiddleware<ResponseTimeMiddleware>();
}
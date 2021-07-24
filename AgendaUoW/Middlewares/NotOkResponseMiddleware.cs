using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgendaUoW.Middlewares
{
    public class NotOkResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public NotOkResponseMiddleware(RequestDelegate next, ILogger<NotOkResponseMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context != null && context.Request != null)
                {
                    var log = new Log
                    {
                        Path = context.Request.Path,
                        QueryString = context.Request.QueryString.ToString()
                    };

                    if (context.Request.Body != null)
                    {
                        context.Request.EnableBuffering();
                        var body = await new StreamReader(context.Request.Body)
                                                            .ReadToEndAsync();
                        context.Request.Body.Position = 0;
                        log.Body = body;
                    }

                    var json = System.Text.Json.JsonSerializer.Serialize(log, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });

                    _logger.LogError(json);
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                var message = ex.Message ?? "Erro generico";
                _logger.LogError(message);

                if (!context.Response.HasStarted)
                {
                    await ExceptionHandlingAsync(ex, context);
                }
            }
        }

        private static async Task ExceptionHandlingAsync(Exception ex, HttpContext context)
        {
            var httpEx = ex as HttpResponseException;

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = httpEx?.StatusCode ?? StatusCodes.Status500InternalServerError;

            var problemDetails = new ValidationProblemDetails(httpEx?.Erros ?? HttpResponseException.DEFAULT_ERRO_500)
            {
                Type = "about:blank",
                Instance = context.Request.Path,
                Status = context.Response.StatusCode,
                Title = "Erro ao realizar solicitação.",
                Detail = $"Exceção gerada por {ex.Source ?? "API WEB"}: {ex.StackTrace ?? "---unknow StackTrace---"}",
            };

            var json = JsonConvert.SerializeObject(problemDetails,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            await context.Response.WriteAsync(json);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class NotOkResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseNotOkResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotOkResponseMiddleware>();
        }
    }

    class Log
    {
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Authorization { get; set; }
        public string Body { get; set; }
    }

}
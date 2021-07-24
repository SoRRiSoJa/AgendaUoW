using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaUoW.Middlewares
{
    public class HttpResponseException : Exception
    {
        public readonly Dictionary<string, string[]> Erros;
        public readonly int StatusCode;

        public static readonly Dictionary<string, string[]> DEFAULT_ERRO_500 = new Dictionary<string, string[]>(
                new KeyValuePair<string, string[]>[]
                { new KeyValuePair<string, string[]>("message", new string[] { "Erro desconhecido do servidor." })});

        public ObjectResult RequestObjectResult { get; }

        public HttpResponseException(int statusCode = 500, params (string, string[])[] erros)
        {
            Erros = erros.Length > 0 ?
                new Dictionary<string, string[]>(erros.Select(tupla => new KeyValuePair<string, string[]>(tupla.Item1, tupla.Item2))) :
                DEFAULT_ERRO_500;

            StatusCode = statusCode;
        }

        public HttpResponseException(int statusCode = 500, params string[] erros)
        {
            Erros = erros.Length > 0 ? new Dictionary<string, string[]>(
                new KeyValuePair<string, string[]>[] { new KeyValuePair<string, string[]>("message", erros) }) :
                DEFAULT_ERRO_500;

            StatusCode = statusCode;
        }

        public HttpResponseException(ObjectResult resultObject) :
            this(resultObject.StatusCode ?? 500, resultObject.Value?.ToString() ?? "Erro desconhecido.")
        {
            RequestObjectResult = resultObject;
        }

        public HttpResponseException(StatusCodeResult statusCodeResultObject, string erro = null) :
            this(statusCodeResultObject.StatusCode, erro ?? "Erro desconhecido.")
        { }

        public HttpResponseException(Exception e, ObjectResult resultObject) :
            this(resultObject.StatusCode ?? 500, (resultObject.Value?.ToString() ?? e.Message) ?? "Erro desconhecido.")
        {
            RequestObjectResult = resultObject;
        }
    }
}

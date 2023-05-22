using Microsoft.AspNetCore.Mvc;

namespace infrastracture_api.Models;

public class HttpResponse
{
    public string? Msg { get; set; } = string.Empty;
    public int StatusCode { get; set; } = 400;

    public HttpResponse(string message)
    {
        Msg = message;
    }

    public HttpResponse(string message, int statusCode)
    {
        Msg = message;
        StatusCode = statusCode;
    }
}
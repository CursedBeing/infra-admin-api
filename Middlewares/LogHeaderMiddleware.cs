namespace infrastracture_api.Middlewares;

public class LogHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public LogHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Add("x-conId", context.Connection.Id);
        context.Response.Headers.Add("x-traceId", context.TraceIdentifier);
        await _next.Invoke(context);
    }
}
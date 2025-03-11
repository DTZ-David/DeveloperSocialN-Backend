using Developer.Domain.Common.Wrappers.CustomResponse;
using Microsoft.AspNetCore.Http;


namespace Developer.Domain.Common.Exceptions;

public class BusinessException : Exception
{
    private int StatusCode { get; set; }
    public BusinessException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public virtual Task HandleError(HttpContext context, Response<object> response)
    {
        context.Response.StatusCode = StatusCode;
        response.StatusCode = StatusCode;
        return Task.CompletedTask;
    }
}

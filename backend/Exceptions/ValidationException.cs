namespace WebApi.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;
    public void OnActionExecuting(ActionExecutingContext context) { }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ValidationException)
        {
            context.Result = new JsonResult(new { message = "Provided data was invalid" }) { StatusCode = StatusCodes.Status400BadRequest };
            context.ExceptionHandled = true;
        }
    }
}

public class ValidationException : Exception
{
    public ValidationException() : base()
    {
    }
}

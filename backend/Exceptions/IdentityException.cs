namespace WebApi.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class IdentityExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;
    public void OnActionExecuting(ActionExecutingContext context) { }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is IdentityException)
        {
            context.Result = new JsonResult(new { message = "User could not be identified" }) { StatusCode = StatusCodes.Status400BadRequest };
            context.ExceptionHandled = true;
        }
    }
}

public class IdentityException : Exception
{
    public IdentityException() : base()
    {
    }
}

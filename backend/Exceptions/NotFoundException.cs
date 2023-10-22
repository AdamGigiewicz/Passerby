namespace WebApi.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class NotFoundExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;
    public void OnActionExecuting(ActionExecutingContext context) { }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new JsonResult(new { message = "Entity not found" }) { StatusCode = StatusCodes.Status404NotFound};
            context.ExceptionHandled = true;
        }
    }
}

public class NotFoundException : Exception
{
    public NotFoundException() : base()
    {
    }
}

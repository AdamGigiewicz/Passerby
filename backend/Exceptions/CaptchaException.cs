namespace WebApi.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CaptchaExceptionFilter: IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;
    public void OnActionExecuting(ActionExecutingContext context) { }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is CaptchaException)
        {
            context.Result = new JsonResult(new { message = "Captcha failed" }) { StatusCode = StatusCodes.Status400BadRequest };
            context.ExceptionHandled = true;
        }
    }
}

public class CaptchaException : Exception
{
    public CaptchaException() : base()
    {
    }
}

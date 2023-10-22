namespace WebApi.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Entities;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
{
  public void OnAuthorization(AuthorizationFilterContext context)
  {
      var user = (User)context.HttpContext.Items["User"];
      if ( user == null || !user.isAdmin)
      {
          context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
      }
  }
}

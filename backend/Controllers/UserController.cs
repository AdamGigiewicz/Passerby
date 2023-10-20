namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
using WebApi.Authorization;
using WebApi.Entities;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult SignIn(UserCredentials form)
    {
        return Ok(_userService.SignIn(form.login, form.password));
    }

    [AuthorizeUser]
    [HttpPatch]
    public IActionResult EditPassword(UserChangePassword form)
    {
        _userService.EditPassword((int)HttpContext.Items["UserId"], form.oldPassword, form.newPassword);
        return Ok();
    }
}

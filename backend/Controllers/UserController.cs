namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
using WebApi.Authorization;
using WebApi.Repositories;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("signin")]
    public IActionResult SignIn(UserCredentials form)
    {
        return Ok(_userService.SignIn(form));
    }

    [AuthorizeUser]
    [HttpPatch("password")]
    public IActionResult EditPassword(UserChangePassword form)
    {
        _userService.EditPassword(form);
        return Ok();
    }
}

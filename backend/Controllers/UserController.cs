namespace WebApi.Controllers;
using WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;
using WebApi.Authorization;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost()]
    public IActionResult Login(UserCredentials form)
    {
        var loggedUser = _userService.Login(form);
        IActionResult response;
        if (loggedUser != null)
        {
            response = Ok(loggedUser);
        }
        else
        {
            response = NotFound(new { message = "Username or password is incorrect" });
        }
        return response;
    }

    [AuthorizeUser]
    [HttpPut]
    public IActionResult EditPassword(UserChangePassword form)
    {
        _userService.EditPassword(form);
        return Ok();
    }
}

namespace WebApi.Controllers;
using WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpDelete("remove/{id}")]
    public IActionResult Remove(int id)
    {
        _userService.Delete(id);
        return Ok();
    }

    [HttpPost("create")]
    public IActionResult Create(AuthenticateRequest model)
    {
        return Ok(_userService.Create(model));
    }
    [HttpPost("edit")]
    public IActionResult Edit(User user)
    {
        _userService.Edit(user);
        return Ok();
    }

    [HttpPost("newpassword")]
    public IActionResult EditPassword(int id, string oldPassword, string newPassword)
    {
        _userService.EditPassword(id, oldPassword, newPassword);
        return Ok();
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
    
    [HttpGet("/{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_userService.GetById(id));
    }
}

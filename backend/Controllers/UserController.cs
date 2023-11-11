namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using WebApi.Models;
using WebApi.Services;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Exceptions;

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

    [HttpPatch]
    public IActionResult ResetPassword(UserCredentials form)
    {
        _userService.ResetPassword(form.login, form.password);
        return Ok();
    }

    [AuthorizeUser]
    [HttpPut]
    public IActionResult EditPassword(UserChangePassword form)
    {
      HttpClient httpClient = new HttpClient();

    var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LcG9AspAAAAAB_SHO_k5xoIbM2nfjQrs__ywzXc&response={form.token}").Result;

    string JSONres = res.Content.ReadAsStringAsync().Result;
    Console.WriteLine(JSONres);
    var node = JsonNode.Parse(JSONres); 
    var success = (bool?)node["success"];
    
    if(success == null || success == false)
    {
      throw new CaptchaException(); 
    }
        _userService.EditPassword(GetUser().id, form.oldPassword, form.newPassword);
        return Ok();
    }

    private User GetUser()
    {
        return (User)HttpContext.Items["User"];
    }
}

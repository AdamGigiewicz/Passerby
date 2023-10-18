namespace WebApi.Controllers;

using WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;
using WebApi.Authorization;

[ApiController]
[Route("[controller]")]
[AuthorizeAdmin]
public class AdminController : ControllerBase
{
    private IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost]
    public IActionResult Create(UserCredentials model)
    {
        _adminService.Create(model);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _adminService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_adminService.GetById(id));
    }

    [HttpPut]
    public IActionResult Edit(User user)
    {
        _adminService.Edit(user);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _adminService.Delete(id);
        return Ok();
    }

}

using Microsoft.AspNetCore.Mvc;
using Task.Api.Contracts;
using Task.Service.Interfaces.Services;

namespace Task.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : Controller
{

    private readonly IUserService _userService;
    private IUserService _id;

    public IActionResult Index()
    {
        return View();
    }

    public UserController(IUserService userService) => _userService = userService;

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetUsers()
    {
        var person = await _userService.GetUsers();
        return View(person);
    }
}
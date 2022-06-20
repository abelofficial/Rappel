using API.Data.Entities;
using API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IRepository<User> _repo;

    public UserController(ILogger<UserController> logger, IRepository<User> repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _repo.GetAll());
    }
}

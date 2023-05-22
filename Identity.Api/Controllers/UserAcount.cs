using Identity.Api.Context;
using Identity.Api.Entity;
using Identity.Api.Model;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAcount : ControllerBase
{
    private readonly AppDbContext _context;

    public UserAcount(AppDbContext context)
    {
        _context = context;
    }



    [HttpPost("signup")]
    public async Task< IActionResult> SignUp([FromForm]SignUpUserDto signUpUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if(await _context.Users.AnyAsync(u=>u.UserName == signUpUserDto.UserName))
        {
            return BadRequest();
        }

        var user = signUpUserDto.Adapt<User>();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignIn([FromForm]SignInUserDto signInUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserName.Equals(signInUserDto.UserName));

        if (user == null || user.Password != signInUserDto.Password)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        return Ok(user);
    }
}


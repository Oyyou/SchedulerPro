using Microsoft.AspNetCore.Mvc;
using SchedulerPro.API.Interfaces;
using SchedulerPro.API.Models.Requests;
using SchedulerPro.API.Models.Responses;
using System.Security.Claims;

namespace SchedulerPro.API.Controllers
{
  [ApiController]
  [Route("api/auth")]
  public class AuthController : ControllerBase
  {
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly ITokenValidationService _tokenValidationService;

    public AuthController(
      IUserService userService,
      IJwtService jwtService,
      ITokenValidationService tokenValidationService)
    {
      _userService = userService;
      _jwtService = jwtService;
      _tokenValidationService = tokenValidationService;
    }

    [HttpGet("verify")]
    public IActionResult Verify()
    {
      var tokenString = Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

      if (string.IsNullOrEmpty(tokenString))
      {
        return Unauthorized(new { error = "Token not provided." });
      }

      try
      {
        var principal = _tokenValidationService.ValidateToken(tokenString);

        var userIdClaim = principal.FindFirst(ClaimTypes.Name);
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
          var user = _userService.GetUserById(userId);
          if (user != null)
          {
            return Ok(new { user = new UserResponse(user) });
          }
        }

        return Unauthorized(new { error = "Invalid token." });
      }
      catch (Exception ex)
      {
        return Unauthorized(new { error = ex.ToString() });
      }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
      var user = _userService.Authenticate(request);

      if (user == null)
      {
        return Unauthorized(new { error = "Invalid username or password." });
      }

      var token = _jwtService.GenerateToken(user);

      Response.Cookies.Append("jwt", token, new CookieOptions
      {
        HttpOnly = true
      });

      return Ok(new { token });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
      if (request.Password.Length < 4)
      {
        return BadRequest(new { error = "Password too short" });
      }

      try
      {
        var user = _userService.Register(request);

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return BadRequest(new { error = "Failed to register user" });
      }
    }
  }
}

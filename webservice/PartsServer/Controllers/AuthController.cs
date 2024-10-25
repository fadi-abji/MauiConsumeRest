using Microsoft.AspNetCore.Mvc;
using PartsServer.Helpers;
using System.Text.Json.Serialization;

namespace PartsServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Validate the user credentials (this is just an example)
            if (loginRequest.Username == "testuser" && loginRequest.Password == "password123")
            {
                // Generate the JWT token
                var token = _jwtTokenGenerator.GenerateToken("1", loginRequest.Username);
                return Ok(new { Token = token });
            }

            // If authentication fails
            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        [JsonPropertyName("user_name")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

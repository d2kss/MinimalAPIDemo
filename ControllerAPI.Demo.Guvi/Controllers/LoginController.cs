using ControllerAPI.Demo.Guvi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ControllerAPI.Demo.Guvi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (user.username == "admin" && user.password == "Password123")
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.username),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
               };


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is a sample secret key - please dont use in production environment"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires:DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest("Could not verify username and password");
        }
    }
}

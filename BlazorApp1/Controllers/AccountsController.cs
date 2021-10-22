using Microsoft.AspNetCore.Mvc;
using BlazorApp1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // POST api/<AccountsController>
        [HttpPost("Login")]
        public IActionResult Post([FromBody] LoginRequest request)
        {
            if (request.SecurityKey != "codegator" ||
                request.AccountKey != "martin")
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("ThisismySecretKey"));
            
            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256
                );

            var securityToken = new JwtSecurityToken(
                issuer: "MyIssuer",
                audience: "MyAudience",
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(securityToken);

            return Ok(new { authToken = jwt });
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieCruiserWebAPI.Models;

namespace MovieCruiserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly MovieDBContext context;
        public AuthController(IConfiguration con, MovieDBContext cont)
        {
            this.config = con;
            this.context = cont;
        }
        [HttpPost("{userName}")]
        public IActionResult Login([FromRoute] string userName, [FromBody] string password)
        {

            var user = (from u in context.UserDetails
                        where u.UserName.Equals(userName)
                        select u).FirstOrDefault();
            if (user == null || !user.Password.Equals(password))
                return BadRequest("Invalid username or password");

            string token = GenerateToken(userName, user.IsAdmin);

            Login userInfo = new Login()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                Token = token
            };
            return Ok(userInfo);
        }
        public string GenerateToken(string userName, bool isAdmin)
        {
            var keys = Encoding.UTF8.GetBytes(config["jwt:key"]);
            var key = new SymmetricSecurityKey(keys);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            if (isAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            else
                claims.Add(new Claim(ClaimTypes.Role, "customer"));
            var jwt = new JwtSecurityToken(
                    issuer: config["jwt:validIssuer"],
                    audience: config["jwt:validAudience"],
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddMinutes(30)
                    );
            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwt);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(UserDetails item)
        {
            context.UserDetails.Add(item);
            var rows = await context.SaveChangesAsync();
            if (rows == 0)
                return BadRequest("Cannot add User");
            return StatusCode(201);
        }
    }
}

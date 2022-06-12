using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyTodoWebApi.Data;
using MyTodoWebApi.Data.ApiModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyTodoAppWebApi.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public IConfiguration _configuration;
        private readonly IUnitOfWork UnitOfWork;

        public UserController(IConfiguration configuration, IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Post(users users)
        {
            if (users != null && users.username != null && users.Passwords != null)
            {
                var userdata = await UnitOfWork.user.GetUser(users.username, users.Passwords);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                if (userdata != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", users.id.ToString()),
                        new Claim("UserName", users.username),
                        new Claim("Password", users.Passwords),
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("somethingyouwantwhichissecurewillworkk"));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }

            }
            else
            {
                return BadRequest("invalid credentials");
            }
        }
    }
}

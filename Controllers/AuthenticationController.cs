using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.Configurations;
using OnlineShopping.Models;
using OnlineShopping.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestdto)
        {
            if (ModelState.IsValid)
            {
                var user_exist = await _userManager.FindByEmailAsync(requestdto.Email);
                if (user_exist != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Error = "Email already Exist"
                    }
                    );
                }
                // Create a user 
                var new_user = new IdentityUser()
                {
                    Email = requestdto.Email,
                    UserName = requestdto.Email
                };

                var IsCreated = await _userManager.CreateAsync(new_user, requestdto.Password);

                if (IsCreated.Succeeded)
                {
                    // Generate Token

                    var token = GenerateJwtToken(new_user);
                    return Ok(new AuthResult() { Result = true, Token = token });

                }

                return Ok(new AuthResult() { Result = false, Error = "Not Created" });

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginreques)
        {
            if (ModelState.IsValid)
            {
                // Check if the use exist or not 
                var user = await _userManager.FindByEmailAsync(loginreques.Email);
                if (user != null)
                {
                    var correcpassword = await _userManager.CheckPasswordAsync(user, loginreques.Password);
                    if(!correcpassword)
                    {
                        return BadRequest(new AuthResult() {Result = false , Error = "The Password is incorrect" });
                    }
                    var jwt_token = GenerateJwtToken(user);
                    return Ok(new AuthResult() { Token = jwt_token ,Result = true });
                }
                return BadRequest();

            }
            return BadRequest(new AuthResult() { Result = false , Error = "Invaild Data"});
        }




        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256)
                
            };

            var token = jwtTokenHandler.CreateToken(TokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}

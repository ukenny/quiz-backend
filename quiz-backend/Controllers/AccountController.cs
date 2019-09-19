using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using quiz_backend.Models;

namespace quiz_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Credentials credentials)
        {
            var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };

            var result = await userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await signInManager.SignInAsync(user, isPersistent: false);


            var jwtToken = GenerateJwtToken(user);
            return Ok(jwtToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            var signinResult = await signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);

            if (!signinResult.Succeeded)
            {
                return BadRequest();
            }

            var user = await userManager.FindByEmailAsync(credentials.Email);

            var jwtToken = GenerateJwtToken(user);

            return Ok(jwtToken);
        }

        public string GenerateJwtToken(IdentityUser identityUser)
        {
            var claim = new Claim[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id)
           };

            // NOTE: Provided for educational purposes only. Do not use in Production!
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A VERY UNSECURE SECRET PHRASE"));

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(signingCredentials: signingCredentials, claims: claim);
            return JsonConvert.SerializeObject(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
        }
    }
}
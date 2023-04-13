using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = requestDto.Username,
                Email = requestDto.Username
            };
            var IdentityResult = await userManager.CreateAsync(IdentityUser, requestDto.Password);
            if(IdentityResult.Succeeded)
            {
                // Add roles to this User
                if (requestDto.Roles.Any() && requestDto.Roles != null)
                {
                    IdentityResult = await userManager.AddToRolesAsync(IdentityUser, requestDto.Roles);
                    if (IdentityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }

            return BadRequest("Something went wrong, please try again");
        }
    }
}

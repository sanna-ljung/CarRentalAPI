using CarRentalAPI.Constants;
using CarRentalAPI.Data;
using CarRentalAPI.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<APIUser> userManager;

        public AuthController(UserManager<APIUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userdDto)
        {
            try
            {
                APIUser user = new APIUser()
                {
                    UserName = userdDto.Email,
                    Email = userdDto.Email,
                    FirstName = userdDto.FirstName,
                    LastName = userdDto.LastName
                };
                var result = await userManager.CreateAsync(user, userdDto.Password);
                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await userManager.AddToRoleAsync(user, APIRoles.User);
                return Accepted();
            }
            catch (Exception ex) 
            {
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userDto.Email);
                var passwordIsValid = await userManager.CheckPasswordAsync(user, userDto.Password);
                if (user == null || passwordIsValid == false)
                {
                    return NotFound();
                }
                // add whatever is needed to create JWT
                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}

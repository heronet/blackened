using System.Threading.Tasks;
using Data.DTO;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    public class AccountController : DefaultController
    {
        private readonly UserManager<EntityUser> _userManager;
        private readonly SignInManager<EntityUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;
        public AccountController(UserManager<EntityUser> userManager, SignInManager<EntityUser> signInManager, JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ValidationResponseDTO>> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            EntityUser user = new EntityUser
            {
                UserName = registerUserDTO.Username,
                Email = registerUserDTO.Email,
                PhoneNumber = registerUserDTO.Phone
            };
            IdentityResult result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
                return ReturnValidation(user);
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ValidationResponseDTO>> SignInUser(LoginUserDTO loginUserDTO)
        {
            EntityUser user = await _userManager.FindByNameAsync(loginUserDTO.Username);

            if (user == null) return BadRequest("Invalid Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDTO.Password, lockoutOnFailure: false);

            if (result.Succeeded)
                return ReturnValidation(user);
            return BadRequest("Invalid Password");
        }

        private ValidationResponseDTO ReturnValidation(EntityUser user)
        {
            return new ValidationResponseDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _jwtTokenService.CreateToken(user)
            };
        }
    }
}
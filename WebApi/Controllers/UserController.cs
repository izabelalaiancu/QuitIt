using AutoMapper;
using DataLayer.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebApi.DTOs;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto payload)
        {
            if (payload.Email == null || payload.FirstName == null || payload.LastName == null ||
                payload.Password == null)
                return BadRequest("Bad data, bro.");

            if (await _userService.EmailExistsAsync(payload.Email))
                return BadRequest("Nu mai furati emailuri pls.");

            if (!payload.Email.CheckValidEmail())
                return BadRequest("BadEmail, dude.");

            if (!payload.Password.CheckValidPassword())
                return BadRequest("Nu ne place despre parola ta. (Trebuie sa contina Upper, Number, SpecialCharacter)");


            var user = new User();
            user = _mapper.Map<UserDto, User>(payload);

            var result = await _userService.RegisterAccountAsync(user, payload.Password);
            if (result.Succeeded)
                return Ok("Yay. Now you're somebody.");
            return BadRequest("Something somewhere exploded. Sorry, try again later. Pwp.");
        }

        [HttpPut("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginDto payload)
        {
            if (payload.Password == null || payload.Password == null)
                return BadRequest("Bad data, bro.");

            var result = await _userService.LoginAsync(payload.Email, payload.Password);
            if (result.email != null)
                return Ok(new TokenResponse
                {
                    AccessToken = result.AccessToken,
                    Email = result.email,
                });
            else
                return BadRequest("Impostors shall burn in hell.");
        }


        [HttpGet("Token/Valid")]
        // [Authorize(Roles = Role.User)]
        public ActionResult CheckIfStillValid()
        {
            return Ok("Yep, still valid.");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/vices")]
    [Authorize]
    public class ViceController : ControllerBase
    {
        private readonly IUserViceService _userViceService;
        private readonly IViceService _viceService;


        public ViceController(IUserViceService userVice, IViceService viceService)
        {
            _userViceService = userVice;
            _viceService = viceService;
        }

        [HttpGet("mine")]
        public async Task<ActionResult<List<ViceDto>>> ReturnVicesAsyncForUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            return await _userViceService.ReturnAsync(userId);
        }

        [HttpGet]
        public async Task<ActionResult<List<ViceDto>>> ReturnAllVicesAsync()
        {
            return await _viceService.GetAllAsync();
        }

        [HttpPut("updateVices")]
        public async Task<ActionResult<string>> UpdateVicesAsync([FromBody]List<AddViceDto> vicesDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            return await _userViceService.UpdateVicesAsync(userId,vicesDto);
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteViceForUserAsync(ViceDto viceDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            return await _viceService.DeleteForUserAsync(userId, viceDto);
        }

        [HttpPut("throw-in-the-thrash/{viceId}")]
        public async Task<ActionResult<string>> ThrowViceInTheThrash([FromRoute] string viceId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            return await _viceService.ThrowViceInTheThrash(viceId, userId);
        }

        [HttpGet("score")]
        public async Task<IActionResult> GetMyScore()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            var scores = await _viceService.GetMyScoreAsync(userId);

            var response = new ScoresDto
            {
                MoneySaved = scores.Item1,
                Score = scores.Item2
            };
            return Ok(response);
        }

        [HttpGet("top-users")]
        public async Task<IActionResult> GetTopUsersAsync()
        {
            var users = await _viceService.GetTopUsersAsync();
            return Ok(users);
        }
    }
}

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
        public async Task<ActionResult<string>> UpdateVicesAsync(List<ViceDto> vicesDto)
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
    }
}

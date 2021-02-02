using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApi.DTOs;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("api/wishlist")]
    [Authorize]
    public class WishlistController : Controller
    {
        private IAchievementService _achievementService { get; set; }

        public WishlistController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet("my-wishlist")]
        public async Task<IActionResult> GetMyWishlist()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            var wishlist = await _achievementService.GetMyAchievementsAsync(userId);
            return Ok(wishlist);
        }

        [HttpPost("add-wish")]
        public async Task<IActionResult> AddWish([FromBody] WishDto payload)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
            await _achievementService.AddWishAsync(userId, payload);
            return Ok("Wish has been added!");
        }
    }
}

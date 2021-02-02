using DataLayer;
using Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models.Entities;
using WebApi.DTOs;

namespace Services
{
    public interface IAchievementService : IBaseService
    {
        Task<List<AchievementDto>> GetMyAchievementsAsync(string userId);
        Task AddWishAsync(string userId, WishDto payload);
    }
    public class AchievementService : BaseService, IAchievementService
    {
        public AchievementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddWishAsync(string userId, WishDto payload)
        {
            var achievement = new Achievement
            {
                Name = payload.Name,
                Price = payload.Price,
                UserId = userId,
                Reached = false
            };
            UnitOfWork.Achievements.Add(achievement);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<AchievementDto>> GetMyAchievementsAsync(string userId)
        {
            var dtos = new List<AchievementDto>();
            var achievements =  await UnitOfWork.Achievements.GetUserAchievementsAsync(userId);
            achievements.ForEach(a =>
            {
                var achDto = new AchievementDto
                {
                    Name = a.Name,
                    Price = a.Price,
                    Reached = a.Reached
                };
                dtos.Add(achDto);
            });
            return dtos;
        }
    }
}

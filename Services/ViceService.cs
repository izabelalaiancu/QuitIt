using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.Models.Entities;
using Services.Dtos;

namespace Services
{
    public interface IViceService : IBaseService
    {
       Task<List<ViceDto>> GetAllAsync();
       Task<string> DeleteForUserAsync(string userId, ViceDto dto);
       Task<string> ThrowViceInTheThrash(string userId, string viceId);
       Task<Tuple<double, double>> GetMyScoreAsync(string userId);
       Task<List<UserWithScoreDto>> GetTopUsersAsync();
    }

    public class ViceService: BaseService, IViceService
    {
        private readonly IMapper _mapper;

        public ViceService(IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<List<ViceDto>> GetAllAsync()
        {
            var vices = await UnitOfWork.Vices.GetAsync();
            return _mapper.Map<List<ViceDto>>(vices);
        }

        public async Task<string> ThrowViceInTheThrash(string viceId, string userId)
        {
            var userVice = await UnitOfWork.UserVices.GetVicesByUserIdAndViceIdAsync(userId, viceId);
            if (userVice == null)
                return "UserVice not found";
            userVice.Score += 1;

            // Id = "1",Name = "Bautura"
            // Id = "2",Name = "Mancare"
            // Id = "3",Name = "Tigari"

            if (userVice.ViceId == "1")
                userVice.Money += 3.5;
            if (userVice.ViceId == "2")
                userVice.Money += 5.75;
            if (userVice.ViceId == "3")
                userVice.Money += 1;

            UnitOfWork.UserVices.Update(userVice);
            await UnitOfWork.SaveChangesAsync();

            var money = await GetUserMoneyAsync(userId);
            await ReachAchivementsAsync(money, userId);

            return "Bine Boss!!";
        }

        private async Task ReachAchivementsAsync(double money, string userId)
        {
            var wishes = await UnitOfWork.Achievements.GetUserAchievementsAsync(userId);
            var wishesToBeReached = wishes.Where(x => !x.Reached).ToList();
            wishesToBeReached.ForEach(w =>
            {
                if (w.Price <= money)
                {
                    w.Reached = true;
                    UnitOfWork.Achievements.Update(w);
                    var notif = new Notification
                    {
                        Title = "Congratz!",
                        Text = " You reached your goal for " + w.Name.ToString() + ", with price " + w.Price + ". Hurray for you!",
                        UserId = userId,
                    };
                    UnitOfWork.Notifications.Add(notif);
                }
                
            });
            await UnitOfWork.SaveChangesAsync();

        }

        private async Task<double> GetUserMoneyAsync(string userId)
        {
            var userVices = await UnitOfWork.UserVices.GetVicesByUserIdAsync(userId);
            var money = userVices.Sum(uv => uv.Money);
            return money;
        }


        public async Task<string> DeleteForUserAsync(string userId, ViceDto dto)
        {
            var userVice = await UnitOfWork.UserVices.GetVicesByUserIdAndViceIdAsync(userId, dto.ViceId);
            if (userVice != null)
            {
                UnitOfWork.UserVices.Delete(userVice);
                await UnitOfWork.SaveChangesAsync();
            }
            return "Vice deleted!";
        }

        public async Task<Tuple<double, double>> GetMyScoreAsync(string userId)
        {
            var money = 0.0;
            var score = 0.0;

            var userVices = await UnitOfWork.UserVices.GetVicesByUserIdAsync(userId);
            userVices.ForEach(x =>
            {
                money += x.Money;
                score += x.Score;
            });

            return new Tuple<double, double>(money, score);
        }

        public async Task<List<UserWithScoreDto>> GetTopUsersAsync()
        {
            var users = await UnitOfWork.Users.GetAllAsync();
            var filteredUsers = users.Where(u =>
                    u.UserVices != null && u.UserVices.Count != 0)
                .ToList();
            var dtos = filteredUsers
                .Select(u => new UserWithScoreDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Score = u.UserVices.Sum(uv => uv.Score),
                    UserId = u.Id
                });

            // var userVices = await UnitOfWork.UserVices.GetWithUsersAndVicesAsync();
            // var users = userVices
            //     .Select(x => new UserWithScoreDto {
            //         FirstName = x.User.FirstName,
            //         LastName = x.User.LastName,
            //         UserId = x.UserId,
            //         Score = 0.0
            //         })
            //     .Distinct()
            //     .ToList();
            // var usersWithScore = new List<UserWithScoreDto>();
            // users.ForEach(u =>
            // {
            //     var user = u;
            //     user.Score = userVices
            //         .Where(x => x.User.Id == u.UserId)
            //         .Sum(x => x.Score);
            //      // = totalScore;
            // });

            var topUsers = dtos.Distinct().OrderByDescending(x => x.Score)
                .Take(3).ToList();

            return topUsers;
        }
    }
}

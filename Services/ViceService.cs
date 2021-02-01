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
       Task<List<Tuple<UserDto, double>>> GetTopUsersAsync();
    }

    public class ViceService: BaseService, IViceService
    {
        private readonly IMapper _mapper;

        public ViceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
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
            UnitOfWork.UserVices.Update(userVice);
            await UnitOfWork.SaveChangesAsync();
            return "Bine Boss!!";
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

        public async Task<List<Tuple<UserDto, double>>> GetTopUsersAsync()
        {
            var userVices = await UnitOfWork.UserVices.GetWithUsersAndVicesAsync();
            var users = userVices
                .Select(x => new UserDto {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    UserId = x.UserId,
                    Score = 0.0
                    }).Distinct()
                .ToList();
            var usersWithScore = new List<Tuple<UserDto, double>>();
            users.ForEach(u =>
            {
                var user = u;
                var totalScore = userVices
                    .Where(x => x.User.Id == u.UserId)
                    .Sum(x => x.Score);
                usersWithScore.Add(new Tuple<UserDto, double>(user, totalScore));
            });

            var topUsers = usersWithScore.OrderBy(x => x.Item2)
                .Take(3).ToList();

            return topUsers;
        }
    }
}

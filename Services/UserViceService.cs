using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.Migrations;
using DataLayer.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Dtos;

namespace Services
{
    public interface IUserViceService : IBaseService
    {
        Task<List<ViceDto>> ReturnAsync(string userId);
        Task<string> UpdateVicesAsync(string userId, List<ViceDto> vicesDto);
    }

    public class UserViceService : BaseService, IUserViceService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserViceService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<ViceDto>> ReturnAsync(string userId)
        {
            var vices = await UnitOfWork.UserVices.GetVicesByUserIdAsync(userId);
            var v = _mapper.Map<List<Vice>>(vices);
            return _mapper.Map<List<ViceDto>>(v);
        }

        public async Task<string> UpdateVicesAsync(string userId, List<ViceDto> vicesDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;
            var vices = _mapper.Map<List<Vice>>(vicesDto);

            var oldVices = await UnitOfWork.UserVices.GetVicesByUserIdAsync(userId);
            var oldVicesIds = oldVices.Select(v => v.ViceId).ToList();

            foreach (var vice in vices)
            {
                if(!oldVicesIds.Contains(vice.Id))
                    UnitOfWork.UserVices.Add(new UserVice
                    {
                        Score = 0,
                        UserId = userId,
                        ViceId = vice.Id,
                       
                    });
            }

            var deletedVices = oldVicesIds.Where(v => vices.All(vn => v != vn.Id)).ToList();
            if (deletedVices.Any())
            {
                foreach (var deletedVice in deletedVices)
                {
                    var vice = oldVices.Where(x => x.ViceId == deletedVice && x.UserId == userId).FirstOrDefault();
                    if(vice != null)
                        UnitOfWork.UserVices.Delete(vice);
                }
            }

            await UnitOfWork.SaveChangesAsync();
            return "User Vices Updated!";
        }
    }
}

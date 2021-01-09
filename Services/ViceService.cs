using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using Services.Dtos;

namespace Services
{
    public interface IViceService : IBaseService
    {
       Task<List<ViceDto>> GetAllAsync();
       Task<string> DeleteForUserAsync(string userId, ViceDto dto);
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IViceRepository : IBaseRepository<Vice>
    {
        Task<Vice> GetByName(string name);
    }

    public class ViceRepository : BaseRepository<Vice>, IViceRepository
    {
        public ViceRepository(Context db):base(db)
        {
        }

        public async Task<Vice> GetByName(string name)
        {
            return await _db.Vices.FirstOrDefaultAsync(v => v.Name == name);
        }
    }
}

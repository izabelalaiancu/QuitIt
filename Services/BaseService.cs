using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IBaseService
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
    public class BaseService : IBaseService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}

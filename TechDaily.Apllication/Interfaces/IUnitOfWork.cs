using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Domain.Entities;

namespace TechDaily.Apllication.Interfaces
{
    public interface IUnitOfWork
    {
        Dictionary<Type, IGenericRepository<BaseEntity>> Repositories { get; set; }
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task CommitAsync();
    }
}

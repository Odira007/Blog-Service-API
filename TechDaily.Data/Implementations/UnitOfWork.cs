using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Apllication.Interfaces;
using TechDaily.Domain.Entities;

namespace TechDaily.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TechDailyDbContext _dbContext;
        public Dictionary<Type, IGenericRepository<BaseEntity>> Repositories { get; set; }

        public UnitOfWork(TechDailyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            //if(Repositories.ContainsKey(typeof(T))) return Repositories[typeof(T)] as IGenericRepository<T>;

            //IGenericRepository<T> repo = new GenericRepository<T>(_dbContext);
            //Repositories.Add(typeof(T), repo);

            return new GenericRepository<T>(_dbContext);
        }
    }
}

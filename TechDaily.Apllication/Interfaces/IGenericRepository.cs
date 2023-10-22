using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechDaily.Domain.Entities;

namespace TechDaily.Apllication.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // CREATE
        Task AddAsync(T entity);

        // READ
        Task<T> GetAsync(Expression<Func<T, bool>> filter, List<string> includes = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, List<string> includes = null);

        // UPDATE
        void Update(T entity);

        // DELETE
        void Delete(T entity);
    }
}

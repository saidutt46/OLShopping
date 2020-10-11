using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCommerce.Data.Shared;
using DCommerce.Repository.Helpers;

namespace DCommerce.Repository.Shared
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(Guid id);
        Task<IList<T>> ListAll();
        Task<T> GetSingleBySpec(ISpecification<T> spec);
        Task<IList<T>> List(ISpecification<T> spec);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}

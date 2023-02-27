using BusinessAccess.Filter;
using DataAccess.Model;
using System.Linq.Expressions;

namespace BusinessAccess.Repository.Interface
{
    // save change to make sure you want update to sql server
    public interface IRepository<T> where T : Document
    {
        Task<List<T>> FindAll(Expression<Func<T, bool>> filter);
        Task<T> FindOne(Expression<Func<T, bool>> filter);
        Task<List<T>> FindWithFilter(PaginationFilter filter);
        Task<int> CountAll();
        Task Insert(T entity);
        Task InsertMany(T[] entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
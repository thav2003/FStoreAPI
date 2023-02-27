using BusinessAccess.Filter;
using BusinessAccess.Repository.Interface;
using Common;
using DataAccess.DBContext;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BusinessAccess.Repository.Implement
{
    public class Repository<T> : IRepository<T> where T : Document
    {
        protected readonly FStoreDBContext context;
        protected DbSet<T> table;

        public Repository(FStoreDBContext context)
        {
            this.context = context;

            table = context.Set<T>();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await table.Where(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<T>> FindWithFilter(PaginationFilter filter)
        {
            try
            {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                return await table
                        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                        .Take(validFilter.PageSize)
                        .ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<List<T>> FindAll(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await table.Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }

        }

        public async Task Insert(T entity)
        {
            try
            {
                if (table == null)
                {
                    throw new ArgumentNullException("table null");
                }
                entity.createdAt = DateTime.Now;
                entity.updatedAt = DateTime.Now;
                await table.AddAsync(entity);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }

        }

        public async Task Update(T entity)
        {
            try
            {
                if (table == null)
                {
                    throw new ArgumentNullException("table null");
                }
                entity.updatedAt = DateTime.Now;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }

        }
        public async Task Delete(T entity)
        {
            try
            {
                if (table == null)
                {
                    throw new ArgumentNullException("table null");
                }
                table.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> CountAll()
        {
            try
            {
                return await table.CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertMany(T[] entity)
        {
            try
            {
                if (table == null)
                {
                    throw new ArgumentNullException("table null");
                }
                var createdAt = DateTime.Now;
                var updatedAt = DateTime.Now;
                for (int i = 0; i < entity.Length; i++) {
                    entity[i].createdAt = createdAt;
                    entity[i].updatedAt = updatedAt;
                }
        
                await table.AddRangeAsync(entity);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
    }
}

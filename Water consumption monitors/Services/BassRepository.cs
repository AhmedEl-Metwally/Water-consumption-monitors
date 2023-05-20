using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Water_consumption_monitors.Date;
using Water_consumption_monitors.Interface;
using Water_consumption_monitors.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Water_consumption_monitors.Services
{
    public class BassRepository<T> : IBass<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);   
            return entities;    
        }

        public async Task<int> CountAsync(T spec)
        {
            return await _context.Set<T>().CountAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include); 

            return await query.SingleOrDefaultAsync(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria,
            int? take, int? skip,
            string[] includes = null,
            Expression<Func<T, object>> orderby = null, string orderByDirection = OrderBy.Ascending
            )
        {
            IQueryable<T> query = _context.Set<T>() .Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Take(skip.Value);

            if (orderby !=null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderby);
                else
                    query = query.OrderByDescending(orderby);
            }

            if(includes !=null)
                foreach (var include in includes)
                    query  = query.Include(include);

            return query.Where(criteria).ToList();    
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        //public Task<T> GetEntityWithSpes(T spec)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

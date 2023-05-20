using System.Linq.Expressions;
using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Interface
{
    public interface IBass<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        //Task<T> GetEntityWithSpes(T spec);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<int> CountAsync(T spec);

        //void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T> Find (Expression<Func<T, bool>> criteria , string[] includes = null);

        IEnumerable<T> FindAll (Expression<Func<T, bool>> criteria,
            int? take, int? skip,
            string[] includes = null,
            Expression<Func<T,object>> orderby = null , string orderByDirection = OrderBy.Ascending
            );

        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);

    }
}

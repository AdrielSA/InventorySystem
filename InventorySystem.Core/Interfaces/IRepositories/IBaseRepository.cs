using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InventorySystem.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
                Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                string IncludProperties = null
            );

        T GetFirst(
                Expression<Func<T, bool>> filter = null,
                string IncludProperties = null
            );

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}

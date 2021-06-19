using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Unis.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IQueryable<T> List(Expression<Func<T, bool>> expression);

    }
}

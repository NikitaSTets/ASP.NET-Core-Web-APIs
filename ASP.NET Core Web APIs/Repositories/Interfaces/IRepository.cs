using System;
using System.Linq.Expressions;

namespace ASP.NET_Core_Web_APIs.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);

        void Update(T entity);

        T GetFirst(Expression<Func<T, bool>> predicate);
    }
}
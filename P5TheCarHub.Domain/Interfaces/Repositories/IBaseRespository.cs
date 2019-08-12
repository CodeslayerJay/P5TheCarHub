using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace P5TheCarHub.Core.Interfaces.Repositories
{
    public interface IBaseRespository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Delete(int id);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll(int? amount = null);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}

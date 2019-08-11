using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Interfaces.Repositories
{
    public interface IBaseRespository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Delete(int id);
        TEntity GetById(int id);
        ICollection<TEntity> GetAll();
        void Update();
        void SaveChanges();
    }
}

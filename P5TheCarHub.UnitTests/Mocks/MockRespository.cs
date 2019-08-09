using P5TheCarHub.Core.Interfaces;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.UnitTests.Mocks
{
    public class MockRespository<TEntity> where TEntity : class, IBaseRespository<TEntity>, IEntity
    {
        private List<TEntity> _dataList;

        public MockRespository(IEnumerable<TEntity> entities)
        {
            _dataList = new List<TEntity>();
            SetupFakeData(entities);
        }

        private void SetupFakeData(IEnumerable<TEntity> entities)
        {
            if (entities.Any())
                _dataList.AddRange(entities);
        }

        private int SetId()
        {
            return _dataList.Count() + 1;
        }

        public TEntity Add(TEntity entity)
        {
            if (entity.Id == 0)
                entity.Id = SetId();

            _dataList.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var entity = _dataList.SingleOrDefault(x => x.Id == id);
            if (entity != null)
                _dataList.Remove(entity);
        }

        public TEntity GetById(int id)
        {
            return _dataList.SingleOrDefault(x => x.Id == id);
        }

        public ICollection<TEntity> GetAll()
        {
            return _dataList.ToList();
        }
    }
}

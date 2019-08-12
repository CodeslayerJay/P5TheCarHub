using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace P5TheCarHub.UnitTests.Mocks
{
    public class PhotoRepositoryMock : IPhotoRepository
    {
        private List<Photo> _context;

        public PhotoRepositoryMock()
        {
            _context = new List<Photo>();
            SetupFakeData();
        }

        private void SetupFakeData()
        {
            var id = 0;
            var seedData = new List<Photo>()
            {
                new Photo { Id = ++id, IsMain = true, VehicleId = 1 },
                new Photo { Id = ++id, IsMain = false, VehicleId = 1 },
                new Photo { Id = ++id, IsMain = false, VehicleId = 2 }
            };

            _context.AddRange(seedData);
        }

        private int SetId()
        {
            return _context.Count() + 1;
        }

        public Photo Add(Photo entity)
        {
            if (entity.Id == 0)
                entity.Id = SetId();

            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var photo = GetById(id);
            _context.Remove(photo);
        }

        public IEnumerable<Photo> GetAll(int? amount)
        {
            if (amount != null)
                return _context.Take(amount.Value).ToList();

            return _context.Where(x => x.Id > 0).ToList();
        }

        public IEnumerable<Photo> GetAllByVehicleId(int vehicleId)
        {
            return _context.Where(x => x.VehicleId == vehicleId).ToList();
        }

        public Photo GetVehicleMainPhoto(int vehicleId)
        {
            return _context.Where(x => x.VehicleId == vehicleId)
                .SingleOrDefault(x => x.IsMain == true);
        }

        public Photo GetById(int id)
        {
            return _context.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Photo> Find(Expression<Func<Photo, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}

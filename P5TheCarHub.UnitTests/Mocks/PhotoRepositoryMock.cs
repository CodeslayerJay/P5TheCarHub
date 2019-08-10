using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new Photo { Id = ++id, IsMain = true, VehicleId = 1 }
            };

            _context.AddRange(seedData);
        }

        private int SetId()
        {
            return _context.Count() + 1;
        }

        public Photo Add(Photo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Photo> GetAll()
        {
            throw new NotImplementedException();
        }

        public Photo GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

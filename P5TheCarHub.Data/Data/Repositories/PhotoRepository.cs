using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace P5TheCarHub.Infrastructure.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _context;

        public PhotoRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Photo Add(Photo entity)
        {
            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var photo = GetById(id);
            _context.Remove(photo);
        }

        public IEnumerable<Photo> GetAll()
        {
            return _context.Photos.Where(x => x.Id > 0).ToList();
        }

        public IEnumerable<Photo> GetAllByVehicleId(int vehicleId)
        {
            return _context.Photos.Where(x => x.VehicleId == vehicleId).ToList();
        }

        public Photo GetVehicleMainPhoto(int vehicleId)
        {
            return _context.Photos.Where(x => x.VehicleId == vehicleId)
                .SingleOrDefault(x => x.IsMain == true);
        }

        public Photo GetById(int id)
        {
            return _context.Photos.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Photo> Find(Expression<Func<Photo, bool>> predicate)
        {
            return _context.Photos.Where(predicate);
        }
    }
}

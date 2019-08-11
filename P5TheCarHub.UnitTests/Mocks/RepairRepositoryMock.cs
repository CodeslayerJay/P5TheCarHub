using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P5TheCarHub.UnitTests.Mocks
{
    public class RepairRepositoryMock : IRepairRepository
    {
        private List<Repair> _context;

        public RepairRepositoryMock()
        {
            _context = new List<Repair>();
            SetupFakeData();
        }

        private void SetupFakeData()
        {
            var id = 0;
            var seedData = new List<Repair>()
            {
                new Repair { Id = ++id, VehicleId = 1, Cost = 35, Description = "Oil Change", RepairDate = DateTime.Now },
                new Repair { Id = ++id, VehicleId = 1, Cost = 35, Description = "Oil Change", RepairDate = DateTime.Now },
                new Repair { Id = ++id, VehicleId = 2, Cost = 35, Description = "Oil Change", RepairDate = DateTime.Now },
                new Repair { Id = ++id, VehicleId = 3, Cost = 35, Description = "Oil Change", RepairDate = DateTime.Now },
                new Repair { Id = ++id, VehicleId = 3, Cost = 35, Description = "Oil Change", RepairDate = DateTime.Now },
                new Repair { Id = ++id, VehicleId = 3, Cost = 35, Description = "Oil Change", RepairDate = DateTime.Now },
            };

            _context.AddRange(seedData);
        }

        private int SetId()
        {
            return _context.Count() + 1;
        }

        public Repair Add(Repair entity)
        {
            if (entity.Id == 0)
                entity.Id = SetId();

            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var repair = _context.SingleOrDefault(x => x.Id == id);

            if (repair != null)
                _context.Remove(repair);
        }

        public ICollection<Repair> GetAll()
        {
            throw new NotImplementedException();
        }

        public Repair GetById(int id)
        {
            return _context.Where(x => x.Id == id).SingleOrDefault();
        }

        public IEnumerable<Repair> GetAllByVehicleId(int vehicleId)
        {
            return _context.Where(x => x.VehicleId == vehicleId).ToList();
        }

        public void Update()
        {
            
        }

        public void SaveChanges()
        {
            
        }
    }
}

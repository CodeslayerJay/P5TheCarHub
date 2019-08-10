using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
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

        public Repair Add(Repair entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Repair> GetAll()
        {
            throw new NotImplementedException();
        }

        public Repair GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

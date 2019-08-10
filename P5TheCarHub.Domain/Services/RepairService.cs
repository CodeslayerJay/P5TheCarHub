using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class RepairService
    {
        private readonly IRepairRepository _repairRepo;

        public RepairService(IRepairRepository repairRepository)
        {
            _repairRepo = repairRepository;
        }

        public Repair AddRepair(Repair repair)
        {
            return _repairRepo.Add(repair);
        }
    }
}

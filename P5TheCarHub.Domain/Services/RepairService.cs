﻿using P5TheCarHub.Core.Entities;
using P5TheCarHub.Core.Exceptions;
using P5TheCarHub.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Services
{
    public class RepairService
    {
        private readonly IRepairRepository _repairRepo;
        private readonly IVehicleRepository _vehicleRepo;

        public RepairService(IRepairRepository repairRepository, IVehicleRepository vehicleRepository)
        {
            _repairRepo = repairRepository;
            _vehicleRepo = vehicleRepository;
        }

        public Repair AddRepair(Repair repair)
        {
            UpdateVehicleSalePrice(repair);
            return _repairRepo.Add(repair);
        }

        private void UpdateVehicleSalePrice(Repair repair)
        {
            var vehicle = _vehicleRepo.GetById(id: repair.VehicleId);

            if (vehicle == null)
                throw new VehicleNotFoundException(repair.VehicleId);

            vehicle.SalePrice += repair.Cost;
        }
    }
}
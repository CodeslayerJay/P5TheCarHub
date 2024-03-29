﻿using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Interfaces.Repositories
{
    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        IEnumerable<Photo> GetAllByVehicleId(int vehicleId);
        Photo GetVehicleMainPhoto(int vehicleId);
        Photo GetFirstPhotoNotSetAsMain(int vehicleId);
        
    }
}

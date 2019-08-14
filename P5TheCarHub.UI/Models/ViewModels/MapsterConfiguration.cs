using Mapster;
using P5TheCarHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class MapsterConfiguration
    {
        public MapsterConfiguration()
        {
            TypeAdapterConfig<Vehicle, VehicleFormModel>.NewConfig()
                .Map(vm => vm.VehicleId, v => v.Id);
            TypeAdapterConfig<VehicleFormModel, Vehicle>.NewConfig()
                .Map(v => v.Id, vm => vm.VehicleId);
            TypeAdapterConfig<Vehicle, VehicleViewModel>.NewConfig()
                .Map(vm => vm.VehicleId, v => v.Id);
            
        }
    }
}

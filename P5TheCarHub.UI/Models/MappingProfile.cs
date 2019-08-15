using AutoMapper;
using P5TheCarHub.Core.Entities;
using P5TheCarHub.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleViewModel>()
            .ForMember(vm => vm.VehicleId, opts => opts.MapFrom(v => v.Id))
            .ForMember(vm => vm.FullVehicleName,
                opts => opts.MapFrom(v => $"{v.Year} {v.Make} {v.Model} {v.Trim}"));
            CreateMap<VehicleFormModel, Vehicle>()
                .ForMember(v => v.Id, opts => opts.MapFrom(vm => vm.VehicleId));
            CreateMap<Vehicle, VehicleFormModel>()
                .ForMember(v => v.VehicleId, opts => opts.MapFrom(vm => vm.Id));
        }
    }
}

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
            // Vehicle
            CreateMap<Vehicle, VehicleViewModel>()
            .ForMember(vm => vm.VehicleId, opts => opts.MapFrom(v => v.Id))
            .ForMember(vm => vm.FullVehicleName,
                opts => opts.MapFrom(v => $"{v.Year} {v.Make} {v.Model} {v.Trim}"));
            CreateMap<VehicleFormModel, Vehicle>()
                .ForMember(v => v.Id, opts => opts.MapFrom(vm => vm.VehicleId));
            CreateMap<Vehicle, VehicleFormModel>()
                .ForMember(v => v.VehicleId, opts => opts.MapFrom(vm => vm.Id));

            // Repair
            CreateMap<RepairFormModel, Repair>()
                .ForMember(r => r.Id, opts => opts.MapFrom(vm => vm.RepairId))
                .ForMember(r => r.VehicleId, opts => opts.MapFrom(vm => vm.VehicleId));
            CreateMap<Repair, RepairFormModel>()
                .ForMember(vm => vm.RepairId, opts => opts.MapFrom(r => r.Id))
                .ForMember(vm => vm.VehicleId, opts => opts.MapFrom(r => r.VehicleId));
            CreateMap<Repair, RepairViewModel>()
                .ForMember(vm => vm.RepairId, opts => opts.MapFrom(r => r.Id))
                .ForMember(vm => vm.VehicleId, opts => opts.MapFrom(r => r.VehicleId));
        }
    }
}

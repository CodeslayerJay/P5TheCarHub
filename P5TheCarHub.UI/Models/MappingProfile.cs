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
                .ForMember(r => r.Id, opts => opts.MapFrom(vm => vm.RepairId));
                
            CreateMap<Repair, RepairFormModel>()
                .ForMember(vm => vm.RepairId, opts => opts.MapFrom(r => r.Id));
                
            CreateMap<Repair, RepairViewModel>()
                .ForMember(vm => vm.RepairId, opts => opts.MapFrom(r => r.Id));
                

            // Invoice
            CreateMap<InvoiceFormModel, Invoice>()
                .ForMember(i => i.Id, opt => opt.MapFrom(vm => vm.InvoiceId));

            CreateMap<Invoice, InvoiceFormModel>()
                .ForMember(vm => vm.InvoiceId, opt => opt.MapFrom(i => i.Id));

            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(vm => vm.InvoiceId, opt => opt.MapFrom(i => i.Id));


            // Photo
            CreateMap<PhotoFormModel, Photo>();
            CreateMap<Photo, PhotoFormModel>();
        }

    }
}

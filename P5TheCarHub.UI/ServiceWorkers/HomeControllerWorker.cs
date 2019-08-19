using AutoMapper;
using P5TheCarHub.Core.Interfaces.Services;
using P5TheCarHub.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.ServiceWorkers
{
    public class HomeControllerWorker : IHomeControllerWorker
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public HomeControllerWorker(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public HomeViewModel ExecuteIndex()
        {
            var vehicles = _vehicleService.GetAll(amount: 3, orderBy: "LotDate").Select(x => _mapper.Map<VehicleViewModel>(x));

            var viewModel = new HomeViewModel
            {
                Vehicles = vehicles,
            };

            return viewModel;
        }

        public ContactFormModel ExecuteContact(int? vehicleId)
        {

            var contactFormModel = new ContactFormModel();

            if (vehicleId.HasValue)
            {
                var vehicle = _vehicleService.GetVehicle(vehicleId.Value, withIncludes: true);

                if(vehicle != null)
                {
                    contactFormModel.Vehicle = new ContactFormVehicleDetails
                    {
                        Id = vehicle.Id,
                        FullVehicleName = _vehicleService.GetFullVehicleName(vehicle),
                        Photo = (vehicle.Photos.Any()) ? vehicle.Photos.FirstOrDefault(x => x.IsMain).ImageUrl : null,
                        Mileage = vehicle.Mileage?.ToString(),
                        VIN = vehicle.VIN,
                        SalePrice = vehicle.SalePrice.ToString("c")
                    };
                }
            }

            return contactFormModel;
        }
    }
}

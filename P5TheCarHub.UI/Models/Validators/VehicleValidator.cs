using FluentValidation;
using P5CarSalesAppBasic.UI.Models.Validators;
using P5TheCarHub.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace P5CarSalesAppBasic.Models.Validators
{
    public class VehicleValidator : BaseValidator<VehicleFormModel>
    {

        public VehicleValidator()
        {
            
            RuleFor(x => x.VIN).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                    .WithMessage("VIN is required.")
                .MaximumLength(500)
                    .WithMessage("Maximum length is 500 characters.");
            RuleFor(x => x.Make).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Make is required.")
                .MaximumLength(100).WithMessage("Maximum length is 100 characters.");
            RuleFor(x => x.Model).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(100).WithMessage("Maximum length is 100 characters.");
            RuleFor(x => x.Trim).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Trim is required.")
                .MaximumLength(100).WithMessage("Maximum length is 100 characters.");
            RuleFor(x => x.Color).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(100).WithMessage("Maximum length is 100 characters.");
            
            RuleFor(x => x.Mileage).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Mileage is required.")
                .Must(Mileage => Double.TryParse(Mileage.ToString(), out double result))
                    .WithMessage("Mileage must contain only numbers and commas.")
                .Must(Mileage => double.Parse(Mileage.ToString()) >= 0)
                    .WithMessage("Mileage must 0 or greater.");

            RuleFor(x => x.Year).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Year is required.")
                .Must(Year => int.TryParse(Year, out int result))
                    .WithMessage("Year must contain only numbers. ")
                .GreaterThan("1990").WithMessage("Must be greater than 1990.")
                .LessThanOrEqualTo(DateTime.Now.Year.ToString()).WithMessage("Must be current year or earlier.");

            
            RuleFor(x => x.PurchasePrice).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Purchase Price is required.")
                .Must(PurchasePrice => PriceMustBeACurrency(PurchasePrice.ToString()))
                    .WithMessage("Purchase price may include only numbers, commas, and decimals.")
                .Must(PurchasePrice => PriceMustBeGreaterThanZero(PurchasePrice.ToString()))
                    .WithMessage("Purchase price must be greater than 0.");
            
            

            RuleFor(x => x.PurchaseDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Purchase date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date must be today or earlier.");
            RuleFor(x => x.LotDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Lot date is required.")
                .GreaterThanOrEqualTo(x => x.PurchaseDate).WithMessage("Date cannot be before purchase date.");

        }

    }
}

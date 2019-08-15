using FluentValidation;
using P5CarSalesAppBasic.UI.Models.Validators;
using P5TheCarHub.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.Validators
{
    public class RepairValidator : BaseValidator<RepairFormModel>
    {
        public RepairValidator()
        {
            RuleFor(x => x.Description).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().MaximumLength(100);
            RuleFor(x => x.Details).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(3000);
            RuleFor(x => x.Cost).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(Cost => PriceMustBeACurrency(Cost.ToString()))
                    .WithMessage("Repair cost can only be numbers, commas, and decimals.")
                .Must(Cost => PriceMustBeGreaterThanZero(Cost.ToString()))
                    .WithMessage("Repair cost must be greater than 0.");

            RuleFor(x => x.RepairDate).Cascade(CascadeMode.StopOnFirstFailure)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date must be today or earlier.");
        }
    }
}

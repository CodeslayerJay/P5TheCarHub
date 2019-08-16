using FluentValidation;
using P5CarSalesAppBasic.UI.Models.Validators;
using P5TheCarHub.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.Validators
{
    public class InvoiceValidator : BaseValidator<InvoiceFormModel>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.CustomerName).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(100);

            RuleFor(x => x.PriceSold).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(PriceSold => Decimal.TryParse(PriceSold.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal result))
                    .WithMessage("Price sold may include only numbers, commas, and decimals.")
                .Must(PriceSold => PriceMustBeGreaterThanZero(PriceSold.ToString()))
                    .WithMessage("Price must be greater than 0");
                

            RuleFor(x => x.DateSold).Cascade(CascadeMode.StopOnFirstFailure)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date must be today or earlier.");
        }

    }
}

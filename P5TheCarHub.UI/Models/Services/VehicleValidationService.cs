using FluentValidation.Results;
using P5CarSalesAppBasic.Models.Validators;
using P5TheCarHub.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.Services
{
    public class VehicleValidationService
    {
        private readonly VehicleValidator _validator;

        public VehicleValidationService()
        {
            _validator = new VehicleValidator();
        }

        /// <summary>
        /// Returns Dictionary [string property name] [string error message] of any input validation errors.
        /// </summary>
        /// <param name="formModel"></param>
        /// <returns></returns>
        public  Dictionary<string, string> CheckForInputErrors(VehicleFormModel formModel)
        {
            var errors = new Dictionary<string, string>();
            
            var result = _validator.Validate(formModel);

            if (result.Errors.Any())
            {
                foreach(var error in result.Errors)
                {
                    errors.Add(error.PropertyName, error.ErrorMessage);
                }
            }

            return errors;
        }
    }
}

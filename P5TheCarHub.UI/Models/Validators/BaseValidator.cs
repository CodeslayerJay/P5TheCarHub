using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace P5CarSalesAppBasic.UI.Models.Validators
{
    public class BaseValidator<T> : AbstractValidator<T> where T : class
    {

        protected virtual bool PriceMustBeACurrency(string price)
        {
            var result = Decimal.TryParse(price.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal value);

            return result;
        }

        
        protected virtual bool PriceMustBeGreaterThanZero(string price)
        {
            var result = Decimal.TryParse(price.ToString(), NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal value);

            if (result)
            {
                return (value > 0) ? true : false;
            }

            return result;
        }
    }
}

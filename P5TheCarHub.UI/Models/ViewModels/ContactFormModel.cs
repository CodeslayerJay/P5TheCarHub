
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P5TheCarHub.UI.Models.ViewModels
{
    public class ContactFormModel
    {

        [Required]
        [Display(Name = "Full Name*")]
        public string Name { get; set; }

        [Display(Name = "Car Info")]
        public string CarInfo { get; set; }

        [MaxLength(1000)]
        public string Note { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        [Display(Name = "Phone Number*")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Display(Name="Best Time To Call")]
        public string BestTimeToCall { get; set; }

        [Display(Name="Contact Method")]
        public string ContactMethod { get; set; }


        public ContactFormVehicleDetails Vehicle { get; set; }
    }
}

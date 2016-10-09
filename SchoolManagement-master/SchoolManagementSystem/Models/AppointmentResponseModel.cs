using SchoolManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class AppointmentResponseModel : IValidatableObject
    {
        public int Id { get; set; }

        public Appointment Appointment { get; set; }

        [Display(Name = "Accept")]
        public bool Accepted { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public bool AlreadyAccepted { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Accepted && string.IsNullOrEmpty(Notes))
            {
                //Reason needs to be given why a appointment is denied.
                yield return new ValidationResult("Fully booked for the day");
            }
        }
    }
}

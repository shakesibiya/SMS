using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SchoolManagementSystem.Models
{
    public class AppointmentModel : IValidatableObject
    {
        //Create a new appointment.
        [Required(ErrorMessage = "Please Enter Location")]
        [Display(Name = "Appointment Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public string DateTimeString { get; set; }

        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please Enter Location")]
        [Display(Name = "Locatie")]
        public string Location { get; set; }

        public StudentViewModel Student { get; set; }

        public bool Accepted { get; set; }

        public bool Noted { get; set; }

        public int Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(DateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime))
            {
                DateTime = dateTime;
            }
            else
            {
                //Making sure a valid date is given.
                yield return new ValidationResult("Making sure a valid date is given");
            }

            if (DateTime < DateTime.Now)
            {
                //Making sure the date planned is in the future.
                yield return new ValidationResult("Making sure the date planned is in the future");
            }
        }
    }
}

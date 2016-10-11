using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Models
{
    public class PaymentViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //item details
        [Required]
        [DisplayName("Price")]
        public string price { get; set; }

        //Address details for credit card payment
        [DisplayName("City")]
        public string city { get; set; }

        [DisplayName("Country Code")]
        public string countryCode { get; set; }

        [DisplayName("Address Line")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string line1 { get; set; }

        [DisplayName("Postal Code")]
        public string postalCode { get; set; }

        [DisplayName("State")]
        [StringLength(2, ErrorMessage = "The {0} must be {2} characters long.")]
        public string state { get; set; }

        //Credit card details
        [DisplayName("CVV")]
        [StringLength(4, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength =3)]
        public string cvv { get; set; }

        [DisplayName("Expire Month")]
        public int expireMonth { get; set; }
        [DisplayName("Expire Year")]
        public int expireYear { get; set; }
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [DisplayName("Last Name")]
        public string lastName { get; set; }
        
        [DisplayName("Credit Card No.")]
        [StringLength(16, ErrorMessage = "The {0} must be {2} characters long.")]
        public string creditCardNo { get; set; }

        [DisplayName("Credit Card Type")]
        public string creditCardType { get; set; }

        //Saved data
        public string total { get; set; }
        public string paymentType { get; set; }

        [Required]
        public virtual int subjectId { get; set; }

        [Required]
        public string studentPin { get; set; }
    }
}
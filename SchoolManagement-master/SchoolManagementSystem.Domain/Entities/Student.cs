using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public string PIN { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string FileName { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        public string Religion { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PostalAddress { get; set; }
        public string StreetAddress { get; set; }
        public string Surbs { get; set; }
        public string PostalCode { get; set; }
        public Avatar Avats { get; set; }
        public virtual List<Avatar> Avatars { get; set; }
        public virtual Country Countryid { get; set; }

        public virtual List<Candidate> Candidates { get; set; }
        public virtual int? Class_Id { get; set; }
    }
}

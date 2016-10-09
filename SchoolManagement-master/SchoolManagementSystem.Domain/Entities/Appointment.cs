using System;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public int StudentID { get; set; }

        public Student Student { get; set; }

        public string PIN { get; set; }

        public Teacher Teacher { get; set; }

        public string Location { get; set; }

        public bool Accepted { get; set; }

        public bool Noted { get; set; }
    }
}

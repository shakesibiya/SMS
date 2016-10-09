using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Models
{
    public class EventViewModel
    {
        public Event Event { get; set; }

        public EventComment EventComment { get; set; }
    }
}
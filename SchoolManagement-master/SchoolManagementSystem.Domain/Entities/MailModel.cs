using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SchoolManagementSystem.Domain.Entities
{
    public class MailModel
    {
        public long Id { get; set; }
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public HttpPostedFileBase Upload { get; set; }
    }
}
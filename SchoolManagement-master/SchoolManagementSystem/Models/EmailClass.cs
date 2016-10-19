using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class EmailClass
    {
        public List<MailAddress> to { get; set; }
        public MailAddress From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool isHtml { get; set; }

        public string Method()
        {
            string message = "Fail to send email";
            try
            {
                SmtpClient client= new SmtpClient();
                //client.Host = "localhost";
                //client.Port = 25;
                client.Host = "mfd.dut.ac.za";
                client.Port = 443;
                client.EnableSsl = true;

                //client.Credentials = new NetworkCredential("", "");
                client.Credentials = new NetworkCredential("21449476@dut.ac.za", "Dut950622");

                MailMessage mail = new MailMessage();
                mail.From = From;
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = isHtml;

                foreach(var i in to)
                {
                    mail.To.Add(i);
                }
                client.Send(mail);

                message = "Email is Sent";
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }
    }
}
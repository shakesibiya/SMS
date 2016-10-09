using SchoolManagementSystem.Domain.Entities;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mail/Create
        [HttpPost]
        public ActionResult Create(MailModel mail)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                string from = "21230174@dut4life.ac.za";
                using (MailMessage m = new MailMessage(from, mail.To))
                {
                    m.Subject = mail.Subject;
                    m.Body = mail.Body;
                    m.IsBodyHtml = true;
                    if (mail.Upload != null && mail.Upload.ContentLength > 0)
                    {
                        m.Attachments.Add(new Attachment(mail.Upload.InputStream, Path.GetFileName(mail.Upload.FileName)));
                    }
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mfd.dut.ac.za";
                    smtp.EnableSsl = true;
                    NetworkCredential netCred = new NetworkCredential(from, "Dut930628");
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = netCred;
                    smtp.Port = 443;
                    smtp.Timeout = 60000;
                    smtp.Send(m);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Mail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mail/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mail/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

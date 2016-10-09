using System;
using System.Linq;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        public ActionResult List(Teacher teacher)
        {
            // Get all the appointments for the logged in counseler.
            var model = repository.Appointments.ToList().Where(a => a.PIN == teacher.PIN);
            return View(model);
        }
        public ActionResult AppointmentByStudent()
        {
            DateTime tommorow = DateTime.Now.AddDays(1);
            var model = new AppointmentByStudentModel
                            {
                                // Default appointment date is tomorrow 10:00.
                                DateTimeString = new DateTime(tommorow.Year, tommorow.Month, tommorow.Day, 10, 0, 0).ToString("dd-MM-yyyy HH:mm")
                            };

            PrepareStudentCounselingRequestModel(model);

            return View(model);
        }

        public ActionResult AppointmentByStudent(AppointmentByStudentModel model, string id)
        {
            // A student wishes to make an appointment with their counseler.
            if (ModelState.IsValid)
            {
                // Get the logged in student and counseler of the class the student is in.
                var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
                ViewBag.id = id;
                var curStudent = repository.Students.FirstOrDefault(x => x.PIN == id);

                Teacher teacher = repository.Teachers.FirstOrDefault(x => x.PIN == currUser.Login);


                if (teacher != null && curStudent != null)
                {
                    // Create the appointment for the student and couseler.
                    var appointment = new Appointment
                                          {
                                              PIN = teacher.PIN,
                                              StudentID = curStudent.StudentID,
                                              DateTime = model.DateTime,
                                              Location = model.Location
                                          };

                    repository.AddAppointment(appointment);

                    // Send the appointment request mail.
                    /*var mail = new AppointmentMail
                    {
                        Counseler = counseler,
                        Student = student,
                        DateTime = appointment.DateTime,
                        Location = appointment.Location,
                        AcceptUrl = FullUrl("AppointmentResponse", new { id }),
                        FromCounseler = false
                    };

                    _mailHelper.SendAppointmentMail(mail);*/

                    return RedirectToAction("Details", "Appointment", new { id });
                }
            }

            PrepareStudentCounselingRequestModel(model);
            return View(model);
        }
        public ActionResult AppointmentByCounseler(int? studentId)
        {
            // A counseler wishes to create an appointment with their student.
            DateTime tommorow = DateTime.Now.AddDays(1);
            var model = new AppointmentByCounselerModel
                            {
                                // Default appointment date is tomorrow 10:00.
                                DateTimeString = new DateTime(tommorow.Year, tommorow.Month, tommorow.Day, 10, 0, 0).ToString("dd-MM-yyyy HH:mm")
                            };

            PrepareCounselerCounselingRequestModel(model);

            if (studentId.HasValue)
            {
                // Prefill the student id from the querystring.
                model.StudentId = studentId.Value;
            }

            return View(model);
        }
        public ActionResult AppointmentByCounseler(AppointmentByCounselerModel model, string id)
        {
            if (ModelState.IsValid)
            {
                // Get the logged in counseler and the chosen student.
                var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
                ViewBag.id = id;
                var curStudent = repository.Students.FirstOrDefault(x => x.PIN == id);

                Teacher teacher = repository.Teachers.FirstOrDefault(x => x.PIN == currUser.Login);
                if (teacher != null && curStudent != null)
                {
                    // Create the appointment for the counseler and student.
                    var appointment = new Appointment
                                          {
                                              PIN = teacher.PIN,
                                              StudentID = model.StudentId,
                                              DateTime = model.DateTime,
                                              Location = model.Location
                                          };

                    repository.AddAppointment(appointment);

                    // Send the appointment mail.
                    /*var mail = new AppointmentMail
                                   {
                                       Counseler = counseler,
                                       Student = student,
                                       DateTime = appointment.DateTime,
                                       Location = appointment.Location,
                                       AcceptUrl = FullUrl("AppointmentResponse", new { id }),
                                       FromCounseler = true
                                   };
                    _mailHelper.SendAppointmentMail(mail);*/

                    return RedirectToAction("Details", "Appointment", new { id });
                }
            }

            PrepareCounselerCounselingRequestModel(model);
            return View(model);
        }
        //public ActionResult Details(int id)
        //{
        //    // Get the appointment by its id.
        //    var appointment = _appointmentRepository.GetById(id);
        //    if (appointment == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (!AuthorizeAppointment(appointment))
        //    {
        //        return new HttpUnauthorizedResult();
        //    }

        //    ViewBag.IsCounseler = Counseler != null;

        //    return View(appointment);
        //}
        public ActionResult AppointmentResponse(int id)
        {
            // Student or counseler wants to respond on the appointment request.
            //var appointment = _appointmentRepository.GetById(id);
            var appointment = db.Appointment.FirstOrDefault(x => x.Id == id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            var model = new AppointmentResponseModel
                            {
                                Appointment = appointment,
                                Accepted = true,
                                AlreadyAccepted = appointment.Accepted
                            };

            return View(model);
        }
        public ActionResult AppointmentResponse(AppointmentResponseModel model)
        {
            var details = db.Appointment.FirstOrDefault(x => x.Id == model.Id);
            if (details == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the appointment status.
                details.Accepted = model.Accepted;
                

                // Create a repsonse mail.
                //var mail = new AppointmentRepsonseMail
                //               {
                //                   Appointment = appointment,
                //                   Notes = model.Notes,
                //                   FromCounseler = Counseler != null
                //               };
                //_mailHelper.SendAppointmentResponseMail(mail);

                return RedirectToAction("","");
            }

            model.Appointment = details;
            return View(model);
        }

        /// <summary>
        /// Checks that the specified <paramref name="appointment"/> belongs to the 
        /// logged in student or counseler.
        /// </summary>
        /// <param name="appointment">The appointment to authorize.</param>
        private bool AuthorizeAppointment(Appointment appointment, Teacher counseler, Student student)
        {
            return student != null && appointment.StudentID == student.StudentID ||
                   counseler != null && appointment.PIN == counseler.PIN;
        }

        private void PrepareCounselerCounselingRequestModel(AppointmentByCounselerModel model)
        {
            //Teacher teacher;

            // Populate the select list with all the students of the logged in counseler.
            /*model.StudentsList = SelectList(db.Students.ToList().Where(s => s.Class.Id == teacher.PIN),
                s => s.Id,
                s => string.Format("{0} ({1})", s.GetFullName(), s.StudentNr));*/
        }

        private void PrepareStudentCounselingRequestModel(AppointmentByStudentModel model)
        {
            // Add the corresponding counseler to the view model.
            /*var student = Student;
            var @class = _classRepository.GetById(student.ClassId);
            var counseler = CounselerRepository.GetById(@class.CounselerId);

            model.Counseler = Mapper.Map<CounselerModel>(counseler);*/
        }
    }
}

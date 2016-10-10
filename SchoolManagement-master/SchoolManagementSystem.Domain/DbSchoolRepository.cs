using System;
using System.Collections.Generic;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Domain
{
    public class DbSchoolRepository
    {
        private DbSchoolContext context = new DbSchoolContext();

        public IEnumerable<Event> Events
        {
            get { return context.Event; }
        }
        public IEnumerable<Administrator> Administrators
        {
            get { return context.Administrators; }
        }

        public IEnumerable<Teacher> Teachers
        {
            get { return context.Teachers; }
        }

        public IEnumerable<Student> Students
        {
            get { return context.Students; }
        }

        public IEnumerable<Class> Classes
        {
            get { return context.Classes; }
        }


        public IEnumerable<Discipline> Disciplines
        {
            get { return context.Disciplines; }
        }

        public IEnumerable<Mark> Marks
        {
            get { return context.Marks; }
        }
        public IEnumerable<Post> News
        {
            get { return context.Post; }
        }
        public IEnumerable<PostPictures> PostPictures
        {
            get { return context.PostPicture; }
        }
        public IEnumerable<Avatar> Avatars
        {
            get { return context.Avatars; }
        }
        public IEnumerable<ApplicationForm> Applications
        {
            get { return context.Applications; }
        }
        public IEnumerable<Appointment> Appointments
        {
            get { return context.Appointment; }
        }

        public IEnumerable<Payment> Payments
        {
            get { return context.Payments; }
        }

        public IEnumerable<Booking> Bookings
        {
            get { return context.Bookings; }
        }

        public IEnumerable<Voting> Votings
        {
            get { return context.Votings; }
        }

        public IEnumerable<Payroll> Payrolls
        {
            get { return context.Payrolls; }
        }

        public int AddEvent (Event item)
        {
            context.Event.Add(item);
            return context.SaveChanges();
        }
        public int AddAdministrator(Administrator item)
        {
            context.Administrators.Add(item);
            return context.SaveChanges();
        }

        public int AddTeacher(Teacher item)
        {
            context.Teachers.Add(item);
            return context.SaveChanges();
        }

        public int AddStudent(Student item)
        {
            context.Students.Add(item);
            return context.SaveChanges();
        }

        public int AddPayment(Payment item)
        {
            context.Payments.Add(item);
            return context.SaveChanges();
        }

        public int AddBooking(Booking item)
        {
            context.Bookings.Add(item);
            return context.SaveChanges();
        }

        public int RemoveStudent(Student item)
        {
            context.Students.Remove(item);
            return context.SaveChanges();
        }

        public int AddClass(Class item)
        {
            context.Classes.Add(item);
            return context.SaveChanges();
        }

        public int AddDiscipline(Discipline item)
        {
            context.Disciplines.Add(item);
            return context.SaveChanges();
        }

        public int AddMark(Mark item)
        {
            context.Marks.Add(item);
            return context.SaveChanges();
        }

        public int AddNews(Post item)
        {
            item.Date = DateTime.Now;
            context.Post.Add(item);
            return context.SaveChanges();
        }

        public int AddPosts (PostPictures item)
        {
            context.PostPicture.Add(item);
            return context.SaveChanges();
        }

        public int AddApplication (ApplicationForm item)
        {
            context.Applications.Add(item);
            return context.SaveChanges();
        }

        public int AddAppointment(Appointment item)
        {
            context.Appointment.Add(item);
            return context.SaveChanges();
        }

        public int AddPayroll(Payroll item)
        {
            context.Payrolls.Add(item);
            return context.SaveChanges();
        }

        //public IEnumerable<ToDoRecord> Records
        //{
        //    get { return context.Records; }
        //}

        //public int AddRecord(ToDoRecord toDo)
        //{
        //    context.Records.Add(toDo);
        //    return context.SaveChanges();
        //}

        //public int MakeDoneRecord(Guid recordId)
        //{
        //    var recordToBeDone = context.Records.FirstOrDefault(x => x.Id == recordId);
        //    if (recordToBeDone != null)
        //    {
        //        recordToBeDone.IsComplete = true;
        //    }

        //    return context.SaveChanges();
        //}

        //public int RemoveRecord(Guid recordId)
        //{
        //    var recordToDelete = context.Records.FirstOrDefault(x => x.Id == recordId);
        //    context.Records.Remove(recordToDelete);
        //    return context.SaveChanges();
        //}

        public void DeleteDatabase()
        {
            this.context.Database.Delete();
            this.context.SaveChanges();
        }
    }
}

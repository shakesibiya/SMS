namespace SchoolManagementSystem.Domain.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolManagementSystem.Domain.DbSchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SchoolManagementSystem.Domain.DbSchoolContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            DbSchoolRepository repository = new DbSchoolRepository();

            //repository.DeleteDatabase();

            repository.AddAdministrator(new Administrator { FirstName = "Director", Login = "admin", Password = "321", Role = "Director" });
            repository.AddAdministrator(new Administrator { FirstName = "Principal", Login = "Principal", Password = "321", Role = "Secretary" });
            repository.AddAdministrator(new Administrator { FirstName = "Secretary", Login = "secretary", Password = "321", Role = "Secretary" });

            repository.AddStudent(new Student { PIN = "2010", FirstName = "Adam", LastName = "Sandler", Password = "654", Class_Id = 1, PhoneNumber = "0792893345", StreetAddress = "Umlazi AA 4", Nationality = "South African", Religion = "Muslim", Email = "Sandler@gmail.com" });
            repository.AddStudent(new Student { PIN = "2011", FirstName = "Jim", LastName = "Carrey", Password = "654", Class_Id = 1, PhoneNumber = "0792893345", StreetAddress = "KwaMashu 86", Nationality = "South African", Religion = "Christian", Email = "Carrey@gmail.com" });
            repository.AddStudent(new Student { PIN = "2012", FirstName = "Anne", LastName = "Hathaway", Password = "654", Class_Id = 1, PhoneNumber = "0792893345", StreetAddress = "Chestervile Road 77", Nationality = "South African", Religion = "Christian", Email = "Hathaway@gmail.com" });
            repository.AddStudent(new Student { PIN = "2013", FirstName = "Robert", LastName = "John Downey", Password = "654", Class_Id = 2, PhoneNumber = "0792893345", StreetAddress = "33 Gilbert Roard Everton", Nationality = "South African", Religion = "Rasta", Email = "Downey105@gmail.com" });
            repository.AddStudent(new Student { PIN = "2014", FirstName = "Chris", LastName = "Evans", Password = "654", Class_Id = 3, PhoneNumber = "0792893345", StreetAddress = "Khunte khinte 66 Umhlanga Ridge", Nationality = "South African", Religion = "Christian", Email = "Evans@gmail.com" });
            repository.AddStudent(new Student { PIN = "2015", FirstName = "Chris", LastName = "Hemsworth", Password = "654", Class_Id = 4, PhoneNumber = "0792893345", StreetAddress = "Zukhare Mirane 77 Biltong", Nationality = "South African", Religion = "Hindu", Email = "Hemsworth@gmail.com" });


            repository.AddClass(new Class { Name = "12-A" });
            repository.AddClass(new Class { Name = "12-B" });
            repository.AddClass(new Class { Name = "11-A" });
            repository.AddClass(new Class { Name = "11-B" });

            List<Class> classes = repository.Classes.ToList();
            List<Class> classesCristina = repository.Classes.Take(2).ToList();

            repository.AddTeacher(new Teacher { PIN = "2021", FirstName = "Emma", LastName = "Watson", Password = "456", Discipline_Id = 1, EducationalGrade = "USM Biology faculty", ClassesToStudy = classes });
            repository.AddTeacher(new Teacher { PIN = "2022", FirstName = "Cristina", LastName = "Purice", Password = "456", Discipline_Id = 3, EducationalGrade = "USM Mathematic and Informatics faculty", ClassesToStudy = classesCristina });

            context.States.AddOrUpdate(new State { Description = "Class Rep" });

            context.Votings.AddOrUpdate(new Voting
            {
                Description = "2016 Class Rep Elections",
                IsEnabledBlankVote = true,
                StateId = 1,
                Remarks = "Together we can make a difference",
                IsForAllUsers = true,
                DateTimeStart = DateTime.Now,
                DateTimeEnd = DateTime.Today,

            });

            repository.AddDiscipline(new Discipline { Subject = "Biology" });
            repository.AddDiscipline(new Discipline { Subject = "History" });
            repository.AddDiscipline(new Discipline { Subject = "Mathematic" });
            repository.AddDiscipline(new Discipline { Subject = "Chemistry" });
            repository.AddDiscipline(new Discipline { Subject = "Physics" });

            //context.Candidates.AddOrUpdate(new Candidate { StudentID = 1, VotingId = 1, QuantityVotes = 20 });
            //context.Candidates.AddOrUpdate(new Candidate { StudentID = 2, VotingId = 1, QuantityVotes = 19 });
            //context.Candidates.AddOrUpdate(new Candidate { StudentID = 3, VotingId = 1, QuantityVotes = 12 });
            //context.Candidates.AddOrUpdate(new Candidate { StudentID = 4, VotingId = 1, QuantityVotes = 10 });
            //context.Candidates.AddOrUpdate(new Candidate { StudentID = 5, VotingId = 1, QuantityVotes = 21 });

            //Countries
            context.Countries.AddOrUpdate(new Country { CountryCode = "Afghanistan" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Albania" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Algeria" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "AmericanSamoa" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Andorra" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Angola" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Anguilla" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Antarctica" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "AntiguaAnd Barbuda" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Argentina" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Armenia" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Aruba" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Australia" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Austria" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Azerbaijan" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Bahamas" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Bahrain" });
            context.Countries.AddOrUpdate(new Country { CountryCode = "Bangladesh" });

            #region Marks
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 1),
                Teacher_PIN = "2011",
                Discipline_Id = 1,
                Value = 10
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 4),
                Teacher_PIN = "2011",
                Discipline_Id = 2,
                Value = -1
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 11),
                Teacher_PIN = "2011",
                Discipline_Id = 2,
                Value = 9
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 15),
                Teacher_PIN = "2011",
                Discipline_Id = 3,
                Value = 9
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 21),
                Teacher_PIN = "2011",
                Discipline_Id = 1,
                Value = 6
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 28),
                Teacher_PIN = "2011",
                Discipline_Id = 4,
                Value = -1
            });
            #endregion

        }
    }
}

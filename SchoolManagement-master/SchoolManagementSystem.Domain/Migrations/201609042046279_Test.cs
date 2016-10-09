namespace SchoolManagementSystem.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Login = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Login);
            
            CreateTable(
                "dbo.ApplicationForms",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        PIN = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        MobilePhoneNumber = c.String(),
                        Nationality = c.String(),
                        FileName = c.String(),
                        Email = c.String(maxLength: 40),
                        Religion = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        PostalAddress = c.String(),
                        StreetAddress = c.String(),
                        Surbs = c.String(),
                        PostalCode = c.String(),
                        Class_Id = c.Int(),
                        Read = c.String(),
                        Date = c.DateTime(),
                        ReadDate = c.DateTime(),
                        Avats_Id = c.Long(),
                        Countryid_Countryid = c.Int(),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Avatars", t => t.Avats_Id)
                .ForeignKey("dbo.Countries", t => t.Countryid_Countryid)
                .Index(t => t.Avats_Id)
                .Index(t => t.Countryid_Countryid);
            
            CreateTable(
                "dbo.Avatars",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        FileName = c.String(),
                        Student_StudentID = c.Int(),
                        ApplicationForm_StudentID = c.Int(),
                        StudentTrush_StudentID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_StudentID)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .ForeignKey("dbo.ApplicationForms", t => t.ApplicationForm_StudentID)
                .ForeignKey("dbo.StudentTrushes", t => t.StudentTrush_StudentID)
                .Index(t => t.StudentID)
                .Index(t => t.Student_StudentID)
                .Index(t => t.ApplicationForm_StudentID)
                .Index(t => t.StudentTrush_StudentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        PIN = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        MobilePhoneNumber = c.String(),
                        Nationality = c.String(),
                        FileName = c.String(),
                        Email = c.String(maxLength: 40),
                        Religion = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        PostalAddress = c.String(),
                        StreetAddress = c.String(),
                        Surbs = c.String(),
                        PostalCode = c.String(),
                        Class_Id = c.Int(),
                        Avats_Id = c.Long(),
                        Countryid_Countryid = c.Int(),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Avatars", t => t.Avats_Id)
                .ForeignKey("dbo.Countries", t => t.Countryid_Countryid)
                .Index(t => t.Avats_Id)
                .Index(t => t.Countryid_Countryid);
            
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        VotingId = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        QuantityVotes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateId)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .ForeignKey("dbo.Votings", t => t.VotingId)
                .Index(t => t.VotingId)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Votings",
                c => new
                    {
                        VotingId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        StateId = c.Int(nullable: false),
                        Remarks = c.String(),
                        DateTimeStart = c.DateTime(nullable: false),
                        DateTimeEnd = c.DateTime(nullable: false),
                        IsForAllUsers = c.Boolean(nullable: false),
                        IsEnabledBlankVote = c.Boolean(nullable: false),
                        QuantityVotes = c.Int(nullable: false),
                        QuantityBlankVotes = c.Int(nullable: false),
                        CandidateWinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VotingId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.votingDetails",
                c => new
                    {
                        votingDetailId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                        VotingID = c.Int(nullable: false),
                        User_StudentID = c.Int(),
                    })
                .PrimaryKey(t => t.votingDetailId)
                .ForeignKey("dbo.Candidates", t => t.CandidateId)
                .ForeignKey("dbo.Students", t => t.User_StudentID)
                .ForeignKey("dbo.Votings", t => t.VotingID)
                .Index(t => t.CandidateId)
                .Index(t => t.VotingID)
                .Index(t => t.User_StudentID);
            
            CreateTable(
                "dbo.VotingGroups",
                c => new
                    {
                        VotingGroupId = c.Int(nullable: false, identity: true),
                        VotingId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VotingGroupId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Votings", t => t.VotingId)
                .Index(t => t.VotingId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupMemberId = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        User_StudentID = c.Int(),
                    })
                .PrimaryKey(t => t.GroupMemberId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Students", t => t.User_StudentID)
                .Index(t => t.GroupId)
                .Index(t => t.User_StudentID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Countryid = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(),
                    })
                .PrimaryKey(t => t.Countryid);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        StudentID = c.Int(nullable: false),
                        PIN = c.String(maxLength: 128),
                        Location = c.String(),
                        Accepted = c.Boolean(nullable: false),
                        Noted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .ForeignKey("dbo.Teachers", t => t.PIN)
                .Index(t => t.StudentID)
                .Index(t => t.PIN);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        PIN = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        MobilePhoneNumber = c.String(),
                        EducationalGrade = c.String(),
                        Email = c.String(),
                        Photo = c.String(maxLength: 200),
                        Discipline_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PIN);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentTrushes",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        PIN = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        MobilePhoneNumber = c.String(),
                        Nationality = c.String(),
                        FileName = c.String(),
                        Email = c.String(maxLength: 40),
                        Religion = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        PostalAddress = c.String(),
                        StreetAddress = c.String(),
                        Surbs = c.String(),
                        PostalCode = c.String(),
                        Class_Id = c.Int(),
                        DateDeleted = c.DateTime(nullable: false),
                        Avats_Id = c.Long(),
                        Countryid_Countryid = c.Int(),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Avatars", t => t.Avats_Id)
                .ForeignKey("dbo.Countries", t => t.Countryid_Countryid)
                .Index(t => t.Avats_Id)
                .Index(t => t.Countryid_Countryid);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        Description = c.String(nullable: false, maxLength: 180),
                        DateCreated = c.DateTime(),
                        EventDate = c.DateTime(nullable: false),
                        Venue = c.String(nullable: false, maxLength: 60),
                        More = c.String(maxLength: 180),
                        Archive = c.Boolean(nullable: false),
                        ArchiveDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventComments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EventId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Discipline_Id = c.Int(nullable: false),
                        Teacher_PIN = c.String(),
                        Student_PIN = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Author = c.String(),
                        Topic = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false, maxLength: 200),
                        Date = c.DateTime(nullable: false),
                        Public = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostPictures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PostId = c.Long(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.SmsModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SenderId = c.String(),
                        RecepientId = c.String(),
                        Text = c.String(maxLength: 120),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassTeachers",
                c => new
                    {
                        Class_Id = c.Int(nullable: false),
                        Teacher_PIN = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Class_Id, t.Teacher_PIN })
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Teacher_PIN, cascadeDelete: true)
                .Index(t => t.Class_Id)
                .Index(t => t.Teacher_PIN);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostPictures", "PostId", "dbo.Posts");
            DropForeignKey("dbo.StudentTrushes", "Countryid_Countryid", "dbo.Countries");
            DropForeignKey("dbo.StudentTrushes", "Avats_Id", "dbo.Avatars");
            DropForeignKey("dbo.Avatars", "StudentTrush_StudentID", "dbo.StudentTrushes");
            DropForeignKey("dbo.Appointments", "PIN", "dbo.Teachers");
            DropForeignKey("dbo.ClassTeachers", "Teacher_PIN", "dbo.Teachers");
            DropForeignKey("dbo.ClassTeachers", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Appointments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.ApplicationForms", "Countryid_Countryid", "dbo.Countries");
            DropForeignKey("dbo.ApplicationForms", "Avats_Id", "dbo.Avatars");
            DropForeignKey("dbo.Avatars", "ApplicationForm_StudentID", "dbo.ApplicationForms");
            DropForeignKey("dbo.Avatars", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Students", "Countryid_Countryid", "dbo.Countries");
            DropForeignKey("dbo.VotingGroups", "VotingId", "dbo.Votings");
            DropForeignKey("dbo.VotingGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupMembers", "User_StudentID", "dbo.Students");
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.votingDetails", "VotingID", "dbo.Votings");
            DropForeignKey("dbo.votingDetails", "User_StudentID", "dbo.Students");
            DropForeignKey("dbo.votingDetails", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Votings", "StateId", "dbo.States");
            DropForeignKey("dbo.Candidates", "VotingId", "dbo.Votings");
            DropForeignKey("dbo.Candidates", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Students", "Avats_Id", "dbo.Avatars");
            DropForeignKey("dbo.Avatars", "Student_StudentID", "dbo.Students");
            DropIndex("dbo.ClassTeachers", new[] { "Teacher_PIN" });
            DropIndex("dbo.ClassTeachers", new[] { "Class_Id" });
            DropIndex("dbo.PostPictures", new[] { "PostId" });
            DropIndex("dbo.StudentTrushes", new[] { "Countryid_Countryid" });
            DropIndex("dbo.StudentTrushes", new[] { "Avats_Id" });
            DropIndex("dbo.Appointments", new[] { "PIN" });
            DropIndex("dbo.Appointments", new[] { "StudentID" });
            DropIndex("dbo.GroupMembers", new[] { "User_StudentID" });
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropIndex("dbo.VotingGroups", new[] { "GroupId" });
            DropIndex("dbo.VotingGroups", new[] { "VotingId" });
            DropIndex("dbo.votingDetails", new[] { "User_StudentID" });
            DropIndex("dbo.votingDetails", new[] { "VotingID" });
            DropIndex("dbo.votingDetails", new[] { "CandidateId" });
            DropIndex("dbo.Votings", new[] { "StateId" });
            DropIndex("dbo.Candidates", new[] { "StudentID" });
            DropIndex("dbo.Candidates", new[] { "VotingId" });
            DropIndex("dbo.Students", new[] { "Countryid_Countryid" });
            DropIndex("dbo.Students", new[] { "Avats_Id" });
            DropIndex("dbo.Avatars", new[] { "StudentTrush_StudentID" });
            DropIndex("dbo.Avatars", new[] { "ApplicationForm_StudentID" });
            DropIndex("dbo.Avatars", new[] { "Student_StudentID" });
            DropIndex("dbo.Avatars", new[] { "StudentID" });
            DropIndex("dbo.ApplicationForms", new[] { "Countryid_Countryid" });
            DropIndex("dbo.ApplicationForms", new[] { "Avats_Id" });
            DropTable("dbo.ClassTeachers");
            DropTable("dbo.SmsModels");
            DropTable("dbo.PostPictures");
            DropTable("dbo.Posts");
            DropTable("dbo.Marks");
            DropTable("dbo.Files");
            DropTable("dbo.EventComments");
            DropTable("dbo.Events");
            DropTable("dbo.Disciplines");
            DropTable("dbo.StudentTrushes");
            DropTable("dbo.Classes");
            DropTable("dbo.Teachers");
            DropTable("dbo.Appointments");
            DropTable("dbo.Countries");
            DropTable("dbo.GroupMembers");
            DropTable("dbo.Groups");
            DropTable("dbo.VotingGroups");
            DropTable("dbo.votingDetails");
            DropTable("dbo.States");
            DropTable("dbo.Votings");
            DropTable("dbo.Candidates");
            DropTable("dbo.Students");
            DropTable("dbo.Avatars");
            DropTable("dbo.ApplicationForms");
            DropTable("dbo.Administrators");
        }
    }
}

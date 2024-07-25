using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.Controllers;
using FamsAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamsAPI.Test.Controllers
{
    /// <summary>
    /// This class setup initialization fake database before test cases was executed
    /// </summary>
    public class TestsFixture : IDisposable
    {
        public readonly UserController _userController;
        public readonly TrainingProgramController _trainingProgramController;
        protected readonly DataLayer.FAMSDBContext _context;

        public TestsFixture()
        {
            var options = new DbContextOptionsBuilder<DataLayer.FAMSDBContext>()
                .UseInMemoryDatabase(databaseName: "SearchByFillter")
                .Options;

            _context = new DataLayer.FAMSDBContext(options);

            // Add mock data to the db
            _context.Users.AddRange(new List<User>
        {
            new User {UserId = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"), Name = "SuperAdmin", Email = "sa1211@gmail.com", Phone = "0334416112"},
            new User {UserId = Guid.Parse("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"), Name = "Trainer1", Email = "trainer1122@email.com", Phone = "0352213411"},
            new User {UserId = Guid.Parse("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"), Name = "Admin", Email = "admin2222@yahoo.com", Phone = "0773622741"}
        });
            _context.TrainingPrograms.AddRange(new List<TrainingProgram>
        {
            new TrainingProgram
            {
                TrainingProgramCode = "TP001",
                Name = "Training Program 1",
                UserId = Guid.Parse("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"), // SuperAdmin
                StartTime = DateTime.Now,
                Duration = 5,
                CreateBy = "SuperAdmin",
                ModifyBy = "SuperAdmin",
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                Status = TrainingProgram.Statuses.Active
            },
            new TrainingProgram
            {
                TrainingProgramCode = "TP002",
                Name = "Training Program 2",
                UserId = Guid.Parse("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"), // Trainer1
                StartTime = DateTime.Now.AddDays(7),
                Duration = 10,
                CreateBy = "SuperAdmin",
                ModifyBy = "Trainer1",
                CreateDate =  DateTime.Parse("2023-12-21 00:00:00.0000000"),
                ModifyDate = DateTime.Now,
                Status = TrainingProgram.Statuses.Active
            },
            new TrainingProgram
            {
                TrainingProgramCode = "TP003",
                Name = "Training Program 3",
                UserId = Guid.Parse("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"), // Admin
                StartTime = DateTime.Now.AddDays(7),
                Duration = 7,
                CreateBy = "Admin",
                ModifyBy = "Admin",
                CreateDate = DateTime.Parse("2023-12-21 00:00:00.0000000"),
                ModifyDate = DateTime.Now,
                Status = TrainingProgram.Statuses.Active
            }
        });
            _context.SaveChanges();
            var userRepo = new UserRepository(_context);
            var userServices = new UserServices(userRepo);
            var trainingProgramRepo = new TrainingProgramRepository(_context);
            var syllabusRepo = new SyllabusRepository(_context);
            var traningProSylRepo = new TrainingProgramSyllabusRepository(_context);
            var classRepo = new ClassRepository(_context);
            var contentRepo= new TrainingContentRepository(_context);
            var trainingProgramService = new TrainingProgramService(trainingProgramRepo, syllabusRepo, traningProSylRepo, classRepo, contentRepo);
            _trainingProgramController = new TrainingProgramController(trainingProgramService);
            _userController = new UserController(userServices);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

}

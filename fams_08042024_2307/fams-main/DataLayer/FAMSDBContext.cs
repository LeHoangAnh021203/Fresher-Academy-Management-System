using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public partial class FAMSDBContext : DbContext
    {
        public FAMSDBContext() { }

        public FAMSDBContext(DbContextOptions<FAMSDBContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassUser> ClassUsers { get; set; }
        public virtual DbSet<LearningObjective> LearningObjectives { get; set; }
        public virtual DbSet<Syllabus> Syllabuses { get; set; }
        public virtual DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public virtual DbSet<TrainingProgramSyllabus> TrainingProgarmSyllabuses { get; set; }
        public virtual DbSet<TrainingContent> TrainingContents { get; set; }
        public virtual DbSet<SyllabusObjective> SyllabusObjectives { get; set; }
        public virtual DbSet<TrainingUnit> TrainingUnits { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Assessment> Assessments { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Fsu> Fsus { get; set; }
        public virtual DbSet<TrainingCalendar> TrainingCalendars { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.UserPermission)
                                       .WithMany(us => us.Users)
                                       .HasForeignKey(u => u.PermissionId)
                                       .HasConstraintName("FK_User_UserPermission_ID");

            modelBuilder.Entity<ClassUser>().HasKey(cu => new { cu.UserId, cu.ClassId });
            modelBuilder.Entity<ClassUser>().HasOne(cu => cu.User)
                                            .WithMany(u => u.ClassUsers)
                                            .HasForeignKey(cu => cu.UserId);

            modelBuilder.Entity<ClassUser>().HasOne(c => c.Class)
                                            .WithMany(cl => cl.ClassUsers)
                                            .HasForeignKey(fk => fk.ClassId);

            modelBuilder.Entity<TrainingProgramSyllabus>().HasKey(tps => new { tps.TopicCode, tps.TrainingProgramCode });
            modelBuilder.Entity<TrainingProgramSyllabus>().HasOne(c => c.TrainingProgram)
                                                          .WithMany(t => t.TrainingProgramSyllabuses)
                                                          .HasForeignKey(tps => tps.TrainingProgramCode);

            modelBuilder.Entity<TrainingProgramSyllabus>().HasOne(b => b.Syllabus)
                                                          .WithMany(t => t.TrainingProgramSyllabuses)
                                                          .HasForeignKey(tps => tps.TopicCode);

            modelBuilder.Entity<TrainingUnit>().HasKey(t => t.UnitCode);
            modelBuilder.Entity<TrainingUnit>().HasOne(t => t.Syllabus)
                                               .WithMany(ts => ts.TrainingUnits)
                                               .HasForeignKey(tps => tps.TopicCode);

            modelBuilder.Entity<TrainingContent>().HasKey(c => c.ContentId);
            modelBuilder.Entity<TrainingContent>().HasOne(cs => cs.TrainingUnit)
                                                  .WithMany(css => css.TrainingContents)
                                                  .HasForeignKey(cps => cps.UnitCode);

            modelBuilder.Entity<Syllabus>().HasKey(s => s.TopicCode);
            modelBuilder.Entity<Syllabus>().HasOne(sb => sb.User)
                                           .WithMany(sbs => sbs.Syllabuses)
                                           .HasForeignKey(sps => sps.UserId);

            modelBuilder.Entity<TrainingProgram>().HasKey(t => t.TrainingProgramCode);
            modelBuilder.Entity<TrainingProgram>().HasOne(ts => ts.User)
                                                  .WithMany(tss => tss.TrainingPrograms)
                                                  .HasForeignKey(fk => fk.UserId)
                                                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SyllabusObjective>().HasKey(so => new { so.TopicCode, so.ObjectiveCode });
            modelBuilder.Entity<SyllabusObjective>().HasOne(s => s.Syllabus)
                                                    .WithMany(so => so.SyllabusObjectives)
                                                    .HasForeignKey(s => s.TopicCode);
            modelBuilder.Entity<SyllabusObjective>().HasOne(lo => lo.LearningObjective)
                                                    .WithMany(so => so.SyllabusObjectives)
                                                    .HasForeignKey(lo => lo.ObjectiveCode);

            modelBuilder.Entity<LearningObjective>().HasMany(t=> t.TrainingContent)
                                                    .WithOne(lo => lo.LearningObjectives)
                                                    .HasForeignKey(fk => fk.Code);


            modelBuilder.Entity<Assessment>().HasKey(a => a.AssessmentID);
            modelBuilder.Entity<Assessment>().HasOne(a => a.Syllabus)
                                             .WithOne(s => s.Assessment)
                                             .HasForeignKey<Syllabus>(s => s.AssessmentID);

            modelBuilder.Entity<Location>().HasKey(l => l.LocationId);
            modelBuilder.Entity<Location>().HasMany(l => l.Classes)
                                           .WithOne(c => c.GetLocation)
                                           .HasForeignKey(fk => fk.LocationId);

            modelBuilder.Entity<Fsu>().HasKey(f => f.FsuId);
            modelBuilder.Entity<Fsu>().HasMany(f => f.ClassFsuses)
                                      .WithOne(c => c.GetFsu)
                                      .HasForeignKey(fk => fk.FsuId);
            modelBuilder.Entity<Class>().HasMany(t => t.TrainingCalendars)
                                        .WithOne(c => c.Class)
                                        .HasForeignKey(fk => fk.ClassId);
               

            #region Data Seeding
            modelBuilder.Entity<UserPermission>().HasData(new UserPermission()
            {
                PermissionId = 1,
                PermissionName = "Super Admin",
                Syllabus = Permissions.FullAccess,
                LearningMaterial = Permissions.FullAccess,
                TrainingProgram = Permissions.FullAccess,
                Class = Permissions.FullAccess,
                UserManagement = Permissions.FullAccess,
                Version = 0
            });
            modelBuilder.Entity<UserPermission>().HasData(new UserPermission()
            {
                PermissionId = 2,
                PermissionName = "Admin",
                Syllabus = Permissions.FullAccess,
                LearningMaterial = Permissions.FullAccess,
                TrainingProgram = Permissions.FullAccess,
                Class = Permissions.FullAccess,
                UserManagement = Permissions.AccessDenied,
                Version = 0
            });
            modelBuilder.Entity<UserPermission>().HasData(new UserPermission()
            {
                PermissionId = 3,
                PermissionName = "Trainer",
                Syllabus = Permissions.Modify,
                LearningMaterial = Permissions.Modify,
                TrainingProgram = Permissions.View,
                Class = Permissions.View,
                UserManagement = Permissions.AccessDenied,
                Version = 0
            });

            modelBuilder.Entity<User>().HasData(new User()
            {
                UserId = Guid.NewGuid(),
                Name = "Super Admin",
                Email = "superadmin@gmail.com",
                Password = "123@",
                Phone = "0987788762",
                DOB = new DateTime(2000, 2, 27),
                Gender = User.Genders.Male,
                Status = User.UserStatus.Active,
                CreateBy = null,
                CreateDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedTime = DateTime.Now,
                PermissionId = 1
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                UserId = Guid.NewGuid(),
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = "123@",
                Phone = "0955422567",
                DOB = new DateTime(2000, 1, 10),
                Gender = User.Genders.Female,
                Status = User.UserStatus.Active,
                CreateBy = null,
                CreateDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedTime = DateTime.Now,
                PermissionId = 2
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                UserId = Guid.NewGuid(),
                Name = "Trainer1",
                Email = "trainer1@gmail.com",
                Password = "123@",
                Phone = "0876588765",
                DOB = new DateTime(2003, 2, 1),
                Gender = User.Genders.Male,
                Status = User.UserStatus.Active,
                CreateBy = null,
                CreateDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedTime = DateTime.Now,
                PermissionId = 3
            });
            modelBuilder.Entity<Location>().HasData(new Location()
            {
                LocationId = "L001",
                LocationName = "FTown 1"
            });
            modelBuilder.Entity<Location>().HasData(new Location()
            {
                LocationId = "L002",
                LocationName = "FTown 2"
            });
            modelBuilder.Entity<Location>().HasData(new Location()
            {
                LocationId = "L003",
                LocationName = "FTown 3"
            });
            modelBuilder.Entity<Location>().HasData(new Location()
            {
                LocationId = "L004",
                LocationName = "FTown 4"
            });
            modelBuilder.Entity<Fsu>().HasData(new Fsu()
            {
                FsuId = "F001",
                FsuName = "FHM"

            });
            modelBuilder.Entity<Fsu>().HasData(new Fsu()
            {
                FsuId = "F002",
                FsuName = "FDM"

            });
            modelBuilder.Entity<Fsu>().HasData(new Fsu()
            {
                FsuId = "F003",
                FsuName = "FSE"

            });
            modelBuilder.Entity<Fsu>().HasData(new Fsu()
            {
                FsuId = "F004",
                FsuName = "FWB"

            });
            modelBuilder.Entity<Fsu>().HasData(new Fsu()
            {
                FsuId = "F005",
                FsuName = "FWA"
            });
            #endregion
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=(local);Database=FAMS;User Id=sa;Password=24112003;Trusted_Connection=False;TrustServerCertificate=True");
        //    }
        //}
    }
}
﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(FAMSDBContext))]
    [Migration("20240306065803_AddData_LocationandFsu_06032024")]
    partial class AddDataLocationandFsu06032024
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataLayer.Entities.Assessment", b =>
                {
                    b.Property<string>("AssessmentID")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("AssignmentCount")
                        .HasColumnType("int");

                    b.Property<double>("AssignmentPercent")
                        .HasColumnType("float");

                    b.Property<double>("FinalPracticePercent")
                        .HasColumnType("float");

                    b.Property<double>("FinalTheoryPercent")
                        .HasColumnType("float");

                    b.Property<int>("QuizCount")
                        .HasColumnType("int");

                    b.Property<double>("QuizPercent")
                        .HasColumnType("float");

                    b.HasKey("AssessmentID");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("DataLayer.Entities.Class", b =>
                {
                    b.Property<string>("ClassID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ClassCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FsuId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("FSU");

                    b.Property<string>("LocationId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Location");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TrainingProgramCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ClassID");

                    b.HasIndex("FsuId");

                    b.HasIndex("LocationId");

                    b.HasIndex("TrainingProgramCode")
                        .IsUnique();

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("DataLayer.Entities.ClassUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClassId")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "ClassId");

                    b.HasIndex("ClassId");

                    b.ToTable("ClassUsers");
                });

            modelBuilder.Entity("DataLayer.Entities.Fsu", b =>
                {
                    b.Property<string>("FsuId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("FsuName")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("FsuId");

                    b.ToTable("Fsus");

                    b.HasData(
                        new
                        {
                            FsuId = "F001",
                            FsuName = "FHM"
                        },
                        new
                        {
                            FsuId = "F002",
                            FsuName = "FDM"
                        },
                        new
                        {
                            FsuId = "F003",
                            FsuName = "FSE"
                        },
                        new
                        {
                            FsuId = "F004",
                            FsuName = "FWB"
                        },
                        new
                        {
                            FsuId = "F005",
                            FsuName = "FWA"
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.LearningObjective", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Code");

                    b.HasIndex("Name");

                    b.ToTable("LearningObjectives");
                });

            modelBuilder.Entity("DataLayer.Entities.Location", b =>
                {
                    b.Property<string>("LocationId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationId = "L001",
                            LocationName = "FTown 1"
                        },
                        new
                        {
                            LocationId = "L002",
                            LocationName = "FTown 2"
                        },
                        new
                        {
                            LocationId = "L003",
                            LocationName = "FTown 3"
                        },
                        new
                        {
                            LocationId = "L004",
                            LocationName = "FTown 4"
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RefreshTokenString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("DataLayer.Entities.Syllabus", b =>
                {
                    b.Property<string>("TopicCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("AssessmentID")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("CourseObjective")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("PulishStatus")
                        .HasColumnType("int");

                    b.Property<string>("TechnicalGroup")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TechnicalRequirement")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TopicOutline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainingAudience")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrainingMaterials")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainingPrinciple")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("TopicCode");

                    b.HasIndex("AssessmentID")
                        .IsUnique()
                        .HasFilter("[AssessmentID] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Syllabuses");
                });

            modelBuilder.Entity("DataLayer.Entities.SyllabusObjective", b =>
                {
                    b.Property<string>("TopicCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ObjectiveCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("TopicCode", "ObjectiveCode");

                    b.HasIndex("ObjectiveCode");

                    b.ToTable("SyllabusObjectives");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingContent", b =>
                {
                    b.Property<string>("ContentId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Content")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("ContentName");

                    b.Property<string>("DeliveryType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrainingFormat")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UnitCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ContentId");

                    b.HasIndex("UnitCode");

                    b.ToTable("TrainingContents");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingProgram", b =>
                {
                    b.Property<string>("TrainingProgramCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TrainingProgramCode");

                    b.HasIndex("UserId");

                    b.ToTable("TrainingPrograms");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingProgramSyllabus", b =>
                {
                    b.Property<string>("TopicCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TrainingProgramCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("TopicCode", "TrainingProgramCode");

                    b.HasIndex("TrainingProgramCode");

                    b.ToTable("TrainingProgarmSyllabuses");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingUnit", b =>
                {
                    b.Property<string>("UnitCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("DayNumber")
                        .HasColumnType("int");

                    b.Property<string>("TopicCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UnitCode");

                    b.HasIndex("TopicCode");

                    b.ToTable("TrainingUnits");
                });

            modelBuilder.Entity("DataLayer.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("PermissionId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("8046fe60-98e4-44c5-81d2-d386c6eed693"),
                            CreateDate = new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7353),
                            DOB = new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "superadmin@gmail.com",
                            Gender = 1,
                            ModifiedTime = new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7361),
                            Name = "Super Admin",
                            Password = "123@",
                            PermissionId = 1,
                            Phone = "0987788762",
                            Status = 0
                        },
                        new
                        {
                            UserId = new Guid("e7aa6c31-d5f0-4c16-89bf-c301de749311"),
                            CreateDate = new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7375),
                            DOB = new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            Gender = 2,
                            ModifiedTime = new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7376),
                            Name = "Admin",
                            Password = "123@",
                            PermissionId = 2,
                            Phone = "0955422567",
                            Status = 0
                        },
                        new
                        {
                            UserId = new Guid("326ffd0c-33a6-447f-9e87-0b111c97684e"),
                            CreateDate = new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7385),
                            DOB = new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "trainer1@gmail.com",
                            Gender = 1,
                            ModifiedTime = new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7385),
                            Name = "Trainer1",
                            Password = "123@",
                            PermissionId = 3,
                            Phone = "0876588765",
                            Status = 0
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.UserPermission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"));

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("LearningMaterial")
                        .HasColumnType("int");

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Syllabus")
                        .HasColumnType("int");

                    b.Property<int>("TrainingProgram")
                        .HasColumnType("int");

                    b.Property<int>("UserManagement")
                        .HasColumnType("int");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("PermissionId");

                    b.ToTable("UserPermissions");

                    b.HasData(
                        new
                        {
                            PermissionId = 1,
                            Class = 4,
                            LearningMaterial = 4,
                            PermissionName = "Super Admin",
                            Syllabus = 4,
                            TrainingProgram = 4,
                            UserManagement = 4,
                            Version = 0
                        },
                        new
                        {
                            PermissionId = 2,
                            Class = 4,
                            LearningMaterial = 4,
                            PermissionName = "Admin",
                            Syllabus = 4,
                            TrainingProgram = 4,
                            UserManagement = 0,
                            Version = 0
                        },
                        new
                        {
                            PermissionId = 3,
                            Class = 1,
                            LearningMaterial = 2,
                            PermissionName = "Trainer",
                            Syllabus = 2,
                            TrainingProgram = 1,
                            UserManagement = 0,
                            Version = 0
                        });
                });

            modelBuilder.Entity("DataLayer.Entities.Class", b =>
                {
                    b.HasOne("DataLayer.Entities.Fsu", "GetFsu")
                        .WithMany("ClassFsuses")
                        .HasForeignKey("FsuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.Location", "GetLocation")
                        .WithMany("Classes")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.TrainingProgram", "TrainingProgram")
                        .WithOne("Class")
                        .HasForeignKey("DataLayer.Entities.Class", "TrainingProgramCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GetFsu");

                    b.Navigation("GetLocation");

                    b.Navigation("TrainingProgram");
                });

            modelBuilder.Entity("DataLayer.Entities.ClassUser", b =>
                {
                    b.HasOne("DataLayer.Entities.Class", "Class")
                        .WithMany("ClassUsers")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.User", "User")
                        .WithMany("ClassUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Entities.LearningObjective", b =>
                {
                    b.HasOne("DataLayer.Entities.TrainingContent", "TrainingContent")
                        .WithMany("LearningObjectives")
                        .HasForeignKey("Name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingContent");
                });

            modelBuilder.Entity("DataLayer.Entities.Syllabus", b =>
                {
                    b.HasOne("DataLayer.Entities.Assessment", "Assessment")
                        .WithOne("Syllabus")
                        .HasForeignKey("DataLayer.Entities.Syllabus", "AssessmentID");

                    b.HasOne("DataLayer.Entities.User", "User")
                        .WithMany("Syllabuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assessment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Entities.SyllabusObjective", b =>
                {
                    b.HasOne("DataLayer.Entities.LearningObjective", "LearningObjective")
                        .WithMany("SyllabusObjectives")
                        .HasForeignKey("ObjectiveCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.Syllabus", "Syllabus")
                        .WithMany("SyllabusObjectives")
                        .HasForeignKey("TopicCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LearningObjective");

                    b.Navigation("Syllabus");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingContent", b =>
                {
                    b.HasOne("DataLayer.Entities.TrainingUnit", "TrainingUnit")
                        .WithMany("TrainingContents")
                        .HasForeignKey("UnitCode");

                    b.Navigation("TrainingUnit");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingProgram", b =>
                {
                    b.HasOne("DataLayer.Entities.User", "User")
                        .WithMany("TrainingPrograms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingProgramSyllabus", b =>
                {
                    b.HasOne("DataLayer.Entities.Syllabus", "Syllabus")
                        .WithMany("TrainingProgramSyllabuses")
                        .HasForeignKey("TopicCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.TrainingProgram", "TrainingProgram")
                        .WithMany("TrainingProgramSyllabuses")
                        .HasForeignKey("TrainingProgramCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Syllabus");

                    b.Navigation("TrainingProgram");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingUnit", b =>
                {
                    b.HasOne("DataLayer.Entities.Syllabus", "Syllabus")
                        .WithMany("TrainingUnits")
                        .HasForeignKey("TopicCode");

                    b.Navigation("Syllabus");
                });

            modelBuilder.Entity("DataLayer.Entities.User", b =>
                {
                    b.HasOne("DataLayer.Entities.UserPermission", "UserPermission")
                        .WithMany("Users")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_UserPermission_ID");

                    b.Navigation("UserPermission");
                });

            modelBuilder.Entity("DataLayer.Entities.Assessment", b =>
                {
                    b.Navigation("Syllabus");
                });

            modelBuilder.Entity("DataLayer.Entities.Class", b =>
                {
                    b.Navigation("ClassUsers");
                });

            modelBuilder.Entity("DataLayer.Entities.Fsu", b =>
                {
                    b.Navigation("ClassFsuses");
                });

            modelBuilder.Entity("DataLayer.Entities.LearningObjective", b =>
                {
                    b.Navigation("SyllabusObjectives");
                });

            modelBuilder.Entity("DataLayer.Entities.Location", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("DataLayer.Entities.Syllabus", b =>
                {
                    b.Navigation("SyllabusObjectives");

                    b.Navigation("TrainingProgramSyllabuses");

                    b.Navigation("TrainingUnits");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingContent", b =>
                {
                    b.Navigation("LearningObjectives");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingProgram", b =>
                {
                    b.Navigation("Class");

                    b.Navigation("TrainingProgramSyllabuses");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingUnit", b =>
                {
                    b.Navigation("TrainingContents");
                });

            modelBuilder.Entity("DataLayer.Entities.User", b =>
                {
                    b.Navigation("ClassUsers");

                    b.Navigation("Syllabuses");

                    b.Navigation("TrainingPrograms");
                });

            modelBuilder.Entity("DataLayer.Entities.UserPermission", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

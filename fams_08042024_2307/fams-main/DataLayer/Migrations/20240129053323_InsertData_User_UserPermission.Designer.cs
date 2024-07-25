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
    [Migration("20240129053323_InsertData_User_UserPermission")]
    partial class InsertDataUserUserPermission
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<string>("FSU")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

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
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Code");

                    b.ToTable("LearningObjectives");
                });

            modelBuilder.Entity("DataLayer.Entities.RefreshToken", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

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

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

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

                    b.HasIndex("UserId");

                    b.ToTable("Syllabuses");
                });

            modelBuilder.Entity("DataLayer.Entities.SyllabusObjective", b =>
                {
                    b.Property<string>("LearningObjectiveCode")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ObjectiveCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SyllabusTopicCode")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TopicCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasIndex("LearningObjectiveCode");

                    b.HasIndex("SyllabusTopicCode");

                    b.ToTable("SyllabusObjectives");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingContent", b =>
                {
                    b.Property<string>("Content")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("DeliveryType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrainingFormat")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UnitCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Content");

                    b.HasIndex("UnitCode");

                    b.ToTable("TrainingContents");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingProgram", b =>
                {
                    b.Property<string>("TrainingProgramCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

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
                        .IsRequired()
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
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("PermissionId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"),
                            CreateDate = new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4910),
                            DOB = new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "superadmin@gmail.com",
                            Gender = 1,
                            ModifiedTime = new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4918),
                            Name = "Super Admin",
                            Password = "123@",
                            PermissionId = 1,
                            Phone = "0987788762",
                            Status = 0
                        },
                        new
                        {
                            UserId = new Guid("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"),
                            CreateDate = new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4969),
                            DOB = new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@gmail.com",
                            Gender = 2,
                            ModifiedTime = new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4969),
                            Name = "Admin",
                            Password = "123@",
                            PermissionId = 2,
                            Phone = "0955422567",
                            Status = 0
                        },
                        new
                        {
                            UserId = new Guid("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"),
                            CreateDate = new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4981),
                            DOB = new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "trainer1@gmail.com",
                            Gender = 1,
                            ModifiedTime = new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4981),
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
                    b.HasOne("DataLayer.Entities.TrainingProgram", "TrainingProgram")
                        .WithOne("Class")
                        .HasForeignKey("DataLayer.Entities.Class", "TrainingProgramCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                        .HasForeignKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingContent");
                });

            modelBuilder.Entity("DataLayer.Entities.Syllabus", b =>
                {
                    b.HasOne("DataLayer.Entities.User", "User")
                        .WithMany("Syllabuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Entities.SyllabusObjective", b =>
                {
                    b.HasOne("DataLayer.Entities.LearningObjective", "LearningObjective")
                        .WithMany()
                        .HasForeignKey("LearningObjectiveCode");

                    b.HasOne("DataLayer.Entities.Syllabus", "Syllabus")
                        .WithMany()
                        .HasForeignKey("SyllabusTopicCode");

                    b.Navigation("LearningObjective");

                    b.Navigation("Syllabus");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingContent", b =>
                {
                    b.HasOne("DataLayer.Entities.TrainingUnit", "TrainingUnit")
                        .WithMany("TrainingContents")
                        .HasForeignKey("UnitCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                    b.HasOne("DataLayer.Entities.Syllabus", null)
                        .WithMany("TrainingProgramSyllabuses")
                        .HasForeignKey("TopicCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Entities.TrainingProgram", "TrainingProgram")
                        .WithMany("TrainingProgramSyllabuses")
                        .HasForeignKey("TopicCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainingProgram");
                });

            modelBuilder.Entity("DataLayer.Entities.TrainingUnit", b =>
                {
                    b.HasOne("DataLayer.Entities.Syllabus", "Syllabus")
                        .WithMany("TrainingUnits")
                        .HasForeignKey("TopicCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("DataLayer.Entities.Class", b =>
                {
                    b.Navigation("ClassUsers");
                });

            modelBuilder.Entity("DataLayer.Entities.Syllabus", b =>
                {
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

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentFileShare6.data;

#nullable disable

namespace StudentFileShare6.Migrations
{
    [DbContext(typeof(UniversityContext))]
    [Migration("20230829085511_nullable UserSavedInfo")]
    partial class nullableUserSavedInfo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StudentFileShare6.Models.Course", b =>
                {
                    b.Property<string>("CourseID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SchoolID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UniversitiesSchoolID")
                        .HasColumnType("varchar(128)");

                    b.HasKey("CourseID");

                    b.HasIndex("UniversitiesSchoolID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("StudentFileShare6.Models.Document", b =>
                {
                    b.Property<string>("DocumentID")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Anonymous")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("CourseID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("DislikeNumber")
                        .HasColumnType("int");

                    b.Property<string>("FirstPageImageLink")
                        .HasColumnType("longtext");

                    b.Property<int?>("LikeNumber")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float?>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("SchoolID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UniversitySchoolID")
                        .HasColumnType("varchar(128)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("DocumentID");

                    b.HasIndex("CourseID");

                    b.HasIndex("UniversitySchoolID");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("StudentFileShare6.Models.UserSavedInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CourseID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CourseName")
                        .HasColumnType("longtext");

                    b.Property<string>("DocumentID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DocumentName")
                        .HasColumnType("longtext");

                    b.Property<string>("SchoolID")
                        .HasColumnType("longtext");

                    b.Property<string>("SchoolName")
                        .HasColumnType("longtext");

                    b.Property<string>("UniversitySchoolID")
                        .HasColumnType("varchar(128)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CourseID");

                    b.HasIndex("DocumentID");

                    b.HasIndex("UniversitySchoolID");

                    b.ToTable("UserSavedInfo");
                });

            modelBuilder.Entity("University", b =>
                {
                    b.Property<string>("SchoolID")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("SchoolID");

                    b.ToTable("universities");
                });

            modelBuilder.Entity("StudentFileShare6.Models.Course", b =>
                {
                    b.HasOne("University", "Universities")
                        .WithMany("Courses")
                        .HasForeignKey("UniversitiesSchoolID");

                    b.Navigation("Universities");
                });

            modelBuilder.Entity("StudentFileShare6.Models.Document", b =>
                {
                    b.HasOne("StudentFileShare6.Models.Course", "Course")
                        .WithMany("Documents")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University", "University")
                        .WithMany("Documents")
                        .HasForeignKey("UniversitySchoolID");

                    b.Navigation("Course");

                    b.Navigation("University");
                });

            modelBuilder.Entity("StudentFileShare6.Models.UserSavedInfo", b =>
                {
                    b.HasOne("StudentFileShare6.Models.Course", "Course")
                        .WithMany("UserSavedInfos")
                        .HasForeignKey("CourseID");

                    b.HasOne("StudentFileShare6.Models.Document", "Document")
                        .WithMany("UserSavedInfos")
                        .HasForeignKey("DocumentID");

                    b.HasOne("University", "University")
                        .WithMany("UserSavedInfos")
                        .HasForeignKey("UniversitySchoolID");

                    b.Navigation("Course");

                    b.Navigation("Document");

                    b.Navigation("University");
                });

            modelBuilder.Entity("StudentFileShare6.Models.Course", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("UserSavedInfos");
                });

            modelBuilder.Entity("StudentFileShare6.Models.Document", b =>
                {
                    b.Navigation("UserSavedInfos");
                });

            modelBuilder.Entity("University", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Documents");

                    b.Navigation("UserSavedInfos");
                });
#pragma warning restore 612, 618
        }
    }
}

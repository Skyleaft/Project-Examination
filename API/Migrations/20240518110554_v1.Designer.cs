﻿// <auto-generated />
using System;
using System.Collections.Generic;
using API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(GenericRepository))]
    [Migration("20240518110554_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.RoomSet.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsRandomize")
                        .HasColumnType("boolean");

                    b.Property<int>("TotalSoal")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("Domain.RoomSet.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ExamId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Jadwal")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Kode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("Domain.RoomSet.Soal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExamId")
                        .HasColumnType("integer");

                    b.Property<string>("KunciJawaban")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Pertanyaan")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Pilihan")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Soal");
                });

            modelBuilder.Entity("Domain.TakeExam.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SoalId")
                        .HasColumnType("integer");

                    b.Property<int>("UserExamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SoalId");

                    b.HasIndex("UserExamId");

                    b.ToTable("UserAnswer");
                });

            modelBuilder.Entity("Domain.TakeExam.UserExam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsOngoing")
                        .HasColumnType("boolean");

                    b.Property<int?>("RoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("UserProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserExam");
                });

            modelBuilder.Entity("Domain.User.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Level = 1,
                            Nama = "SuperUser"
                        },
                        new
                        {
                            Id = 2,
                            Level = 2,
                            Nama = "Operator"
                        },
                        new
                        {
                            Id = 3,
                            Level = 3,
                            Nama = "Dosen"
                        },
                        new
                        {
                            Id = 4,
                            Level = 4,
                            Nama = "User"
                        });
                });

            modelBuilder.Entity("Domain.User.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Device")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("IPAddress")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserAccount");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Password = "@superuser",
                            Username = "sysadmin"
                        });
                });

            modelBuilder.Entity("Domain.User.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NamaLengkap")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NoTelp")
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .HasColumnType("text");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TangalLahir")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("UserProfile");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NamaLengkap = "Super User",
                            RoleId = 1,
                            UserAccountId = 1
                        });
                });

            modelBuilder.Entity("Domain.RoomSet.Room", b =>
                {
                    b.HasOne("Domain.RoomSet.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId");

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("Domain.RoomSet.Soal", b =>
                {
                    b.HasOne("Domain.RoomSet.Exam", null)
                        .WithMany("Soals")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.TakeExam.UserAnswer", b =>
                {
                    b.HasOne("Domain.RoomSet.Soal", "Soal")
                        .WithMany()
                        .HasForeignKey("SoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.TakeExam.UserExam", null)
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Soal");
                });

            modelBuilder.Entity("Domain.TakeExam.UserExam", b =>
                {
                    b.HasOne("Domain.RoomSet.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("Domain.User.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId");

                    b.Navigation("Room");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Domain.User.UserProfile", b =>
                {
                    b.HasOne("Domain.User.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Domain.User.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Domain.RoomSet.Exam", b =>
                {
                    b.Navigation("Soals");
                });

            modelBuilder.Entity("Domain.TakeExam.UserExam", b =>
                {
                    b.Navigation("UserAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Web.Common.Database;

#nullable disable

namespace Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoreLib.BankSoal.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRandomize")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Thumbnail")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("CoreLib.BankSoal.Soal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ExamId")
                        .HasColumnType("integer");

                    b.Property<int>("Nomor")
                        .HasColumnType("integer");

                    b.Property<string>("Pertanyaan")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isMultipleJawaban")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Soal");
                });

            modelBuilder.Entity("CoreLib.BankSoal.SoalJawaban", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsBenar")
                        .HasColumnType("boolean");

                    b.Property<string>("Jawaban")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Point")
                        .HasColumnType("integer");

                    b.Property<Guid>("SoalId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SoalId");

                    b.ToTable("SoalJawaban");
                });

            modelBuilder.Entity("CoreLib.Report.ExamReport", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AsalKota")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Jadwal")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MaxScore")
                        .HasColumnType("integer");

                    b.Property<string>("NamaLengkap")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NamaRoom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Nilai")
                        .HasColumnType("double precision")
                        .HasColumnName("nilai");

                    b.Property<string>("NomorTelepon")
                        .HasColumnType("text");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.ToView("vw_examreport", (string)null);
                });

            modelBuilder.Entity("CoreLib.RoomSet.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Durasi")
                        .HasColumnType("integer");

                    b.Property<int>("ExamId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRepeatable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsShowKunci")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("JadwalEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("JadwalStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Kode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("RetryCount")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Thumbnail")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("CoreLib.TakeExam.UserAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SoalId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SoalJawabanId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserExamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SoalId");

                    b.HasIndex("SoalJawabanId");

                    b.HasIndex("UserExamId");

                    b.ToTable("UserAnswer");
                });

            modelBuilder.Entity("CoreLib.TakeExam.UserExam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<double>>("HistoryScoreNormalize")
                        .HasColumnType("double precision[]");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOngoing")
                        .HasColumnType("boolean");

                    b.Property<int?>("RetryCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<int?>("ScoreData")
                        .HasColumnType("integer");

                    b.Property<double?>("ScoreNormalizeData")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("TimeLeft")
                        .HasColumnType("interval");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("UserExam");
                });

            modelBuilder.Entity("CoreLib.Users.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("KotaId")
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NamaLengkap")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Pekerjaan")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("KotaId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CoreLib.Users.Kota", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NamaKota")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProvinsiId")
                        .IsRequired()
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProvinsiId");

                    b.ToTable("Kota", "reference");
                });

            modelBuilder.Entity("CoreLib.Users.Provinsi", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NamaProvinsi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Provinsi", "reference");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CoreLib.BankSoal.Soal", b =>
                {
                    b.HasOne("CoreLib.BankSoal.Exam", null)
                        .WithMany("Soals")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreLib.BankSoal.SoalJawaban", b =>
                {
                    b.HasOne("CoreLib.BankSoal.Soal", null)
                        .WithMany("PilihanJawaban")
                        .HasForeignKey("SoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreLib.RoomSet.Room", b =>
                {
                    b.HasOne("CoreLib.BankSoal.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("CoreLib.TakeExam.UserAnswer", b =>
                {
                    b.HasOne("CoreLib.BankSoal.Soal", "Soal")
                        .WithMany()
                        .HasForeignKey("SoalId");

                    b.HasOne("CoreLib.BankSoal.SoalJawaban", "SoalJawaban")
                        .WithMany()
                        .HasForeignKey("SoalJawabanId");

                    b.HasOne("CoreLib.TakeExam.UserExam", null)
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Soal");

                    b.Navigation("SoalJawaban");
                });

            modelBuilder.Entity("CoreLib.TakeExam.UserExam", b =>
                {
                    b.HasOne("CoreLib.RoomSet.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreLib.Users.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoreLib.Users.ApplicationUser", b =>
                {
                    b.HasOne("CoreLib.Users.Kota", "Kota")
                        .WithMany()
                        .HasForeignKey("KotaId");

                    b.Navigation("Kota");
                });

            modelBuilder.Entity("CoreLib.Users.Kota", b =>
                {
                    b.HasOne("CoreLib.Users.Provinsi", "Provinsi")
                        .WithMany()
                        .HasForeignKey("ProvinsiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provinsi");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CoreLib.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CoreLib.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreLib.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CoreLib.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreLib.BankSoal.Exam", b =>
                {
                    b.Navigation("Soals");
                });

            modelBuilder.Entity("CoreLib.BankSoal.Soal", b =>
                {
                    b.Navigation("PilihanJawaban");
                });

            modelBuilder.Entity("CoreLib.TakeExam.UserExam", b =>
                {
                    b.Navigation("UserAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}

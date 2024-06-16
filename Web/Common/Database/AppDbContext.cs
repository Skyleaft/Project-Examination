using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.BankSoal;
using Shared.RoomSet;
using Shared.TakeExam;
using Shared.Users;

namespace Web.Common.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Soal> Soal { get; set; }
    public DbSet<SoalJawaban> SoalJawaban { get; set; }
    public DbSet<Exam> Exam { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<UserExam> UserExam { get; set; }
    public DbSet<UserAnswer> UserAnswer { get; set; }
    public DbSet<Provinsi> Provinsi { get; set; }
    public DbSet<Kota> Kota { get; set; }
}
using Domain.RoomSet;
using Domain.TakeExam;
using Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Common.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Soal> Soal { get; set; }
    public DbSet<Exam> Exam { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<UserExam> UserExam { get; set; }
    public DbSet<UserAnswer> UserAnswer { get; set; }
}
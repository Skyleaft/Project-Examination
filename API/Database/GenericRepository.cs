using Domain.RoomSet;
using Domain.TakeExam;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace API.Database;

public class GenericRepository : DbContext
{
    protected readonly IConfiguration Configuration;

    public GenericRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //SQL Server Connection
        //options.UseSqlServer(Configuration.GetConnectionString("DefaultSQLCON"));
        //PGSQL Connection
        options.UseNpgsql(Configuration.GetConnectionString("mzserver"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Pekerjaan>().HasData(GeneratePekerjaan.Set());
        //modelBuilder.Entity<Alamat>()
        //    .HasOne(e => e.Kelurahan)
        //    .WithMany()
        //    .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Role>().HasData(
            new Role() { Id = 1, Level = 1, Nama = "SuperUser" },
            new Role() { Id = 2, Level = 2, Nama = "Operator" },
            new Role() { Id = 3, Level = 3, Nama = "Dosen" },
            new Role() { Id = 4, Level = 4, Nama = "User" }
        );
        modelBuilder.Entity<UserAccount>().HasData(
            new UserAccount()
            {
                Id = 1,
                Username = "sysadmin",
                Password = "@superuser",
                IsActive = true
            }
        );
        modelBuilder.Entity<UserProfile>().HasData(
            new UserProfile()
            {
                Id = 1,
                NamaLengkap = "Super User",
                UserAccountId = 1,
                RoleId = 1
            }
        );
    }
    public DbSet<User> User { get; set; }
    public DbSet<UserAccount> UserAccount { get; set; }
    public DbSet<UserProfile> UserProfile { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Soal> Soal { get; set; }
    public DbSet<Exam> Exam { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<UserExam> UserExam { get; set; }
    public DbSet<UserAnswer> UserAnswer { get; set; }
}
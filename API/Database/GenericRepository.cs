using Domain.User;
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
        options.UseNpgsql(Configuration.GetConnectionString("myLocal"));

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Pekerjaan>().HasData(GeneratePekerjaan.Set());
        //modelBuilder.Entity<Alamat>()
        //    .HasOne(e => e.Kelurahan)
        //    .WithMany()
        //    .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Role> Roles { get; set; }

}

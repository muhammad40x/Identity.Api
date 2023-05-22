using Identity.Api.Entity;
using Microsoft.EntityFrameworkCore;
namespace Identity.Api.Context;

public class AppDbContext :DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Userlarbas");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.UserName)
            .IsRequired();

            entity.HasIndex(e=>e.UserName)
            .IsUnique();

            entity.Property(a => a.Email)
            .IsRequired();
        });
    }

}

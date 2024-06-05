namespace SkillsInternationalServer.Data;

using Microsoft.EntityFrameworkCore;
using SkillsInternationalServer.Models;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<User> Users { get; set; } // Assuming you want to keep 'Users'

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Contact)
            .WithOne()
            .HasForeignKey<Student>(s => s.ContactId)
            .OnDelete(DeleteBehavior.Cascade); // Add this

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Parent)
            .WithOne()
            .HasForeignKey<Student>(s => s.ParentId)
            .OnDelete(DeleteBehavior.Cascade); // Add this

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1, 
                Username = "Admin",
                Password = BCrypt.Net.BCrypt.HashPassword("Skills@123")
            }
        );


    }
}

using Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Infrastructure.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Contact>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Contact>()
            .Property(p => p.ModifiedDate)
            .IsRequired(false);

        modelBuilder.Entity<Contact>()
            .Property(p => p.Title)
            .IsRequired(false);

    }
}

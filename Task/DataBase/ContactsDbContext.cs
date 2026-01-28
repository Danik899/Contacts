using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.DataBase;

public class ContactsDbContext: DbContext
{
    public DbSet<Contacts> Contacts { get; set; }
    
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contacts>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired();

            entity.Property(x => x.MobilePhone);

            entity.Property(x => x.JobTitle);

            entity.Property(x => x.BirthDate)
                .HasColumnType("date");
        });
    }
}
using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.DataBase;

public class ContactsDbContext: DbContext
{
    public DbSet<Contacts> Contacts { get; set; }
    
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=contactsdb;Username=appuser;Password=secret");
        }
    }
}
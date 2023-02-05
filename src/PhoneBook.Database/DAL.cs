using Microsoft.EntityFrameworkCore;
using PhoneBook.Database.Models;

namespace PhoneBook.Database;

public class DAL : DbContext
{
    public DAL(DbContextOptions<DAL> options) : base(options)
    {
    }

    public DAL()
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public void AttachModified(object entity)
    {
        Entry(entity).State = EntityState.Modified;
    }

    public void Detach(object entity)
    {
        Entry(entity).State = EntityState.Detached;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).UseIdentityColumn();
        });
    }
}
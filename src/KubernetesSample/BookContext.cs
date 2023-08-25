namespace DotnetSQLServer.Blueprint;

using System.Security.Policy;

using Microsoft.EntityFrameworkCore;

public class BookContext : DbContext
{
    public DbSet<Book> Book { get; set; }

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.ISBN);
            entity.Property(e => e.Title).IsRequired();
        });
    }
    
}
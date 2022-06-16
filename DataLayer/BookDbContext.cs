using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class BookDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;

    public BookDbContext()
    {
        
    }
    
    public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options)
    {

    }
}
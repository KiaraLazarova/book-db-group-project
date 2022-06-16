using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public interface IBookRepository
{
    public Book? GetById(int id);
    public IEnumerable<Book> GetAll();

    public void Create(Book book);
    public void Update(Book book);
    public void Delete(int id);
}

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;

    public BookRepository(BookDbContext context)
    {
        _context = context;
    }

    public Book? GetById(int id)
    {
        return _context.Books
            .Include(b => b.Author)
            .SingleOrDefault(b => b.Id == id);
    }

    public IEnumerable<Book> GetAll()
    {
        return _context.Books
            .Include(b => b.Author)
            .ToList();
    }

    public void Create(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void Update(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var book = GetById(id);
        _context.Books.Remove(book!);
        _context.SaveChanges();
    }
}
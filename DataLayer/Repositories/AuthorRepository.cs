using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public interface IAuthorRepository
{
    public Author? GetById(int id);
    public IEnumerable<Author> GetAll();

    public void Create(Author author);
    public void Update(Author author);
    public void Delete(int id);
}

public class AuthorRepository : IAuthorRepository
{
    private readonly BookDbContext _context;

    public AuthorRepository(BookDbContext context)
    {
        _context = context;
    }

    public Author? GetById(int id)
    {
        return _context.Authors
            .Include(a => a.Country)
            .SingleOrDefault(a => a.Id == id);
    }

    public IEnumerable<Author> GetAll()
    {
        return _context.Authors
            .Include(a => a.Country)
            .ToList();
    }

    public void Create(Author author)
    {
        _context.Authors.Add(author);
        _context.SaveChanges();
    }

    public void Update(Author author)
    {
        _context.Authors.Update(author);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var author = GetById(id);
        _context.Authors.Remove(author!);
        _context.SaveChanges();
    }
}

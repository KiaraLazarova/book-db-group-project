using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public interface ICountryRepository
{
    public Country? GetById(int id);
    public IEnumerable<Country> GetAll();

    public void Create(Country country);
    public void Update(Country country);
    public void Delete(int id);
}

public class CountryRepository : ICountryRepository
{
    private readonly BookDbContext _context;

    public CountryRepository(BookDbContext context)
    {
        _context = context;
    }

    public Country? GetById(int id)
    {
        return _context.Countries
            .SingleOrDefault(c => c.Id == id);
    }

    public IEnumerable<Country> GetAll()
    {
        return _context.Countries
            .ToList();
    }

    public void Create(Country country)
    {
        _context.Countries.Add(country);
        _context.SaveChanges();
    }

    public void Update(Country country)
    {
        _context.Countries.Update(country);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var country = GetById(id);
        _context.Countries.Remove(country!);
        _context.SaveChanges();
    }
}
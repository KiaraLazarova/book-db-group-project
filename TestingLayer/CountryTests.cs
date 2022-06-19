using System;
using System.Linq;
using BusinessLayer;
using DataLayer;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestingLayer;

public class CountryTests
{
    private BookDbContext _dbContext = null!;
    private ICountryRepository _repository = null!;
    
    [SetUp]
    public void Setup()
    {
        var builder = new DbContextOptionsBuilder<BookDbContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _dbContext = new BookDbContext(builder.Options);
        _repository = new CountryRepository(_dbContext);
    }

    [Test]
    public void CreateCountry_ShouldReturnOk()
    {
        int count = _repository.GetAll().Count();
        
        _repository.Create(new Country{ Name = "Example"});

        Assert.AreNotEqual(count, _repository.GetAll().Count());
    }

    [Test]
    public void ReadCountry_ShouldReturnOk()
    {
        _repository.Create(new Country { Name = "Example"});
        var count = _repository.GetAll().Count();
        Assert.NotZero(count);
    }

    [Test]
    public void UpdateCountry_ShouldReturnOk()
    {
        _repository.Create(new Country { Name = "Example"});
        var country = _repository.GetById(1);
        country.Name = "Example1";
        _repository.Update(country);

        country = _repository.GetById(1);
        Assert.AreEqual(country!.Name, "Example1");
    }

    [Test]
    public void DeleteCountry_ShouldReturnOk()
    {
        _repository.Create(new Country { Name = "Example"});
        _repository.Delete(1);
        Assert.Zero(_repository.GetAll().Count());
    }
}
using System;
using System.Linq;
using BusinessLayer;
using DataLayer;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestingLayer;

public class AuthorTests
{
    private BookDbContext _dbContext = null!;
    private IAuthorRepository _repository = null!;
    
    [SetUp]
    public void Setup()
    {
        var builder = new DbContextOptionsBuilder<BookDbContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _dbContext = new BookDbContext(builder.Options);
        _repository = new AuthorRepository(_dbContext);
    }

    [Test]
    public void CreateAuthor_ShouldReturnOk()
    {
        int count = _repository.GetAll().Count();
        
        _repository.Create(new Author{FirstName = "Example", LastName = "Example", Age = 12});

        Assert.AreNotEqual(count, _repository.GetAll().Count());
    }

    [Test]
    public void ReadAuthor_ShouldReturnOk()
    {
        _repository.Create(new Author{FirstName = "Example", LastName = "Example", Age = 12});
        var count = _repository.GetAll().Count();
        Assert.NotZero(count);
    }

    [Test]
    public void UpdateAuthor_ShouldReturnOk()
    {
        _repository.Create(new Author{FirstName = "Example", LastName = "Example", Age = 12});
        var author = _repository.GetById(1);
        author.FirstName = "Example1";
        _repository.Update(author);

        author = _repository.GetById(1);
        Assert.AreEqual(author!.FirstName, "Example1");
    }

    [Test]
    public void DeleteAuthor_ShouldReturnOk()
    {
        _repository.Create(new Author{FirstName = "Example", LastName = "Example", Age = 12});
        _repository.Delete(1);
        Assert.Zero(_repository.GetAll().Count());
    }
}

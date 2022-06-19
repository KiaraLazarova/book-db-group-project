using System;
using System.Linq;
using BusinessLayer;
using DataLayer;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestingLayer;

public class BookTests
{
    private BookDbContext _dbContext = null!;
    private IBookRepository _repository = null!;
    
    [SetUp]
    public void Setup()
    {
        var builder = new DbContextOptionsBuilder<BookDbContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _dbContext = new BookDbContext(builder.Options);
        _repository = new BookRepository(_dbContext);
    }

    [Test]
    public void CreateBook_ShouldReturnOk()
    {
        int count = _repository.GetAll().Count();
        
        _repository.Create(new Book{ Title = "Example", Annotation = "Example", DateTimeOfPublication = DateTime.Now});

        Assert.AreNotEqual(count, _repository.GetAll().Count());
    }

    [Test]
    public void ReadBook_ShouldReturnOk()
    {
        _repository.Create(new Book{ Title = "Example", Annotation = "Example", DateTimeOfPublication = DateTime.Now});
        var count = _repository.GetAll().Count();
        Assert.NotZero(count);
    }

    [Test]
    public void UpdateBook_ShouldReturnOk()
    {
        _repository.Create(new Book{ Title = "Example", Annotation = "Example", DateTimeOfPublication = DateTime.Now});
        var book = _repository.GetById(1);
        book.Title = "Example1";
        _repository.Update(book);

        book = _repository.GetById(1);
        Assert.AreEqual(book!.Title, "Example1");
    }

    [Test]
    public void DeleteBook_ShouldReturnOk()
    {
        _repository.Create(new Book{ Title = "Example", Annotation = "Example", DateTimeOfPublication = DateTime.Now});
        _repository.Delete(1);
        Assert.Zero(_repository.GetAll().Count());
    }
}

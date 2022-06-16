using System.Globalization;
using System.Text.Json;
using BusinessLayer;
using Cocona;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PresentationLayer.Views;

public static class BookView
{
    public static void RegisterBookView(this CoconaApp app)
    {
        app.AddSubCommand("book", x =>
        {
            x.AddCommand("add", AddBookCommand);
            x.AddCommand("getall", GetBooksCommand);
            x.AddCommand("get", GetBookCommand);
            x.AddCommand("update", UpdateBookCommand);
            x.AddCommand("delete", RemoveBookCommand);
        });
    }

    private static void AddBookCommand(IBookRepository repository, IAuthorRepository authorRepository,
        string title, string annotation, string datetime, string authorname)
    {
        var book = new Book
        {
            Title = title,
            Annotation = annotation,
            DateTimeOfPublication = DateTime.ParseExact(datetime, "dd:MM:yyyy", CultureInfo.CurrentCulture)
        };

        book = repository.Create(book);
        book.Author = authorRepository.GetByName(authorname);
        repository.Update(book);
    }

    private static void GetBooksCommand(IBookRepository repository)
    {
        var books = repository.GetAll();
        Console.WriteLine(JsonSerializer.Serialize(books));
    }

    private static void GetBookCommand(IBookRepository repository, int id)
    {
        var book = repository.GetById(id);
        Console.WriteLine(JsonSerializer.Serialize(book));
    }

    private static void UpdateBookCommand(IBookRepository repository, int id, string annotation)
    {
        var book = repository.GetById(id)!;
        book.Annotation = annotation;
        repository.Update(book);
    }
    
    private static void RemoveBookCommand(IBookRepository repository, int id)
    {
        repository.Delete(id);
    }
}
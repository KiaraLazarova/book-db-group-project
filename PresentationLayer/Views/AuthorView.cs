using System.Globalization;
using System.Text.Json;
using BusinessLayer;
using Cocona;
using DataLayer.Repositories;

namespace PresentationLayer.Views;

public static class AuthorView
{
    public static void RegisterAuthorView(this CoconaApp app)
    {
        app.AddSubCommand("author", x =>
        {
            x.AddCommand("add", AddAuthorCommand);
            x.AddCommand("getall", GetAuthorsCommand);
            x.AddCommand("get", GetAuthorCommand);
            x.AddCommand("update", UpdateAuthorCommand);
            x.AddCommand("delete", RemoveAuthorCommand);
        });
    }

    private static void AddAuthorCommand(IAuthorRepository repository, ICountryRepository countryRepository, 
        string firstname, string lastname, int age, string countryname)
    {
        var author = new Author
        {
            FirstName = firstname,
            LastName = lastname,
            Age = age
        };
        author = repository.Create(author);
        author.Country = countryRepository.GetByName(countryname);
        repository.Update(author);
    }

    private static void GetAuthorsCommand(IAuthorRepository repository)
    {
        var authors = repository.GetAll();
        Console.WriteLine(JsonSerializer.Serialize(authors));
    }

    private static void GetAuthorCommand(IAuthorRepository repository, int id)
    {
        var author = repository.GetById(id);
        Console.WriteLine(JsonSerializer.Serialize(author));
    }

    private static void UpdateAuthorCommand(IAuthorRepository repository, int id, string firstname, string lastname, int age)
    {
        var author = repository.GetById(id)!;
        author.FirstName = firstname;
        author.LastName = lastname;
        author.Age = age;
        repository.Update(author);
    }
    
    private static void RemoveAuthorCommand(IAuthorRepository repository, int id)
    {
        repository.Delete(id);
    }
}
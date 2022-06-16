using System.Text.Json;
using BusinessLayer;
using Cocona;
using DataLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace PresentationLayer.Views;

public static class CountryView
{
    public static void RegisterCountryView(this CoconaApp app)
    {
        app.AddSubCommand("country", x =>
        {
            x.AddCommand("add", AddCountryCommand);
            x.AddCommand("getall", GetCountriesCommand);
            x.AddCommand("get", GetCountryCommand);
            x.AddCommand("update", UpdateCountryCommand);
            x.AddCommand("delete", RemoveCountryCommand);
        });
    }

    private static void AddCountryCommand(ICountryRepository repository, string name)
    {
        repository.Create(new Country { Name = name});
        Console.WriteLine($"Saved country: {name}");
    }

    private static void GetCountriesCommand(ICountryRepository repository)
    {
        var countries = repository.GetAll();
        Console.WriteLine(JsonSerializer.Serialize(countries));
    }

    private static void GetCountryCommand(ICountryRepository repository, int id)
    {
        var country = repository.GetById(id);
        Console.WriteLine(JsonSerializer.Serialize(country));
    }

    private static void UpdateCountryCommand(ICountryRepository repository, int id, string name)
    {
        repository.Update(new Country
        {
            Id = id,
            Name = name
        });
    }
    
    private static void RemoveCountryCommand(ICountryRepository repository, int id)
    {
        repository.Delete(id);
    }
}
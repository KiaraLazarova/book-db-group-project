using Cocona;
using DataLayer;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PresentationLayer.Views;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddDbContext<BookDbContext>(opt => opt.UseSqlServer(@"Data Source=localhost,1433;Initial Catalog=books;User id=sa;Password=Passw0rd123;"));
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<ICountryRepository, CountryRepository>();

var app = builder.Build();

app.RegisterCountryView();
app.RegisterBookView();
app.RegisterAuthorView();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<BookDbContext>();
    db!.Database.EnsureCreated();
}

app.Run();
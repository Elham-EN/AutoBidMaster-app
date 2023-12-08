using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// MongoDB Initialization
await DB.InitAsync("SearchServiceDB", MongoClientSettings
    .FromConnectionString(builder
        .Configuration.GetConnectionString("MongoDbConnection")));
// Create index for the Item (for cenrtain fields) to be able to search on
await DB.Index<Item>()
    .Key(x => x.Make, KeyType.Text)
    .Key(x => x.Model, KeyType.Text)
    .Key(x => x.Color, KeyType.Text)
    .CreateAsync();

app.Run();

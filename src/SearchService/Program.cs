using System.Net;
using Polly;
using Polly.Extensions.Http;
using SearchService.Data;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient<AuctionServiceHttpClient>().AddPolicyHandler(GetPolicy());


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

try
{
    // MongoDB Initialization
    await DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}


app.Run();


// Make Http request repeat until such time as the data is available and get 
// a success response by using Http polling , since it relies on AuctionService 
// to provide data, then we need to keep calling that service until its available
// Create a policy (type - what we get back from a http request)
static IAsyncPolicy<HttpResponseMessage> GetPolicy() 
    => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));


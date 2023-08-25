using HAViz.API.Services;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

//builder.Services.AddSingleton<IHA_DataService, HA_DataService>();
builder.Services.AddSingleton<IHA_DataService, Mock_DataService>();
builder.Services.AddSingleton<HttpClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


using Microsoft.EntityFrameworkCore;
using zd7.Services;
using zd7.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var settings = builder.Configuration.GetSection("ConnectionStrings").Get<CustomAppSettings>();

builder.Services.AddDbContext<zd7.Models.zd7Context>(options => 
    options.UseSqlServer(settings.Default));

builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IClientService, ClientService>();
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

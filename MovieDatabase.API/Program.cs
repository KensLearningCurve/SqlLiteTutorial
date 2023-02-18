using Microsoft.EntityFrameworkCore;
using MovieDatabase.Business;
using MovieDatabase.Business.Interfaces;
using MovieDatabase.Data.Domain.Interfaces;
using MovieDatabase.Data.SQLServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]),
    ServiceLifetime.Scoped);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Api/Movies", (IMovieService movieService) =>
{
    return Results.Ok(movieService.GetAll(null));
});

app.MapDelete("/Api/Movies{id:int}", (int id, IMovieService movieService) =>
{
    movieService.Delete(id);
    return Results.NoContent();
});

app.Run();

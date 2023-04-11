using Microsoft.EntityFrameworkCore;
using MovieAPi.Models;
using MovieAPi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionstring = builder.Configuration.GetConnectionString("DefaultDb");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IGenresServices, GenresServices>();
builder.Services.AddTransient<IMoviesServices, MovieServices>();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieContext>(option => option.UseSqlServer(connectionstring));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// This is Middleware if there are Request from outer Network you can access it by this Credantials 
app.UseCors(c=> c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();

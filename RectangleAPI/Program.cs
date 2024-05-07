using Microsoft.EntityFrameworkCore;
using RectangleAPI.Data;
using RectangleAPI.Models;
using RectangleAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=:memory:;Cache=Shared"),
    ServiceLifetime.Singleton);

builder.Services.AddScoped<RectangleRepository>();

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.OpenConnection(); // Важливо: відкриття з'єднання
    db.Database.EnsureCreated(); // Створення бази, якщо вона не створена
    DbInitializer.Initialize(db); // Ініціалізація даними
}



app.Run();


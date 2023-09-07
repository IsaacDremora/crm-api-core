using Microsoft.OpenApi.Models;
using FoodPositions;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using ContextDb;

var builder = WebApplication.CreateBuilder(args);
var connectionstring = builder.Configuration.GetConnectionString("Positions") ?? "Data Source=Positions.db"; //метод
builder.Services.AddEndpointsApiExplorer(); // метод, который нужен, чтобы работал Swagger. 
builder.Services.AddSqlite<FoodPositionDb>(connectionstring); // подключается класс контекста (Нужно для EntityFrameWorkCore) и подключает ДБ (Пока SQLite. Я не вкурил, как ебануть мюскул, да и его достаточно пока)
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo{
        Title = "FoodPosition",
        Description = "this minimal API will be 'Core' in our first part SPA",
        Version = "v1",
    });
});  // настройка для Swagger
var app = builder.Build();

app.UseSwagger();// метод запускает Swagger в "билде"
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API for our SPA"); 
}
); //метод, который принимает 2 параметра. Первый - путь для жсончика, в котором будет дока, второй чисто описание

app.MapGet("/Foodposition", async (FoodPositionDb db) => await db.positions.ToListAsync()); // GET запрос. добавляет дерикторию и возвращает список позиций
app.MapPost("/Foodposition", async (FoodPositionDb db, FoodPosition position) =>
{
    await db.positions.AddAsync(position);
    await db.SaveChangesAsync();
    return Results.Created($"/Position/{position.id}", position);
});
app.MapGet("/Foodposition/{id}", async (FoodPositionDb db, int id) => await db.positions.FindAsync(id)); // GET запрос по id. возвращает позицию по id
app.MapPut("/Foodposition/{id}",async (FoodPositionDb db, FoodPosition update, int id) => 
{
    var positionname = await db.positions.FindAsync(id);
    if (positionname == null) return Results.NotFound(); //возвращает 404, если введёт несуществующий id
    positionname.name = update.name;
    positionname.price = update.price;
    positionname.shDesc = update.shDesc;
    positionname.isAveliable = update.isAveliable;
    await db.SaveChangesAsync();
    return Results.NoContent();
}); // изменение существующей позиции по id
app.MapDelete("/Foodposition/{id}", async (FoodPositionDb db, int id) =>
{
    var removename = await db.positions.FindAsync(id);
    if (removename == null) return Results.NotFound();
    db.positions.Remove(removename);
    await db.SaveChangesAsync();
    return Results.Ok();
}); //удаляет позицию по id 

app.Run();

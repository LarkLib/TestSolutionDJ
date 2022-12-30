//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using TestMinimalAPIWebApplication;

class Program
{
    static void Main(string[] args)
    {
        TestChartImage.CreateChartImage();
        TestQuickChart.CreateImageByQuickChart();
        TestScottPlot.CrtateImageByScottPlot();

        var builder = WebApplication.CreateBuilder(args);
        //var connectionString = builder.Configuration.GetConnectionString("Pizzas");

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGet("/", (TodoDb db) => "test page.");

        app.MapGet("/todoitems", async (TodoDb db) =>
            await db.Todos.ToListAsync());

        app.MapGet("/todoitems/complete", async (TodoDb db) =>
            await db.Todos.Where(t => t.IsComplete).ToListAsync());

        app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
            await db.Todos.FindAsync(id)
                is Todo todo
                    ? Results.Ok(todo)
                    : Results.NotFound());

        app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
        {
            db.Todos.Add(todo);
            await db.SaveChangesAsync();

            return Results.Created($"/todoitems/{todo.Id}", todo);
        });

        app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
        {
            var todo = await db.Todos.FindAsync(id);

            if (todo is null) return Results.NotFound();

            todo.Name = inputTodo.Name;
            todo.IsComplete = inputTodo.IsComplete;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
        {
            if (await db.Todos.FindAsync(id) is Todo todo)
            {
                db.Todos.Remove(todo);
                await db.SaveChangesAsync();
                return Results.Ok(todo);
            }

            return Results.NotFound();
        });

        app.MapPost("/todoitemsByIds", async ([FromBody] IdRange ids, TodoDb db) =>
            await db.Todos.FindAsync(ids.StartId)
                is Todo todo
                    ? Results.Ok(todo)
                    : Results.NotFound());


        app.Run();
    }
}
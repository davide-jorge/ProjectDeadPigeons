using System.Text.Json;
using api;
using dataccess;
using dataccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine("App Options: " + JsonSerializer.Serialize(appOptions));

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(appOptions.DbConnectionString);
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(config => config
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .SetIsOriginAllowed(x => true));

app.MapGet("/", (
    
    [FromServices] IOptionsMonitor<AppOptions> optionsMonitor,
    [FromServices] MyDbContext dbContext) =>
{
    var myUser = new User()
    {
        Name = "James",
        PasswordHash = "password",
        Role = "Admin",
        CreatedAt = DateTime.Now,
        Id =  Guid.NewGuid()
    };
    dbContext.Users.Add(myUser);
    dbContext.SaveChanges();
    var objects = dbContext.Users.ToList();
    return objects;
});

app.Run();

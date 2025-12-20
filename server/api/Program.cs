using dataccess;
using dataccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetValue<string>("Db"));
});

var app = builder.Build();

app.MapGet("/", ([FromServices] MyDbContext dbContext) =>
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

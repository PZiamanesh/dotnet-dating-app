using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<DatingDbContextEF>(opt =>
{
    opt.UseSqlServer(config["ConnectionStrings:DatingAppConnString"]);
});

var app = builder.Build();

app.MapControllers();

app.Run();

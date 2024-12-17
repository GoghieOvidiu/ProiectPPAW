
using BusinessLayer;
using BusinessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Repository_CodeFirst;
using WebAPICore.Data;
using WebAPICore.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebAPICore.Data.ProiectContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ProiectDbConnection")));

builder.Services.AddScoped<IClient,ClientService>();
builder.Services.AddScoped<IProiectDbContext, Repository_CodeFirst.ProiectContext>();

ContainerConfigurer.ConfigureContainer();
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

using Microsoft.EntityFrameworkCore;
using StudentsApi.Data;
using StudentsApi.Dtos;
using StudentsApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("studentsConnectionString");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 11))));
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add services to DI container
{
    var services = builder.Services;
    services.AddControllers();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    app.MapControllers();
}
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
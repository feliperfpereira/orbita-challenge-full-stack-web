using Microsoft.EntityFrameworkCore;
using StudentsApi.Data;
using StudentsApi.Dtos;
using StudentsApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:8080","http://localhost:8081")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();;
        });
});


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
builder.Services.AddCors();

var app = builder.Build();

// configure HTTP request pipeline
{
    app.MapControllers();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.Run();
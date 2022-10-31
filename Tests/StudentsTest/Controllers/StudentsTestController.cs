using StudentsApi.Controllers;
using StudentsApi.Models;
using StudentsApi.Data;
using StudentsApi.Dtos;
using StudentsApi.Profiles;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace StudentsTest;

public class StudentsTestController
{

    private StudentRepo repository;
    private AutoMapper.IMapper mapper;

    public StudentsTestController()
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "InMemoryDb_Create");
        var context = new AppDbContext(builder.Options);
        Seed(context);
        repository = new StudentRepo(context);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<StudentsProfile>();
        });

        mapper = new Mapper(config);

    }


    #region GetByID
    [Fact]
    public async void Task_GetStudentById_Return_OkResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var StudentId = 2;

        //Act  
        var data = await controller.GetByid(StudentId);

        //Assert  
        Assert.IsType<OkObjectResult>(data);
    }

    [Fact]
    public async void Task_GetStudentById_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var StudentId = 999;

        //Act  
        var data = await controller.GetByid(StudentId);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    [Fact]
    public async void Task_GetStudentById_MatchResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        int StudentId = 1;

        //Act  
        var data = await controller.GetByid(StudentId);

        //Assert  
        Assert.IsType<OkObjectResult>(data);

        var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        var student = okResult.Value.Should().BeAssignableTo<StudentReadDto>().Subject;

        Assert.Equal("Teste Nome", student.Nome);
        Assert.Equal("teste@teste.com", student.Email);
    }
    #endregion

    #region GetAll

    [Fact]
    public async void Task_GetStudents_Return_OkResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);

        //Act  
        var data = await controller.GetAll();

        //Assert  
        Assert.IsType<OkObjectResult>(data);
    }

    [Fact]
    public void Task_GetStudents_Return_BadRequestResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);

        //Act  
        var data = controller.GetAll();
        data = null;

        if (data != null)
            //Assert  
            Assert.IsType<BadRequestResult>(data);
    }

    [Fact]
    public async void Task_GetStudents_MatchResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);

        //Act  
        var data = await controller.GetAll();

        //Assert  
        Assert.IsType<OkObjectResult>(data);

        var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        var student = okResult.Value.Should().BeAssignableTo<List<StudentReadDto>>().Subject;

        Assert.Equal("Teste Nome", student[0].Nome);
        Assert.Equal("teste@teste.com", student[0].Email);

        Assert.Equal("Teste1 Nome", student[1].Nome);
        Assert.Equal("teste1@teste.com", student[1].Email);
    }

    #endregion

    #region Add New Blog  

    [Fact]
    public async void Task_Add_ValidData_Return_OkResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var student = new StudentCreateDto() { CPF = "41447663810", Email = "teste4@teste.com", Nome = "Teste4 Nome", RA = "0004" };

        //Act  
        var data = await controller.createStudent(student);

        //Assert  
        Assert.IsType<CreatedResult>(data);
    }

    [Fact]
    public async void Task_Add_InvalidData_Return_BadRequest()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var student = new StudentCreateDto() { CPF = "cpf errado", Email = "email errado", Nome = "Teste4 Nome", RA = "0004" };

        //Act              
        var data = await controller.createStudent(student);

        //Assert  
        Assert.IsType<BadRequestResult>(data);
    }

    [Fact]
    public async void Task_Add_ValidData_MatchResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var student = new StudentCreateDto() { CPF = "41447663810", Email = "teste5@teste.com", Nome = "Teste5 Nome", RA = "0005" };

        //Act  
        var data = await controller.createStudent(student);

        //Assert  
        Assert.IsType<CreatedResult>(data);

        var okResult = data.Should().BeOfType<CreatedResult>().Subject;
        var result = okResult.Value.Should().BeAssignableTo<StudentReadDto>().Subject;

        Assert.Equal(5, result.id);
    }

    #endregion

    #region Update Existing Blog  

    [Fact]
    public async void Task_Update_ValidData_Return_OkResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var StudentId = 1;

        //Act  
        var existingStudent = await controller.GetByid(StudentId);
        var okResult = existingStudent.Should().BeOfType<OkObjectResult>().Subject;
        var result = okResult.Value.Should().BeAssignableTo<StudentReadDto>().Subject;

        var student = new StudentUpdateDto();
        student.Nome = "Nome Updated";
        student.CPF = result.CPF;
        student.Email = result.Email;

        var updatedData = await controller.updateStudent(StudentId, student);

        //Assert  
        Assert.IsType<NoContentResult>(updatedData);
    }

    [Fact]
    public async void Task_Update_InvalidData_Return_BadRequest()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var StudentId = 2;

        //Act  
        var existingStudent = await controller.GetByid(StudentId);
        var okResult = existingStudent.Should().BeOfType<OkObjectResult>().Subject;
        var result = okResult.Value.Should().BeAssignableTo<StudentReadDto>().Subject;

        var student = new StudentUpdateDto();
        student.Nome = "Nome Updated";
        student.CPF = "55454";
        student.Email = "gddfgfd";

        var data = await controller.updateStudent(StudentId, student);

        //Assert  
        Assert.IsType<BadRequestResult>(data);
    }

    [Fact]
    public async void Task_Update_InvalidData_Return_NotFound()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var StudentId = 2;

        //Act  
        var existingStudent = await controller.GetByid(StudentId);
        var okResult = existingStudent.Should().BeOfType<OkObjectResult>().Subject;
        var result = okResult.Value.Should().BeAssignableTo<StudentReadDto>().Subject;

        var student = new StudentUpdateDto();
        student.Nome = "Nome Updated";
        student.CPF = result.CPF;
        student.Email = result.Email;

        var data = await controller.updateStudent(9999, student);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }

    #endregion

    #region Delete Student  

    [Fact]
    public async void Task_Delete_Student_Return_OkResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var StudentId = 2;

        //Act  
        var data = await controller.deleteStudent(StudentId);

        //Assert  
        Assert.IsType<NoContentResult>(data);
    }

    [Fact]
    public async void Task_Delete_Student_Return_NotFoundResult()
    {
        //Arrange  
        var controller = new StudentsController(repository, mapper);
        var StudentId = 9999;

        //Act  
        var data = await controller.deleteStudent(StudentId);

        //Assert  
        Assert.IsType<NotFoundResult>(data);
    }


    #endregion

    private void Seed(AppDbContext context)
    {
        var students = new[]
        {
                new Student() { CPF = "41447663810", Email = "teste@teste.com", Nome = "Teste Nome", RA = "0000"  },
                new Student() { CPF = "41447663810", Email = "teste1@teste.com", Nome = "Teste1 Nome", RA = "0001"  },
                new Student() { CPF = "41447663810", Email = "teste2@teste.com", Nome = "Teste2 Nome", RA = "0002"  },
                new Student() { CPF = "41447663810", Email = "teste3@teste.com", Nome = "Teste3 Nome", RA = "0003"  }
            };

        context.Students.AddRange(students);
        context.SaveChanges();
    }
}
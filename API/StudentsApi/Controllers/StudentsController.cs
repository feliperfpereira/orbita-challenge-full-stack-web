using AutoMapper;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using StudentsApi.Data;
using StudentsApi.Dtos;
using StudentsApi.Models;
using Microsoft.EntityFrameworkCore;


namespace StudentsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{

    private readonly IStudentRepo _studentRepo;
    private readonly IMapper _mapper;


    public StudentsController(IStudentRepo studentRepo, IMapper mapper)
    {
        _studentRepo = studentRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentRepo.GetAllStudents();
        return Ok(_mapper.Map<IEnumerable<StudentReadDto>>(students));
    }

    [HttpGet("ra/{RA}")]
    public async Task<IActionResult> GetByRA(string RA)
    {
        var student = await _studentRepo.GetStudentByRA(RA);

        if (student != null)
        {
            return Ok(_mapper.Map<StudentReadDto>(student));
        }
        return NotFound();
    }


    [HttpGet("search/{value}")]
    public async Task<IActionResult> GetByValue(string value)
    {
        var students = await _studentRepo.GetStudentByValue(value);

        if (students != null)
        {
            return Ok(_mapper.Map<IEnumerable<StudentReadDto>>(students));
        }
        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByid(int id)
    {
        var student = await _studentRepo.GetStudentById(id);

        if (student != null)
        {
            return Ok(_mapper.Map<StudentReadDto>(student));
        }
        return NotFound();
    }


    [HttpPost]
    public async Task<IActionResult> createStudent(StudentCreateDto studentCreateDto)
    {

        var context = new ValidationContext(studentCreateDto, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(studentCreateDto, context, results, true);
        if (!isValid)
            return BadRequest();

        var studentModel = _mapper.Map<Student>(studentCreateDto);

        await _studentRepo.CreateStudent(studentModel);
        await _studentRepo.SaveChanges();

        var StudentReadDto = _mapper.Map<StudentReadDto>(studentModel);

        return Created($"students/{StudentReadDto.RA}", StudentReadDto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> updateStudent(int id, StudentUpdateDto studentUpdateDto)
    {

        var context = new ValidationContext(studentUpdateDto, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(studentUpdateDto, context, results, true);
        if (!isValid)
            return BadRequest();


        var student = await _studentRepo.GetStudentById(id);
        if (student == null)
        {
            return NotFound();
        }

        _mapper.Map(studentUpdateDto, student);

        await _studentRepo.SaveChanges();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> deleteStudent(int id)
    {

        var student = await _studentRepo.GetStudentById(id);
        if (student == null)
        {
            return NotFound();
        }

        _studentRepo.DeleteStudent(student);

        await _studentRepo.SaveChanges();

        return NoContent();
    }


}

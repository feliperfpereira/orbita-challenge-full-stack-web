using AutoMapper;

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

    [HttpGet("{RA}")]
    public async Task<IActionResult> GetByRA(int RA)
    {
        var student = await _studentRepo.GetStudentByRA(RA);

        if (student != null)
        {
            return Ok(_mapper.Map<StudentReadDto>(student));
        }
        return NotFound();
    }


    [HttpPost]
    public async Task<IActionResult> createStudent(StudentCreateDto studentCreateDto)
    {

        var studentModel = _mapper.Map<Student>(studentCreateDto);

        await _studentRepo.CreateStudent(studentModel);
        await _studentRepo.SaveChanges();

        var StudentReadDto = _mapper.Map<StudentReadDto>(studentModel);

        return Created($"students/{StudentReadDto.RA}", StudentReadDto);
    }


    [HttpPut("{RA}")]
    public async Task<IActionResult> updateStudent(int RA, StudentUpdateDto studentUpdateDto)
    {

        var student = await _studentRepo.GetStudentByRA(RA);
        if (student == null)
        {
            return NotFound();
        }

        _mapper.Map(studentUpdateDto, student);

        await _studentRepo.SaveChanges();

        return NoContent();
    }


    [HttpDelete("{RA}")]
    public async Task<IActionResult> deleteStudent(int RA)
    {

        var student = await _studentRepo.GetStudentByRA(RA);
        if (student == null)
        {
            return NotFound();
        }

        _studentRepo.DeleteStudent(student);

        await _studentRepo.SaveChanges();

        return NoContent();
    }


}

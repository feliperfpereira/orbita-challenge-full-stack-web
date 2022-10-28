using AutoMapper;
using StudentsApi.Dtos;
using StudentsApi.Models;

namespace StudentsApi.Profiles
{
    public class StudentsProfile : Profile
    {
        public StudentsProfile()
        {
            // Source -> Target
            CreateMap<Student, StudentReadDto>();
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentUpdateDto, Student>();
        }
    }
}
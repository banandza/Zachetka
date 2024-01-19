using AutoMapper;
using RESTFull.Domain.Dto;
using RESTFull.Domain.Model;

namespace RESTFull.API.Helper
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherDto, Teacher>();
            CreateMap<Discipline, DisciplineDto>();
            CreateMap<DisciplineDto, Discipline>();
            CreateMap<Statement, StatementDto>();
            CreateMap<StatementDto, Statement>();
        }
    }
}

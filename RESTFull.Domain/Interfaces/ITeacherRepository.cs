using RESTFull.Domain.Dto;
using RESTFull.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Interfaces
{
    public interface ITeacherRepository
    {
        ICollection<Teacher> GetTeachers();
        Teacher GetTeacher(int Id);
        Teacher GetTeacher(string surname);
        Teacher GetTeacherTrimToUpper(TeacherDto teacherCreate);
        bool TeacherExists(int teachId);
        bool CreateTeacher(Teacher teacher);
        bool UpdateTeacher(Teacher teacher);
        bool DeleteTeacher(Teacher teacher);
        bool Save();
    }
}

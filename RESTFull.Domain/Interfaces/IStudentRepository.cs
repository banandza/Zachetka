using RESTFull.Domain.Dto;
using RESTFull.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Interfaces
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        Student GetStudent(String surname);
        Student GetStudentTrimToUpper(StudentDto studentcreate);
        bool StudentExists(int stuId);
        bool StudentExists(string surname);
        bool CreateStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(Student student);
        bool Save();
    }
}

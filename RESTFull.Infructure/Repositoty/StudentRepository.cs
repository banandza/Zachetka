using Microsoft.EntityFrameworkCore;
using RESTFull.Domain.Dto;
using RESTFull.Domain.Interfaces;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure.data;

namespace RESTFull.Infrastructure.Repositoty
{
    public class StudentRepository : IStudentRepository
    {
        private readonly Context _context;

        public StudentRepository(Context context)
        {
            _context = context;
        }
        public bool CreateStudent(Student student)
        {
            _context.Add(student);
            return Save();
        }
        public bool DeleteStudent(Student student)
        {
            _context.Remove(student);
            return Save();
        }
        public Student GetStudent(int id)
        {
            return _context.Students.Where(s => s.Id == id).FirstOrDefault();
        }
        public Student GetStudent(string surname)
        {
            return _context.Students.Where(s => s.Surname == surname).FirstOrDefault();
        }
        public ICollection<Student> GetStudents()
        {
            return _context.Students.OrderBy(s => s.Id).ToList();
        }
        public Student GetStudentTrimToUpper(StudentDto studentCreate)
        {
            return GetStudents().Where(s => s.Surname.Trim().ToUpper() == studentCreate.Surname.TrimEnd().ToUpper()).FirstOrDefault();
        }
        public bool StudentExists(int stuId)
        {
            return _context.Students.Any(s => s.Id == stuId);
        }
        public bool StudentExists(string surname)
        {
            return _context.Students.Any(s => s.Surname == surname);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateStudent(Student student)
        {
            _context.Update(student);
            return Save();
        }
    }
}

using RESTFull.Domain.Dto;
using RESTFull.Domain.Interfaces;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Infrastructure.Repositoty
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly Context _context;
        public TeacherRepository(Context context)
        {
            _context = context;
        }
        public bool CreateTeacher(Teacher teacher)
        {
            _context.Add(teacher);
            return Save();
        }
        public bool DeleteTeacher(Teacher teacher)
        {
            _context.Remove(teacher);
            return Save();
        }
        public Teacher GetTeacher(int id)
        {
            return _context.Teachers.Where(t => t.Id == id).FirstOrDefault();
        }
        public Teacher GetTeacher(string surname)
        {
            return _context.Teachers.Where(t => t.Surname == surname).FirstOrDefault();
        }
        public ICollection<Teacher> GetTeachers()
        {
            return _context.Teachers.OrderBy(t => t.Id).ToList();
        }
        public Teacher GetTeacherTrimToUpper(TeacherDto teacherCreate)
        {
            return GetTeachers().Where(t => t.Surname.Trim().ToUpper() == teacherCreate.Surname.TrimEnd().ToUpper()).FirstOrDefault();
        }
        public bool TeacherExists(int teachId)
        {
            return _context.Teachers.Any(t => t.Id == teachId);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateTeacher(Teacher teacher)
        {
            _context.Update(teacher);
            return Save();
        }
    }
}

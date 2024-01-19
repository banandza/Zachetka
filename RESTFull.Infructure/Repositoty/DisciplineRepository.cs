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
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly Context _context;
        public DisciplineRepository(Context context)
        {
            _context = context;
        }
        public bool CreateDiscipline(Discipline discipline)
        {
            _context.Add(discipline);
            return Save();
        }
        public bool DeleteDiscipline(Discipline discipline)
        {
            _context.Remove(discipline);
            return Save();
        }
        public Discipline GetDiscipline(int id)
        {
            return _context.Disciplines.Where(d => d.Id == id).FirstOrDefault();
        }
        public Discipline GetDiscipline(string title)
        {
            return _context.Disciplines.Where(d => d.Title == title).FirstOrDefault();
        }
        public ICollection<Discipline> GetDisciplines()
        {
            return _context.Disciplines.OrderBy(d => d.Id).ToList();
        }
        public Discipline GetDisciplineTrimToUpper(DisciplineDto disciplineCreate)
        {
            return GetDisciplines().Where(d => d.Title.Trim().ToUpper() == disciplineCreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
        }
        public bool DisciplineExists(int disId)
        {
            return _context.Disciplines.Any(d => d.Id == disId);
        }
        public bool DisciplineExists(string title)
        {
            return _context.Disciplines.Any(d => d.Title == title);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateDiscipline(Discipline discipline)
        {
            _context.Update(discipline);
            return Save();
        }
    }
}

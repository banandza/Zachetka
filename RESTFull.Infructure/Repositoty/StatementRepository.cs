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
    public class StatementRepository : IStatementRepository
    {
        private readonly Context _context;
        public StatementRepository(Context context)
        {
            _context = context;
        }
        public bool CreateStatement(Statement statement)
        {
            _context.Add(statement);
            return Save();
        }
        public bool DeleteStatement(Statement statement)
        {
            _context.Remove(statement);
            return Save();
        }
        public Statement GetStatement(int id)
        {
            return _context.Statements.Where(s => s.Id == id).FirstOrDefault();
        }
        public ICollection<Statement> GetStatements()
        {
            return _context.Statements.OrderBy(s => s.Id).ToList();
        }
        public ICollection<Statement> GetGradesOfStudent(int stuId)
        {
            return _context.Statements.Where(s => s.Student.Id == stuId).ToList();
        }
        public ICollection<Statement> GetGradesOfDiscipline(int disId)
        {
            return _context.Statements.Where(s => s.Discipline.Id == disId).ToList();
        }
        public bool StatementExists(int staId)
        {
            return _context.Statements.Any(s => s.Id == staId);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateStatement(Statement statement)
        {
            _context.Update(statement);
            return Save();
        }
    }
}

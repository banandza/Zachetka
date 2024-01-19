using RESTFull.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Interfaces
{
    public interface IStatementRepository
    {
        ICollection<Statement> GetStatements();
        Statement GetStatement(int id);
        ICollection<Statement> GetGradesOfStudent(int stuId);
        ICollection<Statement> GetGradesOfDiscipline(int disId);
        bool StatementExists(int staId);
        bool CreateStatement(Statement statement);
        bool UpdateStatement(Statement statement);
        bool DeleteStatement(Statement statement);
        bool Save();

    }
}

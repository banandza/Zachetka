using RESTFull.Domain.Dto;
using RESTFull.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Interfaces
{
    public interface IDisciplineRepository
    {
        ICollection<Discipline> GetDisciplines();
        Discipline GetDiscipline(int id);
        Discipline GetDiscipline(string title);
        Discipline GetDisciplineTrimToUpper(DisciplineDto disciplineCreate);
        bool DisciplineExists(int disId);
        bool DisciplineExists(string title);
        bool CreateDiscipline(Discipline discipline);
        bool UpdateDiscipline(Discipline discipline);
        bool DeleteDiscipline(Discipline discipline);
        bool Save();
    }
}

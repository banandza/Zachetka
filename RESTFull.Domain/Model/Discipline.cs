using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Model
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Hours { get; set; } //колличество часов
        public ICollection<Statement> Statements { get; set; }
        public ICollection<CourseLeader> CourseLeaders { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Group_Number { get; set; }
        public int Course { get; set; }
        public int Semester { get; set; }
        public ICollection<Statement> Statements { get; set; }
    }
}

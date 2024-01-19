using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Model
{
    public class Statement
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public int StudentId { get; set; }
        public int DisciplineId { get; set; }
        public Student Student { get; set; }
        public Discipline Discipline { get; set; }
    }
}

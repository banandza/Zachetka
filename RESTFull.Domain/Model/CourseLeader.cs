using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFull.Domain.Model
{
    public class CourseLeader
    {
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }
        public Teacher Teacher { get; set; }
        public Discipline Discipline { get; set; }
    }
}

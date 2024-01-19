namespace RESTFull.Domain.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Group_Number { get; set; }
        public int Course { get; set; } = 0;
        public int Semester { get; set; } = 0;
    }
}

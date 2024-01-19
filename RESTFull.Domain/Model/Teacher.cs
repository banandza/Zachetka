namespace RESTFull.Domain.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Phone_Number { get; set; }
        public string Email_Address { get; set; }
        public ICollection<CourseLeader> CourseLeaders { get; set; }

    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTFull.Domain.Dto;
using RESTFull.Domain.Interfaces;
using RESTFull.Domain.Model;

namespace RESTFull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IStatementRepository _statementRepository;

        public StudentController(IStudentRepository studentRepository, IMapper mapper, IStatementRepository statementRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _statementRepository = statementRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(students);
        }
        [HttpGet("{stuId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int stuId)
        {
            if (!_studentRepository.StudentExists(stuId))
                return NotFound();

            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(stuId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(student);
        }

        [HttpGet("surname/{surname}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(string surname)
        {
            if (!_studentRepository.StudentExists(surname))
                return NotFound();

            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(surname));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(student);
        }

        [HttpGet("{stuId}/grades")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentGrades(int stuId)
        {
            var grades = _mapper.Map<List<StatementDto>>(_statementRepository.GetGradesOfStudent(stuId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(grades);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStudent([FromBody] StudentDto studentCreate)
        {
            if (studentCreate == null)
                return BadRequest(ModelState);
            var students = _studentRepository.GetStudentTrimToUpper(studentCreate);
            if (students != null)
            {
                ModelState.AddModelError("", "Student already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var studentMap = _mapper.Map<Student>(studentCreate);
            if (!_studentRepository.CreateStudent(studentMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{stuId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent(int stuId, [FromBody] StudentDto updatedStudent)
        {
            if (updatedStudent == null)
                return BadRequest(ModelState);
            if (stuId != updatedStudent.Id)
                return BadRequest(ModelState);
            if (!_studentRepository.StudentExists(stuId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var studentMap = _mapper.Map<Student>(updatedStudent);
            if (!_studentRepository.UpdateStudent(studentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating student");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{stuId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudent(int stuId)
        {
            if (!_studentRepository.StudentExists(stuId))
                return NotFound();
            var statementsToDelete = _statementRepository.GetGradesOfStudent(stuId);
            var studentToDelete = _studentRepository.GetStudent(stuId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            foreach (var statement in statementsToDelete)
            {
                if (!_statementRepository.DeleteStatement(statement))
                {
                    ModelState.AddModelError("", "Something went wrong deleting student");
                }
            }
            if (!_studentRepository.DeleteStudent(studentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting student");
            }
            return NoContent();
        }
    }
}

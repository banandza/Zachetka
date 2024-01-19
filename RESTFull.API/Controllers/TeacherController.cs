using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTFull.Domain.Dto;
using RESTFull.Domain.Interfaces;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure.Repositoty;

namespace RESTFull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
        public IActionResult GetTeachers()
        {
            var teachers = _mapper.Map<List<TeacherDto>>(_teacherRepository.GetTeachers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(teachers);
        }
        [HttpGet("{teachId}")]
        [ProducesResponseType(200, Type = typeof(Teacher))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacher(int teachId)
        {
            if (!_teacherRepository.TeacherExists(teachId))
                return NotFound();
            var teacher = _mapper.Map<TeacherDto>(_teacherRepository.GetTeacher(teachId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(teacher);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTeacher([FromBody] TeacherDto teacherCreate)
        {
            if (teacherCreate == null)
                return BadRequest(ModelState);
            var teachers = _teacherRepository.GetTeacherTrimToUpper(teacherCreate);
            if (teachers != null)
            {
                ModelState.AddModelError("", "Teacher already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacherMap = _mapper.Map<Teacher>(teacherCreate);

            if (!_teacherRepository.CreateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{teachId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTeacher(int teachId, [FromBody] TeacherDto updatedTeacher)
        {
            if (updatedTeacher == null)
                return BadRequest(ModelState);
            if (teachId != updatedTeacher.Id)
                return BadRequest(ModelState);
            if (!_teacherRepository.TeacherExists(teachId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var teacherMap = _mapper.Map<Teacher>(updatedTeacher);
            if (!_teacherRepository.UpdateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong updating teacher");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{teachId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTeacher(int teachId)
        {
            if (!_teacherRepository.TeacherExists(teachId))
                return NotFound();
            var teacherToDelete = _teacherRepository.GetTeacher(teachId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_teacherRepository.DeleteTeacher(teacherToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting teacher");
            }
            return NoContent();
        }
    }
}

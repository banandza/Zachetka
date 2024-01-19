using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTFull.Domain.Dto;
using RESTFull.Domain.Interfaces;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure.Repositoty;
using System;

namespace RESTFull.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;
        private readonly IStatementRepository _statementRepository;

        public DisciplineController(IDisciplineRepository disciplineRepository, IMapper mapper, IStatementRepository statementRepository)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
            _statementRepository = statementRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Discipline>))]
        public IActionResult GetDisciplines()
        {
            var disciplines = _mapper.Map<List<DisciplineDto>>(_disciplineRepository.GetDisciplines());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(disciplines);
        }
        [HttpGet("{disId}")]
        [ProducesResponseType(200, Type = typeof(Discipline))]
        [ProducesResponseType(400)]
        public IActionResult GetDiscipline(int disId)
        {
            if (!_disciplineRepository.DisciplineExists(disId))
                return NotFound();
            var discipline = _mapper.Map<DisciplineDto>(_disciplineRepository.GetDiscipline(disId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(discipline);
        }

        [HttpGet("title/{title}")]
        [ProducesResponseType(200, Type = typeof(Discipline))]
        [ProducesResponseType(400)]
        public IActionResult GetDiscipline(string title)
        {
            if (!_disciplineRepository.DisciplineExists(title))
                return NotFound();
            var discipline = _mapper.Map<DisciplineDto>(_disciplineRepository.GetDiscipline(title));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(discipline);
        }

        [HttpGet("{disId}/grades")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetDisciplineGrades(int disId)
        {
            var grades = _mapper.Map<List<StatementDto>>(_statementRepository.GetGradesOfDiscipline(disId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(grades);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDiscipline([FromBody] DisciplineDto disciplineCreate)
        {
            if (disciplineCreate == null)
                return BadRequest(ModelState);
            var disciplines = _disciplineRepository.GetDisciplineTrimToUpper(disciplineCreate);
            if (disciplines != null)
            {
                ModelState.AddModelError("", "Discipline already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var disciplineMap = _mapper.Map<Discipline>(disciplineCreate);
            if (!_disciplineRepository.CreateDiscipline(disciplineMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{disId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDiscipline(int disId, [FromBody] DisciplineDto updatedDiscipline)
        {
            if (updatedDiscipline == null)
                return BadRequest(ModelState);
            if (disId != updatedDiscipline.Id)
                return BadRequest(ModelState);
            if (!_disciplineRepository.DisciplineExists(disId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var disciplineMap = _mapper.Map<Discipline>(updatedDiscipline);
            if (!_disciplineRepository.UpdateDiscipline(disciplineMap))
            {
                ModelState.AddModelError("", "Something went wrong updating discipline");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{disId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDiscipline(int disId)
        {
            if (!_disciplineRepository.DisciplineExists(disId))
                return NotFound();
            var statementsToDelete = _statementRepository.GetGradesOfStudent(disId);
            var disciplineToDelete = _disciplineRepository.GetDiscipline(disId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            foreach (var statement in statementsToDelete)
            {
                if (!_statementRepository.DeleteStatement(statement))
                {
                    ModelState.AddModelError("", "Something went wrong deleting student");
                }
            }
            if (!_disciplineRepository.DeleteDiscipline(disciplineToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting discipline");
            }
            return NoContent();
        }
    }
}

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
    public class StatementController : Controller
    {
        private readonly IStatementRepository _statementRepository;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        public StatementController(IStatementRepository statementRepository, IMapper mapper, IStudentRepository studentRepository,
            IDisciplineRepository disciplineRepository)
        {
            _statementRepository = statementRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _disciplineRepository = disciplineRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Statement>))]
        public IActionResult GetStatements()
        {
            var statements = _mapper.Map<List<StatementDto>>(_statementRepository.GetStatements());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(statements);
        }
        [HttpGet("{staId}")]
        [ProducesResponseType(200, Type = typeof(Statement))]
        [ProducesResponseType(400)]
        public IActionResult GetStatement(int staId)
        {
            if (!_statementRepository.StatementExists(staId))
                return NotFound();
            var statement = _mapper.Map<StatementDto>(_statementRepository.GetStatement(staId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(statement);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStatement([FromQuery] int stuId, [FromQuery] int disId, [FromBody] StatementDto statementCreate)
        {
            if (statementCreate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var statementMap = _mapper.Map<Statement>(statementCreate);
            statementMap.Student = _studentRepository.GetStudent(stuId);
            statementMap.Discipline = _disciplineRepository.GetDiscipline(disId);
            if (disId == null)
            {
                ModelState.AddModelError("", "Discipline not found");
                return BadRequest(ModelState);
            }

            if (!_statementRepository.CreateStatement(statementMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{staId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStatement(int staId, [FromBody] StatementDto updatedStatement)
        {
            if (updatedStatement == null)
                return BadRequest(ModelState);
            if (staId != updatedStatement.Id)
                return BadRequest(ModelState);
            if (!_statementRepository.StatementExists(staId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var statementMap = _mapper.Map<Statement>(updatedStatement);
            if (!_statementRepository.UpdateStatement(statementMap))
            {
                ModelState.AddModelError("", "Something went wrong updating statement");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{staId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStatement(int staId)
        {
            if (!_statementRepository.StatementExists(staId))
                return NotFound();
            var statementToDelete = _statementRepository.GetStatement(staId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_statementRepository.DeleteStatement(statementToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting statement");
            }
            return NoContent();
        }
    }
}

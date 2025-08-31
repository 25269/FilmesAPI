using AutoMapper;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Data;
using FilmsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private FilmContext _context;
        private IMapper _mapper;

        public SessionController(FilmContext filmContext, IMapper mapper)
        {
            _context = filmContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Criação de novas sessões (podendo ser passado um ou N sessões de uma única vez)
        /// </summary>
        /// <param name="sessionDTO"></param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        /// <response code="404">Caso a tentativa de inserção seja feita com a falta de dados obrigatórios</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddSession([FromBody] CreateSessionDTO sessionDTO)
        {
            Session session = _mapper.Map<Session>(sessionDTO);

            _context.Sessions.Add(session);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ReturnSessionForId), new { id = session.Id }, session);
        }

        /// <summary>
        /// Retorna todas as sessões cadastradas
        /// </summary>
        /// <param name="skip">Quantidade máxima de registros a serem buscados</param>
        /// <param name="take">Posição em que deseja iniciar a busca dos registros com base nos IDs das sessões</param>
        /// <response code="200">Retorna as sessões esperadas</response>
        /// <response code="404">Caso não exista nenhuma sessão</response>

        [HttpGet]
        public IEnumerable<ReadSessionDTO> ReadSessions([FromQuery] int skip = 0, [FromQuery] int take = 5)
        {
            return _mapper.Map<List<ReadSessionDTO>>(_context.Sessions.Skip(skip).Take(take));
        }

        /// <summary>
        /// Retorna a sessão cadastrada conforme o Id utilizado como parâmetro
        /// </summary>
        /// <param name="id">Código da sessão</param>
        /// <response code="200">Retorna a sessão esperado</response>
        /// <response code="404">Caso não exista nenhuma sessão com o código passado</response>

        [HttpGet("{id}")]
        public IActionResult ReturnSessionForId(int id)
        {
            Session session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if (session != null)
            {
                ReadSessionDTO sessionDTO = _mapper.Map<ReadSessionDTO>(session);
                return Ok(sessionDTO);
            }

            return NotFound();
        }
    }
}

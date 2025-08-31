using AutoMapper;
using FilmsAPI.Models;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private FilmContext _context;
        private IMapper _mapper;

        public CinemaController(FilmContext filmContext, IMapper mapper)
        {
            _context = filmContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Criação de novos cinemas (podendo ser passado um ou N cinemas de uma única vez)
        /// </summary>
        /// <param name="cinemaDTO"></param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        /// <response code="404">Caso a tentativa de inserção seja feita com a falta de dados obrigatórios</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddCinema([FromBody] CreateCinemaDTO cinemaDTO)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ReturnCinemasForId), new { id = cinema.Id }, cinemaDTO);
        }

        /// <summary>
        /// Retorna todos os cinemas cadastrados
        /// </summary>
        /// <param name="skip">Quantidade máxima de registros a serem buscados</param>
        /// <param name="take">Posição em que deseja iniciar a busca dos registros com base nos IDs dos cinemas</param>
        /// <response code="200">Retorna os cinemas esperados</response>
        /// <response code="404">Caso não exista nenhum cinema</response>

        [HttpGet]
        public IEnumerable<ReadCinemaDTO> ReturnCinemas([FromQuery] int skip = 0, [FromQuery] int take = 5)
        {
            var listForCinemasDB = _context.Cinemas.ToList();
            var listForCinemas = _mapper.Map<List<ReadCinemaDTO>>(listForCinemasDB);
            return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.Skip(skip).Take(take));
        }

        /// <summary>
        /// Retorna o cinema cadastrado conforme o Id utilizado como parâmetro
        /// </summary>
        /// <param name="id">Código do cinema</param>
        /// <response code="200">Retorna o cinema esperado</response>
        /// <response code="404">Caso não exista nenhum cinema com o código passado</response>

        [HttpGet("{id}")]
        public IActionResult ReturnCinemasForId(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema != null)
            {
                ReadCinemaDTO cinemaDTO = _mapper.Map<ReadCinemaDTO>(cinema);
                return Ok(cinemaDTO);
            }

            return NotFound();
        }

        /// <summary>
        /// Atualiza o cinema referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do cinema</param>
        /// <param name="cinemaDTO"></param>
        /// <response code="204">Confirma a atualização do cinema</response>
        /// <response code="404">Caso deixe de preencher algum campo obrigatório para atualização</response>

        [HttpPut("{id}")]
        public IActionResult UpdateCinema(int id, [FromBody] UpdateFilmDTO cinemaDTO)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }

            _mapper.Map(cinemaDTO, cinema);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Atualiza o cinema referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do cinema</param>
        /// <param name="patch"></param>
        /// <response code="204">Confirma a atualização do cinema</response>

        /*[HttpPatch("{id}")] -- Não vamos usar nesse momento
        public IActionResult UpdateFilmPatch(int id, JsonPatchDocument<UpdateCinemaDTO> patch)
        {
            var cinema = _context.Films.FirstOrDefault(film => film.IdFilm == id);

            if (cinema == null)
            {
                return NotFound();
            }

            var updateCinema = _mapper.Map<UpdateCinemaDTO>(cinema);

            patch.ApplyTo(updateCinema, ModelState);

            if (!TryValidateModel(updateCinema))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(updateCinema, cinema);
            _context.SaveChanges();

            return NoContent();
        }*/

        /// <summary>
        /// Exclusão do cinema referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do cinema a ser excluído</param>
        /// <response code="200">Confirma a exclusão do cinema</response>
        /// <response code="404">Caso não exista nenhum cinema com o código passado</response>

        [HttpDelete("{id}")]
        public IActionResult DeleteCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }

            _context.Remove(cinema);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

using AutoMapper;
using FilmsAPI.Helpers.Enums;
using FilmsAPI.Models;
using FilmsAPI.Data;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Profiles;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        private FilmContext _filmContext;
        private IMapper _mapper;

        public FilmController(FilmContext filmContext, IMapper mapper)
        {
            _filmContext = filmContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Criação de novos filmes (podendo ser passado um ou N filmes de uma única vez)
        /// </summary>
        /// <param name="filmDTO"></param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        /// <response code="404">Caso a tentativa de inserção seja feita com a falta de dados obrigatórios</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddFilm([FromBody] CreateFilmDTO filmDTO)
        {
            Film film = _mapper.Map<Film>(filmDTO);

            _filmContext.Films.Add(film);  
            _filmContext.SaveChanges();

            return CreatedAtAction (nameof(ReturnFilmsForId), new { id = _filmContext.ContextId }, film);
        }

        /// <summary>
        /// Retorna todos os filmes cadastrados
        /// </summary>
        /// <param name="skip">Quantidade máxima de registros a serem buscados</param>
        /// <param name="take">Posição em que deseja iniciar a busca dos registros com base nos IDs dos filmes</param>
        /// <response code="200">Retorna os filmes esperados</response>
        /// <response code="404">Caso não exista nenhum filme</response>

        [HttpGet]
        public IEnumerable<ReadFilmDTO> ReturnFilms([FromQuery] int skip = 0, [FromQuery] int take = 5)
        {
            return _mapper.Map<List<ReadFilmDTO>>(_filmContext.Films.Skip(skip).Take(take).ToList());
        }

        /// <summary>
        /// Retorna o filme cadastrado conforme o Id utilizado como parâmetro
        /// </summary>
        /// <param name="id">Código do filme</param>
        /// <response code="200">Retorna o filme esperado</response>
        /// <response code="404">Caso não exista nenhum filme com o código passado</response>

        [HttpGet("{id}")]
        public IActionResult ReturnFilmsForId(int id)
        {
            var film = _filmContext.Films.FirstOrDefault(films => films.IdFilm == id);

            if(film == null)
            {
                return NotFound();
            }

            var filmDTO = _mapper.Map<ReadFilmDTO>(film);

            return Ok(filmDTO);
        }

        /// <summary>
        /// Atualiza o filme referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do filme</param>
        /// <param name="filmDTO"></param>
        /// <response code="204">Confirma a atualização do filme</response>
        /// <response code="404">Caso deixe de preencher algum campo obrigatório para atualização</response>

        [HttpPut("{id}")]
        public IActionResult UpdateFilm (int id, [FromBody] UpdateFilmDTO filmDTO)
        {
            var film = _filmContext.Films.FirstOrDefault(film => film.IdFilm == id);

            if(film == null)
            {
                return NotFound();
            }

            _mapper.Map(filmDTO, film);
            _filmContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Atualiza o filme referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do filme</param>
        /// <param name="patch"></param>
        /// <response code="204">Confirma a atualização do filme</response>

        [HttpPatch("{id}")]
        public IActionResult UpdateFilmPatch (int id, JsonPatchDocument<UpdateFilmDTO> patch)
        {
            var film = _filmContext.Films.FirstOrDefault(film => film.IdFilm == id);

            if (film == null)
            {
                return NotFound();
            }

            var updateFilm = _mapper.Map<UpdateFilmDTO>(film);

            patch.ApplyTo(updateFilm, ModelState);

            if (!TryValidateModel(updateFilm))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(updateFilm, film);
            _filmContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Exclusão do filme referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do filme a ser excluído</param>
        /// <response code="200">Confirma a exclusão do filme</response>
        /// <response code="404">Caso não exista nenhum filme com o código passado</response>

        [HttpDelete("{id}")]
        public IActionResult DeleteFilm(int id)
        {
            var film = _filmContext.Films.FirstOrDefault(film => film.IdFilm == id);

            if (film == null)
            {
                return NotFound();
            }

            _filmContext.Remove(film);
            _filmContext.SaveChanges();

            return NoContent();
        }
    }
}

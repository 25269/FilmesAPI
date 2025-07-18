using AutoMapper;
using FilmesAPI.Helpers.Enums;
using FilmesAPI.Models;
using FilmsAPI.Data;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Models;
using FilmsAPI.Profiles;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
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

        [HttpPost]
        public IActionResult AddFilm([FromBody] CreateFilmDTO filmDTO)
        {
            Film film = _mapper.Map<Film>(filmDTO);

            _filmContext.Films.Add(film);  
            _filmContext.SaveChanges();

            return CreatedAtAction (nameof(ReturnFilmsForId), new { id = _filmContext.ContextId }, film);
        }

        [HttpGet]
        public IEnumerable<ReadFilmDTO> ReturnFilms([FromQuery] int skip = 0, [FromQuery] int take = 5)
        {
            return _mapper.Map<List<ReadFilmDTO>>(_filmContext.Films.Skip(skip).Take(take));
        }

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

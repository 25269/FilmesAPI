using FilmesAPI.Models;
using FilmsAPI.Controllers;
using FilmsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        private static List<Film> films = new List<Film>();
        private static SequenceIdController sequenceIdController;
        private int parmId;

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Film film)
        {
            sequenceIdController  = new SequenceIdController();

            if (films.Count > 0)
            {
                parmId = films.Max(film => film.IdFilm);
                film.IdFilm = sequenceIdController.SequenceIdIncrement(parmId);
                films.Add(film);
            }
            else
            {
                film.IdFilm = sequenceIdController.SequenceIdIncrement(parmId);
                films.Add(film);
            }

            //Console.WriteLine($"Título: {film.Titulo} - Gênero: {film.Genero.ToString()}");

            return CreatedAtAction (nameof(ReturnFilmsForId), new { id = film.IdFilm }, film);
        }

        [HttpGet]
        public IEnumerable<Film> ReturnFilms([FromQuery] int skip = 0, [FromQuery] int take = 5)
        {
            return films.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult ReturnFilmsForId(int id)
        {
            var film = films.FirstOrDefault(films => films.IdFilm == id);

            if(film == null)
            {
                return NotFound();
            }

            return Ok(film);
        }
    }
}

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
        public void AdicionaFilme([FromBody] Film film)
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

            Console.WriteLine($"Título: {film.Titulo} - Gênero: {film.Genero.ToString()}");
        }

        [HttpGet]
        public List<Film> ReturnFilms()
        {
            return films;
        }
    }
}

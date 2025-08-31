using AutoMapper;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Data;
using FilmsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        private FilmContext _context;
        private IMapper _mapper;

        public AddressController(FilmContext filmContext, IMapper mapper)
        {
            _context = filmContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Criação de novos endereços (podendo ser passado um ou N endereços de uma única vez)
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        /// <response code="404">Caso a tentativa de inserção seja feita com a falta de dados obrigatórios</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddAddress([FromBody] CreateAddressDTO addressDTO)
        {
            Address address = _mapper.Map<Address>(addressDTO);

            _context.Addresses.Add(address);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ReturnAddressForId), new { id = address.Id }, addressDTO);
        }

        /// <summary>
        /// Retorna todos os endereços cadastrados
        /// </summary>
        /// <param name="skip">Quantidade máxima de registros a serem buscados</param>
        /// <param name="take">Posição em que deseja iniciar a busca dos registros com base nos IDs dos endereços</param>
        /// <response code="200">Retorna os endereços esperados</response>
        /// <response code="404">Caso não exista nenhum endereço</response>

        [HttpGet]
        public IEnumerable<ReadAddressDTO> ReadAddresses([FromQuery] int skip = 0, [FromQuery] int take = 5)
        {
            return _mapper.Map<List<ReadAddressDTO>>(_context.Addresses.Skip(skip).Take(take));
        }

        /// <summary>
        /// Retorna o endereço cadastrado conforme o Id utilizado como parâmetro
        /// </summary>
        /// <param name="id">Código do endereço</param>
        /// <response code="200">Retorna o endereço esperado</response>
        /// <response code="404">Caso não exista nenhum endereço com o código passado</response>

        [HttpGet("{id}")]
        public IActionResult ReturnAddressForId(int id)
        {
            Address address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address != null)
            {
                ReadAddressDTO addressDTO = _mapper.Map<ReadAddressDTO>(address);
                return Ok(addressDTO);
            }

            return NotFound();
        }

        /// <summary>
        /// Atualiza o endereço referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do endereço</param>
        /// <param name="addressDTO"></param>
        /// <response code="204">Confirma a atualização do endereço</response>
        /// <response code="404">Caso deixe de preencher algum campo obrigatório para atualização</response>

        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDTO addressDTO)
        {
            Address address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null)
            {
                return NotFound();
            }

            _mapper.Map(addressDTO, address);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Atualiza o endereço referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do endereço</param>
        /// <param name="patch"></param>
        /// <response code="204">Confirma a atualização do endereços</response>

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
        /// Exclusão do endereço referente ao Id passado como parâmetro
        /// </summary>
        /// <param name="id">Código do endereço a ser excluído</param>
        /// <response code="200">Confirma a exclusão do endereço</response>
        /// <response code="404">Caso não exista nenhum endereço com o código passado</response>

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            Address address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address == null)
            {
                return NotFound();
            }

            _context.Remove(address);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

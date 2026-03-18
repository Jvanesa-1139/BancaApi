using BancaApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq;
using System.Xml;

namespace BancaApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CreditosController : ControllerBase
    {

        private readonly ICreditoRepository _repo;

        public CreditosController(ICreditoRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var creditos = await _repo.GeAllAsync();

            return Ok(creditos);
        }

        [HttpGet("{id}")]
       public async Task<IActionResult> GetByID(int id)
        {
            var existe = await _repo.GetByIdAsync(id);
                                                 
              if (existe== null)
            {
                return NotFound("Credito no encontraro");
            }

            return Ok(existe);
        }

        [HttpPost]
        public async Task<IActionResult> CreacionCredito([FromBody] CreateCreditoDto dto)
        {
            if (dto == null)
                return BadRequest("Formulario vacio");
                await _repo.CreateAsync(dto);
                return Ok("Cliente Creado correctamente");
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizacionCredito([FromBody] UpdateDto dto, int id)
        {
            var existe = await _repo.UpdateAsync(id,dto);
            if (!existe)
                return BadRequest("Credito no existe");
          
            return Ok("Se actualizo correctamente");
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> EliminacionCredito(int id)
        {
            var existe = await _repo.DeleteAsync(id);

            if (!existe)
                return NotFound("Credito no existe");

            return Ok($"credito N°: {id} eliminado");
                
        }
         
    }


}
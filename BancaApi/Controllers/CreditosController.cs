using BancaApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq;
using System.Xml;

namespace BancaApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class CreditosController : ControllerBase
    {

        private readonly ICreditoRepository _repo;

        public CreditosController(ICreditoRepository repo)
        {
            _repo = repo;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var creditos = await _repo.GeAllAsync();

            return Ok(creditos);
        }

        [Authorize]
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

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreacionCredito([FromBody] CreateCreditoDto dto)
        {
            if (dto == null)
                return BadRequest("Formulario vacio");
                await _repo.CreateAsync(dto);
                return Ok("Cliente Creado correctamente");
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizacionCredito([FromBody] UpdateDto dto, int id)
        {
            var existe = await _repo.UpdateAsync(id,dto);
            if (!existe)
                return BadRequest("Credito no existe");
          
            return Ok("Se actualizo correctamente");
        }

        [Authorize(Roles ="Admin")]
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
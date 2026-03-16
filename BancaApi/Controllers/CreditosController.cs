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

        private readonly BancaContext _bancaContext;

        public CreditosController(BancaContext bancaContext)
        {
            _bancaContext = bancaContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var creditos = await _bancaContext.Creditos
                           .Include(c => c.Cliente)
                           .Select(c => new CreditoDto
                           {
                               Id = c.Id,
                               Titular = c.Titular,
                               MontoAprobado = c.MontoAprobado,
                               PlazoMeses = c.PlazoMeses,
                               NombreCliente = c.Cliente.Nombre
                           })
                           .ToListAsync();

            return Ok(creditos);
        }

        [HttpGet("{id}")]
       public async Task<IActionResult> GetByID(int id)
        {
            var existe = await _bancaContext.Creditos
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.Id == id);
                                                 
              if (existe== null)
            {
                return NotFound("Credito no encontraro");
            }
            var dto = new CreditoDto
            {
                Id = existe.Id,
                Titular = existe.Titular,
                MontoAprobado = existe.MontoAprobado,
                PlazoMeses = existe.PlazoMeses,
                NombreCliente = existe.Cliente.Nombre
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreacionCredito([FromBody] CreateCreditoDto dto)
        {
            if (dto == null)
                return BadRequest("Formulario vacio");
   
                var nuevoCredito = new Credito
                {
                    Titular = dto.Titular,
                    MontoAprobado = dto.MontoAprobado,
                    PlazoMeses = dto.PlazoMeses,
                    TasaAnual = dto.TasaAnual,
                    ClienteId = dto.ClienteId
                };

                _bancaContext.Creditos.Add(nuevoCredito);
                 await _bancaContext.SaveChangesAsync();
            

                return Ok("Cliente Creado correctamente");
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizacionCredito([FromBody] UpdateDto dto, int id)
        {
            var existe = await _bancaContext.Creditos
                        .Include(c => c.Cliente)
                        .FirstOrDefaultAsync(x => x.Id == id);
            if(existe == null)
                return NotFound("No existe credito ingresado");

                existe.Titular = dto.Titular;
                existe.MontoAprobado = dto.MontoAprobado;
                existe.PlazoMeses = dto.PlazoMeses;
                existe.TasaAnual = dto.TasaAnual;
                existe.ClienteId = dto.ClienteId;

            await _bancaContext.SaveChangesAsync();

            return Ok("Se actualizo correctamente");
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> EliminacionCredito(int id)
        {
            var existe = await _bancaContext.Creditos.FirstOrDefaultAsync(c => c.Id == id);

            if (existe == null)
                return NotFound("Credito no existe");

             _bancaContext.Remove(existe);

            return Ok($"credito N°: {id} eliminado");
                
        }
         
    }


}
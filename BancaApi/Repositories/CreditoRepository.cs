
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancaApi.Repositories
{
    public class CreditoRepository : ICreditoRepository
    {
        private readonly BancaContext _bancaContext;

        public CreditoRepository(BancaContext bancaContext)
        {
            _bancaContext = bancaContext;
        }

        public async Task<IEnumerable<CreditoDto>> GeAllAsync()
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
                                } )
                                .ToListAsync();

            return creditos;
        }
        public async Task<bool> CreateAsync(CreateCreditoDto createDto)
        {
            if (createDto == null)
                return false;

            Credito creditoNuevo = new Credito
            {
                Titular = createDto.Titular,
                MontoAprobado = createDto.MontoAprobado,
                PlazoMeses = createDto.PlazoMeses,
                TasaAnual = createDto.TasaAnual,
                ClienteId = createDto.ClienteId
            };

            _bancaContext.Creditos.Add( creditoNuevo );
            await _bancaContext.SaveChangesAsync();

            return true;
             
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existe = await _bancaContext.Creditos.FirstOrDefaultAsync(c=> c.Id==id);

            if (existe == null)
                return false;

              _bancaContext.Creditos.Remove(existe);
            await _bancaContext.SaveChangesAsync();
            return true;
        }

     

        public async Task<CreditoDto?> GetByIdAsync(int id)
        {
            var existe = await _bancaContext.Creditos
                                .Include(c=>c.Cliente)
                                .Select(c=> new CreditoDto
                                {
                                    Id = c.Id,
                                    Titular = c.Titular,
                                    MontoAprobado = c.MontoAprobado,
                                    PlazoMeses= c.PlazoMeses,
                                    NombreCliente = c.Cliente.Nombre
                                })
                                .FirstOrDefaultAsync(c => c.Id == id);
            if (existe == null)
                throw null;
            return existe;

        }

        public async Task<bool> UpdateAsync(int id, UpdateDto updateDto)
        {
            var existe = await _bancaContext.Creditos
                .Include(c=>c.Cliente)
                .FirstOrDefaultAsync(c => c.Id==id);

            if (existe == null)
                return false;

            existe.Titular = updateDto.Titular;
            existe.MontoAprobado = updateDto.MontoAprobado;
            existe.PlazoMeses = updateDto.PlazoMeses;
            existe.TasaAnual = updateDto.TasaAnual;
            existe.ClienteId = updateDto.ClienteId;

            await _bancaContext.SaveChangesAsync();
            return true;

        }
    }
}

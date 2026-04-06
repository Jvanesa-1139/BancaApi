using BancaApi;
using BancaApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace BancaApi.Tests
{
    public class CreditoTests
    {
        private BancaContext CrearContextoEnMemoria()
        {
            var options = new DbContextOptionsBuilder<BancaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new BancaContext(options);
        }

        [Fact]
        public async Task GetAllAsync_DebeRetornarTodosLosCreditos()
        {
            // ARRANGE
            var context = CrearContextoEnMemoria();

            context.Clientes.Add(new Cliente
            {
                Id = 1,
                Nombre = "Jessica Narcizo"
            });

            context.Creditos.AddRange(
                new Credito { Id = 1, Titular = "Jessica", MontoAprobado = 15000, PlazoMeses = 24, TasaAnual = 0.28, ClienteId = 1 },
                new Credito { Id = 2, Titular = "Juan", MontoAprobado = 8000, PlazoMeses = 12, TasaAnual = 0.25, ClienteId = 1 }
            );

            await context.SaveChangesAsync();

            var repo = new CreditoRepository(context);

            // ACT
            var creditos = await repo.GeAllAsync();

            // ASSERT
            Assert.NotNull(creditos);
            Assert.Equal(2, creditos.Count());
        }

        [Fact]
        public async Task GetAllByID_DebeRetornarElCreditoPorID()
        {
            var contexto = CrearContextoEnMemoria();

            contexto.Clientes.Add(new Cliente
            {
                Id = 2,
                Nombre = "Mariana"
            });
            contexto.Creditos.Add(new Credito
            {
                Id = 1,
                Titular = "Jessica",
                MontoAprobado = 15000,
                PlazoMeses = 24,
                TasaAnual = 0.28,
                ClienteId = 1
            });

        await contexto.SaveChangesAsync();

            var repo = new CreditoRepository(contexto);

            var credito = repo.GetByIdAsync(1);

            Assert.NotNull(credito);
            Assert.Equal(1, credito.Id);


        }


        [Fact]
        public async Task GetAllById_DebeRetornarNull_SICreditoNoExiste()
        {
            var contexto = CrearContextoEnMemoria();
            contexto.Clientes.Add(new Cliente
            {
                Id = 1,
                Nombre = "Mariana"
            });
            contexto.Creditos.Add(new Credito
            {
                Id = 1,
                Titular = "Mariana",
                MontoAprobado = 15000,
                PlazoMeses = 24,
                TasaAnual = 0.28,
                ClienteId = 1
            });

            await contexto.SaveChangesAsync();
            var repor = new CreditoRepository(contexto);

            var credito = await repor.GetByIdAsync(1);

          
                Assert.Null(credito);

        }




        [Fact]
        public async Task CreateAsync_DebeRetornartrueSiTieneDatosValidos()
        {
            var context = CrearContextoEnMemoria();

            var repo = new CreditoRepository(context);
  

            var CreditoDto = new CreateCreditoDto
            {
                Titular = "Mariana",
                MontoAprobado = 120000,
                PlazoMeses = 450,
                ClienteId = 1

            };

            var resultado = await repo.CreateAsync(CreditoDto);

            Assert.True(resultado);
        }

        [Fact]
        public async Task CreateAsync_DebeRetornarFalseCuandoDtoEsNUll()
        {
            var context = CrearContextoEnMemoria();

            var repo = new CreditoRepository(context);


            var resultado = await repo.CreateAsync(null);

            Assert.False(resultado);
        }

        //"CreateAsync debe retornar true cuando los datos son válidos"




    }
}
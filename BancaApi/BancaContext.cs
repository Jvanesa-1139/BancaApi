namespace BancaApi
{
    using Microsoft.EntityFrameworkCore;

    public class BancaContext : DbContext
    {
        public BancaContext(DbContextOptions<BancaContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}

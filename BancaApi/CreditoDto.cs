namespace BancaApi
{
    public class CreditoDto
    {
        public int Id { get; set; }
        public string? Titular { get; set; }
        public double MontoAprobado { get; set; }
        public int PlazoMeses { get; set; }
        public string? NombreCliente { get; set; }  // ← solo el nombre, sin ciclo
    }
}

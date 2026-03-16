namespace BancaApi
{
    public class UpdateDto
    {
        public string? Titular { get; set; }
        public double MontoAprobado { get; set; }
        public int PlazoMeses { get; set; }
        public double TasaAnual { get; set; }
        public string? NombreCliente { get; set; }
        public int ClienteId { get; set; }
    }
}

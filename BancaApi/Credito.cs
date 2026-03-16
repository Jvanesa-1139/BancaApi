namespace BancaApi
{
    public class Credito
    {
        public int Id { get; set; }
        public string? Titular { get; set; }
        public double MontoAprobado { get; set; }
        public int PlazoMeses { get; set; }
        public double TasaAnual { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}

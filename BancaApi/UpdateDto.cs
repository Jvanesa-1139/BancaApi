using System.ComponentModel.DataAnnotations;

namespace BancaApi
{
    public class UpdateDto
    {
        [Required]
        [StringLength(100, ErrorMessage ="El titular no puede superar de 100 caracteres")]
        public string? Titular { get; set; }
        [Required]
        [Range(1,10000, ErrorMessage ="El monto debe estar entre 1 y 10000")]
        public double MontoAprobado { get; set; }
        [Required]
        [Range(1,360,ErrorMessage ="El plazo debe estar entre 1 y 360")]
        public int PlazoMeses { get; set; }
        [Required]
        [Range(0.01,1,ErrorMessage ="la tasa debe estar entre 0.01 y 1")]
        public double TasaAnual { get; set; }
        [Required]
        [StringLength(100,ErrorMessage ="El nombre del cliente no debe pasar de 100 caracteres")]
        public string? NombreCliente { get; set; }
        public int ClienteId { get; set; }
    }
}

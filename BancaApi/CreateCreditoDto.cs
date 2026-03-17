using System.ComponentModel.DataAnnotations;

namespace BancaApi
{
    public class CreateCreditoDto
    {
        [Required (ErrorMessage ="El titular es obligatorio")]
        [StringLength(100,ErrorMessage ="No puede supererar 100 caracteres")]
        public string? Titular {  get; set; }
        [Required]
        [Range(1,10000,ErrorMessage ="EL monto debe estar entre 1 y 10000.00")]
        public double MontoAprobado { get; set; }
        [Required]
        [Range(1,360,ErrorMessage ="El plazo debe estar entre 1 y 360")]
        public int PlazoMeses { get; set; }
        [Required]
        [Range(0.01,1, ErrorMessage ="La tasa debe estar entre 0.01 y 1")]
        public double TasaAnual {  get; set; }
        public int ClienteId { get; set; }
    }
}

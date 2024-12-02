using System.ComponentModel.DataAnnotations;

namespace FBQ.Salud_Domain.Dtos
{
    public class PacienteDto
    {
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string Sexo { get; set; }
        [Required]
        public string DNI { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string DirecionNumero { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Foto { get; set; }
    }
}

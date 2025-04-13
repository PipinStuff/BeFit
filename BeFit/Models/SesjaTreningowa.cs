using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class SesjaTreningowa
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fecha de inicio")]
        public DateTime DataRozpocz�cia { get; set; }

        [Required]
        [Display(Name = "Fecha de finalizaci�n")]
        public DateTime DataZako�czenia { get; set; }

        [Display(Name = "ID del usuario")]
        public string? IdU�ytkownika { get; set; }

        [Display(Name = "Ejercicios realizados")]
        public ICollection<Wykonane�wiczenie> Wykonane�wiczenia { get; set; } = new List<Wykonane�wiczenie>();
    }
}

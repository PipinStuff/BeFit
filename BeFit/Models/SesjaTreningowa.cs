using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class SesjaTreningowa
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fecha de inicio")]
        public DateTime DataRozpoczêcia { get; set; }

        [Required]
        [Display(Name = "Fecha de finalización")]
        public DateTime DataZakoñczenia { get; set; }

        [Display(Name = "ID del usuario")]
        public string? IdU¿ytkownika { get; set; }

        [Display(Name = "Ejercicios realizados")]
        public ICollection<WykonaneÆwiczenie> WykonaneÆwiczenia { get; set; } = new List<WykonaneÆwiczenie>();
    }
}

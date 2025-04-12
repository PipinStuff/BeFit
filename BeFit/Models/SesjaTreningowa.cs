using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class SesjaTreningowa
    {
        public int Id { get; set; }
        [Required]
        public DateTime DataRozpoczêcia { get; set; }
        [Required]
        public DateTime DataZakoñczenia { get; set; }
 
        public string? IdU¿ytkownika { get; set; }
        public ICollection<WykonaneÆwiczenie> WykonaneÆwiczenia { get; set; } = new List<WykonaneÆwiczenie>();

    }
}

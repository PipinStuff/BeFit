using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Wykonane∆wiczenie
    {
        public int Id { get; set; }

        [Required]
        public int IdSesjiTreningowej { get; set; }

        [Required]
        public int IdTypu∆wiczenia { get; set; }

        [Range(0, 1000)]
        public double Obciπøenie { get; set; }

        [Range(1, 100)]
        public int LiczbaSerii { get; set; }

        [Range(1, 100)]
        public int PowtÛrzenia { get; set; }

        public SesjaTreningowa SesjaTreningowa { get; set; }
        public Typ∆wiczenia Typ∆wiczenia { get; set; }
        public string Nazwa∆wiczenia => Typ∆wiczenia?.Nazwa;
    }
}

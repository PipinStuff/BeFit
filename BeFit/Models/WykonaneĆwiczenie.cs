using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Wykonane�wiczenie
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "ID de la sesi�n de entrenamiento")]
        public int IdSesjiTreningowej { get; set; }

        [Required]
        [Display(Name = "ID del tipo de ejercicio")]
        public int IdTypu�wiczenia { get; set; }

        [Range(0, 1000)]
        [Display(Name = "Carga (kg)")]
        public double Obci��enie { get; set; }

        [Range(1, 100)]
        [Display(Name = "N�mero de series")]
        public int LiczbaSerii { get; set; }

        [Range(1, 100)]
        [Display(Name = "Repeticiones")]
        public int Powt�rzenia { get; set; }

        [Display(Name = "Sesi�n de entrenamiento")]
        public SesjaTreningowa SesjaTreningowa { get; set; }

        [Display(Name = "Tipo de ejercicio")]
        public Typ�wiczenia Typ�wiczenia { get; set; }

        [Display(Name = "Nombre del ejercicio")]
        public string Nazwa�wiczenia => Typ�wiczenia?.Nazwa;
    }
}

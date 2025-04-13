using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Statystyki
    {
        [Display(Name = "Tipo de ejercicio")]
        public string TypĆwiczenia { get; set; }

        [Display(Name = "Número de ejecuciones")]
        public int LiczbaWykonań { get; set; }

        [Display(Name = "Número total de repeticiones")]
        public int ŁącznaLiczbaPowtórzeń { get; set; }

        [Display(Name = "Carga promedio (kg)")]
        public double ŚrednieObciążenie { get; set; }

        [Display(Name = "Carga máxima (kg)")]
        public double MaksymalneObciążenie { get; set; }
    }
}

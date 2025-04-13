using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Typ∆wiczenia
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre del tipo de ejercicio")]
        public string Nazwa { get; set; }
    }
}

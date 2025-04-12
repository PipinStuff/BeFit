using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Typ∆wiczenia
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nazwa { get; set; }
    }
}

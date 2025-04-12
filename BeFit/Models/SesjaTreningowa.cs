using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class SesjaTreningowa
    {
        public int Id { get; set; }
        [Required]
        public DateTime DataRozpocz�cia { get; set; }
        [Required]
        public DateTime DataZako�czenia { get; set; }
        public string IdU�ytkownika { get; set; }
        public ICollection<Wykonane�wiczenie> Wykonane�wiczenia { get; set; } = new List<Wykonane�wiczenie>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataZako�czenia <= DataRozpocz�cia)
            {
                yield return new ValidationResult("Data zako�czenia musi by� p�niejsza ni� data rozpocz�cia.", new[] { nameof(DataZako�czenia) });
            }
        }
    }
}

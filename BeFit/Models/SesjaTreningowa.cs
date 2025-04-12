using System;
using System.Collections.Generic;
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
        public string IdU¿ytkownika { get; set; }
        public ICollection<WykonaneÆwiczenie> WykonaneÆwiczenia { get; set; } = new List<WykonaneÆwiczenie>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataZakoñczenia <= DataRozpoczêcia)
            {
                yield return new ValidationResult("Data zakoñczenia musi byæ póŸniejsza ni¿ data rozpoczêcia.", new[] { nameof(DataZakoñczenia) });
            }
        }
    }
}

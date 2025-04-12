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
        [Required]
        public string IdU�ytkownika { get; set; }
        public U�ytkownik U�ytkownik { get; set; }
        public ICollection<Wykonane�wiczenie> Wykonane�wiczenia { get; set; } = new List<Wykonane�wiczenie>();
    }
}

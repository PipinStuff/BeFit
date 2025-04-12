using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BeFit.Models
{
    public class U�ytkownik : IdentityUser
    {
        public string? Imi� { get; set; }
        public string? Nazwisko { get; set; }
        public ICollection<SesjaTreningowa> SesjeTreningowe { get; set; } = new List<SesjaTreningowa>();
    }
}
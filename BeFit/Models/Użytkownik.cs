using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BeFit.Models
{
    public class U¿ytkownik : IdentityUser
    {
        public string? Imiê { get; set; }
        public string? Nazwisko { get; set; }
        public ICollection<SesjaTreningowa> SesjeTreningowe { get; set; } = new List<SesjaTreningowa>();
    }
}
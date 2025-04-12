using BeFit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext<Użytkownik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TypĆwiczenia> TypyĆwiczeń { get; set; }
        public DbSet<SesjaTreningowa> SesjeTreningowe { get; set; }
        public DbSet<WykonaneĆwiczenie> WykonaneĆwiczenia { get; set; }
    }

}

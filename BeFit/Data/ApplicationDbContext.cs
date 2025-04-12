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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TypĆwiczenia>().HasData(
                new TypĆwiczenia { Id = 1, Nazwa = "Przysiady" },
                new TypĆwiczenia { Id = 2, Nazwa = "Martwy Ciąg" },
                new TypĆwiczenia { Id = 3, Nazwa = "Pajacyki" }
            );
        }
    }
}

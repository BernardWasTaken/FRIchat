using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FRIchat.Models;

namespace FRIchat.Data
{
    public class FRIchatContext : IdentityDbContext<Uporabnik>
    {
        public FRIchatContext (DbContextOptions<FRIchatContext> options)
            : base(options)
        {
            
        }

        public DbSet<FRIchat.Models.Uporabnik> Uporabnik { get; set; } = default!;
        public DbSet<FRIchat.Models.Odgovor> Odgovor { get; set; } = default!;
        public DbSet<FRIchat.Models.Objava> Objava { get; set; } = default!;
        public DbSet<FRIchat.Models.OdgovorObjava> OdgovorObjava { get; set; } = default!;
        public DbSet<FRIchat.Models.Predmet> Predmet { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Uporabnik>().ToTable("Uporabnik");
            builder.Entity<Odgovor>().ToTable("Odgovor");
            builder.Entity<Objava>().ToTable("Objava");
            builder.Entity<OdgovorObjava>().ToTable("OdgovorObjava");
            builder.Entity<Predmet>().ToTable("Predmet");

            builder.Entity<Predmet>().HasData(
                new Predmet { Id=1, Ime = "Programiranje 1", Predavatelj = "Janez Demšar", Letnik = 1 },
                new Predmet { Id=2, Ime = "Podatkovne Baze", Predavatelj = "Matjaž Kukar", Letnik = 1 },
                new Predmet { Id=3, Ime = "Računalniška Arhitektura", Predavatelj = "Robert Rozman", Letnik = 1 },
                
                new Predmet { Id=4,Ime = "Informacijski sistemi", Predavatelj = "Damjan Vavpotič", Letnik = 2 },
                new Predmet { Id=5, Ime = "Razvoj Informacijskih Sistemov", Predavatelj = "Alenka Kavčič", Letnik = 2 },
                new Predmet { Id=6, Ime = "Vhodno-Izhodne Naprave", Predavatelj = "Robert Rozman", Letnik = 2 },
                 
                new Predmet { Id=7, Ime = "Praksa", Predavatelj = "/", Letnik = 3 }
                
                
                );

        }
    }
}

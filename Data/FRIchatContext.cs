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
        }
    }
}

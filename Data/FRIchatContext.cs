using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FRIchat.Models;

namespace FRIchat.Data
{
    public class FRIchatContext : DbContext
    {
        public FRIchatContext (DbContextOptions<FRIchatContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<FRIchat.Models.Uporabnik> Uporabnik { get; set; } = default!;
        public DbSet<FRIchat.Models.Odgovor> Odgovor { get; set; } = default!;
        public DbSet<FRIchat.Models.Objava> Objava { get; set; } = default!;
        public DbSet<FRIchat.Models.OdgovorObjava> OdgovorObjava { get; set; } = default!;
        public DbSet<FRIchat.Models.Predmet> Predmet { get; set; } = default!;
    }
}

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
    }
}

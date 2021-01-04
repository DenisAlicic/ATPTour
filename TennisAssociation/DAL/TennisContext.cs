using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisAssociation.Models;

namespace TennisAssociation.DAL
{
    public class TennisContext : DbContext 
    {
        public TennisContext(DbContextOptions options) : base(options)
            {}

        public DbSet<Player> Players { get; set; }

    }
}

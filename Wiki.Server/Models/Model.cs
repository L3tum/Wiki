using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiki.Shared;

namespace Wiki.Server.Models
{
    public class Model : DbContext
    {
        public Model(DbContextOptions<Model> options) : base(options)
        {

        }

        public DbSet<WikiEntry> WikiEntries { get; set; }
    }
}

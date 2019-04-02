using SeguridadApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    class ApplicationDbContext : DbContext
    {
        
      public DbSet<Agente> Agentes { get; set; }
    }
}

using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
namespace Repository_CodeFirst
{
    public class ProiectContext : DbContext, IProiectDbContext
    {
        public ProiectContext() : base("name=ProiectDbConnection")
        { }
        public virtual DbSet<Abonatii> Abonatiis { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<TipAbonament> TipAbonaments { get; set; }

    }
}

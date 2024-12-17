

using Microsoft.EntityFrameworkCore;
using WebAPICore.Models;

namespace WebAPICore.Data
{
    public class ProiectContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ProiectContext(DbContextOptions<ProiectContext> options) : base(options)
        { }

        public virtual Microsoft.EntityFrameworkCore.DbSet<Models.Abonatii>? Abonatiis { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<Models.Client>? Clients { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<Models.TipAbonament>? TipAbonaments { get; set; }
        
    }
}

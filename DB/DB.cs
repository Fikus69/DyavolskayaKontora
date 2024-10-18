using DyavolskayaKontora.Model;
using Microsoft.EntityFrameworkCore;

namespace DyavolskayaKontora.DB
{
    public class DB : DbContext 
    {
        public DB() { }

        public DbSet<Devil> Sotrudneki { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet <Disposal> Disposals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

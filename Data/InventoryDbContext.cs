using InventoryManagement_api.Models.Design;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement_api.Data
{
    public class InventoryDbContext:DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<rawMaterials> rawMaterials { get; set; }

    }
}

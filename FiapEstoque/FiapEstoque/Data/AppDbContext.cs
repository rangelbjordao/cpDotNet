using FiapEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace FiapEstoque.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Item> Itens { get; set; }
    }
}
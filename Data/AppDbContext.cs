using Microsoft.EntityFrameworkCore;
using lista.Models;

namespace lista.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ListaModel> Listas { get; set; }
    }
}
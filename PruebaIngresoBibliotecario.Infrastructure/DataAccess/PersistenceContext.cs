using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaIngresoBibliotecario.Domain.Entities;

namespace PruebaIngresoBibliotecario.Infrastructure.DataAccess
{
    public class PersistenceContext : DbContext
    {
        private readonly IConfiguration Config;

        public virtual DbSet<Loan> Loan { get; set; }

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            Config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Config.GetValue<string>("Biblioteca"));
            base.OnModelCreating(modelBuilder);
        }
    }
}

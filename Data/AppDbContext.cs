using Microsoft.EntityFrameworkCore;
using Tarea3_GestionEmpleados.Models;

namespace Tarea3_GestionEmpleados.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
    }
}

}



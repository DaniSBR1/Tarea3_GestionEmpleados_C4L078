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

            // Seed de 10 empleados para demostración
            modelBuilder.Entity<Empleado>().HasData(
                new Empleado
                {
                    Id = 1,
                    Nombre = "Carlos",
                    Apellidos = "Ramírez Solano",
                    Departamento = "TI",
                    Salario = 1200000,
                    FechaIngreso = new DateTime(2020, 3, 15),
                    Activo = true
                },
                new Empleado
                {
                    Id = 2,
                    Nombre = "Ana",
                    Apellidos = "González Mora",
                    Departamento = "TI",
                    Salario = 1100000,
                    FechaIngreso = new DateTime(2021, 6, 1),
                    Activo = true
                },
                new Empleado
                {
                    Id = 3,
                    Nombre = "Laura",
                    Apellidos = "Vargas Pérez",
                    Departamento = "TI",
                    Salario = 980000,
                    FechaIngreso = new DateTime(2022, 1, 10),
                    Activo = true
                },
                new Empleado
                {
                    Id = 4,
                    Nombre = "Jorge",
                    Apellidos = "Méndez Castillo",
                    Departamento = "Finanzas",
                    Salario = 1500000,
                    FechaIngreso = new DateTime(2019, 8, 20),
                    Activo = true
                },
                new Empleado
                {
                    Id = 5,
                    Nombre = "María",
                    Apellidos = "Fernández López",
                    Departamento = "Finanzas",
                    Salario = 1350000,
                    FechaIngreso = new DateTime(2020, 11, 5),
                    Activo = true
                },
                new Empleado
                {
                    Id = 6,
                    Nombre = "Andrés",
                    Apellidos = "Jiménez Arias",
                    Departamento = "Recursos Humanos",
                    Salario = 900000,
                    FechaIngreso = new DateTime(2023, 2, 14),
                    Activo = true
                },
                new Empleado
                {
                    Id = 7,
                    Nombre = "Sofía",
                    Apellidos = "Castro Brenes",
                    Departamento = "Recursos Humanos",
                    Salario = 850000,
                    FechaIngreso = new DateTime(2023, 7, 3),
                    Activo = false
                },
                new Empleado
                {
                    Id = 8,
                    Nombre = "Diego",
                    Apellidos = "Alvarado Rojas",
                    Departamento = "Operaciones",
                    Salario = 760000,
                    FechaIngreso = new DateTime(2021, 4, 22),
                    Activo = true
                },
                new Empleado
                {
                    Id = 9,
                    Nombre = "Valeria",
                    Apellidos = "Herrera Núñez",
                    Departamento = "Operaciones",
                    Salario = 720000,
                    FechaIngreso = new DateTime(2022, 9, 18),
                    Activo = true
                },
                new Empleado
                {
                    Id = 10,
                    Nombre = "Roberto",
                    Apellidos = "Quesada Elizondo",
                    Departamento = "Gerencia",
                    Salario = 3500000,
                    FechaIngreso = new DateTime(2018, 1, 7),
                    Activo = true
                }
            );
        }
    }
}

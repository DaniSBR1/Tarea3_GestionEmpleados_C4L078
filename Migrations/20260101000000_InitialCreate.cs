using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tarea3_GestionEmpleados.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "Id", "Activo", "Apellidos", "Departamento", "FechaIngreso", "Nombre", "Salario" },
                values: new object[,]
                {
                    { 1,  true,  "Ramírez Solano",   "TI",                 new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos",  1200000m },
                    { 2,  true,  "González Mora",    "TI",                 new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),  "Ana",     1100000m },
                    { 3,  true,  "Vargas Pérez",     "TI",                 new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laura",   980000m  },
                    { 4,  true,  "Méndez Castillo",  "Finanzas",           new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jorge",   1500000m },
                    { 5,  true,  "Fernández López",  "Finanzas",           new DateTime(2020, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "María",   1350000m },
                    { 6,  true,  "Jiménez Arias",    "Recursos Humanos",   new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andrés",  900000m  },
                    { 7,  false, "Castro Brenes",    "Recursos Humanos",   new DateTime(2023, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),  "Sofía",   850000m  },
                    { 8,  true,  "Alvarado Rojas",   "Operaciones",        new DateTime(2021, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diego",   760000m  },
                    { 9,  true,  "Herrera Núñez",    "Operaciones",        new DateTime(2022, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Valeria", 720000m  },
                    { 10, true,  "Quesada Elizondo", "Gerencia",           new DateTime(2018, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),  "Roberto", 3500000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Empleados");
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tarea3_GestionEmpleados.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(80, ErrorMessage = "El nombre no puede superar 80 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden superar 100 caracteres.")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El departamento es obligatorio.")]
        [StringLength(100, ErrorMessage = "El departamento no puede superar 100 caracteres.")]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El salario es obligatorio.")]
        [Range(400000, 10000000, ErrorMessage = "El salario debe estar entre ₡400,000 y ₡10,000,000.")]
        [Display(Name = "Salario (₡)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        [Display(Name = "Fecha de Ingreso")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

        [NotMapped]
        public string NombreCompleto { get => $"{Nombre} {Apellidos}"; }
    }
}

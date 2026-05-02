using Tarea3_GestionEmpleados.Models;

namespace Tarea3_GestionEmpleados.ViewModels
{
    public class EmpleadosIndexViewModel
    {
        public IEnumerable<Empleado> Empleados { get; set; } = Enumerable.Empty<Empleado>();
        public string? Busqueda { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public int TamanoPagina { get; set; }

        public bool TienePaginaAnterior => PaginaActual > 1;
        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
    }
}

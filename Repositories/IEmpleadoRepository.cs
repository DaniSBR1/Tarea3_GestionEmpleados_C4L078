using Tarea3_GestionEmpleados.Models;

namespace Tarea3_GestionEmpleados.Repositories
{
    public interface IEmpleadoRepository
    {
        /// <summary>Retorna todos los empleados sin filtro ni paginación.</summary>
        Task<IEnumerable<Empleado>> ObtenerTodos();

        /// <summary>Retorna un empleado por su Id. Retorna null si no existe.</summary>
        Task<Empleado?> ObtenerPorId(int id);

        /// <summary>
        /// Filtra empleados cuyo Nombre, Apellidos o Departamento contengan el término.
        /// Case-insensitive.
        /// </summary>
        Task<IEnumerable<Empleado>> BuscarPorNombreODepartamento(string termino);

        /// <summary>
        /// Retorna la página solicitada aplicando el filtro de búsqueda opcional.
        /// Usa Skip/Take para paginación eficiente.
        /// </summary>
        Task<IEnumerable<Empleado>> ObtenerPaginado(int pagina, int tamano, string? busqueda);

        /// <summary>
        /// Cuenta el total de registros que coinciden con el filtro de búsqueda.
        /// Usa el mismo predicado que ObtenerPaginado.
        /// </summary>
        Task<int> ContarTotal(string? busqueda);

        /// <summary>Agrega un nuevo empleado a la base de datos.</summary>
        Task Agregar(Empleado empleado);

        /// <summary>Actualiza los datos de un empleado existente.</summary>
        Task Actualizar(Empleado empleado);

        /// <summary>Elimina físicamente un empleado (no usado en UI; baja lógica vía Activo).</summary>
        Task Eliminar(int id);
    }
}

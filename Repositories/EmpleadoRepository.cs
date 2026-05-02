using Microsoft.EntityFrameworkCore;
using Tarea3_GestionEmpleados.Data;
using Tarea3_GestionEmpleados.Models;

namespace Tarea3_GestionEmpleados.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly AppDbContext _context;

        public EmpleadoRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Empleado>> ObtenerTodos()
        {

            return await _context.Empleados
                .OrderBy(e => e.Id)
                .ThenBy(e => e.Nombre)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Empleado?> ObtenerPorId(int id)
        {
            return await _context.Empleados.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Empleado>> BuscarPorNombreODepartamento(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return await ObtenerTodos();

            var terminoLower = termino.Trim().ToLower();

            return await _context.Empleados
                .Where(e =>
                    e.Nombre.ToLower().Contains(terminoLower) ||
                    e.Apellidos.ToLower().Contains(terminoLower) ||
                    e.Departamento.ToLower().Contains(terminoLower))
                .OrderBy(e => e.Apellidos)
                .ThenBy(e => e.Nombre)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Empleado>> ObtenerPaginado(int pagina, int tamano, string? busqueda)
        {
            var query = AplicarFiltro(_context.Empleados.AsQueryable(), busqueda);

            return await query
                .OrderBy(e => e.Apellidos)
                .ThenBy(e => e.Nombre)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<int> ContarTotal(string? busqueda)
        {
            var query = AplicarFiltro(_context.Empleados.AsQueryable(), busqueda);
            return await query.CountAsync();
        }

        /// <inheritdoc/>
        public async Task Agregar(Empleado empleado)
        {
            await _context.Empleados.AddAsync(empleado);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task Actualizar(Empleado empleado)
        {
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task Eliminar(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado is not null)
            {
                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();
            }
        }

        // ─── Predicado compartido para búsqueda case-insensitive ──────────────────
        private static IQueryable<Empleado> AplicarFiltro(IQueryable<Empleado> query, string? busqueda)
        {
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var terminoLower = busqueda.Trim().ToLower();
                query = query.Where(e =>
                    e.Nombre.ToLower().Contains(terminoLower) ||
                    e.Apellidos.ToLower().Contains(terminoLower) ||
                    e.Departamento.ToLower().Contains(terminoLower));
            }
            return query;
        }
    }
}

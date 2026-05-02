using Microsoft.AspNetCore.Mvc;
using Tarea3_GestionEmpleados.Models;
using Tarea3_GestionEmpleados.Repositories;
using Tarea3_GestionEmpleados.ViewModels;

namespace Tarea3_GestionEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IEmpleadoRepository _repo;
        private const int TamanoPaginaDefault = 5;

        public EmpleadosController(IEmpleadoRepository repo)
        {
            _repo = repo;
        }

        // GET: /Empleados?busqueda=X&pagina=N
        public async Task<IActionResult> Index(string? busqueda, int pagina = 1)
        {
            const int tamano = TamanoPaginaDefault;

            // Normalizar página mínima
            if (pagina < 1) pagina = 1;

            var totalRegistros = await _repo.ContarTotal(busqueda);
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamano);

            // Si no hay resultados, totalPaginas = 0, pero mostramos al menos página 1
            if (totalPaginas < 1) totalPaginas = 1;

            // Ajustar página si supera el máximo
            if (pagina > totalPaginas) pagina = totalPaginas;

            var empleados = await _repo.ObtenerPaginado(pagina, tamano, busqueda);

            var viewModel = new EmpleadosIndexViewModel
            {
                Empleados = empleados,
                Busqueda = busqueda,
                PaginaActual = pagina,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros,
                TamanoPagina = tamano
            };

            return View(viewModel);
        }

        // GET: /Empleados/Create
        public IActionResult Create()
        {
            // Fecha de ingreso por defecto: hoy
            var empleado = new Empleado
            {
                FechaIngreso = DateTime.Today,
                Activo = true
            };
            return View(empleado);
        }

        // POST: /Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                await _repo.Agregar(empleado);
                TempData["Mensaje"] = $"Empleado {empleado.NombreCompleto} creado exitosamente.";
                TempData["TipoMensaje"] = "success";
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: /Empleados/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var empleado = await _repo.ObtenerPorId(id);
            if (empleado is null)
                return NotFound();

            return View(empleado);
        }

        // POST: /Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empleado empleado)
        {
            if (id != empleado.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _repo.Actualizar(empleado);
                TempData["Mensaje"] = $"Empleado {empleado.NombreCompleto} actualizado exitosamente.";
                TempData["TipoMensaje"] = "success";
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // POST: /Empleados/ToggleActivo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActivo(int id, string? busqueda, int pagina = 1)
        {
            var empleado = await _repo.ObtenerPorId(id);
            if (empleado is null)
                return NotFound();

            empleado.Activo = !empleado.Activo;
            await _repo.Actualizar(empleado);

            var estado = empleado.Activo ? "activado" : "dado de baja";
            TempData["Mensaje"] = $"Empleado {empleado.NombreCompleto} ha sido {estado}.";
            TempData["TipoMensaje"] = empleado.Activo ? "success" : "warning";

            return RedirectToAction(nameof(Index), new { busqueda, pagina });
        }
    }
}

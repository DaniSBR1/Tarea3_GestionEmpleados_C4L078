# Tarea 3 – Sistema de Gestión de Empleados con EF Core

**Universidad de Costa Rica**  
**Curso:** Lenguajes para Aplicaciones Comerciales  
**Semana:** 6 – Capa de Acceso a Datos y Entity Framework Core

---

## Datos del estudiante

| Campo   | Valor |
|---------|-------|
| Nombre  | _(Daniel Vindas Navarro)_ |
| Carnet  | _(C4L078)_ |

---

## Descripción del sistema

Sistema web ASP.NET Core MVC (.NET 10) para administrar la nómina de empleados de una empresa
de consultoría. Permite registrar, editar y realizar bajas lógicas de empleados, con búsqueda
full-text y paginación del lado del servidor.

### Funcionalidades implementadas

- **Listado paginado** de empleados (5 por página por defecto).
- **Búsqueda** por Nombre, Apellidos o Departamento (case-insensitive, LINQ Contains).
- **Alta / Baja lógica** (`ToggleActivo`) sin eliminar el registro físicamente.
- **CRUD completo**: Crear y Editar con validación en cliente y servidor.
- **Badge de estado**: *Activo* (verde) / *Dado de Baja* (gris).
- **Patrón Repositorio** completo sobre EF Core con SQL Server Express.

---

## Arquitectura del proyecto

```
Tarea3_GestionEmpleados/
├── Controllers/
│   └── EmpleadosController.cs       # Index, Create, Edit, ToggleActivo
├── Data/
│   └── AppDbContext.cs               # DbContext + Seed (10 empleados)
├── Migrations/
│   ├── 20260101000000_InitialCreate.cs
│   └── AppDbContextModelSnapshot.cs
├── Models/
│   └── Empleado.cs                   # Modelo + NombreCompleto (calculada)
├── Repositories/
│   ├── IEmpleadoRepository.cs        # Interfaz con 7 métodos
│   └── EmpleadoRepository.cs         # Skip/Take, Contains, CountAsync
├── ViewModels/
│   └── EmpleadosIndexViewModel.cs    # Datos de paginación para la vista
├── Views/
│   ├── Empleados/
│   │   ├── Index.cshtml              # Tabla + buscador + paginación
│   │   ├── Create.cshtml             # Formulario de alta
│   │   └── Edit.cshtml               # Formulario de edición
│   └── Shared/
│       ├── _Layout.cshtml
│       └── _ValidationScriptsPartial.cshtml
├── script_sql/
│   └── GestionEmpleados_DB.sql       # Script SQL alternativo
├── Program.cs                        # AddDbContext + AddScoped
└── appsettings.json                  # Connection string SQL Express
```

---

## Requisitos previos

| Herramienta | Versión mínima |
|-------------|---------------|
| .NET SDK    | **10.0**       |
| SQL Server Express | 2019 o superior |
| Visual Studio 2022 / VS Code | Última versión |

---

## Instrucciones de ejecución

### Opción A — EF Core Migrations (recomendada)

El método más sencillo. Las migraciones y el seed se aplican automáticamente al iniciar.

```bash
# 1. Clonar / descomprimir el proyecto
cd Tarea3_GestionEmpleados_[Carnet]

# 2. Restaurar paquetes NuGet
dotnet restore

# 3. Verificar la connection string en appsettings.json
#    (ajustar la instancia de SQL Express si es necesaria)
#    Valor por defecto: Server=.\SQLEXPRESS;Database=GestionEmpleados;...

# 4. Ejecutar la aplicación (aplica migraciones y seed automáticamente)
dotnet run

# 5. Abrir en el navegador
#    https://localhost:7200   o   http://localhost:5200
```

### Opción B — Script SQL manual

Si prefiere crear la base de datos manualmente antes de ejecutar la app:

```sql
-- En SQL Server Management Studio (SSMS):
-- Abrir y ejecutar: script_sql/GestionEmpleados_DB.sql
```

Luego comentar la línea `db.Database.Migrate();` en `Program.cs` si la BD ya está creada.

---

## Cómo funciona la paginación y búsqueda

### Parámetros de URL

| Parámetro  | Tipo    | Por defecto | Descripción                              |
|------------|---------|-------------|------------------------------------------|
| `busqueda` | string  | `""`        | Término de búsqueda (nombre/apellidos/depto) |
| `pagina`   | int     | `1`         | Número de página solicitada              |

El tamaño de página está fijo en **5 registros** (constante `TamanoPaginaDefault` en el controller).

### Flujo interno

1. `ContarTotal(busqueda)` ejecuta `WHERE ... .CountAsync()` para obtener el total filtrado.
2. Se calcula `TotalPaginas = ⌈TotalRegistros / 5⌉`.
3. `ObtenerPaginado(pagina, 5, busqueda)` aplica `.Skip((pagina-1)*5).Take(5)` sobre el mismo predicado.
4. El ViewModel lleva `TienePaginaAnterior` y `TienePaginaSiguiente` para deshabilitar los botones extremos.

### Predicado de búsqueda

```csharp
e.Nombre.ToLower().Contains(terminoLower) ||
e.Apellidos.ToLower().Contains(terminoLower) ||
e.Departamento.ToLower().Contains(terminoLower)
```

Se aplica **el mismo predicado** en `ObtenerPaginado` y `ContarTotal`, garantizando consistencia.

---

## Ejemplos de URL con búsqueda

```
# Listado completo – página 1 (5 empleados), sin filtro
https://localhost:7200/Empleados

# Segunda página sin filtro
https://localhost:7200/Empleados?pagina=2

# Filtrar por departamento TI
https://localhost:7200/Empleados?busqueda=TI

# Filtrar por nombre "Ana"
https://localhost:7200/Empleados?busqueda=Ana

# Filtrar por "Ramírez" y ver la página 1
https://localhost:7200/Empleados?busqueda=Ramírez&pagina=1

# Filtrar por departamento "Finanzas"
https://localhost:7200/Empleados?busqueda=Finanzas

# Filtrar por "Operaciones" página 1
https://localhost:7200/Empleados?busqueda=Operaciones&pagina=1
```

---

## Connection string

Ubicada en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=GestionEmpleados;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Si su instancia de SQL Express tiene un nombre diferente (p. ej. `LAPTOP\SQLEXPRESS`), actualice el valor de `Server` en consecuencia.


## Tecnologías utilizadas

- **ASP.NET Core MVC** – .NET 10
- **Entity Framework Core 10** – ORM + Migrations
- **SQL Server Express** – Motor de base de datos
- **Bootstrap 5.3** – Estilos y componentes UI
- **Bootstrap Icons** – Iconografía
- **jQuery Validate Unobtrusive** – Validación client-side

---

*Lenguajes para Aplicaciones Comerciales — Universidad de Costa Rica — 2026*

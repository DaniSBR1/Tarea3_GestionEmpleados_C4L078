using Microsoft.EntityFrameworkCore;
using Tarea3_GestionEmpleados.Data;
using Tarea3_GestionEmpleados.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ── Servicios ─────────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

// EF Core + SQL Server (SQL Express)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Patrón Repositorio
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

// ── Pipeline ──────────────────────────────────────────────────────────────────
var app = builder.Build();

// Aplicar migraciones pendientes y seed automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Ruta por defecto: redirige al listado de empleados
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Empleados}/{action=Index}/{id?}");

app.Run();

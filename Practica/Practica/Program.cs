using Practica.Models;
using Practica.Repository.IGenericRepository;
using Practica.Repository.Implementacion;
using System.Data;
using System.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IGenericRepository<Producto>, ProductoRepository>();
builder.Services.AddScoped<IGenericRepository<Proveedor>, ProveedorRepository>();
builder.Services.AddScoped<IGenericRepository<DetalleCompra>, DetalleCompraRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

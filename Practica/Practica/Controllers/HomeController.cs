using Practica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Practica.Repository.IGenericRepository;

namespace Practica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IGenericRepository<Proveedor> _proveedorRepository;
        private readonly IGenericRepository<DetalleCompra> _detalleCompraRepository;

        public HomeController(ILogger<HomeController> logger, 
            IGenericRepository<Producto> productoRepository, 
            IGenericRepository<Proveedor> proveedorRepository, 
            IGenericRepository<DetalleCompra> detalleCompraRepository)
        {
            _logger = logger;
            _productoRepository = productoRepository;
            _proveedorRepository = proveedorRepository;
            _detalleCompraRepository = detalleCompraRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]public async Task<IActionResult> listaProductos()
        {
            List<Producto> _lista = await _productoRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet]public async Task<IActionResult> listaProveedores()
        {
            List<Proveedor> _lista = await _proveedorRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpGet]public async Task<IActionResult> listaDetalleCompras()
        {
            List<DetalleCompra> _lista = await _detalleCompraRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]public async Task<IActionResult> crearDetalleCompra([FromBody] DetalleCompra modelo)
        {
            bool _resultado = await _detalleCompraRepository.Crear(modelo);
            if (!_resultado){
                return StatusCode(StatusCodes.Status200OK, new {valor = _resultado, msg = "Ok"});
            }else{
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error"});
            }
        }

        [HttpPut]public async Task<IActionResult> editarDetalleCompra([FromBody] DetalleCompra modelo)
        {
            bool _resultado = await _detalleCompraRepository.Editar(modelo);
            if (!_resultado)
            {
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "Ok" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
            }
        }

        [HttpPut] public async Task<IActionResult> eliminarDetalleCompra(int idDetalleCompra)
        {
            bool _resultado = await _detalleCompraRepository.Eliminar(idDetalleCompra);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "Ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
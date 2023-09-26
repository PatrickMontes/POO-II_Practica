using Practica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Practica.Repository.IGenericRepository;

namespace Practica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Define los repositorios para Producto, Proveedor y DetalleCompra
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IGenericRepository<Proveedor> _proveedorRepository;
        private readonly IGenericRepository<DetalleCompra> _detalleCompraRepository;


        // Constructor que recibe las dependencias a través de la inyección de dependencias
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


        // Acción para obtener la lista de productos mediante una solicitud GET
        [HttpGet]
        public async Task<IActionResult> listaProductos()
        {
            List<Producto> _lista = await _productoRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }


        // Acción para obtener la lista de proveedores mediante una solicitud GET
        [HttpGet]
        public async Task<IActionResult> listaProveedores()
        {
            List<Proveedor> _lista = await _proveedorRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }


        // Acción para obtener la lista de detalles de compras mediante una solicitud GET
        [HttpGet]
        public async Task<IActionResult> listaDetalleCompras()
        {
            List<DetalleCompra> _lista = await _detalleCompraRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _lista);
        }


        // Acción para crear un nuevo detalle de compra mediante una solicitud POST
        [HttpPost]
        public async Task<IActionResult> crearDetalleCompra([FromBody] DetalleCompra modelo)
        {
            bool _resultado = await _detalleCompraRepository.Crear(modelo);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "Ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }


        // Acción para editar un detalle de compra existente mediante una solicitud PUT
        [HttpPut]
        public async Task<IActionResult> editarDetalleCompra([FromBody] DetalleCompra modelo)
        {
            bool _resultado = await _detalleCompraRepository.Editar(modelo);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "Ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }


        // Acción para eliminar un detalle de compra existente mediante una solicitud PUT
        [HttpPut]
        public async Task<IActionResult> eliminarDetalleCompra(int idDetalleCompra)
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
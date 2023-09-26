namespace Practica.Models
{
    public class DetalleCompra
    {
        public int idDetalleCompra { get; set; }
        public Producto refProducto { get; set; } // Propiedad para la referencia al producto relacionado en la compra
        public Proveedor refProveedor { get; set; } // Propiedad para la referencia al proveedor relacionado en la compra
        public int cantidad { get; set; }
        public double precio { get; set; }
        public string fechaCompra { get; set; }
        public string estadoCompra { get; set; }
    }
}

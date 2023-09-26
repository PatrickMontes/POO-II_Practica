namespace Practica.Models
{
    public class DetalleCompra
    {
        public int idDetalleCompra { get; set; }
        public Producto refProducto { get; set; }
        public Proveedor refProveedor { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public string fechaCompra { get; set; }
        public string estadoCompra { get; set; }
    }
}

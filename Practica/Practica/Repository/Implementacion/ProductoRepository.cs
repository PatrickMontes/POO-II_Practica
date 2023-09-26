using Practica.Models;
using System.Data;
using System.Data.SqlClient;
using Practica.Repository.IGenericRepository; //Importa los nombres para acceder a la interfaz IGenericRepository

namespace Practica.Repository.Implementacion
{
    // Declaración de la clase ProductoRepository que implementa la interfaz
    public class ProductoRepository:IGenericRepository<Producto>
    {
        private readonly string _cadenaSQL = ""; // Cadena de conexión SQL

        public ProductoRepository(IConfiguration configuration) // Constructor de la clase ProductoRepository que toma IConfiguration como parámetro
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL"); // Obtiene la cadena de conexión desde IConfiguration
        }


        // Implementación del método Listar() de la interfaz IGenericRepository
        public async Task<List<Producto>> Listar()
        {
            List<Producto> _lista = new List<Producto>(); // Lista para almacenar 

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaProducto", conexion); // Crea un comando SQL para llamar al procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Producto // Lee los datos del producto desde el SqlDataReader y los agrega a la lista
                        {
                            idProducto = Convert.ToInt32(dr["idProducto"]),
                            nombreProducto = dr["nombreProducto"].ToString()
                        });
                    }
                }
            }
            return _lista; // Retorna la lista de productos
        }

        public Task<bool> Crear(Producto modelo) // Implementación del método Crear de la interfaz IGenericRepository
        {
            throw new NotImplementedException(); // Metodo no imolementado
        }

        public Task<bool> Editar(Producto modelo) // Implementación del método Editar de la interfaz IGenericRepository
        { 
            throw new NotImplementedException(); // Metodo no imolementado
        }

        public Task<bool> Eliminar(int id) // Implementación del método Eliminar de la interfaz IGenericRepository
        {
            throw new NotImplementedException(); // Metodo no imolementado
        }
    }
}

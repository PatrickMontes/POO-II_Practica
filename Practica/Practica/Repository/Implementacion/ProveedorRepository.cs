using Practica.Models;
using Practica.Repository.IGenericRepository; //Importa los nombres para acceder a la interfaz IGenericRepository
using System.Data;
using System.Data.SqlClient;

namespace Practica.Repository.Implementacion
{
    // Declaración de la clase ProveedorRepository que implementa la interfaz
    public class ProveedorRepository:IGenericRepository<Proveedor>
    {
        private readonly string _cadenaSQL = ""; // Cadena de conexión SQL

        public ProveedorRepository(IConfiguration configuration) // Constructor de la clase ProveedorRepository que toma IConfiguration como parámetro
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");  // Obtiene la cadena de conexión desde IConfiguration
        }

        // Implementación del método Listar() de la interfaz IGenericRepository
        public async Task<List<Proveedor>> Listar()
        {
            List<Proveedor> _lista = new List<Proveedor>(); // Lista para almacenar 

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaProveedor", conexion); // Crea un comando SQL para llamar al procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                using(var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new Proveedor // Lee los datos del proveedor desde el SqlDataReader y los agrega a la lista
                        {
                            idProveedor = Convert.ToInt32(dr["idProveedor"]),
                            nombreProveedor = dr["nombreProveedor"].ToString(),
                        });
                    }
                }
            }
            return _lista; // Retorna la lista de proveedores
        }

        
        public  Task<bool> Crear(Proveedor modelo) // Implementación del método Crear de la interfaz IGenericRepository
        {
            throw new NotImplementedException(); // Metodo no imolementado
        }

        public Task<bool> Editar(Proveedor modelo) // Implementación del método Editar de la interfaz IGenericRepository
        {
            throw new NotImplementedException(); // Metodo no imolementado
        }

        public  Task<bool> Eliminar(int  id) // Implementación del método Eliminar de la interfaz IGenericRepository
        {
            throw new NotImplementedException(); // Metodo no imolementado
        }
    }
}

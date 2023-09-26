using Practica.Models;
using System.Data;
using System.Data.SqlClient;
using Practica.Repository.IGenericRepository;

namespace Practica.Repository.Implementacion
{
    public class ProductoRepository:IGenericRepository<Producto>
    {
        private readonly string _cadenaSQL = "";

        public ProductoRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }


        public async Task<List<Producto>> Listar()
        {
            List<Producto> _lista = new List<Producto>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaProducto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Producto
                        {
                            idProducto = Convert.ToInt32(dr["idProducto"]),
                            nombreProducto = dr["nombreProducto"].ToString()
                        });
                    }
                }
            }
            return _lista;
        }

        public Task<bool> Crear(Producto modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(Producto modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}

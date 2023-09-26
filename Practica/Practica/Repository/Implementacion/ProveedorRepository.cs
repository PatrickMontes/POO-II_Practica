using Practica.Models;
using Practica.Repository.IGenericRepository;
using System.Data;
using System.Data.SqlClient;

namespace Practica.Repository.Implementacion
{
    public class ProveedorRepository:IGenericRepository<Proveedor>
    {
        private readonly string _cadenaSQL = "";

        public ProveedorRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }

        public async Task<List<Proveedor>> Listar()
        {
            List<Proveedor> _lista = new List<Proveedor>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaProveedor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using(var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new Proveedor
                        {
                            idProveedor = Convert.ToInt32(dr["idProveedor"]),
                            nombreProveedor = dr["nombreProveedor"].ToString(),
                        });
                    }
                }
            }
            return _lista;
        }

        
        public  Task<bool> Crear(Proveedor modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(Proveedor modelo)
        {
            throw new NotImplementedException();
        }

        public  Task<bool> Eliminar(int  id)
        {
            throw new NotImplementedException();
        }
    }
}

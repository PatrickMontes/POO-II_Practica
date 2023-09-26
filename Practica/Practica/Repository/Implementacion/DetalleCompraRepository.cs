using Practica.Models;
using Practica.Repository.IGenericRepository;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Practica.Repository.Implementacion
{
    public class DetalleCompraRepository : IGenericRepository<DetalleCompra>
    {
        private readonly string _cadenaSQL = "";

        public DetalleCompraRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }

        public async Task<List<DetalleCompra>> Listar()
        {
            List<DetalleCompra> _lista = new List<DetalleCompra>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaDetalleCompra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync()) 
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new DetalleCompra
                        {
                            idDetalleCompra = Convert.ToInt32(dr["idDetalleCompra"]),
                            refProducto = new Producto()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                nombreProducto = dr["nombreProducto"].ToString()
                            },
                            refProveedor = new Proveedor()
                            {
                                idProveedor = Convert.ToInt32(dr["idProveedor"]),
                                nombreProveedor = dr["nombreProveedor"].ToString()
                            },
                            cantidad = Convert.ToInt32(dr["cantidad"]),
                            precio = Convert.ToDouble((dr["precio"])),
                            fechaCompra = dr["fechaCompra"].ToString(),
                        });
                    }
                }
            }
            return _lista;
        }

        public async Task<bool> Crear(DetalleCompra modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_NuevoDetalleCompra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("idProducto", modelo.refProducto.idProducto);
                cmd.Parameters.AddWithValue("idProveedor", modelo.refProveedor.idProveedor);
                cmd.Parameters.AddWithValue("cantidad", modelo.cantidad);
                cmd.Parameters.AddWithValue("precio", modelo.precio);
                cmd.Parameters.AddWithValue("fechaCompra", modelo.fechaCompra);
                cmd.Parameters.AddWithValue("estadoCompra", "A");

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                if(filasAfectadas > 0){ 
                    return true; 
                }else { 
                    return false; 
                }
            }
        }

        public async Task<bool> Editar(DetalleCompra modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ModificarDetalleCompra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("idDetalleCompra", modelo.idDetalleCompra);
                cmd.Parameters.AddWithValue("idProducto", modelo.refProducto.idProducto);
                cmd.Parameters.AddWithValue("idProveedor", modelo.refProveedor.idProveedor);
                cmd.Parameters.AddWithValue("cantidad", modelo.cantidad);
                cmd.Parameters.AddWithValue("precio", modelo.precio);
                cmd.Parameters.AddWithValue("fechaCompra", modelo.fechaCompra);
                cmd.Parameters.AddWithValue("estadoCompra", "A");

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> Eliminar(int  id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarDetalleCompra", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("idDetalleCompra", id);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

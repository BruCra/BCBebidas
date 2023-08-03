using ApiBCB.AD.Models;
using ApiBCB.AD.Models.Productos;
using ApiBCB.AD.Models.Categorias;
using ApiBCB.AD.Models.Envases;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBCB.AD.Services.Productos
{
    public class S_Productos : S_IProductos
    {
        private readonly string _connection;

        public S_Productos(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_Productos>> GetProductosAsync()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Productos_ObtenerProductos", conn))
                {
                    try
                    {
                        var productosList = new List<M_Productos>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Productos = new M_Productos
                                {
                                    Id_Producto = reader.GetInt32(reader.GetOrdinal("Id_Producto")),
                                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                                    Id_Categoria = reader.GetInt32(reader.GetOrdinal("Id_Categoria")),
                                    NombreCategoria = reader.GetString(reader.GetOrdinal("NombreCategoria")),
                                    Id_Bodega = reader.GetInt32(reader.GetOrdinal("Id_Bodega")),
                                    NombreBodega = reader.GetString(reader.GetOrdinal("NombreBodega")),
                                    Id_Envase = reader.GetInt32(reader.GetOrdinal("Id_Envase")),
                                    NombreEnvase = reader.GetString(reader.GetOrdinal("NombreEnvase")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    PrecioCompra = reader.GetInt32(reader.GetOrdinal("PrecioCompra")),
                                    Descuento = reader.GetInt32(reader.GetOrdinal("Descuento")),
                                    PrecioVenta = reader.GetInt32(reader.GetOrdinal("PrecioVenta")),
                                    Imagen = reader.GetString(reader.GetOrdinal("Imagen")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };

                                productosList.Add(Productos);
                            }
                        }
                        return productosList;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al obtener todos los productos " + ex.Message);
                    }
                }
            }
        }

        public async Task<M_Productos> GetProductosByIdAsync(int Id_Producto)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Productos_ObtenerProductosPorId", conn))
                {
                    try
                    {
                        M_Productos? m_Productos = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id_Producto", SqlDbType.Int).Value = Id_Producto;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Productos = new M_Productos
                                {
                                    Id_Producto = reader.GetInt32(reader.GetOrdinal("Id_Producto")),
                                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                                    Id_Categoria = reader.GetInt32(reader.GetOrdinal("Id_Categoria")),
                                    NombreCategoria = reader.GetString(reader.GetOrdinal("NombreCategoria")),
                                    Id_Bodega = reader.GetInt32(reader.GetOrdinal("Id_Bodega")),
                                    NombreBodega = reader.GetString(reader.GetOrdinal("NombreBodega")),
                                    Id_Envase = reader.GetInt32(reader.GetOrdinal("Id_Envase")),
                                    NombreEnvase = reader.GetString(reader.GetOrdinal("NombreEnvase")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    PrecioCompra = reader.GetInt32(reader.GetOrdinal("PrecioCompra")),
                                    Descuento = reader.GetInt32(reader.GetOrdinal("Descuento")),
                                    PrecioVenta = reader.GetInt32(reader.GetOrdinal("PrecioVenta")),
                                    Imagen = reader.GetString(reader.GetOrdinal("Imagen")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };
                            }
                        }
                        return m_Productos;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al buscar productos por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_Productos> GetProductosByNameAsync(string nombreProducto)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Productos_ObtenerProductosPorNombre", conn))
                {
                    try
                    {
                        M_Productos? m_Productos = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NombreProducto", SqlDbType.Int).Value = nombreProducto;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Productos = new M_Productos
                                {
                                    Id_Producto = reader.GetInt32(reader.GetOrdinal("Id_Producto")),
                                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                                    Id_Categoria = reader.GetInt32(reader.GetOrdinal("Id_Categoria")),
                                    NombreCategoria = reader.GetString(reader.GetOrdinal("NombreCategoria")),
                                    Id_Bodega = reader.GetInt32(reader.GetOrdinal("Id_Bodega")),
                                    NombreBodega = reader.GetString(reader.GetOrdinal("NombreBodega")),
                                    Id_Envase = reader.GetInt32(reader.GetOrdinal("Id_Envase")),
                                    NombreEnvase = reader.GetString(reader.GetOrdinal("NombreEnvase")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    PrecioCompra = reader.GetInt32(reader.GetOrdinal("PrecioCompra")),
                                    Descuento = reader.GetInt32(reader.GetOrdinal("Descuento")),
                                    PrecioVenta = reader.GetInt32(reader.GetOrdinal("PrecioVenta")),
                                    Imagen = reader.GetString(reader.GetOrdinal("Imagen")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };
                            }
                        }

                        return m_Productos;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar producto por nombre " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> AddProductosAsync(M_Productos productos)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Productos_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NombreProducto", SqlDbType.VarChar, 50).Value = productos.NombreProducto;
                        cmd.Parameters.Add("@Id_Categoria", SqlDbType.Int).Value = productos.Id_Categoria;
                        cmd.Parameters.Add("@Id_Bodega", SqlDbType.Int).Value = productos.Id_Bodega;
                        cmd.Parameters.Add("@Id_Envase", SqlDbType.Int).Value = productos.Id_Envase;
                        cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = productos.Cantidad;
                        cmd.Parameters.Add("@PrecioCompra", SqlDbType.Money).Value = productos.PrecioCompra;
                        cmd.Parameters.Add("@Descuento", SqlDbType.Int).Value = productos.Descuento;
                        cmd.Parameters.Add("@PrecioVenta", SqlDbType.Money).Value = productos.PrecioVenta;
                        cmd.Parameters.Add("@Foto", SqlDbType.VarChar, -1).Value = productos.Imagen;
                        cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = productos.Activo;
                        var id = await cmd.ExecuteScalarAsync();

                        if (id != null && id != DBNull.Value)
                        {
                            return Convert.ToInt32(id);
                        }
                        else
                        {
                            return 0;
                        };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al agregar producto " + ex.Message);
                    }
                }
            }
        }

        public async Task<int> ModifyProductosAsync(M_Productos productos)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Productos_Modificar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id_Producto", SqlDbType.Int).Value = productos.Id_Producto;
                        cmd.Parameters.Add("@NombreProducto", SqlDbType.VarChar, 50).Value = productos.NombreProducto;
                        cmd.Parameters.Add("@Id_Categoria", SqlDbType.Int).Value = productos.Id_Categoria;
                        cmd.Parameters.Add("@Id_Bodega", SqlDbType.Int).Value = productos.Id_Bodega;
                        cmd.Parameters.Add("@Id_Envase", SqlDbType.Int).Value = productos.Id_Envase;
                        cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = productos.Cantidad;
                        cmd.Parameters.Add("@PrecioCompra", SqlDbType.Money).Value = productos.PrecioCompra;
                        cmd.Parameters.Add("@Descuento", SqlDbType.Int).Value = productos.Descuento;
                        cmd.Parameters.Add("@PrecioVenta", SqlDbType.Money).Value = productos.PrecioVenta;
                        cmd.Parameters.Add("@Foto", SqlDbType.VarChar, -1).Value = productos.Imagen;
                        cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = productos.Activo;
                        var rowAffected = await cmd.ExecuteScalarAsync();

                        if (rowAffected != null && rowAffected != DBNull.Value)
                        {
                            return Convert.ToInt32(rowAffected);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al modificar producto " + ex.Message);
                    }
                }
            }
        }
    }
}

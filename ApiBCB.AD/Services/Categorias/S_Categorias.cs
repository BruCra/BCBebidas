using ApiBCB.AD.Models;
using ApiBCB.AD.Models.Categorias;
using ApiBCB.AD.Models.Envases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBCB.AD.Services.Categorias
{
    public class S_Categorias : S_ICategorias
    {
        private readonly string _connection;

        public S_Categorias(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }
        public async Task<List<M_Categorias>> GetCategoriasAsync()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Categorias_ObtenerCategorias", conn))
                {
                    try
                    {
                        var categoriasList = new List<M_Categorias>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Categorias = new M_Categorias
                                {
                                    Id_Categoria = reader.GetInt32(reader.GetOrdinal("Id_Categoria")),
                                    NombreCategoria = reader.GetString(reader.GetOrdinal("NombreCategoria")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                };

                                categoriasList.Add(Categorias);
                            }
                        }
                        return categoriasList;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al obtener todos las categorias " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_Categorias> GetCategoriasByIdAsync(int Id_Categoria)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Categorias_ObtenerCategoriasPorId", conn))
                {
                    try
                    {
                        M_Categorias? m_Categorias = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id_Categoria", SqlDbType.Int).Value = Id_Categoria;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Categorias = new M_Categorias
                                {
                                    Id_Categoria = reader.GetInt32(reader.GetOrdinal("Id_Categoria")),
                                    NombreCategoria = reader.GetString(reader.GetOrdinal("NombreCategoria")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                };
                            }
                        }
                        return m_Categorias;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al buscar categoria por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_Categorias> GetCategoriasByNameAsync(string nombreCategoria)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Categorias_ObtenerCategoriasPorNombre", conn))
                {
                    try
                    {
                        M_Categorias? m_Categorias = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NombreCategoria", SqlDbType.Int).Value = nombreCategoria;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Categorias = new M_Categorias
                                {
                                    Id_Categoria = reader.GetInt32(reader.GetOrdinal("Id_Categoria")),
                                    NombreCategoria = reader.GetString(reader.GetOrdinal("NombreCategoria")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                };
                            }
                        }

                        return m_Categorias;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar categorias por nombre " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AddCategoriasAsync(M_Categorias categorias)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Categorias_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NombreCategoria", SqlDbType.VarChar, 50).Value = categorias.NombreCategoria;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 50).Value = categorias.Descripcion;

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
                        throw new Exception("Se produjo un error al agregar categoria " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> ModifyCategoriasAsync(M_Categorias categorias)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Categorias_Modificar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id_Categoria", SqlDbType.Int).Value = categorias.Id_Categoria;
                        cmd.Parameters.Add("@NombreCategoria", SqlDbType.VarChar, 50).Value = categorias.NombreCategoria;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 50).Value = categorias.Descripcion;
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
                        throw new Exception("Se produjo un error al modificar categorias " + ex.Message);
                    }
                }
            }
        }
    }
}

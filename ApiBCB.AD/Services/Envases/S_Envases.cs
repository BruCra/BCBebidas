using ApiBCB.AD.Models;
using ApiBCB.AD.Models.Envases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBCB.AD.Services.Envases
{
    public class S_Envases : S_IEnvases
    {
        private readonly string _connection;

        public S_Envases(M_ConnectionToSql connection)
        {
            _connection = connection.GetConnection();
        }

        private SqlConnection GetConn()
        {
            return new SqlConnection(_connection);
        }

        public async Task<List<M_Envases>> GetEnvasesAsync()
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Envases_ObtenerEnvases", conn))
                {
                    try
                    {
                        var envasesList = new List<M_Envases>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Envases = new M_Envases
                                {
                                    Id_Envase = reader.GetInt32(reader.GetOrdinal("Id_Envase")),
                                    Material = reader.GetString(reader.GetOrdinal("Material")),
                                    Milimetros = reader.GetInt32(reader.GetOrdinal("Milimetros")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                };

                                envasesList.Add(Envases);
                            }
                        }
                        return envasesList;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al obtener todos los envases " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_Envases> GetEnvasesByIdAsync(int Id_Envase)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Envases_ObtenerEnvasesPorId", conn))
                {
                    try
                    {
                        M_Envases? m_Envases = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id_Envase", SqlDbType.Int).Value = Id_Envase;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Envases = new M_Envases
                                {
                                    Id_Envase = reader.GetInt32(reader.GetOrdinal("Id_Envase")),
                                    Material = reader.GetString(reader.GetOrdinal("Material")),
                                    Milimetros = reader.GetInt32(reader.GetOrdinal("Milimetros")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                };
                            }
                        }
                        return m_Envases;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Se produjo un error al buscar envases por id " + ex.Message);
                    }
                }
            }
        }
        public async Task<M_Envases> GetEnvasesByNameAsync(int milimetros)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Envases_ObtenerEnvasesPorNombre", conn))
                {
                    try
                    {
                        M_Envases? m_Envases = null;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Milimetros", SqlDbType.Int).Value = milimetros;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                m_Envases = new M_Envases
                                {
                                    Id_Envase = reader.GetInt32(reader.GetOrdinal("Id_Envase")),
                                    Material = reader.GetString(reader.GetOrdinal("Material")),
                                    Milimetros = reader.GetInt32(reader.GetOrdinal("Milimetros")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                };
                            }
                        }

                        return m_Envases;

                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Se produjo un error al buscar envases por nombre " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> AddEnvasesAsync(M_Envases envases)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Envases_Agregar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Material", SqlDbType.VarChar, 50).Value = envases.Material;
                        cmd.Parameters.Add("@Milimetros", SqlDbType.Int).Value = envases.Milimetros;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 50).Value = envases.Descripcion;

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
                        throw new Exception("Se produjo un error al agregar envase " + ex.Message);
                    }
                }
            }
        }
        public async Task<int> ModifyEnvasesAsync(M_Envases envases)
        {
            using (var conn = GetConn())
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand("Envases_Modificar", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id_Envase", SqlDbType.Int).Value = envases.Id_Envase;
                        cmd.Parameters.Add("@Material", SqlDbType.VarChar, 50).Value = envases.Material;
                        cmd.Parameters.Add("@Milimetros", SqlDbType.Int).Value = envases.Milimetros;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 50).Value = envases.Descripcion;
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
                        throw new Exception("Se produjo un error al modificar envases " + ex.Message);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SistemaProductosApp.Datos
{
    // 1. Clase Entidad / DTO (Representa la tabla Insumo)
    public class Insumo
    {
        public int IdInsumo { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
        public int IdTipoInsumo { get; set; }
    }

    // 2. Clase de Acceso a Datos (DAL) para Insumos
    public class Insumos
    {
        // CREAR (Create) - Ejecuta sp_Insumo_Crear
        public bool CrearInsumo(Insumo insumo)
        {
            bool resultado = false;
            
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Insumo_Crear", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                // Parámetros según el SP
                cmd.Parameters.AddWithValue("@nombre", insumo.Nombre);
                cmd.Parameters.AddWithValue("@stock", insumo.Stock);
                cmd.Parameters.AddWithValue("@idTipoInsumo", insumo.IdTipoInsumo);

                try
                {
                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                    }
                }
                catch (Exception ex)
                {
                    // Aquí puedes registrar el error en un log si lo deseas
                    throw new Exception("Error al crear el insumo: " + ex.Message);
                }
            }
            return resultado;
        }

        // LEER (Read) - Ejecuta sp_Insumo_Leer
        public List<Insumo> LeerInsumos(int? idInsumo = null)
        {
            List<Insumo> listaInsumos = new List<Insumo>();
            
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Insumo_Leer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                // Si se envía un ID específico, lo buscamos. Si es null, trae todos los activos.
                if (idInsumo.HasValue)
                {
                    cmd.Parameters.AddWithValue("@idInsumo", idInsumo.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idInsumo", DBNull.Value);
                }

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Insumo obj = new Insumo
                        {
                            IdInsumo = Convert.ToInt32(reader["idInsumo"]),
                            Nombre = reader["nombre"].ToString(),
                            Stock = Convert.ToInt32(reader["stock"]),
                            Estado = Convert.ToBoolean(reader["estado"]),
                            IdTipoInsumo = Convert.ToInt32(reader["idTipoInsumo"])
                        };
                        listaInsumos.Add(obj);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al leer los insumos: " + ex.Message);
                }
            }
            return listaInsumos;
        }

        // ACTUALIZAR (Update) - Ejecuta sp_Insumo_Actualizar
        public bool ActualizarInsumo(Insumo insumo)
        {
            bool resultado = false;
            
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Insumo_Actualizar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                // Parámetros según el SP
                cmd.Parameters.AddWithValue("@idInsumo", insumo.IdInsumo);
                cmd.Parameters.AddWithValue("@nombre", insumo.Nombre);
                cmd.Parameters.AddWithValue("@stock", insumo.Stock);
                cmd.Parameters.AddWithValue("@idTipoInsumo", insumo.IdTipoInsumo);

                try
                {
                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar el insumo: " + ex.Message);
                }
            }
            return resultado;
        }

        // ELIMINAR LÓGICAMENTE (Delete) - Ejecuta sp_Insumo_Eliminar
        public bool EliminarInsumo(int idInsumo)
        {
            bool resultado = false;
            
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_Insumo_Eliminar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                // Parámetro según el SP
                cmd.Parameters.AddWithValue("@idInsumo", idInsumo);

                try
                {
                    con.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        resultado = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al dar de baja el insumo: " + ex.Message);
                }
            }
            return resultado;
        }
    }
}
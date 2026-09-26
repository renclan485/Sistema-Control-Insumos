using Microsoft.Data.SqlClient;

namespace SistemaProductosApp.Datos
{
    public static class Conexion
    {
        // Se agrega '@' antes de la cadena para que acepte la barra invertida '\' sin dar error de escape
        // Cadena de conexión utilizando los datos de la imagen y los requerimientos solicitados
        private static readonly string _connectionString = 
            @"Server=SJAPLA3040281\SQLEXPRESS;Database=dbinsumo;User Id=SA;Password=User.sede;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
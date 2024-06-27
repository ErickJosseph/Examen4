using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Procedimientos
    {
        public void RegistrarRegion(string NombreRegion, bool EstadoRegion) 
        {
            using (var connection = new SqlConnection(Conexion.cadena))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SP_CrearRegion", connection);
                command.CommandType = CommandType.StoredProcedure;

                //Enviar los parámetros
                SqlParameter parameter = new SqlParameter("@RegionName", SqlDbType.VarChar, 50);
                parameter.Value = NombreRegion;
                command.Parameters.Add(parameter);

                SqlParameter parameter2 = new SqlParameter("@Enabled", SqlDbType.Bit);
                parameter2.Value = EstadoRegion;
                command.Parameters.Add(parameter2);

                command.ExecuteNonQuery();

            }

        }

        public void ListarRegion(bool NombreRegion)
        {
            List<RegionDto> Lista = new List<RegionDto>();

            using (var connection = new SqlConnection(Conexion.cadena))
            {
               
                SqlCommand command = new SqlCommand("SP_ListarRegion", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                //Enviar los parámetros
                SqlParameter parameter = new SqlParameter("@RegionId", SqlDbType.VarChar, 50);
                parameter.Value = NombreRegion;
                command.Parameters.Add(parameter);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int RegionId = reader["RegionId"] != DBNull.Value ? Convert.ToInt32(reader["RegionId"]) : 0;
                    string NombreRegion = reader["RegionName"] != DBNull.Value ? Convert.ToString(reader["RegionName"]) : "";
                    string EstadoRegion = reader["[Enabled]"] != DBNull.Value ? Convert.ToString(reader["[Enabled]"]) : "";
                }
            }

        }

    }
}

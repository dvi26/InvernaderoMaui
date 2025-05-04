using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class clsDalBDD
    {
        /// <summary>
        /// Obtiene una lista de invernaderos desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos clsInvernadero.</returns>
        public static List<clsInvernadero> getInvernaderos()
        {
            SqlConnection miConexion;
            SqlDataReader miLector;
            SqlCommand miComando = new SqlCommand();
            clsInvernadero oInvernadero;
            List<clsInvernadero> listadoInvernaderos = new List<clsInvernadero>();

            try
            {
                miConexion = clsConexion.getConexion();
                miComando.CommandText = "SELECT IdInvernadero, Nombre FROM clsInvernadero"; 
                miComando.Connection = miConexion;
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int id = (int)miLector["IdInvernadero"];
                        string nombre = miLector["Nombre"] != DBNull.Value ? (string)miLector["Nombre"] : "";

                        oInvernadero = new clsInvernadero(id, nombre);
                        listadoInvernaderos.Add(oInvernadero);
                    }
                }

                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return listadoInvernaderos;
        }
        public static bool existeFechaEnInvernadero(int idInvernadero, DateTime fechaSeleccionada)
        {
            SqlConnection miConexion;
            SqlCommand miComando = new SqlCommand();
            bool existe = false;

            try
            {
                miConexion = clsConexion.getConexion();
                miComando.CommandText = @"
            SELECT COUNT(*) 
            FROM clsTemperaturas
            WHERE idInvernadero = @idInvernadero AND CAST(fecha AS DATE) = @fechaSeleccionada";

                miComando.Parameters.AddWithValue("@idInvernadero", idInvernadero);
                miComando.Parameters.AddWithValue("@fechaSeleccionada", fechaSeleccionada.Date);

                miComando.Connection = miConexion;

                int count = (int)miComando.ExecuteScalar(); 

                if (count > 0)
                {
                    existe = true;
                }

                miConexion.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return existe;
        }
        public static clsTemperaturas GetTemperaturasPorFecha(int idInvernadero, DateTime fechaSeleccionada)
        {
            SqlConnection miConexion;
            SqlDataReader miLector;
            SqlCommand miComando = new SqlCommand();
            clsTemperaturas datosTemperatura=new clsTemperaturas();

            try
            {
                miConexion = clsConexion.getConexion();
                miComando.CommandText = @"
            SELECT idInvernadero, fecha,
                   temp1, temp2, temp3,
                   humedad1, humedad2, humedad3
            FROM clsTemperaturas
            WHERE idInvernadero = @idInvernadero AND CAST(fecha AS DATE) = @fechaSeleccionada";

                miComando.Parameters.AddWithValue("@idInvernadero", idInvernadero);
                miComando.Parameters.AddWithValue("@fechaSeleccionada", fechaSeleccionada.Date);

                miComando.Connection = miConexion;
                miLector = miComando.ExecuteReader();

                if (miLector.Read())
                {
                    DateTime fecha = (DateTime)miLector["fecha"];
                    int id = (int)miLector["idInvernadero"];
                    double? t1 = miLector["temp1"] != DBNull.Value ? Convert.ToDouble(miLector["temp1"]) : (double?)null;
                    double? t2 = miLector["temp2"] != DBNull.Value ? Convert.ToDouble(miLector["temp2"]) : (double?)null;
                    double? t3 = miLector["temp3"] != DBNull.Value ? Convert.ToDouble(miLector["temp3"]) : (double?)null;

                    int? h1 = miLector["humedad1"] != DBNull.Value ? Convert.ToInt32(miLector["humedad1"]) : (int?)null;
                    int? h2 = miLector["humedad2"] != DBNull.Value ? Convert.ToInt32(miLector["humedad2"]) : (int?)null;
                    int? h3 = miLector["humedad3"] != DBNull.Value ? Convert.ToInt32(miLector["humedad3"]) : (int?)null;

                    datosTemperatura = new clsTemperaturas(fecha, id, t1, t2, t3, h1, h2, h3);
                }

                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return datosTemperatura;
        }
    }
}
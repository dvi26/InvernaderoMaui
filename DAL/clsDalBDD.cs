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
    }
}
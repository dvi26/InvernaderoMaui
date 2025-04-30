using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class clsConexion
    {
        public static SqlConnection getConexion()
        {
            SqlConnection miConexion = new SqlConnection();
            try
            {
                miConexion.ConnectionString = "server=davidser.database.windows.net;database=DavidDB;uid=usuario;pwd=LaCampana123;trustServerCertificate=true;";
                miConexion.Open();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //miConexion.Close();
            }
            return miConexion;
        }
        /*
        public static SqlConnection cerrarConexion(ref SqlConnection conexion)
        {

        }*/
    }
}

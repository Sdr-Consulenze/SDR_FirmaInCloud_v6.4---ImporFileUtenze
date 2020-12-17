 
using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.BL.FirmaInCloud.Mapper
{
    public class TemplateLinqDB
    {
        public static void SaveTemplateLinq(int idKeyUte)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");
                 
                MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                comm.CommandText = "INSERT INTO TEMPLATE_LINQ(KEY_USER,DTSIGN) VALUES(@KEY_USER,@DTSIGN)";                     
                comm.Parameters.AddWithValue("@KEY_USER", idKeyUte); 
                comm.Parameters.AddWithValue("@DTSIGN", DateTime.Now.ToString("yyyyMMddHHmm")); 
                comm.ExecuteNonQuery(); 

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                
                MessageBox.Show("Errore: " + ex.Message);
            }
        }
    }
}

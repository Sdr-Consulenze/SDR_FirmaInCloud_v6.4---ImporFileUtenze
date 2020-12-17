using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDR_FirmaInCloud.BL.Helpers
{
    public static class GestioneMySql
    {
        public static string StringaConnessione = "server=localhost;user id=root;database=firmaincloud;password=lelino04";
        
        public static MySqlConnection connection = new MySqlConnection(StringaConnessione);

        public static bool OpenConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = GestioneMySql.StringaConnessione;
                    connection.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CloseConnection()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Le transazioni solo con database di tipo INNODB
        private static MySqlTransaction Transazione = null;

        public static void TransazioneBegin()
        {
            Transazione = connection.BeginTransaction();
        }

        public static void TransazioneCommit()
        {
            Transazione.Commit();
        }

        public static void TransazioneRollback()
        {
            Transazione.Rollback();
        }
    }    
}

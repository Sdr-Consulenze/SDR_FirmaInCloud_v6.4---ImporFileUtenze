using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Windows.Forms;

namespace Sdr_FirmaInCloud.BL
{
    public class TemplateXmlDB
    {
        public static bool Exists(string strFileName)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM TEMPLATEXML", GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("FILE_NM", strFileName));

                MySqlDataReader dr = cmd.ExecuteReader();
                bool res = false;
                while (dr.Read())
                {
                    res = (dr["FILE_NM"] != DBNull.Value) ? true : false;
                }
                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SaveTemplate(TemplateXml templateXml)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                if (!string.IsNullOrEmpty(templateXml.FILE_NM))
                {
                    MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                    comm.CommandText = "INSERT INTO TEMPLATEXML(FILE_NM,FILE_PATH,DTINS) VALUES(@FILE_NM,@FILE_PATH,@DTINS)";
                    comm.Parameters.AddWithValue("@FILE_NM", templateXml.FILE_NM);
                    comm.Parameters.AddWithValue("@FILE_PATH", templateXml.FILE_PATH);
                    comm.Parameters.AddWithValue("@DTINS", DateTime.Now.ToString("yyyyMMddHHmm"));
                    comm.ExecuteNonQuery();
                }
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

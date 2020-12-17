using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Text;
using System.Windows.Forms;

namespace Sdr_FirmaInCloud.BL.FirmaInCloud
{
    public class DocumentDB
    {
        public static Document LoadDocument(int? intKeyDoc)
        {
            try
            {
                if (string.IsNullOrEmpty(intKeyDoc.ToString()))
                    return null;

                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                Document document = new Document();

                var sb = new StringBuilder();
                sb.AppendLine("SELECT * FROM DOCUMENT WHERE KEY_DOC = @KEY_DOC");
                
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("KEY_DOC", intKeyDoc.ToString()));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_DOC")))
                    {
                        document.KEY_DOC = dr.GetInt32("KEY_DOC");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("TYP_DOC")))
                    {
                        document.TYP_DOC = dr.GetInt32("TYP_DOC");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("NUM_DOC")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("NUM_DOC")))
                            document.NUM_DOC = dr.GetString("NUM_DOC");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("ISS_DOC"))) 
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("CITY_DOC")))
                            document.ISS_DOC =  DateTime.ParseExact(dr.GetString("ISS_DOC"), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None);
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("CITY_DOC")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("CITY_DOC")))
                            document.CITY_DOC = dr.GetString("CITY_DOC");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("ID_DOC")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("ID_DOC")))
                            document.ID_DOC = dr.GetString("ID_DOC");
                    };
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return document;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static Document SaveDocument(Document document)
        {
            try
            {
                MySqlCommand comm = GestioneMySql.connection.CreateCommand(); 
                if (document.KEY_DOC.Equals(0))
                {
                    comm.CommandText = "INSERT INTO DOCUMENT(TYP_DOC, NUM_DOC, ISS_DOC, CITY_DOC, ID_DOC) VALUES (@TYP_DOC, @NUM_DOC, @ISS_DOC, @CITY_DOC, @ID_DOC)";
                }
                else
                {
                    comm.CommandText = " UPDATE DOCUMENT SET TYP_DOC=@TYP_DOC, NUM_DOC=@NUM_DOC, ISS_DOC=@ISS_DOC, CITY_DOC=@CITY_DOC, ID_DOC=@ID_DOC WHERE KEY_DOC=@KEY_DOC";
                    comm.Parameters.AddWithValue("@KEY_DOC", document.KEY_DOC);
                }

                comm.Parameters.AddWithValue("@TYP_DOC", document.TYP_DOC);
                comm.Parameters.AddWithValue("@NUM_DOC", document.NUM_DOC);
                comm.Parameters.AddWithValue("@ISS_DOC", document.ISS_DOC.Date.ToString("yyyyMMdd").StringOrNull());
                comm.Parameters.AddWithValue("@CITY_DOC", document.CITY_DOC);
                comm.Parameters.AddWithValue("@ID_DOC", document.ID_DOC);
                comm.ExecuteNonQuery();

                MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID() AS KEY_DOC", GestioneMySql.connection);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    document.KEY_DOC = dr.GetInt16("KEY_DOC");
                }
                dr.Close();

                return document;
                
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }
    }
}

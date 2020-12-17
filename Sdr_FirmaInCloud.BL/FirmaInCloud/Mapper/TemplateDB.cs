using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Text;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.BL.FirmaInCloud.Mapper
{
    public class TemplateDB
    {
        public static Template LoadTemplate(int strKeyTempl)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                Template template = new Template();

                sb.AppendLine("SELECT *");
                sb.AppendLine("FROM TEMPLATE");
                sb.AppendLine("WHERE KEY_TEMPL = @KEY_TEMPL");

                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("KEY_TEMPL", strKeyTempl));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    template.KEY_TEMPL = dr.GetInt16("KEY_TEMPL");

                    if (!string.IsNullOrEmpty(dr.GetString("PATH")))
                        template.PATH = dr.GetString("PATH");

                    if (!string.IsNullOrEmpty(dr.GetString("DESCR")))
                        template.DESCR = dr.GetString("DESCR");
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return template;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static void SaveTemplate(Template template)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                if (template.KEY_TEMPL.Equals(0))
                {
                    MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                    comm.CommandText = "INSERT INTO TEMPLATE(DESCR,PATH,DTINS,X_SIGN) VALUES(@DESCR,@PATH,@DTINS,@X_SIGN)";
                    comm.Parameters.AddWithValue("@DESCR", template.DESCR);
                    comm.Parameters.AddWithValue("@PATH", template.PATH);
                    comm.Parameters.AddWithValue("@DTINS", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    comm.Parameters.AddWithValue("@X_SIGN", template.X_SIGN);
                    comm.ExecuteNonQuery();
                }

                MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID() AS KEY_TEMPL", GestioneMySql.connection);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    template.KEY_TEMPL = dr.GetInt16("KEY_TEMPL");
                }
                dr.Close();

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
            }
        }
    
        public static void DeleteTemplate(int? intKeyTempl) 
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                if (intKeyTempl.HasValue)
                {

                    GestioneMySql.TransazioneBegin();                    

                    MySqlCommand comm = GestioneMySql.connection.CreateCommand(); 

                    comm.CommandText = "DELETE FROM TEMPLATE WHERE KEY_TEMPL = @KEY_TEMPL";
                    comm.Parameters.AddWithValue("@KEY_TEMPL", intKeyTempl);
                    comm.ExecuteNonQuery();

                    comm.CommandText = "UPDATE USERS SET KEY_TEMPL = NULL WHERE KEY_TEMPL = @KEY_TEMPL";
                    comm.Parameters.AddWithValue("@KEY_TEMPL", intKeyTempl);
                    comm.ExecuteNonQuery();

                    comm.CommandText = "DELETE FROM TEMPLATE_LINQ WHERE KEY_TEMPL = @KEY_TEMPL";
                    comm.Parameters.AddWithValue("@KEY_TEMPL", intKeyTempl);
                    comm.ExecuteNonQuery();

                    GestioneMySql.TransazioneCommit();
                }

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");
            }
            catch (Exception ex)
            {
                GestioneMySql.TransazioneRollback();
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
            }
        }

        public static void RunScriptTemplate(int intKeyTempl, string strName)
        {
            StringBuilder strQuery = new StringBuilder();

            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                strQuery.AppendLine(string.Format(@"DELETE FROM PDFASSOCIATION WHERE KEY_TEMPL = {0};", intKeyTempl));

                string strInsert = " INSERT INTO PDFASSOCIATION (KEY_TEMPL, PDF_FIELD, TYPE, FIELD, BIOMETRIC, NUM_PAGE) VALUES ";
                

                switch (strName)
                {
                    case "Informativa Clienti Antico Convento ITA":

                        strQuery.AppendLine(strInsert + string.Format(@"({0}, 'txtNameSurname', 'TXT', 'user.COGNOME,user.NOME', NULL, NULL);", intKeyTempl));
                        strQuery.AppendLine(strInsert + string.Format(@"({0}, 'txtCell', 'TXT', 'user.CELL', NULL, NULL);", intKeyTempl));
                        strQuery.AppendLine(strInsert + string.Format(@"({0}, 'txtDate', 'TXT', NULL, NULL, NULL);", intKeyTempl));
                        strQuery.AppendLine(strInsert + string.Format(@"({0}, 'txtMail', 'TXT', 'user.MAIL', NULL, NULL);", intKeyTempl));
                        strQuery.AppendLine(strInsert + string.Format(@"({0}, 'txtSignature', 'SIG', NULL, NULL, 2);", intKeyTempl));
                        break; 
                }

                MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                comm.CommandText = strQuery.ToString();
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

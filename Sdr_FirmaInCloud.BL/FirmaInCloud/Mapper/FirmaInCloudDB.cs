using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.BL
{
    public static class FirmaInCloudDB
    {          
        public static DataTable LoadTemplate(int? intKey, string strXSign)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                string strQuery = "SELECT KEY_TEMPL, DESCR, PATH, DTINS FROM TEMPLATE WHERE 1 = 1 ";

                if(intKey != null) 
                { 
                
                    strQuery = strQuery + string.Format(@"AND KEY_TEMPL = {0}", intKey);
                }
                if (!string.IsNullOrEmpty(strXSign))
                {
                    strQuery = strQuery + "AND X_SIGN != 'X'";
                }

                var mySqlDataAdapter = new MySqlDataAdapter(strQuery, GestioneMySql.connection);

                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return dataTable;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static DataTable LoadTemplateFirmati(TemplateLinq template)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                string strQuery = string.Format(@"SELECT TEMPLATE_LINQ.DESCR, TEMPLATE_LINQ.PATH, TEMPLATE_LINQ.DTSIGN 
                FROM TEMPLATE_LINQ                
                INNER JOIN Users ON Users.KEY_USER = TEMPLATE_LINQ.KEY_USER
                WHERE Users.KEY_USER = {0} ORDER BY TEMPLATE_LINQ.DTSIGN DESC", template.KEY_USER.ToString());

                var mySqlDataAdapter = new MySqlDataAdapter(strQuery, GestioneMySql.connection);

                DataTable dataTable = new DataTable();
                mySqlDataAdapter.Fill(dataTable);

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return dataTable;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static void SaveTemplate_Linq(TemplateLinq templateLinq, Users user)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();

                sb.AppendLine("SELECT *");
                sb.AppendLine("FROM TEMPLATE_LINQ");
                sb.AppendLine("WHERE KEY_USER = @KEY_USER AND KEY_TEMPL_L = @KEY_TEMPL_L");

                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("KEY_USER", templateLinq.KEY_USER));
                cmd.Parameters.Add(new MySqlParameter("KEY_TEMPL_L", templateLinq.KEY_TEMPL_L));

                MySqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();

                    MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                    comm.CommandText = "INSERT INTO TEMPLATE_LINQ(KEY_USER,DTSIGN,DESCR,PATH,KEY_TEMPL) VALUES(@KEY_USER,@DTSIGN,@DESCR,@PATH,@KEY_TEMPL)";
                    comm.Parameters.AddWithValue("@KEY_USER", templateLinq.KEY_USER);                  
                    comm.Parameters.AddWithValue("@DTSIGN", DateTime.Now.ToString("yyyyMMddHHmm"));
                    comm.Parameters.AddWithValue("@DESCR", templateLinq.DESCR);
                    comm.Parameters.AddWithValue("@PATH", templateLinq.PATH);
                    comm.Parameters.AddWithValue("@KEY_TEMPL", templateLinq.KEY_TEMPL);
                    comm.ExecuteNonQuery();
                }
                dr.Close();
                //deve essere gestita la parte delle check box    

                //Deve aggiornare la KEY PERM della ute
                //UtenzeDB.SaveUtenze(ute);

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");
                
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
            }
        }

        public static MySqlDataReader LoadNazione()
        {
            try
            {
                var command = new MySqlCommand("SELECT * FROM NAZIONE", GestioneMySql.connection);
                var reader = command.ExecuteReader();
                return reader;                
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }
         
        public static MySqlDataAdapter LoadRegione(int idKey)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(string.Format(@"SELECT * FROM REGIONE WHERE ID_NAZIONE = {0} ORDER BY REGIONE ASC", idKey), GestioneMySql.StringaConnessione);

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return mySqlDataAdapter;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static MySqlDataAdapter LoadProvince(int idKey)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(string.Format(@"SELECT * FROM PROVINCE WHERE COD_REG = {0} ORDER BY PROVINCIA ASC", idKey), GestioneMySql.StringaConnessione);

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return mySqlDataAdapter;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static MySqlDataAdapter LoadComuni(int idKey)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(string.Format(@"SELECT * FROM COMUNI WHERE ID_PROV = {0} ORDER BY COMUNE ASC", idKey), GestioneMySql.StringaConnessione);

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return mySqlDataAdapter;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static MySqlDataAdapter LoadLocalita(int idKey)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(string.Format(@"SELECT 0 ID, NULL LOCALITA, NULL ID_COMU UNION SELECT ID, LOCALITA, ID_COMU FROM LOCALITA WHERE ID_COMU = {0} ORDER BY LOCALITA ASC", idKey), GestioneMySql.StringaConnessione);

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return mySqlDataAdapter;
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

 
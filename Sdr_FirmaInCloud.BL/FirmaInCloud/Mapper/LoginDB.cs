using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.BL.FirmaInCloud.Mapper
{
    public class LoginDB
    {
        public static string ToSha256(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString)) return string.Empty;
            using (SHA256 shaM = new SHA256Managed())
            {
                return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(inputString)));
            }
        }

        public static Login LoadLogin(string strUserID, string strPassword)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                Login login = new Login();

                sb.AppendLine("SELECT KEY_LOG, USER_ID, PASSWORD_ID, X_PERMISSION, X_ADMIN, X_TEMPLATE, X_FIRST_LOG, DATE_EXPIRED, NAME, SURNAME FROM LOGIN");
                sb.AppendLine("WHERE BINARY USER_ID = @USER_ID ");

                if (!string.IsNullOrEmpty(strPassword)) 
                {
                    sb.AppendLine("AND BINARY PASSWORD_ID = @PASSWORD_ID");
                }

                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("USER_ID", strUserID));
                cmd.Parameters.Add(new MySqlParameter("PASSWORD_ID", strPassword));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    login.KEY_LOG = dr.GetInt16("KEY_LOG");

                    if (!string.IsNullOrEmpty(dr.GetString("USER_ID")))
                        login.USER_ID = dr.GetString("USER_ID");

                    if (!string.IsNullOrEmpty(dr.GetString("PASSWORD_ID")))
                        login.PASSWORD_ID = dr.GetString("PASSWORD_ID");

                    if (!string.IsNullOrEmpty(dr.GetString("NAME")))
                        login.NAME = dr.GetString("NAME");

                    if (!string.IsNullOrEmpty(dr.GetString("SURNAME")))
                        login.SURNAME= dr.GetString("SURNAME");

                    if (!dr.IsDBNull(dr.GetOrdinal("X_PERMISSION")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("X_PERMISSION")))
                            login.X_PERMISSION = dr.GetString("X_PERMISSION");
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("X_ADMIN")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("X_ADMIN")))
                            login.X_ADMIN = dr.GetString("X_ADMIN");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("X_TEMPLATE")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("X_TEMPLATE")))
                            login.X_TEMPLATE = dr.GetString("X_TEMPLATE");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("X_FIRST_LOG")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("X_FIRST_LOG")))
                            login.X_FIRST_LOG = dr.GetString("X_FIRST_LOG");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("DATE_EXPIRED")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("DATE_EXPIRED")))
                            login.DATE_EXPIRED = DateTime.Parse(dr.GetString("DATE_EXPIRED"));
                    }
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return login;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static void SaveNewLogin(Login login) 
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlCommand comm = GestioneMySql.connection.CreateCommand();

                comm.CommandText = "INSERT INTO LOGIN (USER_ID, PASSWORD_ID, X_TEMPLATE, X_FIRST_LOG, DATE_EXPIRED, NAME, SURNAME) VALUES (@USER_ID, @PASSWORD_ID, @X_TEMPLATE, @X_FIRST_LOG, @DATE_EXPIRED, @NAME, @SURNAME)";
                comm.Parameters.AddWithValue("@USER_ID", login.USER_ID);
                comm.Parameters.AddWithValue("@PASSWORD_ID", ToSha256(login.PASSWORD_ID));
                comm.Parameters.AddWithValue("@X_TEMPLATE", login.X_TEMPLATE);
                comm.Parameters.AddWithValue("@X_FIRST_LOG", login.X_FIRST_LOG);
                comm.Parameters.AddWithValue("@DATE_EXPIRED", login.DATE_EXPIRED.Date.ToString("yyyyMMdd").StringOrNull());
                comm.Parameters.AddWithValue("@NAME", login.NAME);
                comm.Parameters.AddWithValue("@SURNAME", login.SURNAME);

                comm.ExecuteNonQuery();
                
                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                throw ex;
            }
        }

        public static void SaveLogin(int? intKeyLog, string strPassword, string strFirstAccess)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                MySqlCommand comm = GestioneMySql.connection.CreateCommand();

                sb.AppendLine("UPDATE LOGIN SET X_FIRST_LOG=@X_FIRST_LOG, DATE_EXPIRED=@DATE_EXPIRED "); 
                if (!string.IsNullOrEmpty(strPassword)) 
                {
                    sb.AppendLine(",PASSWORD_ID = @PASSWORD_ID ");
                }
                sb.AppendLine("WHERE KEY_LOG = @KEY_LOG");

                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);
                cmd.Parameters.AddWithValue("@KEY_LOG", intKeyLog);
                cmd.Parameters.AddWithValue("@PASSWORD_ID", ToSha256(strPassword));
                cmd.Parameters.AddWithValue("@X_FIRST_LOG", strFirstAccess.StringOrNull());
                cmd.Parameters.AddWithValue("@DATE_EXPIRED", DateTime.Now.AddDays(90).ToString("yyyyMMdd"));

                cmd.ExecuteNonQuery();
                  
                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                throw ex;
            }
        }

        public static LoginAccess LoadLoginAccess() 
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                LoginAccess loginAccess;                 
                using (MySqlCommand cmd = new MySqlCommand("SELECT DT_LOGIN, KEY_LOG, USER_ID FROM LOGIN_ACCESS INNER JOIN LOGIN ON LOGIN.KEY_LOG = KEY_LOG_ACC ORDER BY DT_LOGIN DESC LIMIT 1", GestioneMySql.connection))
                {
                    loginAccess= new LoginAccess();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        loginAccess.USER_ID = dr.GetString("USER_ID");
                        loginAccess.DT_LOGIN = DateTime.ParseExact(dr.GetString("DT_LOGIN"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None);
                        loginAccess.KEY_LOG = dr.GetUInt16("KEY_LOG");
                    }
                    dr.Close();
                }
                GestioneMySql.CloseConnection();
                return loginAccess;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static void SaveLoginAccess(int? intKeyLog)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlCommand comm = GestioneMySql.connection.CreateCommand();

                comm.CommandText = "INSERT INTO LOGIN_ACCESS (KEY_LOG_ACC, DT_LOGIN) VALUES (@KEY_LOG_ACC, @DT_LOGIN)";                  
                comm.Parameters.AddWithValue("@KEY_LOG_ACC", intKeyLog);
                comm.Parameters.AddWithValue("@DT_LOGIN", DateTime.Now.ToString("yyyyMMddHHmmss"));
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

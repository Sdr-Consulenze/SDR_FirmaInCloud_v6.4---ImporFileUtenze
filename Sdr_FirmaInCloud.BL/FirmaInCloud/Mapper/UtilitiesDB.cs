using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.BL
{
    public class UsersDB
    {
        public static void SaveListUsers(List<Users> listUsers)
        {
            foreach (var user in listUsers)
            {
                SaveUser(user);
            }
        }

        public static Users SaveUser(Users user)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                string query;
                if (user.KEY_USER.Equals(0))
                {
                    query = "INSERT INTO Users(NOME, COGNOME, DTINS, DTNAS, SST, STRADA, KEY_NAZ, KEY_REG, KEY_PRO, KEY_COM, KEY_LOC, TYP_UTE, SEX, COD_FIS, P_IVA, NCN, CELL, MAIL, KEY_TEMPL, PERS1, PERS2, PERS3, PERS4, PERS5, PERS6, KEY_NAZ_NAS, KEY_REG_NAS, KEY_PRO_NAS, KEY_COM_NAS, KEY_LOC_NAS, TEL, KEY_DOC )";
                    query = query + " VALUES (@NOME, @COGNOME, @DTINS, @DTNAS, @SST, @STRADA, @KEY_NAZ, @KEY_REG, @KEY_PRO, @KEY_COM, @KEY_LOC, @TYP_UTE, @SEX, @COD_FIS, @P_IVA, @NCN, @CELL, @MAIL, @KEY_TEMPL, @PERS1, @PERS2, @PERS3, @PERS4, @PERS5, @PERS6, @KEY_NAZ_NAS, @KEY_REG_NAS, @KEY_PRO_NAS, @KEY_COM_NAS, @KEY_LOC_NAS, @TEL, @KEY_DOC )";
                    comm.CommandText = query;
                }
                else
                {
                    query = "UPDATE Users SET ";
                    query = query + " NOME=@NOME, COGNOME=@COGNOME, DTINS=@DTINS, DTNAS=@DTNAS, SST=@SST, STRADA=@STRADA, KEY_NAZ=@KEY_NAZ, KEY_REG=@KEY_REG, KEY_PRO=@KEY_PRO, KEY_COM=@KEY_COM, KEY_LOC=@KEY_LOC, TYP_UTE=@TYP_UTE, SEX=@SEX, COD_FIS=@COD_FIS, P_IVA=@P_IVA, NCN=@NCN, CELL=@CELL, MAIL=@MAIL, KEY_TEMPL=@KEY_TEMPL, PERS1=@PERS1, PERS2=@PERS2, PERS3=@PERS3, PERS4=@PERS4, PERS5=@PERS5, PERS6=@PERS6, KEY_NAZ_NAS=@KEY_NAZ_NAS, KEY_REG_NAS=@KEY_REG_NAS, KEY_PRO_NAS=@KEY_PRO_NAS, KEY_COM_NAS=@KEY_COM_NAS, KEY_LOC_NAS=@KEY_LOC_NAS, TEL=@TEL, KEY_DOC=@KEY_DOC ";
                    query = query + " WHERE KEY_USER=@KEY_USER";
                    comm.CommandText = query;
                    comm.Parameters.AddWithValue("@KEY_USER", user.KEY_USER);
                }

                comm.Parameters.AddWithValue("@NOME", user.NOME);
                comm.Parameters.AddWithValue("@COGNOME", user.COGNOME);
                comm.Parameters.AddWithValue("@DTINS", DateTime.Now.ToString("yyyyMMdd"));
                comm.Parameters.AddWithValue("@DTNAS", user.DTNAS.Date.ToString("yyyyMMdd").StringOrNull());
                comm.Parameters.AddWithValue("@SST", user.SST.StringOrNull());
                comm.Parameters.AddWithValue("@STRADA", user.STRADA.StringOrNull());

                comm.Parameters.AddWithValue("@KEY_NAZ",user.KEY_NAZ); 
                comm.Parameters.AddWithValue("@KEY_REG",user.KEY_REG); 
                comm.Parameters.AddWithValue("@KEY_PRO",user.KEY_PRO); 
                comm.Parameters.AddWithValue("@KEY_COM",user.KEY_COM); 
                comm.Parameters.AddWithValue("@KEY_LOC",user.KEY_LOC);

                comm.Parameters.AddWithValue("@TYP_UTE", user.TYP_UTE.StringOrNull());
                comm.Parameters.AddWithValue("@SEX", user.SEX.StringOrNull());
                comm.Parameters.AddWithValue("@COD_FIS", user.COD_FIS.StringOrNull());
                comm.Parameters.AddWithValue("@P_IVA", user.P_IVA.StringOrNull());
                comm.Parameters.AddWithValue("@NCN", user.NCN.StringOrNull());
                comm.Parameters.AddWithValue("@CELL", user.CELL.StringOrNull());
                comm.Parameters.AddWithValue("@TEL", user.TEL.StringOrNull());
                comm.Parameters.AddWithValue("@MAIL", user.MAIL.StringOrNull());
                comm.Parameters.AddWithValue("@KEY_TEMPL", user.KEY_TEMPL);

                comm.Parameters.AddWithValue("@PERS1", user.PERS1.StringOrNull());
                comm.Parameters.AddWithValue("@PERS2", user.PERS2.StringOrNull());
                comm.Parameters.AddWithValue("@PERS3", user.PERS3.StringOrNull());
                comm.Parameters.AddWithValue("@PERS4", user.PERS4.StringOrNull());
                comm.Parameters.AddWithValue("@PERS5", user.PERS5.StringOrNull());
                comm.Parameters.AddWithValue("@PERS6", user.PERS6.StringOrNull());

                comm.Parameters.AddWithValue("@KEY_NAZ_NAS", user.KEY_NAZ_NAS);
                comm.Parameters.AddWithValue("@KEY_REG_NAS", user.KEY_REG_NAS);
                comm.Parameters.AddWithValue("@KEY_PRO_NAS", user.KEY_PRO_NAS);
                comm.Parameters.AddWithValue("@KEY_COM_NAS", user.KEY_COM_NAS);
                comm.Parameters.AddWithValue("@KEY_LOC_NAS", user.KEY_LOC_NAS);
                comm.Parameters.AddWithValue("@KEY_DOC", user.KEY_DOC);

                comm.ExecuteNonQuery();                 

                MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID() AS KEY_USER", GestioneMySql.connection);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    user.KEY_USER = dr.GetInt16("KEY_USER");
                }
                dr.Close();

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return user;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static List<Users> LoadUser(int? intKeyUte)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                List<Users> listUsers  = new List<Users>();
                
                sb.AppendLine("SELECT *");
                sb.AppendLine("FROM Users");
                if (intKeyUte.HasValue)
                {
                    sb.AppendLine("WHERE KEY_USER = @KEY_USER");
                }

                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);
                
                cmd.Parameters.Add(new MySqlParameter("KEY_USER", intKeyUte));
                
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Users user = new Users();
                    user.KEY_USER = dr.GetInt16("KEY_USER");

                    if (!string.IsNullOrEmpty(dr.GetString("Nome")))
                        user.NOME = dr.GetString("Nome");

                    if (!string.IsNullOrEmpty(dr.GetString("Cognome")))
                        user.COGNOME = dr.GetString("Cognome");

                    if (!dr.IsDBNull(dr.GetOrdinal("DTNAS")))
                    {
                        user.DTNAS = DateTime.ParseExact(dr.GetString("DTNAS"), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None);
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("TYP_UTE")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("TYP_UTE")))
                            user.TYP_UTE = dr.GetString("TYP_UTE");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("P_IVA")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("P_IVA")))
                            user.P_IVA = dr.GetString("P_IVA");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("COD_FIS")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("COD_FIS")))
                            user.COD_FIS = dr.GetString("COD_FIS");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_NAZ")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_NAZ")))
                            user.KEY_NAZ = dr.GetInt16("KEY_NAZ");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_REG")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_REG")))
                            user.KEY_REG = dr.GetInt16("KEY_REG");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_PRO")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_PRO")))
                            user.KEY_PRO = dr.GetInt16("KEY_PRO");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_COM")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_COM")))
                            user.KEY_COM = dr.GetInt16("KEY_COM");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_LOC")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_LOC")))
                            user.KEY_LOC = dr.GetInt16("KEY_LOC");
                    } 
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_NAZ_NAS")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_NAZ_NAS")))
                            user.KEY_NAZ_NAS = dr.GetInt16("KEY_NAZ_NAS");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_REG_NAS")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_REG_NAS")))
                            user.KEY_REG_NAS = dr.GetInt16("KEY_REG_NAS");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_PRO_NAS")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_PRO_NAS")))
                            user.KEY_PRO_NAS = dr.GetInt16("KEY_PRO_NAS");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_COM_NAS")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_COM_NAS")))
                            user.KEY_COM_NAS = dr.GetInt16("KEY_COM_NAS");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_LOC_NAS")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_LOC_NAS")))
                            user.KEY_LOC_NAS = dr.GetInt16("KEY_LOC_NAS");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_TEMPL")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_TEMPL")))
                            user.KEY_TEMPL = dr.GetInt16("KEY_TEMPL");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("Sst")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("Sst")))
                            user.SST = dr.GetString("Sst");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("STRADA")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("STRADA")))
                            user.STRADA = dr.GetString("STRADA");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("NCN")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("NCN")))
                            user.NCN = dr.GetString("NCN");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("CELL")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("CELL")))
                            user.CELL = dr.GetString("CELL");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("TEL")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("TEL")))
                            user.TEL = dr.GetString("TEL");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("MAIL")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("MAIL")))
                            user.MAIL = dr.GetString("MAIL");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("PERS1")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("PERS1")))
                            user.PERS1 = dr.GetString("PERS1");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("PERS2")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("PERS2")))
                            user.PERS2 = dr.GetString("PERS2");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("PERS3")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("PERS3")))
                            user.PERS3 = dr.GetString("PERS3");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("PERS4")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("PERS4")))
                            user.PERS4 = dr.GetString("PERS4");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("PERS5")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("PERS5")))
                            user.PERS5 = dr.GetString("PERS5");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("PERS6")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("PERS6")))
                            user.PERS6 = dr.GetString("PERS6");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("KEY_DOC")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("KEY_DOC")))
                            user.KEY_DOC = dr.GetInt16("KEY_DOC");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("SEX")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("SEX")))
                            user.SEX = dr.GetString("SEX");
                    }
                    listUsers.Add(user);
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return listUsers;

            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static void DeleteUser(int intKeyUte)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                comm.CommandText = "DELETE FROM Users WHERE KEY_USER = @KEY_USER";
                comm.Parameters.AddWithValue("@KEY_USER", intKeyUte);
                comm.ExecuteNonQuery();

                GestioneMySql.CloseConnection();
            }
            catch (Exception e)
            {
                GestioneMySql.CloseConnection();
                Console.WriteLine(e.Message);
            }
        }

        //public static DataTable LoadUser(string strKeyTempl)
        //{
        //    try
        //    {
        //        if (!GestioneMySql.OpenConnection())
        //            throw new Exception("Errore nell'apertura della connessione."); 

        //        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(GetQueryUser(strKeyTempl), GestioneMySql.connection);

        //        DataTable dataTable = new DataTable();
        //        mySqlDataAdapter.Fill(dataTable);

        //        if (!GestioneMySql.CloseConnection())
        //            throw new Exception("Errore nella chiusura della connessione.");


        //        return dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        GestioneMySql.CloseConnection();
        //        MessageBox.Show("Errore: " + ex.Message);
        //        return null;
        //    }
        //}
        //public static string GetQueryUser(string strKeyTempl) 
        //{
        //    var sb = new StringBuilder();

        //    sb.AppendLine("SELECT *");
        //    sb.AppendLine("FROM USERS");
        //    if (!string.IsNullOrEmpty(strKeyTempl))
        //    {
        //        sb.AppendLine(string.Format(@"WHERE KEY_USER IN (SELECT KEY_USER FROM TEMPLATE_LINQ WHERE KEY_TEMPL = {0})", strKeyTempl));
        //    }
        //    sb.AppendLine("ORDER BY DTINS DESC");

        //    return sb.ToString();
        //}

        public static string SetQueryUser(string strFilter) 
        {
            var sb = new StringBuilder();

            sb.AppendLine("SELECT KEY_USER, NOME, COGNOME, DATE_FORMAT(DTNAS,'%d/%m/%Y') 'DATA DI NASCITA', CONCAT(SST,' ',STRADA,' ',NCN) 'INDIRIZZO', CELL CELLULARE, DATE_FORMAT(DTINS,'%d/%m/%Y') 'DATA INSERIMENTO', MAIL ");
            sb.AppendLine("FROM USERS");
            if (!string.IsNullOrEmpty(strFilter))
            {
                sb.AppendLine(string.Format(@"WHERE {0}", strFilter));
            }
            sb.AppendLine("ORDER BY DTINS DESC");

            return sb.ToString();
        }

    }
}

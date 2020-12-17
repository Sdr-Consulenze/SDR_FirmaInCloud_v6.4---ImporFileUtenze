using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.BL
{
    public class FlagDB
    {
        public static List<Flag> LoadFlag(string strKeyTempl, string strTypFlag)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                List<Flag> flagsList = new List<Flag>();

                string strQuery = string.Format(@"SELECT KEY_FLAG, KEY_TEMPL, DFLAG, X_FLAG, X_CORD, Y_CORD, WIDTH, N_PAGE
                FROM FLAG
                WHERE FLAG.KEY_TEMPL = {0} ", strKeyTempl);

                if (!string.IsNullOrEmpty(strTypFlag)) 
                {
                    strQuery = strQuery + string.Format(@"AND FLAG.TYP_FLAG = '{0}'", strTypFlag);
                    if (strTypFlag == "C") 
                    {
                        strQuery = strQuery + "GROUP BY DFLAG";
                    }
                }

                MySqlCommand cmd = new MySqlCommand(strQuery, GestioneMySql.connection);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Flag flag = new Flag();

                    if (!dr.IsDBNull(dr.GetOrdinal("DFLAG")))
                    {
                        flag.DFLAG = dr.GetString("DFLAG");
                    }
                    flag.KEY_TEMPL = dr.GetInt16("KEY_TEMPL");

                    if (!dr.IsDBNull(dr.GetOrdinal("X_CORD")))
                    {
                        flag.X_CORD = dr.GetInt16("X_CORD");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("Y_CORD")))
                    {
                        flag.Y_CORD = dr.GetInt16("Y_CORD");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("WIDTH")))
                    {
                        flag.WIDTH = dr.GetInt16("WIDTH");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("N_PAGE")))
                    {
                        flag.N_PAGE = dr.GetInt32("N_PAGE");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("X_FLAG")))
                    {
                        flag.X_FLAG = dr.GetString("X_FLAG");
                    };
                    flagsList.Add(flag);                    
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return flagsList;

            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static List<Flag> LoadSingleFlagSignature(string strKeyTempl, string strTypFlag)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                string strQuery = string.Format(@"SELECT X_CORD, Y_CORD, WIDTH, N_PAGE
                FROM FLAG
                WHERE FLAG.KEY_TEMPL = {0} AND FLAG.TYP_FLAG = '{1}'", strKeyTempl, strTypFlag);

                MySqlCommand cmd = new MySqlCommand(strQuery, GestioneMySql.connection);
                MySqlDataReader dr = cmd.ExecuteReader();
                
                List<Flag> flagsList = new List<Flag>();

                while (dr.Read())
                {
                    Flag flag = new Flag();
                    if (!dr.IsDBNull(dr.GetOrdinal("X_CORD")))
                    {
                        flag.X_CORD = dr.GetInt32("X_CORD");
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("WIDTH")))
                    {
                        flag.WIDTH = dr.GetInt32("WIDTH");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("Y_CORD")))
                    {
                        flag.Y_CORD = dr.GetInt32("Y_CORD");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("N_PAGE")))
                    {
                        flag.N_PAGE = dr.GetInt32("N_PAGE");
                    };
                    flagsList.Add(flag);
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return flagsList;

            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static Flag LoadSingleFlag(string strKeyTempl, string strTypFlag, int? intIdField, string strValue) 
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                Flag flags = new Flag();

                string strQuery = string.Format(@"SELECT X_CORD, Y_CORD, WIDTH, N_PAGE
                FROM FLAG
                WHERE FLAG.KEY_TEMPL = {0} AND FLAG.TYP_FLAG = '{1}' AND FLAG.ID_FLAG = {2} AND FLAG.X_FLAG ", strKeyTempl, strTypFlag, intIdField);


                if (string.IsNullOrEmpty(strValue)) 
                {
                    strQuery = strQuery + "IS NULL ";
                }
                else
                {
                    strQuery = strQuery + " = '" + strValue + "'" ;
                }

                MySqlCommand cmd = new MySqlCommand(strQuery, GestioneMySql.connection);
                MySqlDataReader dr = cmd.ExecuteReader();
                
                Flag flag = new Flag();

                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("X_CORD")))
                    {
                        flag.X_CORD = dr.GetInt32("X_CORD");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("WIDTH")))
                    {
                        flag.WIDTH = dr.GetInt32("WIDTH");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("Y_CORD")))
                    {
                        flag.Y_CORD = dr.GetInt32("Y_CORD");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("N_PAGE")))
                    {
                        flag.N_PAGE = dr.GetInt32("N_PAGE");
                    };
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return flag;

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

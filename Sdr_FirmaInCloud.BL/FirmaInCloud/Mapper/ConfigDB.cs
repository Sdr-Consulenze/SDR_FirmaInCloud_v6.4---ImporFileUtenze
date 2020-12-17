using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.BL.FirmaInCloud.Mapper
{
    public class ConfigDB
    {
        public static Config LoadConfig(Config.ParamValues paramValues)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                Config config = new Config();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM CONFIG WHERE CONF_PARAM = @CONF_PARAM", GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("CONF_PARAM", paramValues.ToString()));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {   
                    if (!dr.IsDBNull(dr.GetOrdinal("CONF_VALUE")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("CONF_VALUE")))
                            config.CONF_VALUE = dr.GetString("CONF_VALUE");
                    }                    
                }

                dr.Close();

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                return config;

            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }
        public static void SaveConfig(Config.ParamValues paramValues, string value)
        {
            try
            {
                MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                if (LoadConfig(paramValues).CONF_VALUE == null)
                {
                    comm.CommandText = "INSERT INTO CONFIG(CONF_PARAM,CONF_VALUE) VALUES(@CONF_PARAM,@CONF_VALUE)";
                }
                else
                {
                    comm.CommandText = "UPDATE CONFIG SET CONF_VALUE=@CONF_VALUE WHERE CONF_PARAM=@CONF_PARAM";                     
                }

                comm.Parameters.AddWithValue("@CONF_PARAM", paramValues.ToString());
                comm.Parameters.AddWithValue("@CONF_VALUE", value);

                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");
                
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

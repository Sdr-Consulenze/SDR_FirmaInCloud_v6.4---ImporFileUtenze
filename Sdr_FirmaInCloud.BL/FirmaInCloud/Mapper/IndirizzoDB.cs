using MySql.Data.MySqlClient;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Domain;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sdr_FirmaInCloud.BL.FirmaInCloud.Mapper
{
    public class IndirizzoDB
    {
        public static Citta LoadCitta(int intkeyLoc)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                Citta citta = new Citta();

                sb.AppendLine("SELECT *");
                sb.AppendLine("FROM LOCALITA"); 
                sb.AppendLine("WHERE ID = @ID");
                
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);

                cmd.Parameters.Add(new MySqlParameter("ID", intkeyLoc));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {   
                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                    {
                        citta.ID = dr.GetInt32("ID");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("ID_COMU")))
                    {
                        citta.ID_COMU = dr.GetInt32("ID_COMU");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("LOCALITA")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("LOCALITA")))
                            citta.LOCALITA = dr.GetString("LOCALITA");
                    }
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return citta;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static Nazione LoadNazione(int? intkeyNaz, string strProv)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                Nazione nazione = new Nazione();

                sb.AppendLine("SELECT *");
                sb.AppendLine("FROM NAZIONE");
                if (!string.IsNullOrEmpty(strProv)) 
                {
                    sb.AppendLine("WHERE PROV = @PROV LIMIT 1");
                } 
                else 
                {
                    sb.AppendLine("WHERE ID = @ID");
                }                

                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("ID", intkeyNaz));
                cmd.Parameters.Add(new MySqlParameter("PROV", strProv));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                    {
                        nazione.ID = dr.GetInt32("ID");
                    };                    
                    if (!dr.IsDBNull(dr.GetOrdinal("COD_REG")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("COD_REG")))
                            nazione.COD_REG = dr.GetString("COD_REG");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("COD_ISTAT")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("COD_ISTAT")))
                            nazione.COD_ISTAT = dr.GetString("COD_ISTAT");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("NAZIONE")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("NAZIONE")))
                            nazione.NAZIONE = dr.GetString("NAZIONE");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("PROV")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("PROV")))
                            nazione.PROV = dr.GetString("PROV");
                    }
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return nazione;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static Comuni LoadComuni(int? keyCom) 
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                Comuni comuni = new Comuni();

                sb.AppendLine("SELECT *");
                sb.AppendLine("FROM COMUNI ");
                sb.AppendLine("WHERE ID = @ID");

                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);

                cmd.Parameters.Add(new MySqlParameter("ID", keyCom));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                    {
                        comuni.ID = dr.GetInt32("ID");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("CAP")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("CAP")))
                            comuni.CAP = dr.GetString("CAP");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("COMUNE")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("COMUNE")))
                            comuni.COMUNE = dr.GetString("COMUNE");
                    };
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return comuni;
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            }
        }

        public static Localita LoadLocalita(int? keyLoc)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                Localita localita = new Localita();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM LOCALITA WHERE ID = @ID", GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("ID", keyLoc));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                    {
                        localita.ID = dr.GetInt32("ID");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("LOCALITA")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("LOCALITA")))
                            localita.LOCALITA = dr.GetString("LOCALITA");
                    };
                    if (!dr.IsDBNull(dr.GetOrdinal("ID_COMU")))
                    {
                        localita.ID_COMU = dr.GetInt32("ID_COMU");
                    };
                }
                dr.Close();
                GestioneMySql.CloseConnection();
                return localita;
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

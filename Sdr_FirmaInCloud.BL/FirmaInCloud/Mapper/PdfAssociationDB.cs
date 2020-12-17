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
    public class PdfAssociationDB
    {
        public static List<PdfAssociation> LoadPdfAssociation(int intKeyPdf)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                var sb = new StringBuilder();
                List<PdfAssociation> pdfAssociationList = new List<PdfAssociation>();

                sb.AppendLine("SELECT *");
                sb.AppendLine("FROM PDFASSOCIATION");
                sb.AppendLine("WHERE KEY_TEMPL = @KEY_TEMPL");

                MySqlCommand cmd = new MySqlCommand(sb.ToString(), GestioneMySql.connection);
                cmd.Parameters.Add(new MySqlParameter("KEY_TEMPL", intKeyPdf));

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    PdfAssociation pdfAssociation = new PdfAssociation();

                    pdfAssociation.KEY_PDF = dr.GetInt16("KEY_PDF");

                    if (!string.IsNullOrEmpty(dr.GetString("PDF_FIELD")))
                        pdfAssociation.PDF_FIELD = dr.GetString("PDF_FIELD");

                    if (!string.IsNullOrEmpty(dr.GetString("TYPE")))
                        pdfAssociation.TYPE = dr.GetString("TYPE");

                    if (!dr.IsDBNull(dr.GetOrdinal("FIELD")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("FIELD")))
                            pdfAssociation.FIELD = dr.GetString("FIELD");
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("BIOMETRIC")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("BIOMETRIC")))
                            pdfAssociation.FIELD = dr.GetString("BIOMETRIC");
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("NUM_PAGE")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("NUM_PAGE")))
                            pdfAssociation.NUM_PAGE = dr.GetInt32("NUM_PAGE");
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("FORM_DATE")))
                    {
                        if (!string.IsNullOrEmpty(dr.GetString("FORM_DATE")))
                            pdfAssociation.FORM_DATE = dr.GetString("FORM_DATE");
                    }
                    pdfAssociationList.Add(pdfAssociation);
                }
                dr.Close();

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusurua della connessione."); 

                return pdfAssociationList;
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

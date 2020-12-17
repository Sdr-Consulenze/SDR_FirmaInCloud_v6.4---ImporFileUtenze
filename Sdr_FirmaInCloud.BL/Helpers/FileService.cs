using Sdr_FirmaInCloud.BL;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SDR_FirmaInCloud.BL.Helpers
{
    public class FileService
    {
        public static string CaricaFile(string strExt, string strFilter)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = strExt;  
            dlg.Filter = strFilter; 
            dlg.ShowDialog();

            return dlg.FileName;
        }

        public static string CreateFolder(string strPathFolder) 
        {
            Directory.CreateDirectory(strPathFolder);
            return strPathFolder;
        }

        public static string GetXMLPathFileToSave()
        {
            string strPathXML = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Guest.xml";

            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            
            var dialog_result = saveFileDialog.ShowDialog();

            if (dialog_result == DialogResult.OK)
                strPathXML = saveFileDialog.FileName;

            return strPathXML;
        }

        public static void AcquireXML(TextReader reader, string fileName)
        {
            string xml;

            using (reader)
            {
                xml = reader.ReadToEnd();
            }

            object enitiyBase;
            
            try
            {
                using (StreamReader readerDue = new StreamReader(fileName, Encoding.UTF8, true))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PASSPORTSCAN));//, new XmlRootAttribute("EricsoftGuestData"));
                    enitiyBase = serializer.Deserialize(readerDue);
                }
            }
            catch (InvalidOperationException operationEx)
            {
                throw new ApplicationException("Il file selezionato non è corretto. " + operationEx.Message);
            }

            try 
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione. ");

                GestioneMySql.TransazioneBegin();

                PassportscanDB.Save(enitiyBase as PASSPORTSCAN);
                FileInfo fileInfo = new FileInfo(fileName);

                TemplateXml templateXml = new TemplateXml();
                templateXml.FILE_NM = fileInfo.Name;
                templateXml.FILE_PATH = fileInfo.FullName;

                TemplateXmlDB.SaveTemplate(templateXml);

                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione. ");

                GestioneMySql.TransazioneCommit();

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione. ");
            }
            catch (Exception e)
            {
                GestioneMySql.TransazioneRollback();
                throw e;
            }
        }

 

    }
}


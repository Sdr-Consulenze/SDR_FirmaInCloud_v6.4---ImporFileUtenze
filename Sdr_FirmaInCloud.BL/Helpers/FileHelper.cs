using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.BL.Helpers
{
    public class FileHelper
    {
        public static void OpenFile(string sFileName)
        {
            //string path = @"C:\1\C# Threading Handbook.pdf";
            //System.Diagnostics.Process.Start("IExplore.exe", path);
            Process.Start(sFileName);
        }

        public static bool CreateFileFromText(string pathFileNameWithExtension, string text, bool ReplaceExist)
        {
            bool res = false;
            try
            {
                if (File.Exists(pathFileNameWithExtension))
                {
                    if (!ReplaceExist)
                        return res;

                    File.Delete(pathFileNameWithExtension);                    
                }
                File.WriteAllText(pathFileNameWithExtension, text);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        internal static byte[] GetRawData(String pathFile)
        {
            if (!File.Exists(pathFile))
                throw new ApplicationException(String.Format("Raw data path not found: {0}", pathFile));
            return File.ReadAllBytes(pathFile);
        }

        public static string GetPath(string strKeySetting)
        {
            try
            {
                string[] dirInfos = System.Environment.GetLogicalDrives();

                System.Configuration.ConfigurationManager.AppSettings.Set(strKeySetting, $"{dirInfos[0]}" + $"FirmaInCloud\\Pdf\\{strKeySetting}\\");

                string pdfPath = System.Configuration.ConfigurationManager.AppSettings[strKeySetting];

                if (!Directory.Exists(pdfPath))
                    Directory.CreateDirectory(pdfPath);

                return pdfPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message, "Configurazione errata - impossibile recuperare il percorso di elaborazione del file pdf");
                return null;
            }
        }

    }
}

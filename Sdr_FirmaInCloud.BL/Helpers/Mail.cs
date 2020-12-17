using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;

namespace Sdr_FirmaInCloud.BL.Helpers
{
    public class Mail
    {
        public static bool SendMail(string strPathFile, string strTo, string strDisplayName, string strBodyRandom)
        {
            try
            {
                string strHost = "";
                var config = ConfigDB.LoadConfig(Config.ParamValues.DOMINIO_MAIL);
                if (config.CONF_VALUE == null)
                {
                    throw new Exception("Dominio mail non valido");
                }
                strHost = config.CONF_VALUE;

                if (!Mail.IsValidEmail(strTo))
                {
                    MessageBox.Show("L'indirizzo mail del destinatario non è valido", "Indirizzo Mail Non Valido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                config = ConfigDB.LoadConfig(Config.ParamValues.MITTENTE_MAIL);
                if (config.CONF_VALUE == null)
                {
                    throw new Exception("Indirizzo mail del mittente non è valido");
                }
                MailAddress from = new MailAddress(config.CONF_VALUE, config.CONF_VALUE);
                MailAddress to = new MailAddress(strTo, strDisplayName);

                List<MailAddress> cc = new List<MailAddress>();
                //cc.Add(new MailAddress("Someone@domain.topleveldomain", "Name and stuff"));

                string strObject = "";
                string strBody = strBodyRandom;

                if (string.IsNullOrEmpty(strBody))
                {
                    config = ConfigDB.LoadConfig(Config.ParamValues.OGGETTO_MAIL);
                    if (!string.IsNullOrEmpty(config.CONF_VALUE))
                        strObject = config.CONF_VALUE;

                    config = ConfigDB.LoadConfig(Config.ParamValues.TESTO_MAIL);
                    if (!string.IsNullOrEmpty(config.CONF_VALUE))
                        strBody = config.CONF_VALUE;
                }
                SendMail(strObject, strBody, strHost, strPathFile, from, to, cc);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Non è stato possibile inviare la mail.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private static void SendMail(string _subject, string _sText, string _sHost, string _sFile, MailAddress _from, MailAddress _to, List<MailAddress> _cc, List<MailAddress> _bcc = null)
        {
            //SmtpClient mailClient = new SmtpClient("mail.sdrconsulenze.it",25);
            using (SmtpClient mailClient = new SmtpClient(_sHost, 25))
            {
                MailMessage msgMail;
                msgMail = new MailMessage();
                msgMail.From = _from;
                msgMail.To.Add(_to);
                foreach (MailAddress addr in _cc)
                {
                    msgMail.CC.Add(addr);
                }
                if (_bcc != null)
                {
                    foreach (MailAddress addr in _bcc)
                    {
                        msgMail.Bcc.Add(addr);
                    }
                }
                msgMail.Subject = _subject;
                msgMail.Body = _sText;
                msgMail.IsBodyHtml = true;

                if (!string.IsNullOrEmpty(_sFile))
                {
                    Attachment data = new Attachment(_sFile, MediaTypeNames.Application.Octet);
                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(_sFile);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(_sFile);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(_sFile);
                    msgMail.Attachments.Add(data);
                }

                try
                {
                    //var basicCredential = new NetworkCredential("lapo17ster@gmail.com", "password");
                    mailClient.UseDefaultCredentials = true;
                    //mailClient.Credentials = basicCredential;
                    mailClient.Send(msgMail);
                    msgMail.Dispose();
                }
                catch (Exception ex)
                {
                    //Error, could not send the message
                    throw ex;
                }
            }
        }
        public static bool IsValidEmail(string strEmail)
        {
            bool res = new EmailAddressAttribute().IsValid(strEmail);         //true
            return res;
        }
    }
}

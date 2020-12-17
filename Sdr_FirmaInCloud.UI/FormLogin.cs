using MySql.Data.MySqlClient;
using Sdr_FirmaInCloud.BL.Helpers;
using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SdrPDF_VersionBeta.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Tulpep.NotificationWindow; 

namespace SDR_FirmaInCloud.UI
{
    public partial class FormLogin : Form
    { 
        public FormLogin(MySqlConnection mySqlConnection)
        {
            InitializeComponent(); 
        }

        private bool ValidateControls(bool getPass) 
        {
            bool res = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                errorProvider1.SetError(txtUserID, "Inserire un User ID valido.");
                txtUserID.Focus();
                res = false;
            }
            if (!getPass)
            {
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    errorProvider1.SetError(txtPassword, "Inserire una password valida.");
                    txtPassword.Focus();
                    res = false;
                }
            }
            return res;
        }
        
        private bool IsFirstAccess(string strFirstAccess) 
        {
            return !string.IsNullOrEmpty(strFirstAccess);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        { 
            if (!ValidateControls(false))
                return;

            try
            {
                var login = LoginDB.LoadLogin(txtUserID.Text, LoginDB.ToSha256(txtPassword.Text));

                if (login == null)
                    throw new Exception("anagrafica utenza non correttamente popolata. Contattare l'assistenza !");

                if (!login.KEY_LOG.HasValue)
                {
                    MessageBox.Show("Errore: utenza non presente " + txtUserID.Text + " o password errata !", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (IsFirstAccess(login.X_FIRST_LOG))
                {
                    if (DialogResult.No == MessageBox.Show("Primo accesso: è necessario procedere con la procedura di reset della password." + Environment.NewLine + " Vuoi continuare ?", "Primo accesso", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) return;
                    if (!GetForgotPassword()) return;
                    LoginDB.SaveLogin(login.KEY_LOG, null, null);
                }

                if (DateTime.Now > login.DATE_EXPIRED)
                {
                    if (DialogResult.No == MessageBox.Show("Password scaduta: è necessario procedere con la procedura di reset della password." + Environment.NewLine + " Vuoi continuare ?", "Primo accesso", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) return;
                    if (!GetForgotPassword()) return;
                }

                LoginDB.SaveLoginAccess(login.KEY_LOG);

                FormManagement frm = new FormManagement(login);
                frm.ShowDialog();

                //if (!string.IsNullOrEmpty(login.X_ADMIN)) 
                //{
                //    if (DialogResult.Yes == MessageBox.Show("E' possibile creare un nuovo account." + Environment.NewLine + "Premendo il bottone SI verrai reindirizzato alla maschera per le creazione di un nuovo account, diversamente alla griglia per la gestione degli ospiti. Vuoi continuare ?", "Creazione account", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //    {
                //        FormAccount formAccount = new FormAccount();
                //        formAccount.ShowDialog();
                //        return;
                //    }
                //}

                //FormPrivacy frmPrivacy = new FormPrivacy(login.X_TEMPLATE);
                //frm.ShowDialog();

            }
            catch(Exception ex) 
            {
                MessageBox.Show("Errore! Attenzione: " +ex.Message,"Configurazione errata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            var loginAccess = LoginDB.LoadLoginAccess();

            if (loginAccess == null)
                return;

            if (!loginAccess.KEY_LOG.HasValue)
                return;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this.pictureBox2, "Ultimo Accesso " + Environment.NewLine + loginAccess.USER_ID + Environment.NewLine + "ore: " + loginAccess.DT_LOGIN.ToString());
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            PopupNotifier popupNotifier = new PopupNotifier();
            popupNotifier.HeaderHeight = 20;
            popupNotifier.Image = SdrPDF_VersionBeta.UI.Properties.Resources.info1_512;
            popupNotifier.TitleText = "SDR Consulenze S.r.l";            
            popupNotifier.TitleFont = new System.Drawing.Font("Microsoft Tai Le", 16F, System.Drawing.FontStyle.Bold);                   
            popupNotifier.TitleColor = System.Drawing.Color.Black; 
            popupNotifier.ContentFont = new System.Drawing.Font("Tahoma", 12F); 
            popupNotifier.ContentText = Environment.NewLine + "https://sdrconsulenze.it/";
            popupNotifier.BodyColor = System.Drawing.Color.LightGray;
            popupNotifier.ShowCloseButton = true;
            popupNotifier.ShowOptionsButton = false;
            popupNotifier.Scroll = true;
            popupNotifier.AnimationInterval = 10;
            popupNotifier.AnimationDuration = 3000;
            popupNotifier.Click += new EventHandler(popup_ClickForm);
            popupNotifier.Popup();
        }

        private void popup_ClickForm(object sender, EventArgs e)
        {
            try
            {
                string fileName = "IExplore.exe";
                //if (!ExistsOnPath(fileName))
                //    throw new Exception("Eseguibile non disponibile");
                  
                Process.Start(fileName, "www.sdrconsulenze.it");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message,"Internet Explorer non presente");                 
            }            
        }

        private bool ExistsOnPath(string fileName)
        {
            return GetFullPath(fileName) != null;
        }

        private string GetFullPath(string fileName)
        {
            if (File.Exists(fileName))
                return Path.GetFullPath(fileName);

            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                    return fullPath;
            }
            return null;
        }

        private bool GetForgotPassword() 
        {
            string strFromMail = GetAddressee();
            if (!Mail.IsValidEmail(strFromMail))
            {
                MessageBox.Show("L'indirizzo mail del destinatario non è valido. Verificare l'indirizzo inserito.", "Indirizzo Mail Non Valido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (DialogResult.No == MessageBox.Show("Verrà inviata una mail all'indirizzo " + strFromMail + " con il codice di sicurezza che dovrà essere inserito nel campo preposto." + Environment.NewLine + "Vuoi proseguire ?", "Recupero Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return false;

            var param1 = new Random();
            string strCode = param1.Next(100, 10000).ToString();

            ConfigDB.SaveConfig(Config.ParamValues.RANDOM_CODE, strCode);

            if (!Mail.SendMail("", strFromMail, "", strCode))
                return false;

            bool ret = false;
            using (var form = new FormForgotPass(txtUserID.Text))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                    ret = form.recoveryPassword;
            }
            return ret;
        }

        private string GetAddressee() 
        {
            return Microsoft.VisualBasic.Interaction.InputBox("Inserire un indirizzo mail valido:", "Indirizzo mail");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!ValidateControls(true))
                return;

            GetForgotPassword();
            /*
            // se admin mail CONF_VALUE 
            string strFromMail = ConfigDB.LoadConfig(Config.ParamValues.MITTENTE_MAIL).CONF_VALUE;


            if (DialogResult.No == MessageBox.Show("Verrà inviata una mail all'indirizzo: " + strFromMail + " con il codice di sicurezza che dovrà essere inserito nel campo preposto."+Environment.NewLine + "Vuoi proseguire ?", "Recupero Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            var param1 = new Random();
            string strCode = param1.Next(100, 10000).ToString();

            ConfigDB.SaveConfig(Config.ParamValues.RANDOM_CODE, strCode);

            if (!Mail.SendMail("", strFromMail, "", strCode))
                return;

            FormForgotPass frm = new FormForgotPass(txtUserID.Text);
            frm.ShowDialog();
            */
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShow.Checked;
        }
    }
}

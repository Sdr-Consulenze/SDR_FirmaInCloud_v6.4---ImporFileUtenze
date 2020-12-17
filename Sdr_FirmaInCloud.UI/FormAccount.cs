using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SdrPDF_VersionBeta.UI
{
    public partial class FormAccount : Form
    {
        private bool isModified;
        public FormAccount()
        {
            InitializeComponent();
            this.isModified = false;
        }
        private bool ValidateControls()
        {
             
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                errorProvider1.SetError(txtUserID, "Inserire un user Id valido.");
                txtUserID.Focus();
                return false;
            }
            var login = LoginDB.LoadLogin(txtUserID.Text, string.Empty);
            if(login.KEY_LOG.HasValue)
            {
                errorProvider1.SetError(txtUserID, "Esiste già un accounto con lo stesso user ID.");
                txtUserID.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Inserire il nome.");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSurname.Text))
            {
                errorProvider1.SetError(txtSurname, "Inserire il cognome.");
                txtSurname.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Inserire la password.");
                txtPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtConfirmPass.Text))
            {
                errorProvider1.SetError(txtConfirmPass, "Confermare la password.");
                txtConfirmPass.Focus();
                return false;
            }
            if (txtPassword.Text != txtConfirmPass.Text)
            {
                errorProvider1.SetError(txtConfirmPass, "Le due password non coincidono.");
                txtConfirmPass.Focus();
                return false;
            } 
            return true;
        } 

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        } 

        private void Save() 
        {
            try
            {
                if (!ValidateControls()) return;

                Login login = new Login();
                login.USER_ID = txtUserID.Text;
                login.PASSWORD_ID = txtPassword.Text;
                login.X_FIRST_LOG = chkFirstLog.Checked == true ? "X" : null;
                login.X_TEMPLATE = chkTemplate.Checked == true ? "X" : null;
                login.DATE_EXPIRED = DateTime.Now.AddDays(90);
                login.NAME = txtName.Text;
                login.SURNAME = txtSurname.Text;

                LoginDB.SaveNewLogin(login);

                MessageBox.Show("La nuova utenza è stata correttamente registrata.", "Nuovo Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Non è stato possibile registrare correttamente la nuova utenza: " + ex.Message);
            }
        } 


        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked == true)
            {
                txtConfirmPass.UseSystemPasswordChar = false;
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtConfirmPass.UseSystemPasswordChar = true;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void chkAutoPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoPass.Checked == false)
            {
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmPass.UseSystemPasswordChar = true;
                txtPassword.Text = string.Empty;
                txtConfirmPass.Text = string.Empty;
                return;
            }

            string strAutoPass = CreatePassword(10);
            txtPassword.UseSystemPasswordChar = false;
            txtConfirmPass.UseSystemPasswordChar = false;

            txtPassword.Text = strAutoPass;
            txtConfirmPass.Text = strAutoPass;
        }

        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#][*";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (this.isModified)
            {
                if (DialogResult.Yes == MessageBox.Show("Vuoi uscire senza salvare le modifiche ?", "Salvataggio Dati", MessageBoxButtons.YesNo))
                {
                    this.Close();
                    return;
                }
                Save();
            }
            this.Close();
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
                MessageBox.Show("Errore: " + ex.Message, "Internet Explorer non presente");
            }
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }
          
        private void chkTemplate_CheckedChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void chkFirstLog_CheckedChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }
    }
}

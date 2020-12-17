using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using System;
using System.Text;
using System.Windows.Forms;

namespace SdrPDF_VersionBeta.UI
{
    public partial class FormForgotPass : Form
    {
        private string sCode;
        private string sUserId;
        private bool isModified;
        public bool recoveryPassword { get; set; }
        public FormForgotPass(string logUserId)
        {
            InitializeComponent(); 
            txtPassword.Enabled = false;
            txtConfirmPass.Enabled = false;
            btnSave.Enabled = false;
            sCode = ConfigDB.LoadConfig(Config.ParamValues.RANDOM_CODE).CONF_VALUE;
            sUserId = logUserId;
            this.isModified = false;
        }
        private bool ValidateControls(bool onlyCode)
        {
            bool res = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                errorProvider1.SetError(txtCode, "Inserire il codice ricevuto via mail.");
                txtCode.Focus();
                res = false;
            }
            if (sCode != txtCode.Text)
            {
                errorProvider1.SetError(txtCode, "Il codice inserito non è valido.");
                txtCode.Focus();
                res = false;
            }
            if (onlyCode)
            {
                return res;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Inserire una password valida.");
                txtPassword.Focus();
                res = false;
            }
            if (string.IsNullOrEmpty(txtConfirmPass.Text))
            {
                errorProvider1.SetError(txtConfirmPass, "Confermare la password.");
                txtConfirmPass.Focus();
                res = false;
            }
            if(txtPassword.Text != txtConfirmPass.Text)
            {
                errorProvider1.SetError(txtConfirmPass, "Le due password non coincidono.");
                txtConfirmPass.Focus();
                res = false;
            }
            var txtPass = LoginDB.LoadLogin(sUserId, null).PASSWORD_ID;
            if (txtPass == LoginDB.ToSha256(txtPassword.Text)) 
            {
                errorProvider1.SetError(txtPassword, "Inserire una password differente rispetto all'ultima.");
                txtPassword.Focus();
                res = false;
            }
            if (!IsCorrectPassword(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Violazione delle regole. La password deve contenere: una lettera maiuscola, una minuscola, un numero, un carattere speciale.");
                txtPassword.Focus();
                return false;
            }
            return res;
        }
        private bool IsCorrectPassword(string strPassword)
        {
            bool retUpper = false;
            bool retLower = false;
            bool retNumber = false;
            bool retSpecial = false;
            foreach (var charPass in strPassword.ToCharArray())
            {
                if (retUpper == false)
                    retUpper = ExistsUpperCase(charPass);

                if (retLower == false)
                    retLower = ExistsLowerCase(charPass);

                if (retNumber == false)
                    retNumber = ExistsNumber(charPass);

                if (retSpecial == false)
                    retSpecial = ExistsSpecialChar(charPass);
            }

            return retUpper && retLower && retNumber && retSpecial;
        }
        private bool ExistsUpperCase(char charPass) { return Char.IsUpper(charPass); }
        private bool ExistsLowerCase(char charPass) { return Char.IsLower(charPass); }
        private bool ExistsNumber(char charPass) { return Char.IsNumber(charPass); }
        private bool ExistsSpecialChar(char charPass)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (charPass.Equals(item)) return true;
            }

            return false;
        }
        private void btnCodeVerify_Click(object sender, EventArgs e)
        {
            if (!ValidateControls(true))
                return;

            if (DialogResult.No == MessageBox.Show("E' possibile proseguire con la procedure di reset della password." + Environment.NewLine + "Vuoi proseguire ?", "Reset Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            txtPassword.Enabled = true;
            txtConfirmPass.Enabled = true;
            btnSave.Enabled = true;
        }
        private void Save()
        {
            try
            {
                if (!ValidateControls(false)) return;

                var login = LoginDB.LoadLogin(sUserId, string.Empty);
                if (login == null)
                    throw new Exception("Utente non riconosciuto.");

                LoginDB.SaveLogin(login.KEY_LOG, txtPassword.Text, null);

                MessageBox.Show("La password è stata correttamente aggiornata.", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                recoveryPassword = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Non è stato possibile aggiornare la password: " + ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
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
            errorProvider1.Clear();
            if (chkAutoPass.Checked == false)
            {
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmPass.UseSystemPasswordChar = true;
                txtPassword.Text = string.Empty;
                txtConfirmPass.Text = string.Empty;
                return;
            }

            string strAutoPass = CreatePassword(3);
            txtPassword.UseSystemPasswordChar = false;
            txtConfirmPass.UseSystemPasswordChar = false;

            txtPassword.Text = strAutoPass;
            txtConfirmPass.Text = strAutoPass;
            this.isModified = true;
        }
        private string CreatePassword(int length)
        {
            StringBuilder res = new StringBuilder();            
            string[] pass = new string[] { "abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", @"!@#][*" + @"\|!#$%&/()=?»«@£§€{}.-;'<>_,", "1234567890" };
            for (int i = 0; i < pass.Length; i++)
            {
                res.Append(StrPassword(pass[i], length));
            }
            return res.ToString();
        }
        private string StrPassword(string strPass, int length) 
        {
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(strPass[rnd.Next(strPass.Length)]);
            }
            return res.ToString(); 
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }
        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }
        private void txtPassword_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.Show("La password deve contenere: una lettera maiuscola, una minuscola, un numero, un carattere speciale.", txtPassword, 500);
        }
    }
}

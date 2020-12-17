using MySql.Data.MySqlClient;
using Sdr_FirmaInCloud.BL.FirmaInCloud;
using Sdr_FirmaInCloud.BL.Helpers;
using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.util;
using System.Windows.Forms;
using System.IO;

namespace SDR_FirmaInCloud.UI
{
    public partial class FormUser : Form
    {
        private Users user;        
        private Sdr_FirmaInCloud.BL.FirmaInCloud.Document document;
        private int? intKeyUtenze;
        private int? intKeyDoc;
        private bool isModified;
        private bool bFreeze;
        private int rowIndex = 0;
        DataTable dataTableLoadTemplateFirma;
        private int selectedTemplate;
        Dictionary<int, bool> keyValuePairs = new Dictionary<int, bool>();

        public FormUser(int? intKeyUte)
        {
            InitializeComponent();
            bFreeze = true;
            intKeyUtenze = intKeyUte;
            FillControls();
            DisplayData(intKeyUtenze);
            this.isModified = false;
        }

        private void FillControls()
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("SELECT * FROM NAZIONE ORDER BY NAZIONE", GestioneMySql.connection);

                DataSet cmbNazDataSet = new DataSet();
                DataSet cmbNazNasDataSet = new DataSet();
                mySqlDataAdapter.Fill(cmbNazDataSet);
                mySqlDataAdapter.Fill(cmbNazNasDataSet);

                cmbNaz.DataSource = cmbNazDataSet.Tables[0];
                cmbNaz.DisplayMember = "NAZIONE";
                cmbNaz.ValueMember = "ID";
                cmbNaz.SelectedIndex = 118;

                cmbNazNas.DataSource = cmbNazNasDataSet.Tables[0];
                cmbNazNas.DisplayMember = "NAZIONE";
                cmbNazNas.ValueMember = "ID";
                cmbNazNas.SelectedIndex = 118;

                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");

                cmbSst.Items.Add("Via");
                cmbSst.Items.Add("Strada");
                cmbSst.Items.Add("Piazza");
                cmbSst.SelectedIndex = 0;

                Dictionary<string, string> cmb = new Dictionary<string, string>
                {
                    { "1", "Pers. fisica" },
                    { "2", "Sogg. giuridico" }
                };
                cmbTYP_UTE.DataSource = new BindingSource(cmb, null);
                cmbTYP_UTE.DisplayMember = "Value";
                cmbTYP_UTE.ValueMember = "Key";

                Dictionary<int, string> cmbDoc = new Dictionary<int, string>
                {
                    { 0, "" },
                    { 1, "Carta Identità" },
                    { 2, "Passaporto" },
                    { 3, "Patente di guida" },
                    { 4,  "Passaporto" },
                    { 5,  "Carta ident elettronica" },
                    { 6,  "Certificato di ident" },
                    { 7,  "Tess. App.to ag custodia" },
                    { 8,  "Tess. Sott.li ag custodia" },
                    { 9,  "Tess. Uff.li ag custodia" },
                    { 10, "Tess. App.to ag custodia" },
                    { 11, "Tess. Sott.li ag custodia" },
                    { 12, "Tess. Uff.li ag custodia" },
                    { 13, "Tess. Militare truppa A.M." },
                    { 14, "Tess. Sottoufficiali" },
                    { 15, "Tess. Ufficiali A.M." },
                    { 16, "Tess. App.to Carabinieri" },
                    { 17, "Tess. Sottoufficiali CC" },
                    { 18, "Tess. Ufficiale" },
                    { 19, "Tess. Af. e AG.SC. C.F.S" },
                    { 20, "Tess. Sottoufficiali C.F.S." },
                    { 21, "Tess. Ufficiali" }
                };
                cmbTyp_DOC.DataSource = new BindingSource(cmbDoc, null);
                cmbTyp_DOC.DisplayMember = "Value";
                cmbTyp_DOC.ValueMember = "Key";

                Dictionary<string, string> cmbSex = new Dictionary<string, string>
                {
                    { "", "" },
                    { "M", "M" },
                    { "F", "F" }
                };
                cmbSEX.DataSource = new BindingSource(cmbSex, null);
                cmbSEX.DisplayMember = "Value";
                cmbSEX.ValueMember = "Key";

                txtCF.ReadOnly = false;
                txtPiva.ReadOnly = true;

                string strQuery = "SELECT 0 KEY_TEMPL, '' DESCR, NULL PATH, NULL DTINS UNION SELECT KEY_TEMPL, DESCR, PATH, DTINS FROM TEMPLATE";
                MySqlDataAdapter mySqlDataAdapterTempl = new MySqlDataAdapter(strQuery, GestioneMySql.connection);

                DataSet comboTemplateDataSet = new DataSet();
                mySqlDataAdapterTempl.Fill(comboTemplateDataSet);

                cmbTemplate.DataSource = comboTemplateDataSet.Tables[0];
                cmbTemplate.DisplayMember = "DESCR";
                cmbTemplate.ValueMember = "KEY_TEMPL";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message);
            }
        }

        private void DisplayData(int? intKeyUte)
        {
            if (intKeyUte.HasValue)
            {
                LoadData(intKeyUte);
            }
            else
            {
                NewData(false);
            }
        }

        private void LoadData(int? intKeyUte)
        {
            user = UsersDB.LoadUser(intKeyUte).First();

            if (user == null)
                return;

            txtNome.Text = user.NOME;
            txtCognome.Text = user.COGNOME;
            cmbSst.SelectedItem = user.SST;
            txtStrada.Text = user.STRADA;
            dateTimePicker1.Value = user.DTNAS;

            if(user.KEY_NAZ != null)
                cmbNaz.SelectedValue = user.KEY_NAZ;

            if(user.KEY_REG != null)
                cmbReg.SelectedValue = user.KEY_REG;

            if(user.KEY_PRO != null)
                cmbProv.SelectedValue = user.KEY_PRO;

            if(user.KEY_COM != null)
                cmbCom.SelectedValue = user.KEY_COM;

            if(user.KEY_LOC != null)
                cmbLoc.SelectedValue = user.KEY_LOC;

            if(user.TYP_UTE != null) 
                cmbTYP_UTE.SelectedIndex = int.Parse(user.TYP_UTE);

            if(user.SEX !=null)
                cmbSEX.SelectedValue = user.SEX;

            txtCF.Text = user.COD_FIS;
            txtPiva.Text = user.P_IVA;
            txtNcn.Text = user.NCN;
            txtCell.Text = user.CELL;
            txtTel.Text = user.TEL;
            txtMail.Text = user.MAIL;
            
            if ((cmbTemplate.SelectedIndex != -1) && (cmbTemplate.SelectedItem != null) && (cmbTemplate.Items.Count > 1)) 
                cmbTemplate.SelectedIndex = user.KEY_TEMPL;
                         
            if(user.KEY_NAZ_NAS != null)
                cmbNazNas.SelectedValue = user.KEY_NAZ_NAS;

            if (user.KEY_REG_NAS != null)
                cmbRegNas.SelectedValue = user.KEY_REG_NAS;

            if (user.KEY_PRO_NAS != null)
                cmbProvNas.SelectedValue = user.KEY_PRO_NAS;

            if (user.KEY_COM_NAS != null)
                cmbComNas.SelectedValue = user.KEY_COM_NAS;

            if (user.KEY_LOC_NAS !=null)
                cmbLocNas.SelectedValue = user.KEY_LOC_NAS;

            if (user.KEY_DOC.HasValue)
                intKeyDoc = user.KEY_DOC;

            document = DocumentDB.LoadDocument(user.KEY_DOC);
            if (document != null)
            {
                if (document.TYP_DOC.HasValue)
                    cmbTyp_DOC.SelectedIndex = document.TYP_DOC.GetValueOrDefault();

                txtIdDoc.Text = document.ID_DOC;
                if (DateTime.MinValue < document.ISS_DOC && document.ISS_DOC < DateTime.MaxValue)
                    dtmPickDocument.Value = document.ISS_DOC;

                txtCityDoc.Text = document.CITY_DOC;
                txtNumber.Text = document.NUM_DOC;
            }

            TemplateLinq template = new TemplateLinq();
            template.KEY_USER = user.KEY_USER;
            template.KEY_TEMPL_L = user.KEY_TEMPL;

            LoadTemplate_Linq(template);
            bFreeze = false;
        }

        private void NewData(bool question)
        {
            if (question)
            {
                if (this.isModified)
                {
                    if (DialogResult.No == MessageBox.Show("Verranno ripuliti tutti i campi. Vuoi proseguire ?", "Inserimento Dati", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        return;
                }
            }
            txtNome.Text = "";
            txtCognome.Text = "";
            cmbSst.SelectedItem = "";
            txtStrada.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;            
            cmbTYP_UTE.SelectedIndex = 0;
            txtCF.Text = "";
            txtPiva.Text = "";
            txtNcn.Text = "";
            txtCell.Text = "";
            txtTel.Text = "";
            txtMail.Text = "";
             
            cmbTemplate.SelectedIndex = -1;
            this.isModified = true;

            cmbTyp_DOC.SelectedIndex = -1;
            txtIdDoc.Text = "";
            dtmPickDocument.Value = DateTime.Now.Date;
            txtCityDoc.Text = "";
            this.LoadTemplate_Linq(null);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewData(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private bool ValidateControls()
        {
            bool isValid = true;
            ep.Clear();
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                isValid = false;
                ep.SetError(txtNome, "Inserire un nome valido.");
                txtNome.Focus();
            }
            if (string.IsNullOrEmpty(txtCognome.Text))
            {
                isValid = false;
                ep.SetError(txtCognome, "Inserire un cognome valido.");
                txtCognome.Focus();
            }
            if (!string.IsNullOrEmpty(txtNcn.Text))
            {
                int n;
                bool isNumeric = int.TryParse(txtNcn.Text, out n);
                if (!isNumeric)
                {
                    isValid = false;
                    ep.SetError(txtNcn, "Inserire un numero civico valido.");
                    txtNcn.Focus();
                }
            }
            if(dateTimePicker1.Value.Date > DateTime.Now.Date) 
            {
                isValid = false;
                ep.SetError(dateTimePicker1, "La data di nascita non può essere successiva alla data odierna.");
                dateTimePicker1.Focus();
            }
            if (dtmPickDocument.Value.Date > DateTime.Now.Date)
            {
                isValid = false;
                ep.SetError(dtmPickDocument, "La data di rilascio del documento di identità non può essere successivo alla data odierna.");
                dtmPickDocument.Focus();
            }
            //if (!string.IsNullOrEmpty(txtMail.Text))
            //{
            //    if (!IsValidEmail(txtMail.Text))
            //    {
            //        isValid = false;
            //        ep.SetError(txtMail, "Inserire un indirizzo mail valido.");
            //        txtMail.Focus();
            //    }
            //}
            if (!string.IsNullOrEmpty(txtCell.Text))
            {
                if (txtCell.Text.Contains("ABCDEFGHIJKLMNOPQRSTUVWXYZ"))
                {
                    isValid = false;
                    ep.SetError(txtCell, "Inserire un numero di telefono valido.");
                    txtCell.Focus();
                }
            }

            return isValid;
        }

        private void Save(bool question)
        {
            if (!this.isModified)
                return;

            if (question)
            {
                if (DialogResult.No == MessageBox.Show("Vuoi salvare le modifiche ?", "Salvataggio Dati", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;
            }

            if (!ValidateControls())
                return;

            Save();

            MessageBox.Show("Salvataggio eseguito", "Salvataggio", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.isModified = false;
        }

        internal void Save()
        {
            if (user == null)
            {
                user = new Users();
            }
            else
            {
                user.KEY_USER = intKeyUtenze.GetValueOrDefault(); 
            }

            user.NOME = txtNome.Text;
            user.COGNOME = txtCognome.Text;
            user.DTINS = DateTime.Now;
            user.DTNAS = dateTimePicker1.Value.Date;

            if (cmbSst.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(cmbSst.SelectedItem.ToString()))
                    user.SST = cmbSst.SelectedItem.ToString();
            }
            user.STRADA = txtStrada.Text;   
            user.TYP_UTE = (cmbTYP_UTE.SelectedIndex).ToString();
            user.COD_FIS = txtCF.Text;
            user.P_IVA = txtPiva.Text;
            user.NCN = txtNcn.Text;
            user.CELL = txtCell.Text;
            user.TEL = txtTel.Text;
            user.MAIL = txtMail.Text;

            if (cmbNaz.SelectedItem != null)
                user.KEY_NAZ = Convert.ToInt32(((DataRowView)cmbNaz.SelectedItem).Row.ItemArray[0]);

            if (cmbReg.SelectedItem != null)
                user.KEY_REG = Convert.ToInt32(((DataRowView)cmbReg.SelectedItem).Row.ItemArray[0]);

            if (cmbProv.SelectedItem != null)
                user.KEY_PRO = Convert.ToInt32(((DataRowView)cmbProv.SelectedItem).Row.ItemArray[0]);

            if (cmbCom.SelectedItem != null)
                user.KEY_COM = Convert.ToInt32(((DataRowView)cmbCom.SelectedItem).Row.ItemArray[0]);

            if (cmbLoc.SelectedItem != null)
                user.KEY_LOC = Convert.ToInt32(((DataRowView)cmbLoc.SelectedItem).Row.ItemArray[0]);


            if (cmbNazNas.SelectedItem != null)
                user.KEY_NAZ_NAS = Convert.ToInt32(((DataRowView)cmbNazNas.SelectedItem).Row.ItemArray[0]);

            if (cmbRegNas.SelectedItem != null)
                user.KEY_REG_NAS = Convert.ToInt32(((DataRowView)cmbRegNas.SelectedItem).Row.ItemArray[0]);

            if (cmbProvNas.SelectedItem != null)
                user.KEY_PRO_NAS = Convert.ToInt32(((DataRowView)cmbProvNas.SelectedItem).Row.ItemArray[0]);

            if (cmbComNas.SelectedItem != null)
                user.KEY_COM_NAS = Convert.ToInt32(((DataRowView)cmbComNas.SelectedItem).Row.ItemArray[0]);

            if (cmbLocNas.SelectedItem != null)
                user.KEY_LOC_NAS = Convert.ToInt32(((DataRowView)cmbLocNas.SelectedItem).Row.ItemArray[0]);

            if (cmbSEX.SelectedItem != null)
                user.SEX = ((KeyValuePair<string, string>)cmbSEX.SelectedItem).Key;

            user.KEY_TEMPL = cmbTemplate.SelectedIndex;

            if (document == null)
            {
                document = new Document();
            }
            else
            {
                document.KEY_DOC = intKeyDoc.GetValueOrDefault();
            }
            
            document.TYP_DOC = cmbTyp_DOC.SelectedIndex;
            document.NUM_DOC = txtNumber.Text;

            if (DateTime.MinValue < dtmPickDocument.Value.Date && dtmPickDocument.Value.Date < DateTime.MaxValue)
                document.ISS_DOC = dtmPickDocument.Value.Date;

            document.CITY_DOC = txtCityDoc.Text;
            document.ID_DOC = txtIdDoc.Text;

            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");
                
                DocumentDB.SaveDocument(document);
                user.KEY_DOC = document.KEY_DOC;
                UsersDB.SaveUser(user);
                
                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
            }

            intKeyUtenze = user.KEY_USER;
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
                Save(false);
            }
            this.Close();
        }

        protected bool CheckDate(string date)
        {
            bool res = false;
            Regex reg = new Regex(@"(((0|1)[0-9]|[2(0-9)|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
            res = reg.IsMatch(date);
            DateTime dt;
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt))
            {
                res = true;
            }
            return res;
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.rowIndex = e.RowIndex;
            if (e.Button == MouseButtons.Left)
                return;

            if (e.RowIndex == -1)
                return;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.Selected = false;
            }

            this.dataGridView2.Rows[e.RowIndex].Selected = true;

            this.contextMenuStrip1.Show(this.dataGridView2, e.Location);
            this.contextMenuStrip1.Show(Cursor.Position);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow row = dataTableLoadTemplateFirma.Rows[rowIndex];
            if (row == null)
                return;

            FileHelper.OpenFile(row.ItemArray[1].ToString());
        }

        private void viewTemplate_Click(object sender, EventArgs e)
        {
            DataRow row = dataTableLoadTemplateFirma.Rows[rowIndex];
            if (row == null)
                return;

            FileHelper.OpenFile(row.ItemArray[1].ToString());
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void cmbNaz_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbNaz.SelectedItem == null || this.cmbNaz.SelectedIndex == -1)
                return;

            this.isModified = true;

            var idx = Convert.ToInt32(((DataRowView)cmbNaz.SelectedItem).Row.ItemArray[0]);
            
            var mySqlDataAdapter = FirmaInCloudDB.LoadRegione(idx);// cmbNaz.SelectedIndex + 1);
            DataSet cmbRegDataSet = new DataSet();
            mySqlDataAdapter.Fill(cmbRegDataSet);

            cmbReg.DataSource = cmbRegDataSet.Tables[0];
            cmbReg.DisplayMember = "REGIONE";
            cmbReg.ValueMember = "ID";

            if (cmbRegDataSet.Tables[0].Rows.Count == 0)
            {
                cmbProv.Text = ""; 
                //cmbProv.SelectedItem = -1;
            }
        }

        private void cmbReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;
            
            if (cmbReg.SelectedItem == null)
                return;

            var idx = Convert.ToInt32(((DataRowView)cmbReg.SelectedItem).Row.ItemArray[0]);
            
            var mySqlDataAdapter = FirmaInCloudDB.LoadProvince(idx);

            DataSet comboProDataSet = new DataSet();
            mySqlDataAdapter.Fill(comboProDataSet);

            cmbProv.DataSource = comboProDataSet.Tables[0];
            cmbProv.DisplayMember = "PROVINCIA";
            cmbProv.ValueMember = "ID";
        }

        private void cmbProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;

            if (cmbProv.SelectedItem == null)
                return;

            var idx = Convert.ToInt32(((DataRowView)cmbProv.SelectedItem).Row.ItemArray[0]);

            var mySqlDataAdapter = FirmaInCloudDB.LoadComuni(idx);

            DataSet comboComDataSet = new DataSet();
            mySqlDataAdapter.Fill(comboComDataSet);

            cmbCom.DataSource = comboComDataSet.Tables[0];
            cmbCom.DisplayMember = "COMUNE";
            cmbCom.ValueMember = "ID";
        }

        private void cmbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;

            if (cmbCom.SelectedItem == null)
                return;

            var idx = Convert.ToInt32(((DataRowView)cmbCom.SelectedItem).Row.ItemArray[0]);

            var mySqlDataAdapter = FirmaInCloudDB.LoadLocalita(idx);

            DataSet comboLocDataSet = new DataSet();
            mySqlDataAdapter.Fill(comboLocDataSet);

            cmbLoc.DataSource = comboLocDataSet.Tables[0];
            cmbLoc.DisplayMember = "LOCALITA";
            cmbLoc.ValueMember = "ID";
        }

        private void cmbLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void cmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((selectedTemplate != cmbTemplate.SelectedIndex) && (cmbTemplate.SelectedIndex >= 1))
            {
                selectedTemplate = cmbTemplate.SelectedIndex;
                this.isModified = true;
            }
            //if (bFreeze) return;
            //Template template = TemplateDB.LoadTemplate(selectedTemplate);
            //FileHelper.OpenFile(template.PATH);
        }
     
        private void cmbSst_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void cmbTYP_UTE_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;
            if ((cmbTYP_UTE.SelectedIndex + 1).ToString() == "1")
            {
                txtPiva.ReadOnly = true;
                txtCF.ReadOnly = false;
                cmbSEX.Enabled = true;
            }
            else
            {
                txtPiva.ReadOnly = false;
                txtCF.ReadOnly = true;
                cmbSEX.Enabled = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtStrada_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtNcn_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtCF_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
            txtCF.Text = txtCF.Text.ToUpper();
        }

        private void txtPiva_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtCell_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtCF_TextChanged_1(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void btnFirma_Click(object sender, EventArgs e)
        {
            if (this.isModified)
            {
                if (DialogResult.No == MessageBox.Show("Per proseguire è necessario salvare i dati inseriti. Vuoi proseguire ?", "Firma documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;

                if (!ValidateControls())
                    return;

                Save();
            }

            //if (DialogResult.No == MessageBox.Show("Selezionare una directory in cui salvare il documento." +  Environment.NewLine + "Vuoi proseguire ?", "Firma documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            //    return;

            //SaveFileDialog dlg = new SaveFileDialog
            //{
            //    Title = "Salvataggio Pdf",
            //    AddExtension = true,
            //    Filter = "PDF|*.pdf"
            //};

            //if (dlg.ShowDialog() != DialogResult.OK)
            //    return;

            //if (string.IsNullOrEmpty(dlg.FileName))
            //    return;

            if (cmbTemplate.SelectedIndex == 0)
            {
                MessageBox.Show("E' necessario selezionare un template valido !", "Firma documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.isModified = false;
                return;
            } 

            string strFileNameOutput = $"{FileHelper.GetPath("pdfElab")}{user.COGNOME}" + " " + $"{user.NOME}" + " " + $"{DateTime.Today:yyyyMMdd}" + ".pdf";
            string strFileName = PDFManager.RunSignPro(strFileNameOutput,cmbTemplate.SelectedIndex,user);

            if (string.IsNullOrEmpty(strFileName)) return;

            var pathString = FirmaInCloudDB.LoadTemplate(cmbTemplate.SelectedIndex, "").AsEnumerable().First();

            FileNameParser fileNameParser = new FileNameParser(strFileName);

            TemplateLinq templateLinq = new TemplateLinq
            {
                KEY_USER = user.KEY_USER,
                DTSIGN = DateTime.Now,
                PATH = strFileName,
                DESCR = fileNameParser.DESCR,
                KEY_TEMPL = Convert.ToInt32(pathString.ItemArray[0])
            };

            FirmaInCloudDB.SaveTemplate_Linq(templateLinq, user);

            ConfigDB.SaveConfig(Config.ParamValues.COUNT, (int.Parse(ConfigDB.LoadConfig(Config.ParamValues.COUNT).CONF_VALUE) + 1).ToString());

            LoadTemplate_Linq(templateLinq);

            if (DialogResult.No == MessageBox.Show("E' possibile inviare il documento appena firmato all'indirizzo mail: " + user.MAIL + Environment.NewLine + "Vuoi proseguire ?", "Firma documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            Mail.SendMail(strFileName,txtMail.Text,user.NOME + " " + user.COGNOME,""); 
        }
                
        private void LoadTemplate_Linq(TemplateLinq template)
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.ReadOnly = true;
                if (template == null)
                    return;
            
                dataTableLoadTemplateFirma = FirmaInCloudDB.LoadTemplateFirmati(template); 
                //dataGridView2.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                int n = 0;
                foreach (DataRow item in dataTableLoadTemplateFirma.Rows)
                {
                    n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridView2.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridView2.Rows[n].Cells[2].Value = DateTime.ParseExact(item[2].ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture); ;
                    dataGridView2.Rows[n].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView2.Rows[n].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private void cmbNazNas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbNazNas.SelectedItem == null || this.cmbNazNas.SelectedIndex == -1)
                return;

            this.isModified = true;

            var idx = Convert.ToInt32(((DataRowView)cmbNazNas.SelectedItem).Row.ItemArray[0]);

            var mySqlDataAdapter = FirmaInCloudDB.LoadRegione(idx); 
            DataSet cmbRegNazDataSet = new DataSet();
            mySqlDataAdapter.Fill(cmbRegNazDataSet);

            cmbRegNas.DataSource = cmbRegNazDataSet.Tables[0];
            cmbRegNas.DisplayMember = "REGIONE";
            cmbRegNas.ValueMember = "ID";

            if (cmbRegNazDataSet.Tables[0].Rows.Count == 0)
                cmbProvNas.Text = "";
        }

        private void cmbRegNas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;

            if (cmbRegNas.SelectedItem == null)
                return;

            var idx = Convert.ToInt32(((DataRowView)cmbRegNas.SelectedItem).Row.ItemArray[0]);

            var mySqlDataAdapter = FirmaInCloudDB.LoadProvince(idx);

            DataSet comboProNazDataSet = new DataSet();
            mySqlDataAdapter.Fill(comboProNazDataSet);

            cmbProvNas.DataSource = comboProNazDataSet.Tables[0];
            cmbProvNas.DisplayMember = "PROVINCIA";
            cmbProvNas.ValueMember = "ID";
        }

        private void cmbProvNas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;

            if (cmbProvNas.SelectedItem == null)
                return;

            var idx = Convert.ToInt32(((DataRowView)cmbProvNas.SelectedItem).Row.ItemArray[0]);

            var mySqlDataAdapter = FirmaInCloudDB.LoadComuni(idx);

            DataSet comboComNazDataSet = new DataSet();
            mySqlDataAdapter.Fill(comboComNazDataSet);

            cmbComNas.DataSource = comboComNazDataSet.Tables[0];
            cmbComNas.DisplayMember = "COMUNE";
            cmbComNas.ValueMember = "ID";
        }

        private void cmbComNas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;

            if (cmbComNas.SelectedItem == null)
                return;

            var idx = Convert.ToInt32(((DataRowView)cmbComNas.SelectedItem).Row.ItemArray[0]);

            var mySqlDataAdapter = FirmaInCloudDB.LoadLocalita(idx);

            DataSet comboLocNazDataSet = new DataSet();
            mySqlDataAdapter.Fill(comboLocNazDataSet);

            cmbLocNas.DataSource = comboLocNazDataSet.Tables[0];
            cmbLocNas.DisplayMember = "LOCALITA";
            cmbLocNas.ValueMember = "ID";
        }

        private void cmbLocNas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtTel_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void cmbTyp_DOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtIdDoc_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void txtCityDoc_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void dtmPickDocument_ValueChanged(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void cmbSEX_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isModified = true;   
        }
    }
}



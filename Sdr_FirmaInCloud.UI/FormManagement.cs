using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using Sdr_FirmaInCloud.BL;
using Sdr_FirmaInCloud.BL.FirmaInCloud;
using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL.Helpers;
using SDR_FirmaInCloud.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SdrPDF_VersionBeta.UI
{
    public partial class FormManagement : Form
    {
        Timer t = null;
        DataTable dataTableUser;
        DataTable dataTableTemplate;
        int rowIndex = 0;
        int rowIndexTemplate = 0;
        public FormManagement(Login login)
        {
            InitializeComponent();
            this.lblOperatore.Text = "Operatore: " + login.SURNAME + " " + login.NAME;
            DisplayData(login.X_ADMIN, login.X_TEMPLATE);
        }

        private void DisplayData(string strXAdmin, string strXTemplate)
        {
            StartTimer();
            FillFilterCombo();
            LoadGridUtenze(UsersDB.SetQueryUser(string.Empty));
            LoadGridTemplate();
            SetShowPanel1(string.IsNullOrEmpty(strXAdmin));
            SetShowPanel2(string.IsNullOrEmpty(strXTemplate));
            chkShowPanel.Enabled = !string.IsNullOrEmpty(strXAdmin);
            chkShowTemplate.Enabled = !string.IsNullOrEmpty(strXTemplate);

        }

        private void FillFilterCombo()
        {
            Dictionary<string, string> cmb = new Dictionary<string, string>
            {
                { "0", "" },
                { "1", "Nome" },
                { "2", "Cognome" }
            };
            cmbFilter.DataSource = new BindingSource(cmb, null);
            cmbFilter.DisplayMember = "Value";
            cmbFilter.ValueMember = "Key";

            txtFilter.Enabled = false;
        }
        private void LoadGridUtenze(DataGridView dgv)
        {
            dataGridView1.ReadOnly = true;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dgv.Columns[column.Index].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView1.Columns[column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.Columns["NOME"].Width = 200;
            dgv.Columns["COGNOME"].Width = 200;
            dgv.Columns["DATA DI NASCITA"].Width = 155;
            dgv.Columns["INDIRIZZO"].Width = 350;
            dgv.Columns["CELLULARE"].Width = 160;
            dgv.Columns["DATA INSERIMENTO"].Width = 155;

            dgv.Columns["DATA DI NASCITA"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["DATA DI NASCITA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["DATA INSERIMENTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["DATA INSERIMENTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        private void LoadGridUtenze(string strQuery)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                using (MySqlConnection con = new MySqlConnection(GestioneMySql.StringaConnessione))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            dataTableUser = new DataTable();
                            sda.Fill(dataTableUser);

                            dataTableUser.Columns["KEY_USER"].ColumnMapping = MappingType.Hidden;
                            DataRow dr = dataTableUser.NewRow();
                            dr["NOME"] = "Aggiungi Utenza";
                            dataTableUser.Rows.Add(dr);
                            dataGridView1.DataSource = dataTableUser;
                            LoadGridUtenze(dataGridView1);
                        }
                    }
                }
                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore caricamento griglia utenze: " + ex.Message);
            }
        }
        private void LoadGridTemplate(DataGridView dgv)
        {
            dataGridView2.ReadOnly = true;
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                dgv.Columns[column.Index].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView2.Columns[column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.Columns["NOME FILE"].Width = 370;
            dgv.Columns["PATH"].Width = 700;
            dgv.Columns["DATA INSERIMENTO"].Width = 155;

            dgv.Columns["DATA INSERIMENTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["DATA INSERIMENTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void LoadGridTemplate()
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                using (MySqlConnection con = new MySqlConnection(GestioneMySql.StringaConnessione))
                {
                    var sb = new StringBuilder();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT KEY_TEMPL, DESCR 'NOME FILE', PATH, DATE_FORMAT(DTINS,'%d/%m/%Y %H:%i') 'DATA INSERIMENTO' FROM TEMPLATE ", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            dataTableTemplate = new DataTable();
                            sda.Fill(dataTableTemplate);

                            dataTableTemplate.Columns["KEY_TEMPL"].ColumnMapping = MappingType.Hidden;
                            DataRow dr = dataTableTemplate.NewRow();
                            dr["NOME FILE"] = "Aggiungi Template";
                            dataTableTemplate.Rows.Add(dr);

                            dataGridView2.DataSource = dataTableTemplate;
                            LoadGridTemplate(dataGridView2);
                        }
                    }
                }
                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore caricamento griglia utenze: " + ex.Message);
            }
        }


        /*
        var sb = new StringBuilder();

        sb.AppendLine("SELECT NOME");
        sb.AppendLine("FROM Users");
        sb.AppendLine("ORDER BY DTINS DESC");

        if (!GestioneMySql.OpenConnection())
            throw new Exception("Errore nell'apertura della connessione.");

        MySqlCommand comm = GestioneMySql.connection.CreateCommand();
        comm.CommandText = sb.ToString();
        comm.ExecuteNonQuery();

        DataTable dataTableUser = new DataTable();
        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(comm);

        mySqlDataAdapter.Fill(dataTableUser);
        dataGridView1.DataSource = dataTableUser;

        dataGridView1.Refresh();
        */




        //dataGridView1.Rows[dataTableUser.Rows.Count].Cells[0].Value = "Aggiungi Utenza";

        //dataTableUser = UsersDB.LoadUser(string.Empty);

        //dataGridView1.Rows.Clear();
        //dataGridView1.ReadOnly = true;
        //dataGridView1.Columns[0].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
        //dataGridView1.Columns[1].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
        //dataGridView1.Columns[2].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
        //dataGridView1.Columns[3].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
        //dataGridView1.Columns[4].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);

        //foreach (DataRow item in dataTableUser.Rows)
        //{
        //    int n = dataGridView1.Rows.Add();

        //    dataGridView1.Rows[n].Cells[0].Value = item[1].ToString();
        //    dataGridView1.Rows[n].Cells[1].Value = item[2].ToString();

        //    if (!string.IsNullOrEmpty(item[4].ToString()))
        //    {
        //        dataGridView1.Rows[n].Cells[2].Value = DateTime.ParseExact(item[4].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //        dataGridView1.Rows[n].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //        dataGridView1.Rows[n].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    }

        //    dataGridView1.Rows[n].Cells[3].Value = item[15].ToString() + " " + item[16].ToString() + " " + item[17].ToString();
        //    dataGridView1.Rows[n].Cells[4].Value = DateTime.ParseExact(item[3].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //    dataGridView1.Rows[n].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dataGridView1.Rows[n].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //}

        //dataGridView1.Rows[dataTableUser.Rows.Count].Cells[0].Value = "Aggiungi Utenza";


        //}

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetShowPanel1(bool showing)
        {
            splitContainer1.Panel1Collapsed = showing;
        }
        private void SetShowPanel2(bool showing)
        {
            splitContainer2.Panel2Collapsed = showing;
        }
        private void chkShowPanel_CheckedChanged(object sender, EventArgs e)
        {
            SetShowPanel1(!splitContainer1.Panel1Collapsed);
        }
        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("E' possibile creare un nuovo account." + Environment.NewLine + "Vuoi continuare ?", "Creazione account", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            FormAccount formAccount = new FormAccount();
            formAccount.ShowDialog();
        }
        private void StartTimer()
        {
            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }
        private void t_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString();
        }
        private void chkShowTemplate_CheckedChanged(object sender, EventArgs e)
        {
            SetShowPanel2(!splitContainer2.Panel2Collapsed);
        }
        private void btnWacom_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("E' possibile aprire il programma Wacom Sign PRO PDF Vuoi continuare ?", "Creazione account", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            try
            {
                var runCommand = ConfigDB.LoadConfig(Config.ParamValues.SIGN_PRO_PDF).CONF_VALUE;
                if (string.IsNullOrEmpty(runCommand))
                    throw new Exception("stringa di configurazione per Wacom Sign Pro Pdf non settata correttamente.");

                var process = Process.Start(runCommand);
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message, "Configurazione errata");
            }
        }
        private void btnAddUtenzeFromFile_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(string.Format("Verrà acquisito un file xml per l'importazione delle utenze." + Environment.NewLine + "Vuoi proseguire?", ""), "Acquisizione File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            string filePath;
            filePath = FileService.CaricaFile("xml", "XML files (*.xml)|*.xml|All files (*.*)|*.*");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Impossibile trovare il file nella posizione indicata.", "File non trovato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TemplateXmlDB.Exists(filePath))
            {
                MessageBox.Show("Il file risulta già importato.", "File già presente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                FileService.AcquireXML(new StreamReader(filePath), filePath);

                MessageBox.Show("File correttamente acquisito.", "Acquisizione File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGridUtenze(UsersDB.SetQueryUser(string.Empty));
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Errore: " + ex.Message,"Errore Importazione",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
        }
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Enabled = (cmbFilter.SelectedIndex != 0);
        }
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            string strFilter = ((KeyValuePair<string, string>)cmbFilter.SelectedItem).Value + " LIKE '" + txtFilter.Text + "%'";
            LoadGridUtenze(UsersDB.SetQueryUser(strFilter));
        } 
        
        #region EventDatagridView1
        private void ViewUser(int rowIndex)
        {
            int? intKey = null;
            DataRow row = dataTableUser.Rows[rowIndex];
            if (row != null)
            {
                int outIntKey;
                if (int.TryParse(row.ItemArray[0].ToString(), out outIntKey))
                    intKey = outIntKey;
            }

            FormUser frm = new FormUser(intKey);
            frm.ShowDialog();
            LoadGridUtenze(UsersDB.SetQueryUser(string.Empty));
        }
        private void DeleteUser(int rowIndex) 
        {
            int? intKey = null;
            DataRow row = dataTableUser.Rows[rowIndex];
            if (row != null)
            {
                if (DialogResult.No == MessageBox.Show(("Verrà eliminata l'utenza selezionata. Vuoi proseguire ?"), "Elimina Utenza", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;

                int outIntKey;
                if (int.TryParse(row.ItemArray[0].ToString(), out outIntKey))
                {
                    intKey = outIntKey;
                    UsersDB.DeleteUser(outIntKey);
                    LoadGridUtenze(UsersDB.SetQueryUser(string.Empty));
                }
            }
        }        
        private void ExportXML(int rowIndex) 
        {
            if (DialogResult.No == MessageBox.Show(string.Format("Verrà esportato un file xml relativo all'utenza selezionata." + Environment.NewLine + "Vuoi proseguire?", ""), "Esportazione File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            try
            {
                DataRow row = dataTableUser.Rows[rowIndex];
                if (row == null)
                    return;

                if (int.TryParse(row.ItemArray[0].ToString(), out int intKey) == false)
                    throw new Exception();

                Users user = UsersDB.LoadUser(intKey).FirstOrDefault();
                if (user == null)
                    return;

                EricsoftDB.GetXML(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore" + ex.Message);
            }
        }
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            if (e.RowIndex == (dataGridView1.Rows.Count - 1))
                return;

            if (e.Button == MouseButtons.Left)
                return;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
            }
            dataGridView1_CellClick(this.dataGridView1, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
            this.contextMenu.Show(this.dataGridView1, e.Location);
            this.contextMenu.Show(Cursor.Position);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                return;

            dataGridView1.Rows[e.RowIndex].Selected = true;
            this.rowIndex = e.RowIndex;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name != "NOME")
                return;

            ViewUser(e.RowIndex);
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((row.Cells[0].RowIndex) == (dataGridView1.Rows.Count - 1))
                    row.DefaultCellStyle.ForeColor = Color.Blue;
            }
        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dataGridView1.SortCompare += new DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
        }
        private void toolStripViewUte_Click(object sender, EventArgs e)
        {
            ViewUser(this.rowIndex);
        }
        private void toolStripDel_Click(object sender, EventArgs e)
        {
            DeleteUser(this.rowIndex);
        }
        private void toolStripXml_Click(object sender, EventArgs e)
        {
            ExportXML(this.rowIndex);
        }

        #endregion

        #region EventDatagridView2
        private void ViewTemplate(int rowIndex)
        {
            try
            {
                string strPathFile = this.dataGridView2.Rows[rowIndex].Cells[1].Value.ToString();

                if (!File.Exists(strPathFile))
                    throw new FileNotFoundException("Impossibile trovare il file nella posizione indicata.");

                FileHelper.OpenFile(strPathFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message, "File inesistente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void DeleteTemplate(int rowIndex) 
        {
            try
            {
                if (dataTableTemplate.Rows.Count == 0)
                    return;

                if (DialogResult.No == MessageBox.Show(string.Format("Verrà eliminato il template selezionato." + Environment.NewLine + "Vuoi proseguire?", ""), "Cancellazione File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;

                string strPathFile = this.dataGridView2.Rows[rowIndex].Cells[1].Value.ToString();

                if (!File.Exists(strPathFile))
                    throw new FileNotFoundException("Impossibile trovare il file nella posizione indicata.");

                TemplateDB.DeleteTemplate(rowIndex);

                this.LoadGridTemplate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message, "File inesistente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ViewListUte(int rowIndex) 
        {
            try
            {
                DataRow row = dataTableTemplate.Rows[rowIndex];
                
                FormUtenzeFirmatarie frm = new FormUtenzeFirmatarie(row);//,strFilter, row["NOME FILE"].ToString());
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message, "Utenze firmatarie", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if ((row.Cells[0].RowIndex) == (dataGridView2.Rows.Count - 1))
                    row.DefaultCellStyle.ForeColor = Color.Blue;
            }
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 index = dataGridView2.Rows.Count - 1;
                if (dataGridView2.Rows.Count - 1 == e.RowIndex)
                {
                    string strFileName;
                    strFileName = FileService.CaricaFile("pdf", "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*");

                    if (string.IsNullOrEmpty(strFileName))
                        return;

                    if (!File.Exists(strFileName))
                        throw new FileNotFoundException("Impossibile trovare il file nella posizione indicata.");

                    IImporter.ImportaTemplate(strFileName);

                    this.LoadGridTemplate();
                }
                else
                    ViewTemplate(e.RowIndex);                
            }
            catch (FileNotFoundException fex)
            {
                MessageBox.Show("Errore: " + fex.Message, "File inesistente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message,"Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
                return;

            if (e.RowIndex == (dataGridView2.Rows.Count - 1))
                return;

            if (e.Button == MouseButtons.Left)
                return;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
            }
            dataGridView2_CellClick(this.dataGridView2, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
            this.contextMenuTemplate.Show(this.dataGridView2, e.Location);
            this.contextMenuTemplate.Show(Cursor.Position);
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                return;

            dataGridView2.Rows[e.RowIndex].Selected = true;
            this.rowIndexTemplate = e.RowIndex;
        }
        private void toolStripViewTemplate_Click(object sender, EventArgs e)
        {
            ViewTemplate(this.rowIndexTemplate);
        }
        private void toolStripDelTemplate_Click(object sender, EventArgs e)
        {
            DeleteTemplate(this.rowIndexTemplate);
        }
        private void toolStripPrivacyListUte_Click(object sender, EventArgs e)
        {
            ViewListUte(this.rowIndexTemplate);
        }

        #endregion


    }
} 

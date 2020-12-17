using Sdr_FirmaInCloud.BL;
using Sdr_FirmaInCloud.BL.FirmaInCloud;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Domain;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.UI
{
    public partial class FormPrivacy : Form
    { 
        DataTable dataTable;
        DataTable dataTableTemplate;
        int rowIndex = 0;
        
        public FormPrivacy(string strXPermission)
        {
            InitializeComponent();
            DisplayData(strXPermission);            
        }

        private void DisplayData(string strXPermission)
        {
            LoadGridUtenze();
            EnableTab(tabPage2, !string.IsNullOrEmpty(strXPermission));
            LoadTemplate();
        }
        private void EnableTab(TabPage page, bool enable)
        {
            btmImporta.Enabled = enable;
            foreach (Control ctl in page.Controls) ctl.Enabled = enable;
        }
        private void LoadTemplate()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView2.ReadOnly = true;

                dataTableTemplate = FirmaInCloudDB.LoadTemplate(null,"");

                dataGridView2.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.Columns[1].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView2.Columns[2].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView2.Columns[3].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView2.Columns[0].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);  

                foreach (DataRow item in dataTableTemplate.Rows)
                {
                    int n = dataGridView2.Rows.Add();

                    dataGridView2.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridView2.Rows[n].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView2.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridView2.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridView2.Rows[n].Cells[3].Value = DateTime.ParseExact(item[3].ToString(), "yyyyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture); ;
                    dataGridView2.Rows[n].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore caricamento griglia template: " + ex.Message);
            }
            finally 
            {
                dataTableTemplate = null;
            } 
        }

        private void LoadGridUtenze()
        {
            try
            {
                dataTable = new DataTable();
                dataTable = UsersDB.LoadUser(string.Empty);
                
                dataGridView1.Rows.Clear();
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[0].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView1.Columns[1].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView1.Columns[2].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView1.Columns[3].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                dataGridView1.Columns[4].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold);
                 
                foreach (DataRow item in dataTable.Rows)
                {
                    int n = dataGridView1.Rows.Add();

                    dataGridView1.Rows[n].Cells[0].Value = item[1].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item[2].ToString();

                    if (!string.IsNullOrEmpty(item[4].ToString()))
                    {
                        dataGridView1.Rows[n].Cells[2].Value = DateTime.ParseExact(item[4].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        dataGridView1.Rows[n].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView1.Rows[n].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    dataGridView1.Rows[n].Cells[3].Value = item[15].ToString() + " " + item[16].ToString() + " " + item[17].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = DateTime.ParseExact(item[3].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dataGridView1.Rows[n].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Rows[n].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; 
                } 
                dataGridView1.Rows[dataTable.Rows.Count].Cells[0].Value = "Aggiungi Utenza";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore caricamento griglia utenze: " + ex.Message);
            }            
        }

        #region DataGridView1Event

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                return;

            dataGridView1.CurrentRow.Selected = true;
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            
            if (e.RowIndex == (dataGridView1.Rows.Count - 1))
                return;

            if (e.Button == MouseButtons.Left)
                return;

            dataGridView1.CurrentRow.Selected = true;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
            }
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                this.dataGridView1.Rows[e.ColumnIndex].Selected = true;
            }
            this.rowIndex = e.RowIndex;
            dataGridView1.CurrentRow.Selected = true;
            this.contextMenuPrivacy.Show(this.dataGridView1, e.Location);
            this.contextMenuPrivacy.Show(Cursor.Position);

            dataGridView1_CellClick(this.dataGridView1, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name != "Name")
                return;

            int? intKey = null;
            if (dataTable.Rows.Count != dataGridView1.Rows[e.RowIndex].Index)
            {
                DataRow row = dataTable.Rows[e.RowIndex];
                if (row != null)
                {
                    int outIntKey;
                    if (int.TryParse(row.ItemArray[0].ToString(), out outIntKey))
                    {
                        intKey = outIntKey;
                    }
                }
            }
            FormUser frm = new FormUser(intKey);
            frm.ShowDialog();
            LoadGridUtenze();
        }

        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((row.Cells[0].RowIndex) == (dataGridView1.Rows.Count - 1))
                    row.DefaultCellStyle.ForeColor = Color.Blue;
            }
        }
        
        #endregion

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                return;

            if (e.RowIndex == -1)
                return;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.Selected = false;
            }
            this.dataGridView2.Rows[e.RowIndex].Selected = true;
            this.rowIndex = e.RowIndex;
            this.contextMenuTemplate.Show(this.dataGridView2, e.Location);
            this.contextMenuTemplate.Show(Cursor.Position);
        }
        
        private void btnAddUtenza_Click(object sender, EventArgs e)
        {    
            FormUser frm = new FormUser(null);
            frm.ShowDialog();
            LoadGridUtenze();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btmImporta_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(string.Format("Verrà acquisito un nuovo template. " + Environment.NewLine + "Vuoi proseguire ?", ""), "Acquisizione File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            try
            {
                string strFileName;
                strFileName = FileService.CaricaFile("pdf", "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*");

                if (string.IsNullOrEmpty(strFileName))
                    return;

                if (!File.Exists(strFileName))
                    throw new FileNotFoundException("Impossibile trovare il file nella posizione indicata.");

                IImporter.ImportaTemplate(strFileName);

                this.LoadTemplate();
            }
            catch (FileNotFoundException fex)
            {
                MessageBox.Show("Errore: " + fex.Message,"File inesistente",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Errore: " + ex.Message);
            }
            
        }

        private void viewPermessi_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count > 0) 
                FileHelper.OpenFile(this.dataGridView2.Rows[rowIndex].Cells[2].Value.ToString());
            
            //if (dataTableTemplate.Rows.Count == 0)
            //    return;

            //DataRow row = dataTableTemplate.Rows[rowIndex];
            //if (row == null)
            //    return; 
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
                FileHelper.OpenFile(this.dataGridView2.Rows[rowIndex].Cells[2].Value.ToString());

            //int? intKey = null;
            //if (dataTable.Rows.Count != dataGridView1.Rows[e.RowIndex].Index)
            //{
            //    DataRow row = dataTable.Rows[e.RowIndex];
            //    if (row != null)
            //    {
            //        int outIntKey;
            //        if (int.TryParse(row.ItemArray[0].ToString(), out outIntKey))
            //        {
            //            intKey = outIntKey;
            //        }
            //    }
            //}
            //FormUser frm = new FormUser(intKey);
            //frm.ShowDialog();
            //LoadGridUtenze();
        }


        private void deletePermessi_Click(object sender, EventArgs e)
        {
            if (dataTableTemplate.Rows.Count == 0)
                return;

            DataRow row = dataTableTemplate.Rows[rowIndex];
            if (row == null)
                return;

            if (DialogResult.No == MessageBox.Show(string.Format("Verrà eliminato il template selezionato." + Environment.NewLine + "Vuoi proseguire?", ""), "Acquisizione File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            TemplateDB.DeleteTemplate(1);// row.ItemArray[0].ToString());

            this.LoadTemplate();
        }

        private void btnUtenza_Click(object sender, EventArgs e)
        {
            FormUser frm = new FormUser(null);
            frm.ShowDialog();
            LoadGridUtenze();
        }

        private void btnAddUtenzeFromFile_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(string.Format("Verrà acquisito un file xml per l'importazione delle utenze." + Environment.NewLine + "Vuoi proseguire?", ""), "Acquisizione File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            string filePath;
            filePath = FileService.CaricaFile("xml", "XML files (*.xml)|*.xml|All files (*.*)|*.*");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Impossibile trovare il file nella posizione indicata.", "File non trovato", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if (TemplateXmlDB.Exists(filePath))
            {
                MessageBox.Show("Il file risulta già importato.", "File già presente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FileService.AcquireXML(new StreamReader(filePath), filePath);

            MessageBox.Show("File correttamente acquisito.", "Acquisizione File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadGridUtenze();

        }
 
        private void toolStripViewUte_Click(object sender, EventArgs e)
        {
            if (dataTable.Rows.Count == 0)
                return;

            DataRow row = dataTable.Rows[rowIndex];
            if (row != null)
            {
                int res;
                if (int.TryParse(row.ItemArray[0].ToString(),out res))
                {
                    FormUser frm = new FormUser(res);
                    frm.ShowDialog();
                    LoadGridUtenze();
                }
            }
        }

        private void toolStripDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            if (!this.dataGridView1.Rows[rowIndex].IsNewRow)
            {
                if (DialogResult.No == MessageBox.Show(("Verrà eliminata l'utenza selezionata. Vuoi proseguire ?"), "Elimina Utenza", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;

                DataRow row = dataTable.Rows[rowIndex];
                if (row == null)
                    return;

                if (int.TryParse(row.ItemArray[0].ToString(), out int intKey))
                {
                    UsersDB.DeleteUser(intKey);
                    LoadGridUtenze();
                }
            }
        }

        private void toolStripXml_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(string.Format("Verrà esportato un file xml relativo all'utenza selezionata." + Environment.NewLine + "Vuoi proseguire?", ""), "Esportazione File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;
            
            try
            {
                DataRow row = dataTable.Rows[rowIndex];
                if (row == null)
                    return;

                if (int.TryParse(row.ItemArray[0].ToString(), out int intKey) == false)
                    throw new Exception();

                Users user = UsersDB.LoadUser(intKey).FirstOrDefault();

                EricsoftDB.GetXML(user);
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Errore" + ex.Message);
            }
        }

        private void toolStripPrivacyListUte_Click(object sender, EventArgs e)
        {
            if (dataTableTemplate.Rows.Count == 0)
                return;

            DataRow row = dataTableTemplate.Rows[rowIndex];

            FormUtenzeFirmatarie frm = new FormUtenzeFirmatarie(row.ItemArray[0].ToString(), row.ItemArray[1].ToString());
            frm.ShowDialog();
        }

        
    }
}



using MySql.Data.MySqlClient;
using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.UI
{
    public partial class FormUtenzeFirmatarie : Form
    {
        //string strFilter = string.Format(" KEY_USER IN (SELECT KEY_USER FROM TEMPLATE_LINQ WHERE KEY_TEMPL = {0})", row["KEY_TEMPL"]);

        DataTable dataTableUser;
        bool validDate;
        string sFilter;
        bool bFreeze;
        public FormUtenzeFirmatarie(DataRow dataRow)//  string strFilter, string strName)
        {
            InitializeComponent();            
            DisplayData(Convert.ToInt32(dataRow["KEY_TEMPL"]),dataRow["NOME FILE"].ToString());
        }
        private void FillFilterCombo()
        {
            Dictionary<string, string> cmb = new Dictionary<string, string>
            {
                { "0", "" },
                { "1", "Nome" },
                { "2", "Cognome" },
                { "3", "Data Inserimento" }
            };
            cmbFilter.DataSource = new BindingSource(cmb, null);
            cmbFilter.DisplayMember = "Value";
            cmbFilter.ValueMember = "Key";

            cmb = new Dictionary<string, string>
            {
                { "0", "" },
                { "1", "=" },
                { "2", "<" },
                { "3", ">" },
            };
            cmbTime.DataSource = new BindingSource(cmb, null);
            cmbTime.DisplayMember = "Value";
            cmbTime.ValueMember = "Key";

            txtFilter.Enabled = false;
        }
        private void DisplayData(int intKeyTempl, string strName)
        {
            bFreeze = true;
            sFilter = string.Format(" KEY_USER IN (SELECT KEY_USER FROM TEMPLATE_LINQ WHERE KEY_TEMPL = {0})", intKeyTempl);
            this.lblTemplate.Text = "Template: " + strName;
            LoadGridUtenze(UsersDB.SetQueryUser(sFilter));
            FillFilterCombo();            
            this.Refresh();
            bFreeze = false;
        }
        private void LoadGridUtenze(DataGridView dgv)
        {
            dataGridView1.ReadOnly = true;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dgv.Columns[column.Index].HeaderCell.Style.Font = new Font("Microsoft Tai Le", 10, FontStyle.Bold); 
            }

            dgv.Columns["NOME"].Width = 200;
            dgv.Columns["COGNOME"].Width = 200;
            dgv.Columns["DATA DI NASCITA"].Width = 155;
            dgv.Columns["INDIRIZZO"].Width = 350;
            dgv.Columns["MAIL"].Width = 170;
            //dgv.Columns["DATA INSERIMENTO"].Width = 180;

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
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bFreeze)
                return;

            validDate = false;
            cmbTime.SelectedIndex =-1;
            cmbTime.Enabled = (cmbFilter.SelectedIndex == 3);
            txtFilter.Enabled = (cmbFilter.SelectedIndex != 0); 
        }
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (bFreeze)
                return;

            string strFilter = "";
            if (cmbFilter.SelectedIndex == 3)
            {
                if (DateTime.TryParseExact(txtFilter.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    if (cmbTime.SelectedIndex == 0)
                    {
                        MessageBox.Show("Impostare il riferimento (<,>,=)", "Riferimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    validDate = true;
                    strFilter = sFilter + " AND '" + DateTime.ParseExact(txtFilter.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") +"'"+ ((KeyValuePair<string, string>)cmbTime.SelectedItem).Value + " DTINS ";
                }
                else
                    return;
            }
            else 
            {
                if (string.IsNullOrEmpty(txtFilter.Text))
                    return;
                
                strFilter = sFilter + " AND " + ((KeyValuePair<string, string>)cmbFilter.SelectedItem).Value + " LIKE '" + txtFilter.Text + "%'";
            }
            LoadGridUtenze(UsersDB.SetQueryUser(strFilter));
        }

        private void txtFilter_MouseHover(object sender, EventArgs e)
        {
            if (bFreeze)
                return;

            if (cmbFilter.SelectedIndex != 3)
                return;

            if (validDate)
                return;

            Point locationOnForm = txtFilter.FindForm().PointToClient
            (
                txtFilter.Parent.PointToScreen(txtFilter.Location)
            );
            ToolTip toolTip = new ToolTip();
            toolTip.InitialDelay = 0;
            toolTip.Show("La data deve essere nel formato dd/MM/yyyy",(TextBox)sender, locationOnForm.X, locationOnForm.Y, 2000);
        }

        private void cmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bFreeze)
                return;

            this.txtFilter_KeyUp(sender, new KeyEventArgs(Keys.Control));
        }
    }
}

namespace SDR_FirmaInCloud.UI
{
    partial class FormPrivacy
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrivacy));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAddUtenzeFromFile = new System.Windows.Forms.Button();
            this.btnUtenza = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOfBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateInsert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btmImporta = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuPrivacy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripViewUte = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripXml = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripPrivacyListUte = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuPrivacy.SuspendLayout();
            this.contextMenuTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Tai Le", 11F);
            this.tabControl1.Location = new System.Drawing.Point(11, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1124, 476);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tabPage1.Controls.Add(this.btnAddUtenzeFromFile);
            this.tabPage1.Controls.Add(this.btnUtenza);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1116, 444);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ospiti";
            // 
            // btnAddUtenzeFromFile
            // 
            this.btnAddUtenzeFromFile.BackColor = System.Drawing.SystemColors.Info;
            this.btnAddUtenzeFromFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddUtenzeFromFile.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAddUtenzeFromFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnAddUtenzeFromFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddUtenzeFromFile.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUtenzeFromFile.ForeColor = System.Drawing.Color.Black;
            this.btnAddUtenzeFromFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUtenzeFromFile.Image")));
            this.btnAddUtenzeFromFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddUtenzeFromFile.Location = new System.Drawing.Point(6, 403);
            this.btnAddUtenzeFromFile.Name = "btnAddUtenzeFromFile";
            this.btnAddUtenzeFromFile.Size = new System.Drawing.Size(94, 33);
            this.btnAddUtenzeFromFile.TabIndex = 12;
            this.btnAddUtenzeFromFile.Text = "Importa";
            this.btnAddUtenzeFromFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddUtenzeFromFile.UseVisualStyleBackColor = false;
            this.btnAddUtenzeFromFile.Click += new System.EventHandler(this.btnAddUtenzeFromFile_Click);
            // 
            // btnUtenza
            // 
            this.btnUtenza.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnUtenza.BackColor = System.Drawing.SystemColors.Info;
            this.btnUtenza.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUtenza.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnUtenza.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnUtenza.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUtenza.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnUtenza.ForeColor = System.Drawing.Color.Black;
            this.btnUtenza.Image = ((System.Drawing.Image)(resources.GetObject("btnUtenza.Image")));
            this.btnUtenza.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUtenza.Location = new System.Drawing.Point(497, 403);
            this.btnUtenza.Name = "btnUtenza";
            this.btnUtenza.Size = new System.Drawing.Size(105, 33);
            this.btnUtenza.TabIndex = 11;
            this.btnUtenza.Text = "Aggiungi";
            this.btnUtenza.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUtenza.UseVisualStyleBackColor = false;
            this.btnUtenza.Click += new System.EventHandler(this.btnUtenza_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.Surname,
            this.DateOfBirth,
            this.Address,
            this.DateInsert});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Highlight;
            this.dataGridView1.Location = new System.Drawing.Point(3, 6);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Tai Le", 11F);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1077, 237);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting_1);
            this.dataGridView1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
            // 
            // Name
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name.DefaultCellStyle = dataGridViewCellStyle2;
            this.Name.Frozen = true;
            this.Name.HeaderText = "Nome";
            this.Name.Name = "Name";
            this.Name.Width = 150;
            // 
            // Surname
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Surname.DefaultCellStyle = dataGridViewCellStyle3;
            this.Surname.Frozen = true;
            this.Surname.HeaderText = "Cognome";
            this.Surname.Name = "Surname";
            this.Surname.Width = 200;
            // 
            // DateOfBirth
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateOfBirth.DefaultCellStyle = dataGridViewCellStyle4;
            this.DateOfBirth.Frozen = true;
            this.DateOfBirth.HeaderText = "Data di Nascita";
            this.DateOfBirth.Name = "DateOfBirth";
            // 
            // Address
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Address.DefaultCellStyle = dataGridViewCellStyle5;
            this.Address.Frozen = true;
            this.Address.HeaderText = "Indirizzo";
            this.Address.Name = "Address";
            this.Address.Width = 500;
            // 
            // DateInsert
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateInsert.DefaultCellStyle = dataGridViewCellStyle6;
            this.DateInsert.Frozen = true;
            this.DateInsert.HeaderText = "Data Inserimento";
            this.DateInsert.Name = "DateInsert";
            this.DateInsert.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DateInsert.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DateInsert.Width = 110;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPage2.Controls.Add(this.btmImporta);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1116, 444);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Template";
            // 
            // btmImporta
            // 
            this.btmImporta.BackColor = System.Drawing.SystemColors.Info;
            this.btmImporta.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btmImporta.FlatAppearance.BorderSize = 2;
            this.btmImporta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btmImporta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btmImporta.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold);
            this.btmImporta.ForeColor = System.Drawing.Color.Black;
            this.btmImporta.Image = ((System.Drawing.Image)(resources.GetObject("btmImporta.Image")));
            this.btmImporta.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btmImporta.Location = new System.Drawing.Point(1000, 405);
            this.btmImporta.Name = "btmImporta";
            this.btmImporta.Size = new System.Drawing.Size(110, 33);
            this.btmImporta.TabIndex = 6;
            this.btmImporta.Text = "Importa documento";
            this.btmImporta.UseVisualStyleBackColor = false;
            this.btmImporta.Click += new System.EventHandler(this.btmImporta_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Tai Le", 11F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.GridColor = System.Drawing.SystemColors.Highlight;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Tai Le", 11F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView2.RowHeadersVisible = false;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView2.Size = new System.Drawing.Size(1107, 396);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
            this.dataGridView2.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseUp);
            // 
            // Id
            // 
            this.Id.Frozen = true;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Nome File";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 300;
            // 
            // Column2
            // 
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "Path";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.ToolTipText = "Percorso di salvataggio del file";
            this.Column2.Width = 500;
            // 
            // Column3
            // 
            this.Column3.Frozen = true;
            this.Column3.HeaderText = "Data Inserimento";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.ToolTipText = "Data inserimento file";
            this.Column3.Width = 150;
            // 
            // contextMenuPrivacy
            // 
            this.contextMenuPrivacy.BackColor = System.Drawing.SystemColors.Info;
            this.contextMenuPrivacy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripViewUte,
            this.toolStripDel,
            this.toolStripSeparator2,
            this.toolStripXml});
            this.contextMenuPrivacy.Name = "contextMenuStrip1";
            this.contextMenuPrivacy.Size = new System.Drawing.Size(182, 104);
            // 
            // toolStripViewUte
            // 
            this.toolStripViewUte.BackColor = System.Drawing.SystemColors.Info;
            this.toolStripViewUte.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripViewUte.Name = "toolStripViewUte";
            this.toolStripViewUte.Size = new System.Drawing.Size(181, 24);
            this.toolStripViewUte.Text = "Visualizza Utenza";
            this.toolStripViewUte.Click += new System.EventHandler(this.toolStripViewUte_Click);
            // 
            // toolStripDel
            // 
            this.toolStripDel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripDel.Name = "toolStripDel";
            this.toolStripDel.Size = new System.Drawing.Size(181, 24);
            this.toolStripDel.Text = "Elimina Utenza";
            this.toolStripDel.Click += new System.EventHandler(this.toolStripDel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(178, 6);
            // 
            // toolStripXml
            // 
            this.toolStripXml.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripXml.Name = "toolStripXml";
            this.toolStripXml.Size = new System.Drawing.Size(181, 24);
            this.toolStripXml.Text = "Esporta Utenza";
            this.toolStripXml.Click += new System.EventHandler(this.toolStripXml_Click);
            // 
            // contextMenuTemplate
            // 
            this.contextMenuTemplate.BackColor = System.Drawing.SystemColors.Info;
            this.contextMenuTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.toolStripSeparator1,
            this.toolStripPrivacyListUte});
            this.contextMenuTemplate.Name = "contextMenuStrip1";
            this.contextMenuTemplate.Size = new System.Drawing.Size(242, 82);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(241, 24);
            this.toolStripMenuItem1.Text = "Visualizza Template";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.viewPermessi_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(241, 24);
            this.toolStripMenuItem3.Text = "Elimina Template";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.deletePermessi_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(238, 6);
            // 
            // toolStripPrivacyListUte
            // 
            this.toolStripPrivacyListUte.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStripPrivacyListUte.Name = "toolStripPrivacyListUte";
            this.toolStripPrivacyListUte.Size = new System.Drawing.Size(241, 24);
            this.toolStripPrivacyListUte.Text = "Visualizza utenze firmatarie";
            this.toolStripPrivacyListUte.ToolTipText = "Visualizza le utenze firmatarie del template selezionato";
            this.toolStripPrivacyListUte.Click += new System.EventHandler(this.toolStripPrivacyListUte_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(1038, 486);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 33);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Esci";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FormPrivacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1145, 531);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false; 
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDR Consulenze - Privacy";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuPrivacy.ResumeLayout(false);
            this.contextMenuTemplate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuPrivacy;
        private System.Windows.Forms.ToolStripMenuItem toolStripViewUte;
        private System.Windows.Forms.ToolStripMenuItem toolStripDel;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ContextMenuStrip contextMenuTemplate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripPrivacyListUte;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.Button btmImporta;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripXml;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOfBirth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateInsert;
        private System.Windows.Forms.Button btnUtenza;
        private System.Windows.Forms.Button btnAddUtenzeFromFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}


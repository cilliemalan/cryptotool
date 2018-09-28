namespace CryptoTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ColumnHeader chName;
            System.Windows.Forms.ColumnHeader chInfo;
            this.txtWorkingDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tpCsrs = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cbCsrAltNamesInclude = new System.Windows.Forms.CheckBox();
            this.cbCsrAltNamesCritical = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtCsrBasicConstraintPathLength = new System.Windows.Forms.TextBox();
            this.lblCsrBasicConstraintPathLength = new System.Windows.Forms.Label();
            this.rbCsrBasicConstraintCA = new System.Windows.Forms.RadioButton();
            this.rbCsrBasicConstraintEndEntity = new System.Windows.Forms.RadioButton();
            this.rbCsrBasicConstraintExclude = new System.Windows.Forms.RadioButton();
            this.cbCsrBasicConstraintCritical = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbCsrExtendedUsageCritical = new System.Windows.Forms.CheckBox();
            this.cbCsrExtendedUsage = new System.Windows.Forms.CheckedListBox();
            this.btnCsrProcess = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbCsrCertUsageCritical = new System.Windows.Forms.CheckBox();
            this.cbCsrUsage = new System.Windows.Forms.CheckedListBox();
            this.gbKeyBits = new System.Windows.Forms.GroupBox();
            this.cbCsKeyBits = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdCsrKeyEcdsa = new System.Windows.Forms.RadioButton();
            this.rdCsrKeyDsa = new System.Windows.Forms.RadioButton();
            this.rdCsrKeyRsa = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdCsrGenerateNewKey = new System.Windows.Forms.RadioButton();
            this.rdCsrUseExistingKey = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCsrSubjEmail = new System.Windows.Forms.TextBox();
            this.txtCsrSubjL = new System.Windows.Forms.TextBox();
            this.txtCsrSubjST = new System.Windows.Forms.TextBox();
            this.txtCsrSubjC = new System.Windows.Forms.TextBox();
            this.txtCsrSubjOU = new System.Windows.Forms.TextBox();
            this.txtCsrSubjO = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCsrDomains = new System.Windows.Forms.TextBox();
            chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            chInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tpCsrs.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbKeyBits.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chName
            // 
            chName.Text = "File";
            chName.Width = 200;
            // 
            // chInfo
            // 
            chInfo.Text = "Info";
            chInfo.Width = 132;
            // 
            // txtWorkingDir
            // 
            this.txtWorkingDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkingDir.Location = new System.Drawing.Point(116, 12);
            this.txtWorkingDir.Name = "txtWorkingDir";
            this.txtWorkingDir.Size = new System.Drawing.Size(1101, 20);
            this.txtWorkingDir.TabIndex = 0;
            this.txtWorkingDir.Text = "C:\\Users\\Cillie Malan\\Google Drive\\myneca";
            this.txtWorkingDir.TextChanged += new System.EventHandler(this.txtWorkingDir_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Working Directory:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(1223, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            chName,
            chInfo});
            this.lstFiles.Location = new System.Drawing.Point(6, 6);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(358, 564);
            this.lstFiles.TabIndex = 3;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tpCsrs);
            this.tabControl1.Location = new System.Drawing.Point(12, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1286, 602);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1278, 576);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Inspect";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tpCsrs
            // 
            this.tpCsrs.Controls.Add(this.groupBox7);
            this.tpCsrs.Controls.Add(this.groupBox6);
            this.tpCsrs.Controls.Add(this.groupBox5);
            this.tpCsrs.Controls.Add(this.btnCsrProcess);
            this.tpCsrs.Controls.Add(this.groupBox4);
            this.tpCsrs.Controls.Add(this.gbKeyBits);
            this.tpCsrs.Controls.Add(this.groupBox3);
            this.tpCsrs.Controls.Add(this.groupBox2);
            this.tpCsrs.Controls.Add(this.groupBox1);
            this.tpCsrs.Controls.Add(this.label2);
            this.tpCsrs.Controls.Add(this.txtCsrDomains);
            this.tpCsrs.Location = new System.Drawing.Point(4, 22);
            this.tpCsrs.Name = "tpCsrs";
            this.tpCsrs.Padding = new System.Windows.Forms.Padding(3);
            this.tpCsrs.Size = new System.Drawing.Size(1278, 576);
            this.tpCsrs.TabIndex = 1;
            this.tpCsrs.Text = "Generate CSRs";
            this.tpCsrs.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cbCsrAltNamesInclude);
            this.groupBox7.Controls.Add(this.cbCsrAltNamesCritical);
            this.groupBox7.Location = new System.Drawing.Point(377, 236);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(254, 68);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Subject Alternative Name";
            // 
            // cbCsrAltNamesInclude
            // 
            this.cbCsrAltNamesInclude.AutoSize = true;
            this.cbCsrAltNamesInclude.Checked = true;
            this.cbCsrAltNamesInclude.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCsrAltNamesInclude.Location = new System.Drawing.Point(13, 19);
            this.cbCsrAltNamesInclude.Name = "cbCsrAltNamesInclude";
            this.cbCsrAltNamesInclude.Size = new System.Drawing.Size(189, 17);
            this.cbCsrAltNamesInclude.TabIndex = 16;
            this.cbCsrAltNamesInclude.Text = "Include Subject Alternative Names";
            this.cbCsrAltNamesInclude.UseVisualStyleBackColor = true;
            // 
            // cbCsrAltNamesCritical
            // 
            this.cbCsrAltNamesCritical.AutoSize = true;
            this.cbCsrAltNamesCritical.Location = new System.Drawing.Point(13, 42);
            this.cbCsrAltNamesCritical.Name = "cbCsrAltNamesCritical";
            this.cbCsrAltNamesCritical.Size = new System.Drawing.Size(139, 17);
            this.cbCsrAltNamesCritical.TabIndex = 15;
            this.cbCsrAltNamesCritical.Text = "This Extension is Critical";
            this.cbCsrAltNamesCritical.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtCsrBasicConstraintPathLength);
            this.groupBox6.Controls.Add(this.lblCsrBasicConstraintPathLength);
            this.groupBox6.Controls.Add(this.rbCsrBasicConstraintCA);
            this.groupBox6.Controls.Add(this.rbCsrBasicConstraintEndEntity);
            this.groupBox6.Controls.Add(this.rbCsrBasicConstraintExclude);
            this.groupBox6.Controls.Add(this.cbCsrBasicConstraintCritical);
            this.groupBox6.Location = new System.Drawing.Point(637, 194);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(254, 139);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Basic Constraints";
            // 
            // txtCsrBasicConstraintPathLength
            // 
            this.txtCsrBasicConstraintPathLength.Enabled = false;
            this.txtCsrBasicConstraintPathLength.Location = new System.Drawing.Point(77, 88);
            this.txtCsrBasicConstraintPathLength.Name = "txtCsrBasicConstraintPathLength";
            this.txtCsrBasicConstraintPathLength.Size = new System.Drawing.Size(59, 20);
            this.txtCsrBasicConstraintPathLength.TabIndex = 14;
            this.txtCsrBasicConstraintPathLength.Text = "0";
            // 
            // lblCsrBasicConstraintPathLength
            // 
            this.lblCsrBasicConstraintPathLength.AutoSize = true;
            this.lblCsrBasicConstraintPathLength.Enabled = false;
            this.lblCsrBasicConstraintPathLength.Location = new System.Drawing.Point(6, 91);
            this.lblCsrBasicConstraintPathLength.Name = "lblCsrBasicConstraintPathLength";
            this.lblCsrBasicConstraintPathLength.Size = new System.Drawing.Size(65, 13);
            this.lblCsrBasicConstraintPathLength.TabIndex = 13;
            this.lblCsrBasicConstraintPathLength.Text = "Path Length";
            // 
            // rbCsrBasicConstraintCA
            // 
            this.rbCsrBasicConstraintCA.AutoSize = true;
            this.rbCsrBasicConstraintCA.Location = new System.Drawing.Point(6, 65);
            this.rbCsrBasicConstraintCA.Name = "rbCsrBasicConstraintCA";
            this.rbCsrBasicConstraintCA.Size = new System.Drawing.Size(116, 17);
            this.rbCsrBasicConstraintCA.TabIndex = 10;
            this.rbCsrBasicConstraintCA.Text = "Certificate Authority";
            this.rbCsrBasicConstraintCA.UseVisualStyleBackColor = true;
            this.rbCsrBasicConstraintCA.Click += new System.EventHandler(this.rbCsrBasicConstraint_Click);
            // 
            // rbCsrBasicConstraintEndEntity
            // 
            this.rbCsrBasicConstraintEndEntity.AutoSize = true;
            this.rbCsrBasicConstraintEndEntity.Checked = true;
            this.rbCsrBasicConstraintEndEntity.Location = new System.Drawing.Point(6, 42);
            this.rbCsrBasicConstraintEndEntity.Name = "rbCsrBasicConstraintEndEntity";
            this.rbCsrBasicConstraintEndEntity.Size = new System.Drawing.Size(72, 17);
            this.rbCsrBasicConstraintEndEntity.TabIndex = 10;
            this.rbCsrBasicConstraintEndEntity.TabStop = true;
            this.rbCsrBasicConstraintEndEntity.Text = "End entity";
            this.rbCsrBasicConstraintEndEntity.UseVisualStyleBackColor = true;
            this.rbCsrBasicConstraintEndEntity.Click += new System.EventHandler(this.rbCsrBasicConstraint_Click);
            // 
            // rbCsrBasicConstraintExclude
            // 
            this.rbCsrBasicConstraintExclude.AutoSize = true;
            this.rbCsrBasicConstraintExclude.Location = new System.Drawing.Point(6, 19);
            this.rbCsrBasicConstraintExclude.Name = "rbCsrBasicConstraintExclude";
            this.rbCsrBasicConstraintExclude.Size = new System.Drawing.Size(130, 17);
            this.rbCsrBasicConstraintExclude.TabIndex = 10;
            this.rbCsrBasicConstraintExclude.Text = "Exclude this extension";
            this.rbCsrBasicConstraintExclude.UseVisualStyleBackColor = true;
            this.rbCsrBasicConstraintExclude.Click += new System.EventHandler(this.rbCsrBasicConstraint_Click);
            // 
            // cbCsrBasicConstraintCritical
            // 
            this.cbCsrBasicConstraintCritical.AutoSize = true;
            this.cbCsrBasicConstraintCritical.Checked = true;
            this.cbCsrBasicConstraintCritical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCsrBasicConstraintCritical.Location = new System.Drawing.Point(6, 114);
            this.cbCsrBasicConstraintCritical.Name = "cbCsrBasicConstraintCritical";
            this.cbCsrBasicConstraintCritical.Size = new System.Drawing.Size(139, 17);
            this.cbCsrBasicConstraintCritical.TabIndex = 9;
            this.cbCsrBasicConstraintCritical.Text = "This Extension is Critical";
            this.cbCsrBasicConstraintCritical.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbCsrExtendedUsageCritical);
            this.groupBox5.Controls.Add(this.cbCsrExtendedUsage);
            this.groupBox5.Location = new System.Drawing.Point(637, 339);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(254, 204);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Extened Usage";
            // 
            // cbCsrExtendedUsageCritical
            // 
            this.cbCsrExtendedUsageCritical.AutoSize = true;
            this.cbCsrExtendedUsageCritical.Location = new System.Drawing.Point(6, 179);
            this.cbCsrExtendedUsageCritical.Name = "cbCsrExtendedUsageCritical";
            this.cbCsrExtendedUsageCritical.Size = new System.Drawing.Size(139, 17);
            this.cbCsrExtendedUsageCritical.TabIndex = 9;
            this.cbCsrExtendedUsageCritical.Text = "This Extension is Critical";
            this.cbCsrExtendedUsageCritical.UseVisualStyleBackColor = true;
            // 
            // cbCsrExtendedUsage
            // 
            this.cbCsrExtendedUsage.FormattingEnabled = true;
            this.cbCsrExtendedUsage.Items.AddRange(new object[] {
            "SSL/TLS Web Server Authentication",
            "SSL/TLS Web Client Authentication",
            "Code signing",
            "E-mail Protection (S/MIME)",
            "Trusted Timestamping",
            "OCSP Signing",
            "ipsec Internet Key Exchange",
            "Microsoft Individual Code Signing (authenticode)",
            "Microsoft Commercial Code Signing (authenticode)",
            "Microsoft Trust List Signing",
            "Microsoft Encrypted File System"});
            this.cbCsrExtendedUsage.Location = new System.Drawing.Point(6, 19);
            this.cbCsrExtendedUsage.Name = "cbCsrExtendedUsage";
            this.cbCsrExtendedUsage.Size = new System.Drawing.Size(242, 154);
            this.cbCsrExtendedUsage.TabIndex = 8;
            this.cbCsrExtendedUsage.Click += new System.EventHandler(this.cbCsrExtendedUsage_Click);
            // 
            // btnCsrProcess
            // 
            this.btnCsrProcess.Location = new System.Drawing.Point(377, 547);
            this.btnCsrProcess.Name = "btnCsrProcess";
            this.btnCsrProcess.Size = new System.Drawing.Size(75, 23);
            this.btnCsrProcess.TabIndex = 7;
            this.btnCsrProcess.Text = "Process";
            this.btnCsrProcess.UseVisualStyleBackColor = true;
            this.btnCsrProcess.Click += new System.EventHandler(this.btnCsrProcess_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbCsrCertUsageCritical);
            this.groupBox4.Controls.Add(this.cbCsrUsage);
            this.groupBox4.Location = new System.Drawing.Point(377, 310);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(254, 203);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Certificate Usage";
            // 
            // cbCsrCertUsageCritical
            // 
            this.cbCsrCertUsageCritical.AutoSize = true;
            this.cbCsrCertUsageCritical.Checked = true;
            this.cbCsrCertUsageCritical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCsrCertUsageCritical.Location = new System.Drawing.Point(6, 179);
            this.cbCsrCertUsageCritical.Name = "cbCsrCertUsageCritical";
            this.cbCsrCertUsageCritical.Size = new System.Drawing.Size(139, 17);
            this.cbCsrCertUsageCritical.TabIndex = 8;
            this.cbCsrCertUsageCritical.Text = "This Extension is Critical";
            this.cbCsrCertUsageCritical.UseVisualStyleBackColor = true;
            // 
            // cbCsrUsage
            // 
            this.cbCsrUsage.FormattingEnabled = true;
            this.cbCsrUsage.Items.AddRange(new object[] {
            "Digital Signature",
            "Non Repudiation",
            "Key Encipherment",
            "Data Encipherment",
            "Key Agreement",
            "Key Certificate Sign",
            "CRL Sign",
            "Encipher Only",
            "Decipher Only"});
            this.cbCsrUsage.Location = new System.Drawing.Point(6, 19);
            this.cbCsrUsage.Name = "cbCsrUsage";
            this.cbCsrUsage.Size = new System.Drawing.Size(242, 154);
            this.cbCsrUsage.TabIndex = 7;
            this.cbCsrUsage.Click += new System.EventHandler(this.cbCsrExtendedUsage_Click);
            // 
            // gbKeyBits
            // 
            this.gbKeyBits.Controls.Add(this.cbCsKeyBits);
            this.gbKeyBits.Location = new System.Drawing.Point(377, 178);
            this.gbKeyBits.Name = "gbKeyBits";
            this.gbKeyBits.Size = new System.Drawing.Size(254, 52);
            this.gbKeyBits.TabIndex = 5;
            this.gbKeyBits.TabStop = false;
            this.gbKeyBits.Text = "Key Parameters";
            // 
            // cbCsKeyBits
            // 
            this.cbCsKeyBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCsKeyBits.FormattingEnabled = true;
            this.cbCsKeyBits.Items.AddRange(new object[] {
            "1024",
            "2048",
            "4096"});
            this.cbCsKeyBits.Location = new System.Drawing.Point(6, 19);
            this.cbCsKeyBits.Name = "cbCsKeyBits";
            this.cbCsKeyBits.Size = new System.Drawing.Size(242, 21);
            this.cbCsKeyBits.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdCsrKeyEcdsa);
            this.groupBox3.Controls.Add(this.rdCsrKeyDsa);
            this.groupBox3.Controls.Add(this.rdCsrKeyRsa);
            this.groupBox3.Location = new System.Drawing.Point(377, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(254, 90);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Key Type";
            // 
            // rdCsrKeyEcdsa
            // 
            this.rdCsrKeyEcdsa.AutoSize = true;
            this.rdCsrKeyEcdsa.Location = new System.Drawing.Point(6, 65);
            this.rdCsrKeyEcdsa.Name = "rdCsrKeyEcdsa";
            this.rdCsrKeyEcdsa.Size = new System.Drawing.Size(61, 17);
            this.rdCsrKeyEcdsa.TabIndex = 0;
            this.rdCsrKeyEcdsa.Text = "ECDSA";
            this.rdCsrKeyEcdsa.UseVisualStyleBackColor = true;
            this.rdCsrKeyEcdsa.CheckedChanged += new System.EventHandler(this.rdCsrKeyDsa_CheckedChanged);
            // 
            // rdCsrKeyDsa
            // 
            this.rdCsrKeyDsa.AutoSize = true;
            this.rdCsrKeyDsa.Location = new System.Drawing.Point(6, 42);
            this.rdCsrKeyDsa.Name = "rdCsrKeyDsa";
            this.rdCsrKeyDsa.Size = new System.Drawing.Size(77, 17);
            this.rdCsrKeyDsa.TabIndex = 0;
            this.rdCsrKeyDsa.Text = "DSA (slow)";
            this.rdCsrKeyDsa.UseVisualStyleBackColor = true;
            this.rdCsrKeyDsa.CheckedChanged += new System.EventHandler(this.rdCsrKeyDsa_CheckedChanged);
            // 
            // rdCsrKeyRsa
            // 
            this.rdCsrKeyRsa.AutoSize = true;
            this.rdCsrKeyRsa.Checked = true;
            this.rdCsrKeyRsa.Location = new System.Drawing.Point(6, 19);
            this.rdCsrKeyRsa.Name = "rdCsrKeyRsa";
            this.rdCsrKeyRsa.Size = new System.Drawing.Size(47, 17);
            this.rdCsrKeyRsa.TabIndex = 0;
            this.rdCsrKeyRsa.TabStop = true;
            this.rdCsrKeyRsa.Text = "RSA";
            this.rdCsrKeyRsa.UseVisualStyleBackColor = true;
            this.rdCsrKeyRsa.CheckedChanged += new System.EventHandler(this.rdCsrKeyDsa_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdCsrGenerateNewKey);
            this.groupBox2.Controls.Add(this.rdCsrUseExistingKey);
            this.groupBox2.Location = new System.Drawing.Point(377, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 70);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Overwrite Keys";
            // 
            // rdCsrGenerateNewKey
            // 
            this.rdCsrGenerateNewKey.AutoSize = true;
            this.rdCsrGenerateNewKey.Checked = true;
            this.rdCsrGenerateNewKey.Location = new System.Drawing.Point(6, 37);
            this.rdCsrGenerateNewKey.Name = "rdCsrGenerateNewKey";
            this.rdCsrGenerateNewKey.Size = new System.Drawing.Size(146, 17);
            this.rdCsrGenerateNewKey.TabIndex = 1;
            this.rdCsrGenerateNewKey.TabStop = true;
            this.rdCsrGenerateNewKey.Text = "Always generate new key";
            this.rdCsrGenerateNewKey.UseVisualStyleBackColor = true;
            // 
            // rdCsrUseExistingKey
            // 
            this.rdCsrUseExistingKey.AutoSize = true;
            this.rdCsrUseExistingKey.Location = new System.Drawing.Point(6, 19);
            this.rdCsrUseExistingKey.Name = "rdCsrUseExistingKey";
            this.rdCsrUseExistingKey.Size = new System.Drawing.Size(139, 17);
            this.rdCsrUseExistingKey.TabIndex = 1;
            this.rdCsrUseExistingKey.Text = "Use existing key if exists";
            this.rdCsrUseExistingKey.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCsrSubjEmail);
            this.groupBox1.Controls.Add(this.txtCsrSubjL);
            this.groupBox1.Controls.Add(this.txtCsrSubjST);
            this.groupBox1.Controls.Add(this.txtCsrSubjC);
            this.groupBox1.Controls.Add(this.txtCsrSubjOU);
            this.groupBox1.Controls.Add(this.txtCsrSubjO);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(637, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 182);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Subject";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Email";
            // 
            // txtCsrSubjEmail
            // 
            this.txtCsrSubjEmail.Location = new System.Drawing.Point(110, 149);
            this.txtCsrSubjEmail.MaxLength = 255;
            this.txtCsrSubjEmail.Name = "txtCsrSubjEmail";
            this.txtCsrSubjEmail.Size = new System.Drawing.Size(138, 20);
            this.txtCsrSubjEmail.TabIndex = 11;
            // 
            // txtCsrSubjL
            // 
            this.txtCsrSubjL.Location = new System.Drawing.Point(110, 123);
            this.txtCsrSubjL.MaxLength = 128;
            this.txtCsrSubjL.Name = "txtCsrSubjL";
            this.txtCsrSubjL.Size = new System.Drawing.Size(138, 20);
            this.txtCsrSubjL.TabIndex = 10;
            // 
            // txtCsrSubjST
            // 
            this.txtCsrSubjST.Location = new System.Drawing.Point(110, 97);
            this.txtCsrSubjST.MaxLength = 16;
            this.txtCsrSubjST.Name = "txtCsrSubjST";
            this.txtCsrSubjST.Size = new System.Drawing.Size(138, 20);
            this.txtCsrSubjST.TabIndex = 9;
            // 
            // txtCsrSubjC
            // 
            this.txtCsrSubjC.Location = new System.Drawing.Point(110, 71);
            this.txtCsrSubjC.MaxLength = 2;
            this.txtCsrSubjC.Name = "txtCsrSubjC";
            this.txtCsrSubjC.Size = new System.Drawing.Size(138, 20);
            this.txtCsrSubjC.TabIndex = 8;
            // 
            // txtCsrSubjOU
            // 
            this.txtCsrSubjOU.Location = new System.Drawing.Point(110, 45);
            this.txtCsrSubjOU.MaxLength = 32;
            this.txtCsrSubjOU.Name = "txtCsrSubjOU";
            this.txtCsrSubjOU.Size = new System.Drawing.Size(138, 20);
            this.txtCsrSubjOU.TabIndex = 7;
            // 
            // txtCsrSubjO
            // 
            this.txtCsrSubjO.Location = new System.Drawing.Point(110, 19);
            this.txtCsrSubjO.MaxLength = 64;
            this.txtCsrSubjO.Name = "txtCsrSubjO";
            this.txtCsrSubjO.Size = new System.Drawing.Size(138, 20);
            this.txtCsrSubjO.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "State";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Organizational Unit";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Locality";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Country";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Organization";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 57);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter domains to generate CSRs for in this box.\r\n\r\nYou may specify multiple domai" +
    "ns seperated by commas. The first domain will be used as the common name and the" +
    " rest as alternate names.";
            // 
            // txtCsrDomains
            // 
            this.txtCsrDomains.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCsrDomains.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCsrDomains.Location = new System.Drawing.Point(6, 63);
            this.txtCsrDomains.Multiline = true;
            this.txtCsrDomains.Name = "txtCsrDomains";
            this.txtCsrDomains.Size = new System.Drawing.Size(365, 507);
            this.txtCsrDomains.TabIndex = 0;
            this.txtCsrDomains.Text = "forexample.com, www.forexample.com";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 653);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWorkingDir);
            this.Name = "MainForm";
            this.Text = "Crypto Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tpCsrs.ResumeLayout(false);
            this.tpCsrs.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbKeyBits.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWorkingDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ListView lstFiles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tpCsrs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdCsrGenerateNewKey;
        private System.Windows.Forms.RadioButton rdCsrUseExistingKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCsrDomains;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbCsKeyBits;
        private System.Windows.Forms.RadioButton rdCsrKeyEcdsa;
        private System.Windows.Forms.RadioButton rdCsrKeyDsa;
        private System.Windows.Forms.RadioButton rdCsrKeyRsa;
        private System.Windows.Forms.GroupBox gbKeyBits;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox cbCsrExtendedUsage;
        private System.Windows.Forms.CheckedListBox cbCsrUsage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCsrSubjEmail;
        private System.Windows.Forms.TextBox txtCsrSubjL;
        private System.Windows.Forms.TextBox txtCsrSubjST;
        private System.Windows.Forms.TextBox txtCsrSubjC;
        private System.Windows.Forms.TextBox txtCsrSubjOU;
        private System.Windows.Forms.TextBox txtCsrSubjO;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCsrProcess;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtCsrBasicConstraintPathLength;
        private System.Windows.Forms.Label lblCsrBasicConstraintPathLength;
        private System.Windows.Forms.RadioButton rbCsrBasicConstraintCA;
        private System.Windows.Forms.RadioButton rbCsrBasicConstraintEndEntity;
        private System.Windows.Forms.RadioButton rbCsrBasicConstraintExclude;
        private System.Windows.Forms.CheckBox cbCsrBasicConstraintCritical;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbCsrExtendedUsageCritical;
        private System.Windows.Forms.CheckBox cbCsrCertUsageCritical;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox cbCsrAltNamesInclude;
        private System.Windows.Forms.CheckBox cbCsrAltNamesCritical;
    }
}


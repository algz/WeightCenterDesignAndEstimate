namespace WeightCenterDesignAndEstimateSoft.Task
{
    partial class CoreEnvelopeDesignForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("重心包线节点");
            this.dgvParaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvParaEmpty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvParaRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvParaType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvParaUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvParaValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridViewParaInput = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanelParaImport = new System.Windows.Forms.FlowLayoutPanel();
            this.btnImportFromXml = new System.Windows.Forms.Button();
            this.btnImportFromIde = new System.Windows.Forms.Button();
            this.btnExportToXml = new System.Windows.Forms.Button();
            this.btnCompute = new System.Windows.Forms.Button();
            this.flowLayoutPanelParaExport = new System.Windows.Forms.FlowLayoutPanel();
            this.btnWriteParaToTDE = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbWeightMethod = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWeightEstName = new System.Windows.Forms.TextBox();
            this.treeViewClass = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParaInput)).BeginInit();
            this.flowLayoutPanelParaImport.SuspendLayout();
            this.flowLayoutPanelParaExport.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvParaName
            // 
            this.dgvParaName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvParaName.HeaderText = "参数名";
            this.dgvParaName.MinimumWidth = 60;
            this.dgvParaName.Name = "dgvParaName";
            this.dgvParaName.ReadOnly = true;
            this.dgvParaName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvParaName.Width = 120;
            // 
            // dgvParaEmpty
            // 
            this.dgvParaEmpty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvParaEmpty.HeaderText = "";
            this.dgvParaEmpty.Name = "dgvParaEmpty";
            this.dgvParaEmpty.ReadOnly = true;
            this.dgvParaEmpty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvParaRemark
            // 
            this.dgvParaRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvParaRemark.HeaderText = "备注";
            this.dgvParaRemark.MinimumWidth = 20;
            this.dgvParaRemark.Name = "dgvParaRemark";
            this.dgvParaRemark.ReadOnly = true;
            this.dgvParaRemark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvParaRemark.Width = 190;
            // 
            // dgvParaType
            // 
            this.dgvParaType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvParaType.HeaderText = "参数类型";
            this.dgvParaType.MinimumWidth = 40;
            this.dgvParaType.Name = "dgvParaType";
            this.dgvParaType.ReadOnly = true;
            this.dgvParaType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvParaUnit
            // 
            this.dgvParaUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvParaUnit.HeaderText = "参数单位";
            this.dgvParaUnit.MinimumWidth = 60;
            this.dgvParaUnit.Name = "dgvParaUnit";
            this.dgvParaUnit.ReadOnly = true;
            this.dgvParaUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvParaUnit.Width = 80;
            // 
            // dgvParaValue
            // 
            this.dgvParaValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgvParaValue.HeaderText = "参数值";
            this.dgvParaValue.MinimumWidth = 60;
            this.dgvParaValue.Name = "dgvParaValue";
            this.dgvParaValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvParaValue.Width = 80;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(527, 616);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消(&A)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dataGridViewParaInput
            // 
            this.dataGridViewParaInput.AllowUserToAddRows = false;
            this.dataGridViewParaInput.AllowUserToDeleteRows = false;
            this.dataGridViewParaInput.AllowUserToResizeRows = false;
            this.dataGridViewParaInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewParaInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParaInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvParaName,
            this.dgvParaValue,
            this.dgvParaUnit,
            this.dgvParaType,
            this.dgvParaRemark,
            this.dgvParaEmpty});
            this.dataGridViewParaInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewParaInput.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewParaInput.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewParaInput.Name = "dataGridViewParaInput";
            this.dataGridViewParaInput.RowHeadersWidth = 24;
            this.dataGridViewParaInput.RowTemplate.Height = 23;
            this.dataGridViewParaInput.Size = new System.Drawing.Size(591, 526);
            this.dataGridViewParaInput.TabIndex = 0;
            this.dataGridViewParaInput.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewParaInput_CellEndEdit);
            this.dataGridViewParaInput.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewParaInput_DataError);
            // 
            // flowLayoutPanelParaImport
            // 
            this.flowLayoutPanelParaImport.AutoSize = true;
            this.flowLayoutPanelParaImport.Controls.Add(this.btnImportFromXml);
            this.flowLayoutPanelParaImport.Controls.Add(this.btnImportFromIde);
            this.flowLayoutPanelParaImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelParaImport.Enabled = false;
            this.flowLayoutPanelParaImport.Location = new System.Drawing.Point(0, 584);
            this.flowLayoutPanelParaImport.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelParaImport.Name = "flowLayoutPanelParaImport";
            this.flowLayoutPanelParaImport.Size = new System.Drawing.Size(485, 29);
            this.flowLayoutPanelParaImport.TabIndex = 13;
            // 
            // btnImportFromXml
            // 
            this.btnImportFromXml.Location = new System.Drawing.Point(3, 3);
            this.btnImportFromXml.Name = "btnImportFromXml";
            this.btnImportFromXml.Size = new System.Drawing.Size(155, 23);
            this.btnImportFromXml.TabIndex = 0;
            this.btnImportFromXml.Text = "从数据文件导入参数值(I)";
            this.btnImportFromXml.UseVisualStyleBackColor = true;
            this.btnImportFromXml.Click += new System.EventHandler(this.btnImportFromXml_Click);
            // 
            // btnImportFromIde
            // 
            this.btnImportFromIde.Location = new System.Drawing.Point(164, 3);
            this.btnImportFromIde.Name = "btnImportFromIde";
            this.btnImportFromIde.Size = new System.Drawing.Size(144, 23);
            this.btnImportFromIde.TabIndex = 0;
            this.btnImportFromIde.Text = "从TDE导入参数值(P)";
            this.btnImportFromIde.UseVisualStyleBackColor = true;
            this.btnImportFromIde.Click += new System.EventHandler(this.btnImportFromIde_Click);
            // 
            // btnExportToXml
            // 
            this.btnExportToXml.Location = new System.Drawing.Point(3, 3);
            this.btnExportToXml.Name = "btnExportToXml";
            this.btnExportToXml.Size = new System.Drawing.Size(155, 23);
            this.btnExportToXml.TabIndex = 0;
            this.btnExportToXml.Text = "导出参数值到数据文件(E)";
            this.btnExportToXml.UseVisualStyleBackColor = true;
            this.btnExportToXml.Click += new System.EventHandler(this.btnExportToXml_Click);
            // 
            // btnCompute
            // 
            this.btnCompute.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCompute.Enabled = false;
            this.btnCompute.Location = new System.Drawing.Point(527, 587);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 12;
            this.btnCompute.Text = "计算(C)";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // flowLayoutPanelParaExport
            // 
            this.flowLayoutPanelParaExport.AutoSize = true;
            this.flowLayoutPanelParaExport.Controls.Add(this.btnExportToXml);
            this.flowLayoutPanelParaExport.Controls.Add(this.btnWriteParaToTDE);
            this.flowLayoutPanelParaExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelParaExport.Enabled = false;
            this.flowLayoutPanelParaExport.Location = new System.Drawing.Point(0, 613);
            this.flowLayoutPanelParaExport.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelParaExport.Name = "flowLayoutPanelParaExport";
            this.flowLayoutPanelParaExport.Size = new System.Drawing.Size(485, 29);
            this.flowLayoutPanelParaExport.TabIndex = 14;
            // 
            // btnWriteParaToTDE
            // 
            this.btnWriteParaToTDE.Location = new System.Drawing.Point(164, 3);
            this.btnWriteParaToTDE.Name = "btnWriteParaToTDE";
            this.btnWriteParaToTDE.Size = new System.Drawing.Size(144, 23);
            this.btnWriteParaToTDE.TabIndex = 1;
            this.btnWriteParaToTDE.Text = "将参数写入TDE(W)";
            this.btnWriteParaToTDE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnWriteParaToTDE.UseMnemonic = false;
            this.btnWriteParaToTDE.UseVisualStyleBackColor = true;
            this.btnWriteParaToTDE.Click += new System.EventHandler(this.btnWriteParaToTDE_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewParaInput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(591, 526);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "所有参数";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设计数据名称：";
            // 
            // cmbWeightMethod
            // 
            this.cmbWeightMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbWeightMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeightMethod.FormattingEnabled = true;
            this.cmbWeightMethod.Location = new System.Drawing.Point(122, 55);
            this.cmbWeightMethod.Name = "cmbWeightMethod";
            this.cmbWeightMethod.Size = new System.Drawing.Size(215, 20);
            this.cmbWeightMethod.TabIndex = 5;
            this.cmbWeightMethod.SelectedIndexChanged += new System.EventHandler(this.cmbWeightMethod_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "重心包线算法选择：";
            // 
            // txtWeightEstName
            // 
            this.txtWeightEstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWeightEstName.Location = new System.Drawing.Point(122, 3);
            this.txtWeightEstName.MaxLength = 50;
            this.txtWeightEstName.Name = "txtWeightEstName";
            this.txtWeightEstName.Size = new System.Drawing.Size(215, 21);
            this.txtWeightEstName.TabIndex = 3;
            // 
            // treeViewClass
            // 
            this.treeViewClass.BackColor = System.Drawing.SystemColors.Control;
            this.treeViewClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewClass.Location = new System.Drawing.Point(122, 81);
            this.treeViewClass.Name = "treeViewClass";
            treeNode1.Name = "Root";
            treeNode1.Text = "重心包线节点";
            this.treeViewClass.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewClass.Size = new System.Drawing.Size(215, 558);
            this.treeViewClass.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(949, 642);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbWeightMethod, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtWeightEstName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeViewClass, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(340, 642);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "重心包线节点:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanelParaImport, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnCompute, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanelParaExport, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(605, 642);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "参数输入：";
            // 
            // tabControl1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 29);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(599, 552);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // CoreEnvelopeDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 642);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "CoreEnvelopeDesignForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重心包线设计";
            this.Load += new System.EventHandler(this.CoreEnvelopeDesignForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CoreEnvelopeDesignForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParaInput)).EndInit();
            this.flowLayoutPanelParaImport.ResumeLayout(false);
            this.flowLayoutPanelParaExport.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParaEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParaRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParaType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParaUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvParaValue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dataGridViewParaInput;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelParaImport;
        private System.Windows.Forms.Button btnImportFromXml;
        private System.Windows.Forms.Button btnImportFromIde;
        private System.Windows.Forms.Button btnExportToXml;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelParaExport;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbWeightMethod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWeightEstName;
        private System.Windows.Forms.TreeView treeViewClass;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnWriteParaToTDE;

    }
}
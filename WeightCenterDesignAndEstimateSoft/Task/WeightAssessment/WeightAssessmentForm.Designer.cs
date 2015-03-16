namespace WeightCenterDesignAndEstimateSoft.Task
{
    partial class WeightAssessmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeightAssessmentForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基准重量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FromCurrentWeightDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FromCurrentAdjustmentDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FromModelWeightDBMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FromDesignWeightDBMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportDatumFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportDatumFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AssessWeightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportWDMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportAssessFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExprotAssessFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPreferences = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.weightDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.resultNameTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weightDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基准重量ToolStripMenuItem,
            this.AssessWeightMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(389, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "基准重量";
            // 
            // 基准重量ToolStripMenuItem
            // 
            this.基准重量ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FromCurrentWeightDataMenuItem,
            this.FromCurrentAdjustmentDataMenuItem,
            this.toolStripSeparator1,
            this.FromModelWeightDBMenuItem,
            this.FromDesignWeightDBMenuItem,
            this.toolStripSeparator2,
            this.ImportDatumFileMenuItem,
            this.ExportDatumFileMenuItem});
            this.基准重量ToolStripMenuItem.Name = "基准重量ToolStripMenuItem";
            this.基准重量ToolStripMenuItem.Size = new System.Drawing.Size(108, 21);
            this.基准重量ToolStripMenuItem.Text = "基准重量数据(&B)";
            // 
            // FromCurrentWeightDataMenuItem
            // 
            this.FromCurrentWeightDataMenuItem.Name = "FromCurrentWeightDataMenuItem";
            this.FromCurrentWeightDataMenuItem.Size = new System.Drawing.Size(240, 22);
            this.FromCurrentWeightDataMenuItem.Text = "从当前重量设计数据导入(&D)";
            this.FromCurrentWeightDataMenuItem.Click += new System.EventHandler(this.FromCurrentWeightDataMenuItem_Click);
            // 
            // FromCurrentAdjustmentDataMenuItem
            // 
            this.FromCurrentAdjustmentDataMenuItem.Name = "FromCurrentAdjustmentDataMenuItem";
            this.FromCurrentAdjustmentDataMenuItem.Size = new System.Drawing.Size(240, 22);
            this.FromCurrentAdjustmentDataMenuItem.Text = "从当前重量调整数据导入(&J)";
            this.FromCurrentAdjustmentDataMenuItem.Click += new System.EventHandler(this.FromCurrentAdjustmentDataMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
            // 
            // FromModelWeightDBMenuItem
            // 
            this.FromModelWeightDBMenuItem.Name = "FromModelWeightDBMenuItem";
            this.FromModelWeightDBMenuItem.Size = new System.Drawing.Size(240, 22);
            this.FromModelWeightDBMenuItem.Text = "从数据库型号重量数据导入(&M)";
            this.FromModelWeightDBMenuItem.Click += new System.EventHandler(this.FromModelWeightDBMenuItem_Click);
            // 
            // FromDesignWeightDBMenuItem
            // 
            this.FromDesignWeightDBMenuItem.Name = "FromDesignWeightDBMenuItem";
            this.FromDesignWeightDBMenuItem.Size = new System.Drawing.Size(240, 22);
            this.FromDesignWeightDBMenuItem.Text = "从数据库设计重量数据导入(&I)";
            this.FromDesignWeightDBMenuItem.Click += new System.EventHandler(this.FromDesignWeightDBMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(237, 6);
            // 
            // ImportDatumFileMenuItem
            // 
            this.ImportDatumFileMenuItem.Name = "ImportDatumFileMenuItem";
            this.ImportDatumFileMenuItem.Size = new System.Drawing.Size(240, 22);
            this.ImportDatumFileMenuItem.Tag = "1";
            this.ImportDatumFileMenuItem.Text = "从数据文件导入(&F)";
            this.ImportDatumFileMenuItem.Click += new System.EventHandler(this.ImportWeightDataFileMenuItem_Click);
            // 
            // ExportDatumFileMenuItem
            // 
            this.ExportDatumFileMenuItem.Enabled = false;
            this.ExportDatumFileMenuItem.Name = "ExportDatumFileMenuItem";
            this.ExportDatumFileMenuItem.Size = new System.Drawing.Size(240, 22);
            this.ExportDatumFileMenuItem.Tag = "1";
            this.ExportDatumFileMenuItem.Text = "导出至数据文件(&E)";
            this.ExportDatumFileMenuItem.Click += new System.EventHandler(this.ExportDatumFileMenuItem_Click);
            // 
            // AssessWeightMenuItem
            // 
            this.AssessWeightMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportWDMMenuItem,
            this.ImportAssessFileMenuItem,
            this.ExprotAssessFileMenuItem});
            this.AssessWeightMenuItem.Enabled = false;
            this.AssessWeightMenuItem.Name = "AssessWeightMenuItem";
            this.AssessWeightMenuItem.Size = new System.Drawing.Size(107, 21);
            this.AssessWeightMenuItem.Text = "评估重量数据(&E)";
            // 
            // ImportWDMMenuItem
            // 
            this.ImportWDMMenuItem.Name = "ImportWDMMenuItem";
            this.ImportWDMMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ImportWDMMenuItem.Text = "从WDM系统导入(&I)";
            this.ImportWDMMenuItem.Click += new System.EventHandler(this.ImportWDMMenuItem_Click);
            // 
            // ImportAssessFileMenuItem
            // 
            this.ImportAssessFileMenuItem.Name = "ImportAssessFileMenuItem";
            this.ImportAssessFileMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ImportAssessFileMenuItem.Tag = "2";
            this.ImportAssessFileMenuItem.Text = "从数据文件导入(&F)";
            this.ImportAssessFileMenuItem.Click += new System.EventHandler(this.ImportWeightDataFileMenuItem_Click);
            // 
            // ExprotAssessFileMenuItem
            // 
            this.ExprotAssessFileMenuItem.Enabled = false;
            this.ExprotAssessFileMenuItem.Name = "ExprotAssessFileMenuItem";
            this.ExprotAssessFileMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ExprotAssessFileMenuItem.Tag = "2";
            this.ExprotAssessFileMenuItem.Text = "导出至数据文件(&O)";
            this.ExprotAssessFileMenuItem.Click += new System.EventHandler(this.ExportDatumFileMenuItem_Click);
            // 
            // btnPreferences
            // 
            this.btnPreferences.Enabled = false;
            this.btnPreferences.Location = new System.Drawing.Point(3, 3);
            this.btnPreferences.Name = "btnPreferences";
            this.btnPreferences.Size = new System.Drawing.Size(108, 23);
            this.btnPreferences.TabIndex = 2;
            this.btnPreferences.Text = "评估参数设置(&S)";
            this.btnPreferences.UseVisualStyleBackColor = true;
            this.btnPreferences.Click += new System.EventHandler(this.btnPreferences_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(223, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确认(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(304, 3);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 4;
            this.btnCancle.Text = "取消(&A)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.405941F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.59406F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(389, 404);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.weightDataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 328);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重量数据表";
            // 
            // weightDataGridView
            // 
            this.weightDataGridView.AllowUserToAddRows = false;
            this.weightDataGridView.AllowUserToDeleteRows = false;
            this.weightDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.weightDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.weightDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weightDataGridView.Location = new System.Drawing.Point(3, 17);
            this.weightDataGridView.Name = "weightDataGridView";
            this.weightDataGridView.ReadOnly = true;
            this.weightDataGridView.RowHeadersVisible = false;
            this.weightDataGridView.RowTemplate.Height = 23;
            this.weightDataGridView.Size = new System.Drawing.Size(377, 308);
            this.weightDataGridView.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "节点名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "基准重量数据";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "评估重量数据";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.resultNameTxt);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 28);
            this.panel1.TabIndex = 3;
            // 
            // resultNameTxt
            // 
            this.resultNameTxt.Location = new System.Drawing.Point(92, 7);
            this.resultNameTxt.Name = "resultNameTxt";
            this.resultNameTxt.Size = new System.Drawing.Size(100, 21);
            this.resultNameTxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "评估结果名称:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.0897F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.9103F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.Controls.Add(this.btnPreferences, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancle, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnConfirm, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 371);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(383, 30);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // WeightAssessmentForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size(389, 429);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WeightAssessmentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "重量评估对话框";
            this.Load += new System.EventHandler(this.WeightAssessmentForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.weightDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基准重量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FromCurrentWeightDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FromCurrentAdjustmentDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FromModelWeightDBMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FromDesignWeightDBMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportDatumFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportDatumFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AssessWeightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportWDMMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportAssessFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExprotAssessFileMenuItem;
        private System.Windows.Forms.Button btnPreferences;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView weightDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox resultNameTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
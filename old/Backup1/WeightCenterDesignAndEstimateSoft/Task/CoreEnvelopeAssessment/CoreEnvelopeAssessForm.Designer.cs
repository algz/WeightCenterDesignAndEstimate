namespace WeightCenterDesignAndEstimateSoft.Task.CoreEnvelopeAssessment
{
    partial class CoreEnvelopeAssessForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基准重心包线数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FromCurrentCoreDesignMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FromCurrentCoreCutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.fromCoreDBMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportCoreDatumFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportCoreDatumMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CoreAssessMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportCoreWDMMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportAssessDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportCoreAssessMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCoreAssessConfig = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.datumCoreGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.assessCoreGridView = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtResultName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datumCoreGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.assessCoreGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基准重心包线数据ToolStripMenuItem,
            this.CoreAssessMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(718, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 基准重心包线数据ToolStripMenuItem
            // 
            this.基准重心包线数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FromCurrentCoreDesignMenuItem,
            this.FromCurrentCoreCutMenuItem,
            this.toolStripMenuItem1,
            this.fromCoreDBMenuItem,
            this.toolStripMenuItem2,
            this.ImportCoreDatumFileMenuItem,
            this.ExportCoreDatumMenuItem});
            this.基准重心包线数据ToolStripMenuItem.Name = "基准重心包线数据ToolStripMenuItem";
            this.基准重心包线数据ToolStripMenuItem.Size = new System.Drawing.Size(132, 21);
            this.基准重心包线数据ToolStripMenuItem.Text = "基准重心包线数据(&B)";
            // 
            // FromCurrentCoreDesignMenuItem
            // 
            this.FromCurrentCoreDesignMenuItem.Name = "FromCurrentCoreDesignMenuItem";
            this.FromCurrentCoreDesignMenuItem.Size = new System.Drawing.Size(260, 22);
            this.FromCurrentCoreDesignMenuItem.Tag = "0";
            this.FromCurrentCoreDesignMenuItem.Text = "从当前重心包线设计数据导入(&D)";
            this.FromCurrentCoreDesignMenuItem.Click += new System.EventHandler(this.FromCurrentCoreDesignMenuItem_Click);
            // 
            // FromCurrentCoreCutMenuItem
            // 
            this.FromCurrentCoreCutMenuItem.Name = "FromCurrentCoreCutMenuItem";
            this.FromCurrentCoreCutMenuItem.Size = new System.Drawing.Size(259, 22);
            this.FromCurrentCoreCutMenuItem.Tag = "1";
            this.FromCurrentCoreCutMenuItem.Text = "从当前重心包线剪裁数据导入(&I)";
            this.FromCurrentCoreCutMenuItem.Click += new System.EventHandler(this.FromCurrentCoreDesignMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(241, 6);
            // 
            // fromCoreDBMenuItem
            // 
            this.fromCoreDBMenuItem.Name = "fromCoreDBMenuItem";
            this.fromCoreDBMenuItem.Size = new System.Drawing.Size(264, 22);
            this.fromCoreDBMenuItem.Tag = "2";
            this.fromCoreDBMenuItem.Text = "从数据库设计重心包线数据导入(&M)";
            this.fromCoreDBMenuItem.Click += new System.EventHandler(this.FromCurrentCoreDesignMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(241, 6);
            // 
            // ImportCoreDatumFileMenuItem
            // 
            this.ImportCoreDatumFileMenuItem.Name = "ImportCoreDatumFileMenuItem";
            this.ImportCoreDatumFileMenuItem.Size = new System.Drawing.Size(260, 22);
            this.ImportCoreDatumFileMenuItem.Tag = "0";
            this.ImportCoreDatumFileMenuItem.Text = "从数据文件导入(&F)";
            this.ImportCoreDatumFileMenuItem.Click += new System.EventHandler(this.ImportCoreFileMenuItem_Click);
            // 
            // ExportCoreDatumMenuItem
            // 
            this.ExportCoreDatumMenuItem.Enabled = false;
            this.ExportCoreDatumMenuItem.Name = "ExportCoreDatumMenuItem";
            this.ExportCoreDatumMenuItem.Size = new System.Drawing.Size(259, 22);
            this.ExportCoreDatumMenuItem.Tag = "0";
            this.ExportCoreDatumMenuItem.Text = "导出至数据文件(&O)";
            this.ExportCoreDatumMenuItem.Click += new System.EventHandler(this.ExportCoreMenuItem_Click);
            // 
            // CoreAssessMenuItem
            // 
            this.CoreAssessMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportCoreWDMMenuItem,
            this.ImportAssessDataMenuItem,
            this.ExportCoreAssessMenuItem});
            this.CoreAssessMenuItem.Enabled = false;
            this.CoreAssessMenuItem.Name = "CoreAssessMenuItem";
            this.CoreAssessMenuItem.Size = new System.Drawing.Size(131, 21);
            this.CoreAssessMenuItem.Text = "评估重心包线数据(&E)";
            // 
            // ImportCoreWDMMenuItem
            // 
            this.ImportCoreWDMMenuItem.Name = "ImportCoreWDMMenuItem";
            this.ImportCoreWDMMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ImportCoreWDMMenuItem.Text = "从WDM系统导入(I)";
            // 
            // ImportAssessDataMenuItem
            // 
            this.ImportAssessDataMenuItem.Name = "ImportAssessDataMenuItem";
            this.ImportAssessDataMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ImportAssessDataMenuItem.Tag = "1";
            this.ImportAssessDataMenuItem.Text = "从数据文件导入(&F)";
            this.ImportAssessDataMenuItem.Click += new System.EventHandler(this.ImportCoreFileMenuItem_Click);
            // 
            // ExportCoreAssessMenuItem
            // 
            this.ExportCoreAssessMenuItem.Enabled = false;
            this.ExportCoreAssessMenuItem.Name = "ExportCoreAssessMenuItem";
            this.ExportCoreAssessMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ExportCoreAssessMenuItem.Tag = "1";
            this.ExportCoreAssessMenuItem.Text = "导出至数据文件(&O)";
            this.ExportCoreAssessMenuItem.Click += new System.EventHandler(this.ExportCoreMenuItem_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.04269F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.95731F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel2.Controls.Add(this.btnCoreAssessConfig, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnConfirm, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancle, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 383);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(712, 29);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // btnCoreAssessConfig
            // 
            this.btnCoreAssessConfig.Location = new System.Drawing.Point(3, 3);
            this.btnCoreAssessConfig.Name = "btnCoreAssessConfig";
            this.btnCoreAssessConfig.Size = new System.Drawing.Size(103, 23);
            this.btnCoreAssessConfig.TabIndex = 0;
            this.btnCoreAssessConfig.Text = "评估参数设置(&S)";
            this.btnCoreAssessConfig.UseVisualStyleBackColor = true;
            this.btnCoreAssessConfig.Click += new System.EventHandler(this.btnCoreAssessConfig_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(535, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确认(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(621, 3);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消(&A)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(712, 341);
            this.splitContainer1.SplitterDistance = 304;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.datumCoreGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 341);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基准重心包线数据";
            // 
            // datumCoreGridView
            // 
            this.datumCoreGridView.AllowUserToAddRows = false;
            this.datumCoreGridView.AllowUserToDeleteRows = false;
            this.datumCoreGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datumCoreGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.xCoordinates,
            this.yCoordinates});
            this.datumCoreGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datumCoreGridView.Location = new System.Drawing.Point(3, 17);
            this.datumCoreGridView.Name = "datumCoreGridView";
            this.datumCoreGridView.ReadOnly = true;
            this.datumCoreGridView.RowHeadersVisible = false;
            this.datumCoreGridView.RowTemplate.Height = 23;
            this.datumCoreGridView.Size = new System.Drawing.Size(298, 321);
            this.datumCoreGridView.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "节点名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // xCoordinates
            // 
            this.xCoordinates.HeaderText = "横坐标";
            this.xCoordinates.Name = "xCoordinates";
            this.xCoordinates.ReadOnly = true;
            // 
            // yCoordinates
            // 
            this.yCoordinates.HeaderText = "纵坐标";
            this.yCoordinates.Name = "yCoordinates";
            this.yCoordinates.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.assessCoreGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 341);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "评估重心数据";
            // 
            // assessCoreGridView
            // 
            this.assessCoreGridView.AllowUserToAddRows = false;
            this.assessCoreGridView.AllowUserToDeleteRows = false;
            this.assessCoreGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.assessCoreGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.assessCoreGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assessCoreGridView.Location = new System.Drawing.Point(3, 17);
            this.assessCoreGridView.Name = "assessCoreGridView";
            this.assessCoreGridView.ReadOnly = true;
            this.assessCoreGridView.RowHeadersVisible = false;
            this.assessCoreGridView.RowTemplate.Height = 23;
            this.assessCoreGridView.Size = new System.Drawing.Size(398, 321);
            this.assessCoreGridView.TabIndex = 0;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "节点名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "横坐标";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "纵标坐";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "是否评估";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.684211F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.31579F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(718, 415);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtResultName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 27);
            this.panel1.TabIndex = 6;
            // 
            // txtResultName
            // 
            this.txtResultName.Location = new System.Drawing.Point(92, 3);
            this.txtResultName.Name = "txtResultName";
            this.txtResultName.Size = new System.Drawing.Size(100, 21);
            this.txtResultName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "评估数据名称:";
            // 
            // CoreEnvelopeAssessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 440);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CoreEnvelopeAssessForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "重心包线评估对话框";
            this.Load += new System.EventHandler(this.CoreEnvelopeAssessForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datumCoreGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.assessCoreGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基准重心包线数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FromCurrentCoreDesignMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FromCurrentCoreCutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fromCoreDBMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ImportCoreDatumFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportCoreDatumMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CoreAssessMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportCoreWDMMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportCoreAssessMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnCoreAssessConfig;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView datumCoreGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn xCoordinates;
        private System.Windows.Forms.DataGridViewTextBoxColumn yCoordinates;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView assessCoreGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtResultName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem ImportAssessDataMenuItem;
    }
}
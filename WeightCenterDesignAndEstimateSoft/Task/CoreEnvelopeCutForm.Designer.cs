namespace WeightCenterDesignAndEstimateSoft.Task
{
    partial class CoreEnvelopeCutForm
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControlCoreEnvelope = new ZedGraph.ZedGraphControl();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.原始重心包线数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从当前设计数据导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从当前剪裁数据导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从数据库导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从数据文件导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.查看修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.导出至数据文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.离散ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.离散设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出离散点至数据文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪裁ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪裁方式ToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.从文件导入燃油数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从文件导入评估数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.手动点选包线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCutName = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.menuStripMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedGraphControlCoreEnvelope
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.zedGraphControlCoreEnvelope, 3);
            this.zedGraphControlCoreEnvelope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControlCoreEnvelope.EditButtons = System.Windows.Forms.MouseButtons.None;
            this.zedGraphControlCoreEnvelope.EditModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraphControlCoreEnvelope.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zedGraphControlCoreEnvelope.IsShowLegend = false;
            this.zedGraphControlCoreEnvelope.LinkButtons = System.Windows.Forms.MouseButtons.None;
            this.zedGraphControlCoreEnvelope.LinkModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraphControlCoreEnvelope.Location = new System.Drawing.Point(3, 28);
            this.zedGraphControlCoreEnvelope.Name = "zedGraphControlCoreEnvelope";
            this.zedGraphControlCoreEnvelope.ScrollGrace = 0D;
            this.zedGraphControlCoreEnvelope.ScrollMaxX = 0D;
            this.zedGraphControlCoreEnvelope.ScrollMaxY = 0D;
            this.zedGraphControlCoreEnvelope.ScrollMaxY2 = 0D;
            this.zedGraphControlCoreEnvelope.ScrollMinX = 0D;
            this.zedGraphControlCoreEnvelope.ScrollMinY = 0D;
            this.zedGraphControlCoreEnvelope.ScrollMinY2 = 0D;
            this.zedGraphControlCoreEnvelope.SelectButtons = System.Windows.Forms.MouseButtons.None;
            this.zedGraphControlCoreEnvelope.SelectModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraphControlCoreEnvelope.Size = new System.Drawing.Size(788, 568);
            this.zedGraphControlCoreEnvelope.TabIndex = 2;
            this.zedGraphControlCoreEnvelope.ZoomButtons = System.Windows.Forms.MouseButtons.None;
            this.zedGraphControlCoreEnvelope.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zedGraphControlCoreEnvelope_MouseDownEvent);
            this.zedGraphControlCoreEnvelope.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zedGraphControlCoreEnvelope_MouseMoveEvent);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.原始重心包线数据ToolStripMenuItem,
            this.离散ToolStripMenuItem,
            this.剪裁ToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(794, 25);
            this.menuStripMain.TabIndex = 13;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // 原始重心包线数据ToolStripMenuItem
            // 
            this.原始重心包线数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.从当前设计数据导入ToolStripMenuItem,
            this.从当前剪裁数据导入ToolStripMenuItem,
            this.从数据库导入ToolStripMenuItem,
            this.从数据文件导入ToolStripMenuItem,
            this.toolStripSeparator1,
            this.查看修改ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.导出至数据文件ToolStripMenuItem,
            this.toolStripMenuItem3});
            this.原始重心包线数据ToolStripMenuItem.Name = "原始重心包线数据ToolStripMenuItem";
            this.原始重心包线数据ToolStripMenuItem.Size = new System.Drawing.Size(134, 21);
            this.原始重心包线数据ToolStripMenuItem.Text = "原始重心包线数据(&O)";
            // 
            // 从当前设计数据导入ToolStripMenuItem
            // 
            this.从当前设计数据导入ToolStripMenuItem.Name = "从当前设计数据导入ToolStripMenuItem";
            this.从当前设计数据导入ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.从当前设计数据导入ToolStripMenuItem.Text = "从当前设计数据导入(&D)";
            this.从当前设计数据导入ToolStripMenuItem.Click += new System.EventHandler(this.buttonImportFromDesignData_Click);
            // 
            // 从当前剪裁数据导入ToolStripMenuItem
            // 
            this.从当前剪裁数据导入ToolStripMenuItem.Name = "从当前剪裁数据导入ToolStripMenuItem";
            this.从当前剪裁数据导入ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.从当前剪裁数据导入ToolStripMenuItem.Text = "从当前剪裁数据导入(&C)";
            this.从当前剪裁数据导入ToolStripMenuItem.Click += new System.EventHandler(this.buttonImportFromCutData_Click);
            // 
            // 从数据库导入ToolStripMenuItem
            // 
            this.从数据库导入ToolStripMenuItem.Name = "从数据库导入ToolStripMenuItem";
            this.从数据库导入ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.从数据库导入ToolStripMenuItem.Text = "从数据库导入(&I)";
            this.从数据库导入ToolStripMenuItem.Click += new System.EventHandler(this.buttonImportFromDatabase_Click);
            // 
            // 从数据文件导入ToolStripMenuItem
            // 
            this.从数据文件导入ToolStripMenuItem.Name = "从数据文件导入ToolStripMenuItem";
            this.从数据文件导入ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.从数据文件导入ToolStripMenuItem.Text = "从数据文件导入(&F)";
            this.从数据文件导入ToolStripMenuItem.Click += new System.EventHandler(this.buttonImportFromFile);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // 查看修改ToolStripMenuItem
            // 
            this.查看修改ToolStripMenuItem.Name = "查看修改ToolStripMenuItem";
            this.查看修改ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.查看修改ToolStripMenuItem.Text = "查看/修改(&S)";
            this.查看修改ToolStripMenuItem.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 6);
            // 
            // 导出至数据文件ToolStripMenuItem
            // 
            this.导出至数据文件ToolStripMenuItem.Name = "导出至数据文件ToolStripMenuItem";
            this.导出至数据文件ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.导出至数据文件ToolStripMenuItem.Text = "导出至数据文件(&E)";
            this.导出至数据文件ToolStripMenuItem.Click += new System.EventHandler(this.buttonExportToFile_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(198, 6);
            // 
            // 离散ToolStripMenuItem
            // 
            this.离散ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.离散设置ToolStripMenuItem,
            this.导出离散点至数据文件ToolStripMenuItem});
            this.离散ToolStripMenuItem.Name = "离散ToolStripMenuItem";
            this.离散ToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.离散ToolStripMenuItem.Text = "离散(&D)";
            // 
            // 离散设置ToolStripMenuItem
            // 
            this.离散设置ToolStripMenuItem.Name = "离散设置ToolStripMenuItem";
            this.离散设置ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.离散设置ToolStripMenuItem.Text = "离散设置(&S)";
            this.离散设置ToolStripMenuItem.Click += new System.EventHandler(this.buttonDiscreteSet_Click);
            // 
            // 导出离散点至数据文件ToolStripMenuItem
            // 
            this.导出离散点至数据文件ToolStripMenuItem.Name = "导出离散点至数据文件ToolStripMenuItem";
            this.导出离散点至数据文件ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.导出离散点至数据文件ToolStripMenuItem.Text = "导出离散点至数据文件(&E)";
            this.导出离散点至数据文件ToolStripMenuItem.Click += new System.EventHandler(this.导出离散点至数据文件ToolStripMenuItem_Click);
            // 
            // 剪裁ToolStripMenuItem
            // 
            this.剪裁ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.剪裁方式ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.从文件导入燃油数据ToolStripMenuItem,
            this.从文件导入评估数据ToolStripMenuItem,
            this.查看ToolStripMenuItem,
            this.toolStripMenuItem5,
            this.手动点选包线ToolStripMenuItem});
            this.剪裁ToolStripMenuItem.Enabled = false;
            this.剪裁ToolStripMenuItem.Name = "剪裁ToolStripMenuItem";
            this.剪裁ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.剪裁ToolStripMenuItem.Text = "剪裁(&C)";
            // 
            // 剪裁方式ToolStripMenuItem
            // 
            this.剪裁方式ToolStripMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.剪裁方式ToolStripMenuItem.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.剪裁方式ToolStripMenuItem.Items.AddRange(new object[] {
            "燃油特性剪裁",
            "飞行品质剪裁",
            "飞行载荷剪裁"});
            this.剪裁方式ToolStripMenuItem.Name = "剪裁方式ToolStripMenuItem";
            this.剪裁方式ToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.剪裁方式ToolStripMenuItem.SelectedIndexChanged += new System.EventHandler(this.剪裁方式ToolStripMenuItem_SelectedIndexChanged);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(209, 6);
            // 
            // 从文件导入燃油数据ToolStripMenuItem
            // 
            this.从文件导入燃油数据ToolStripMenuItem.Name = "从文件导入燃油数据ToolStripMenuItem";
            this.从文件导入燃油数据ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.从文件导入燃油数据ToolStripMenuItem.Text = "从文件导入燃油数据(&F)";
            this.从文件导入燃油数据ToolStripMenuItem.Click += new System.EventHandler(this.buttonImportFuelFromFile);
            // 
            // 从文件导入评估数据ToolStripMenuItem
            // 
            this.从文件导入评估数据ToolStripMenuItem.Name = "从文件导入评估数据ToolStripMenuItem";
            this.从文件导入评估数据ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.从文件导入评估数据ToolStripMenuItem.Text = "从文件导入评估数据(&E)";
            this.从文件导入评估数据ToolStripMenuItem.Click += new System.EventHandler(this.从文件导入评估数据ToolStripMenuItem_Click);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.查看ToolStripMenuItem.Text = "查看/修改(&M)";
            this.查看ToolStripMenuItem.Click += new System.EventHandler(this.buttonCutDataModify_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(209, 6);
            // 
            // 手动点选包线ToolStripMenuItem
            // 
            this.手动点选包线ToolStripMenuItem.Name = "手动点选包线ToolStripMenuItem";
            this.手动点选包线ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.手动点选包线ToolStripMenuItem.Text = "图形选取包线(&S)";
            this.手动点选包线ToolStripMenuItem.Click += new System.EventHandler(this.手动点选包线ToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControlCoreEnvelope, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCutName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConfirm, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCancle, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(794, 632);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "设计数据名称:";
            // 
            // txtCutName
            // 
            this.txtCutName.Location = new System.Drawing.Point(96, 3);
            this.txtCutName.Name = "txtCutName";
            this.txtCutName.Size = new System.Drawing.Size(214, 21);
            this.txtCutName.TabIndex = 4;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfirm.Location = new System.Drawing.Point(635, 602);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 27);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "保存(&S)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancle.Location = new System.Drawing.Point(716, 602);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 27);
            this.btnCancle.TabIndex = 6;
            this.btnCancle.Text = "取消(&B)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // CoreEnvelopeCutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 657);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.MinimizeBox = false;
            this.Name = "CoreEnvelopeCutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重心包线剪裁";
            this.Load += new System.EventHandler(this.CoreEnvelopeCutForm_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControlCoreEnvelope;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem 原始重心包线数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪裁ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从当前设计数据导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从当前剪裁数据导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从数据库导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从数据文件导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 导出至数据文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 查看修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripComboBox 剪裁方式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从文件导入燃油数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem 手动点选包线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 从文件导入评估数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 离散ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 离散设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出离散点至数据文件ToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCutName;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
    }
}
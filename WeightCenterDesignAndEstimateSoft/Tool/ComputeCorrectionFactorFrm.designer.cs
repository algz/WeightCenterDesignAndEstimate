namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class ComputeCorrectionFactorFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputeCorrectionFactorFrm));
            this.menuStripDialog = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemWeightData1 = new System.Windows.Forms.ToolStripMenuItem();
            this.memu1FromCurrentWeighDesignImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFormAdjustWeightImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.memu1FromTypeDBImport = new System.Windows.Forms.ToolStripMenuItem();
            this.memu1FromWeightDesignDBImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.memu1FromDataFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.memu1ExportToDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemWeightData2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMemuWeihtData2FormWeightDataImport = new System.Windows.Forms.ToolStripMenuItem();
            this.memu2CurrentAdjustmentImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.memu2FromDBTypeDataImport = new System.Windows.Forms.ToolStripMenuItem();
            this.memu2FromDBWeightDataImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.memu2FromDataFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.memu2ExportToDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.修正因子ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMemuCalculateJHRatio = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMemuCalculateTechRatio = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMemuExportRatio = new System.Windows.Forms.ToolStripMenuItem();
            this.gridWeightData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gruSort = new System.Windows.Forms.GroupBox();
            this.treeViewSort = new System.Windows.Forms.TreeView();
            this.gruDataList = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gruBarPic = new System.Windows.Forms.GroupBox();
            this.zedGraphControlPic = new ZedGraph.ZedGraphControl();
            this.btnCalculateTechRatio = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnCalculateJHRatio = new System.Windows.Forms.Button();
            this.menuStripDialog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWeightData)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.gruSort.SuspendLayout();
            this.gruDataList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gruBarPic.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripDialog
            // 
            this.menuStripDialog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemWeightData1,
            this.ToolStripMenuItemWeightData2,
            this.修正因子ToolStripMenuItem});
            this.menuStripDialog.Location = new System.Drawing.Point(0, 0);
            this.menuStripDialog.Name = "menuStripDialog";
            this.menuStripDialog.Size = new System.Drawing.Size(988, 25);
            this.menuStripDialog.TabIndex = 2;
            this.menuStripDialog.Text = "menuStrip1";
            // 
            // ToolStripMenuItemWeightData1
            // 
            this.ToolStripMenuItemWeightData1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memu1FromCurrentWeighDesignImport,
            this.toolFormAdjustWeightImport,
            this.toolStripSeparator1,
            this.memu1FromTypeDBImport,
            this.memu1FromWeightDesignDBImport,
            this.toolStripSeparator2,
            this.memu1FromDataFileImport,
            this.memu1ExportToDataFile});
            this.ToolStripMenuItemWeightData1.Name = "ToolStripMenuItemWeightData1";
            this.ToolStripMenuItemWeightData1.Size = new System.Drawing.Size(75, 21);
            this.ToolStripMenuItemWeightData1.Text = "重量数据&1";
            // 
            // memu1FromCurrentWeighDesignImport
            // 
            this.memu1FromCurrentWeighDesignImport.Name = "memu1FromCurrentWeighDesignImport";
            this.memu1FromCurrentWeighDesignImport.Size = new System.Drawing.Size(240, 22);
            this.memu1FromCurrentWeighDesignImport.Text = "从当前重量设计数据导入(&D)";
            this.memu1FromCurrentWeighDesignImport.Click += new System.EventHandler(this.memu1FromCurrentWeighDesignImport_Click);
            // 
            // toolFormAdjustWeightImport
            // 
            this.toolFormAdjustWeightImport.Name = "toolFormAdjustWeightImport";
            this.toolFormAdjustWeightImport.Size = new System.Drawing.Size(240, 22);
            this.toolFormAdjustWeightImport.Text = "从当前重量调整数据导入(&A)";
            this.toolFormAdjustWeightImport.Click += new System.EventHandler(this.toolFormAdjustWeightImport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
            // 
            // memu1FromTypeDBImport
            // 
            this.memu1FromTypeDBImport.Name = "memu1FromTypeDBImport";
            this.memu1FromTypeDBImport.Size = new System.Drawing.Size(240, 22);
            this.memu1FromTypeDBImport.Text = "从数据库型号重量数据导入(&M)";
            this.memu1FromTypeDBImport.Click += new System.EventHandler(this.memu1FromTypeDBImport_Click);
            // 
            // memu1FromWeightDesignDBImport
            // 
            this.memu1FromWeightDesignDBImport.Name = "memu1FromWeightDesignDBImport";
            this.memu1FromWeightDesignDBImport.Size = new System.Drawing.Size(240, 22);
            this.memu1FromWeightDesignDBImport.Text = "从数据库重量设计数据导入(&I)";
            this.memu1FromWeightDesignDBImport.Click += new System.EventHandler(this.memu1FromWeightDesignDBImport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(237, 6);
            // 
            // memu1FromDataFileImport
            // 
            this.memu1FromDataFileImport.Name = "memu1FromDataFileImport";
            this.memu1FromDataFileImport.Size = new System.Drawing.Size(240, 22);
            this.memu1FromDataFileImport.Text = "从数据文件导入(&F)";
            this.memu1FromDataFileImport.Click += new System.EventHandler(this.memu1FromDataFileImport_Click);
            // 
            // memu1ExportToDataFile
            // 
            this.memu1ExportToDataFile.Name = "memu1ExportToDataFile";
            this.memu1ExportToDataFile.Size = new System.Drawing.Size(240, 22);
            this.memu1ExportToDataFile.Text = "导出至数据文件(&E)";
            this.memu1ExportToDataFile.Click += new System.EventHandler(this.memu1ExportToDataFile_Click);
            // 
            // ToolStripMenuItemWeightData2
            // 
            this.ToolStripMenuItemWeightData2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMemuWeihtData2FormWeightDataImport,
            this.memu2CurrentAdjustmentImport,
            this.toolStripSeparator3,
            this.memu2FromDBTypeDataImport,
            this.memu2FromDBWeightDataImport,
            this.toolStripSeparator4,
            this.memu2FromDataFileImport,
            this.memu2ExportToDataFile});
            this.ToolStripMenuItemWeightData2.Name = "ToolStripMenuItemWeightData2";
            this.ToolStripMenuItemWeightData2.Size = new System.Drawing.Size(75, 21);
            this.ToolStripMenuItemWeightData2.Text = "重量数据&2";
            // 
            // toolMemuWeihtData2FormWeightDataImport
            // 
            this.toolMemuWeihtData2FormWeightDataImport.Name = "toolMemuWeihtData2FormWeightDataImport";
            this.toolMemuWeihtData2FormWeightDataImport.Size = new System.Drawing.Size(240, 22);
            this.toolMemuWeihtData2FormWeightDataImport.Text = "从当前重量设计数据导入(&D)";
            this.toolMemuWeihtData2FormWeightDataImport.Click += new System.EventHandler(this.toolMemuWeihtData2FormWeightDataImport_Click);
            // 
            // memu2CurrentAdjustmentImport
            // 
            this.memu2CurrentAdjustmentImport.Name = "memu2CurrentAdjustmentImport";
            this.memu2CurrentAdjustmentImport.Size = new System.Drawing.Size(240, 22);
            this.memu2CurrentAdjustmentImport.Text = "从当前重量调整数据导入(&A)";
            this.memu2CurrentAdjustmentImport.Click += new System.EventHandler(this.memu2CurrentAdjustmentImport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(237, 6);
            // 
            // memu2FromDBTypeDataImport
            // 
            this.memu2FromDBTypeDataImport.Name = "memu2FromDBTypeDataImport";
            this.memu2FromDBTypeDataImport.Size = new System.Drawing.Size(240, 22);
            this.memu2FromDBTypeDataImport.Text = "从数据库型号重量数据导入(&M)";
            this.memu2FromDBTypeDataImport.Click += new System.EventHandler(this.memu2FromDBTypeDataImport_Click);
            // 
            // memu2FromDBWeightDataImport
            // 
            this.memu2FromDBWeightDataImport.Name = "memu2FromDBWeightDataImport";
            this.memu2FromDBWeightDataImport.Size = new System.Drawing.Size(240, 22);
            this.memu2FromDBWeightDataImport.Text = "从数据库重量设计数据导入(&I)";
            this.memu2FromDBWeightDataImport.Click += new System.EventHandler(this.memu2FromDBWeightDataImport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(237, 6);
            // 
            // memu2FromDataFileImport
            // 
            this.memu2FromDataFileImport.Name = "memu2FromDataFileImport";
            this.memu2FromDataFileImport.Size = new System.Drawing.Size(240, 22);
            this.memu2FromDataFileImport.Text = "从数据文件导入(&F)";
            this.memu2FromDataFileImport.Click += new System.EventHandler(this.memu2FromDataFileImport_Click);
            // 
            // memu2ExportToDataFile
            // 
            this.memu2ExportToDataFile.Name = "memu2ExportToDataFile";
            this.memu2ExportToDataFile.Size = new System.Drawing.Size(240, 22);
            this.memu2ExportToDataFile.Text = "导出至数据文件(&E)";
            this.memu2ExportToDataFile.Click += new System.EventHandler(this.memu2ExportToDataFile_Click);
            // 
            // 修正因子ToolStripMenuItem
            // 
            this.修正因子ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMemuCalculateJHRatio,
            this.toolMemuCalculateTechRatio,
            this.toolMemuExportRatio});
            this.修正因子ToolStripMenuItem.Name = "修正因子ToolStripMenuItem";
            this.修正因子ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.修正因子ToolStripMenuItem.Text = "修正因子(&R)";
            // 
            // toolMemuCalculateJHRatio
            // 
            this.toolMemuCalculateJHRatio.Name = "toolMemuCalculateJHRatio";
            this.toolMemuCalculateJHRatio.Size = new System.Drawing.Size(164, 22);
            this.toolMemuCalculateJHRatio.Text = "计算校核因子(&A)";
            this.toolMemuCalculateJHRatio.Click += new System.EventHandler(this.toolMemuCalculateJHRatio_Click);
            // 
            // toolMemuCalculateTechRatio
            // 
            this.toolMemuCalculateTechRatio.Name = "toolMemuCalculateTechRatio";
            this.toolMemuCalculateTechRatio.Size = new System.Drawing.Size(164, 22);
            this.toolMemuCalculateTechRatio.Text = "计算技术因子(&T)";
            this.toolMemuCalculateTechRatio.Click += new System.EventHandler(this.toolMemuCalculateTechRatio_Click);
            // 
            // toolMemuExportRatio
            // 
            this.toolMemuExportRatio.Name = "toolMemuExportRatio";
            this.toolMemuExportRatio.Size = new System.Drawing.Size(164, 22);
            this.toolMemuExportRatio.Text = "导出修正因子(&E)";
            this.toolMemuExportRatio.Click += new System.EventHandler(this.toolMemuExportRatio_Click);
            // 
            // gridWeightData
            // 
            this.gridWeightData.AllowUserToAddRows = false;
            this.gridWeightData.AllowUserToDeleteRows = false;
            this.gridWeightData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridWeightData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWeightData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.gridWeightData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridWeightData.Location = new System.Drawing.Point(3, 17);
            this.gridWeightData.Name = "gridWeightData";
            this.gridWeightData.RowTemplate.Height = 23;
            this.gridWeightData.Size = new System.Drawing.Size(355, 434);
            this.gridWeightData.TabIndex = 3;
            this.gridWeightData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this._CellEndEdit);
            this.gridWeightData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridWeightData_DataError);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "重量名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "重量数据1";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "重量数据2";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "因子";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(988, 454);
            this.splitContainer1.SplitterDistance = 552;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gruSort);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gruDataList);
            this.splitContainer2.Size = new System.Drawing.Size(552, 454);
            this.splitContainer2.SplitterDistance = 187;
            this.splitContainer2.TabIndex = 0;
            // 
            // gruSort
            // 
            this.gruSort.Controls.Add(this.treeViewSort);
            this.gruSort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruSort.Location = new System.Drawing.Point(0, 0);
            this.gruSort.Name = "gruSort";
            this.gruSort.Size = new System.Drawing.Size(187, 454);
            this.gruSort.TabIndex = 0;
            this.gruSort.TabStop = false;
            this.gruSort.Text = "重量分类";
            // 
            // treeViewSort
            // 
            this.treeViewSort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewSort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSort.Enabled = false;
            this.treeViewSort.Location = new System.Drawing.Point(3, 17);
            this.treeViewSort.Name = "treeViewSort";
            this.treeViewSort.Size = new System.Drawing.Size(181, 434);
            this.treeViewSort.TabIndex = 0;
            // 
            // gruDataList
            // 
            this.gruDataList.Controls.Add(this.gridWeightData);
            this.gruDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruDataList.Location = new System.Drawing.Point(0, 0);
            this.gruDataList.Name = "gruDataList";
            this.gruDataList.Size = new System.Drawing.Size(361, 454);
            this.gruDataList.TabIndex = 4;
            this.gruDataList.TabStop = false;
            this.gruDataList.Text = "数据列表";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel1.Controls.Add(this.gruBarPic, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCalculateTechRatio, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnExport, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCancle, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCalculateJHRatio, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 454);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gruBarPic
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gruBarPic, 2);
            this.gruBarPic.Controls.Add(this.zedGraphControlPic);
            this.gruBarPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruBarPic.Location = new System.Drawing.Point(3, 3);
            this.gruBarPic.Name = "gruBarPic";
            this.gruBarPic.Size = new System.Drawing.Size(426, 378);
            this.gruBarPic.TabIndex = 5;
            this.gruBarPic.TabStop = false;
            this.gruBarPic.Text = "修正因子柱状图";
            // 
            // zedGraphControlPic
            // 
            this.zedGraphControlPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControlPic.Location = new System.Drawing.Point(3, 17);
            this.zedGraphControlPic.Name = "zedGraphControlPic";
            this.zedGraphControlPic.ScrollGrace = 0D;
            this.zedGraphControlPic.ScrollMaxX = 0D;
            this.zedGraphControlPic.ScrollMaxY = 0D;
            this.zedGraphControlPic.ScrollMaxY2 = 0D;
            this.zedGraphControlPic.ScrollMinX = 0D;
            this.zedGraphControlPic.ScrollMinY = 0D;
            this.zedGraphControlPic.ScrollMinY2 = 0D;
            this.zedGraphControlPic.Size = new System.Drawing.Size(420, 358);
            this.zedGraphControlPic.TabIndex = 0;
            // 
            // btnCalculateTechRatio
            // 
            this.btnCalculateTechRatio.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCalculateTechRatio.Location = new System.Drawing.Point(325, 390);
            this.btnCalculateTechRatio.Name = "btnCalculateTechRatio";
            this.btnCalculateTechRatio.Size = new System.Drawing.Size(104, 23);
            this.btnCalculateTechRatio.TabIndex = 4;
            this.btnCalculateTechRatio.Text = "计算技术因子(&T)";
            this.btnCalculateTechRatio.UseVisualStyleBackColor = true;
            this.btnCalculateTechRatio.Click += new System.EventHandler(this.btnCalculateTechRatio_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.Location = new System.Drawing.Point(204, 425);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(104, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导出(&E)";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancle.Location = new System.Drawing.Point(325, 425);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(104, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消(&C)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnCalculateJHRatio
            // 
            this.btnCalculateJHRatio.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCalculateJHRatio.Location = new System.Drawing.Point(204, 390);
            this.btnCalculateJHRatio.Name = "btnCalculateJHRatio";
            this.btnCalculateJHRatio.Size = new System.Drawing.Size(104, 23);
            this.btnCalculateJHRatio.TabIndex = 1;
            this.btnCalculateJHRatio.Text = "计算校核因子(&A)";
            this.btnCalculateJHRatio.UseVisualStyleBackColor = true;
            this.btnCalculateJHRatio.Click += new System.EventHandler(this.btnCalculateJHRatio_Click);
            // 
            // ComputeCorrectionFactorFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 479);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStripDialog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripDialog;
            this.Name = "ComputeCorrectionFactorFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修正因子求解";
            this.menuStripDialog.ResumeLayout(false);
            this.menuStripDialog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWeightData)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.gruSort.ResumeLayout(false);
            this.gruDataList.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gruBarPic.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripDialog;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWeightData1;
        private System.Windows.Forms.ToolStripMenuItem memu1FromCurrentWeighDesignImport;
        private System.Windows.Forms.ToolStripMenuItem memu1FromTypeDBImport;
        private System.Windows.Forms.ToolStripMenuItem memu1FromWeightDesignDBImport;
        private System.Windows.Forms.DataGridView gridWeightData;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWeightData2;
        private System.Windows.Forms.ToolStripMenuItem memu2FromDBTypeDataImport;
        private System.Windows.Forms.ToolStripMenuItem memu2CurrentAdjustmentImport;
        private System.Windows.Forms.ToolStripMenuItem memu2FromDBWeightDataImport;
        private System.Windows.Forms.ToolStripMenuItem memu1FromDataFileImport;
        private System.Windows.Forms.ToolStripMenuItem memu1ExportToDataFile;
        private System.Windows.Forms.ToolStripMenuItem memu2FromDataFileImport;
        private System.Windows.Forms.ToolStripMenuItem memu2ExportToDataFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 修正因子ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolMemuCalculateJHRatio;
        private System.Windows.Forms.ToolStripMenuItem toolMemuExportRatio;
        private System.Windows.Forms.ToolStripMenuItem toolMemuCalculateTechRatio;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeViewSort;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ZedGraph.ZedGraphControl zedGraphControlPic;
        private System.Windows.Forms.Button btnCalculateJHRatio;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCalculateTechRatio;
        private System.Windows.Forms.GroupBox gruBarPic;
        private System.Windows.Forms.GroupBox gruDataList;
        private System.Windows.Forms.GroupBox gruSort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ToolStripMenuItem toolFormAdjustWeightImport;
        private System.Windows.Forms.ToolStripMenuItem toolMemuWeihtData2FormWeightDataImport;
    }
}
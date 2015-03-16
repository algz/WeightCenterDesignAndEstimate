namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class CoreEnvelopeDesignManageForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gruOperSel = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnJYNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtHelicopterName = new System.Windows.Forms.TextBox();
            this.labHelicopterName = new System.Windows.Forms.Label();
            this.labRemark = new System.Windows.Forms.Label();
            this.txtDesignTakingWeight = new System.Windows.Forms.TextBox();
            this.labDesignTakingWeight = new System.Windows.Forms.Label();
            this.gruWeightData = new System.Windows.Forms.GroupBox();
            this.btnCancleModify = new System.Windows.Forms.Button();
            this.btnConfimModify = new System.Windows.Forms.Button();
            this.btnEditCoreEnvelope = new System.Windows.Forms.Button();
            this.gruDesignResult = new System.Windows.Forms.GroupBox();
            this.gruCoreEnvelope = new System.Windows.Forms.GroupBox();
            this.treeCoreEnvelope = new System.Windows.Forms.TreeView();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtSubmitter = new System.Windows.Forms.TextBox();
            this.labSubmitter = new System.Windows.Forms.Label();
            this.txtLastModifyTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDesignDataName = new System.Windows.Forms.TextBox();
            this.labDesignDataName = new System.Windows.Forms.Label();
            this.gruTypeList = new System.Windows.Forms.GroupBox();
            this.treeViewList = new System.Windows.Forms.TreeView();
            this.btnConfirmModify = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gruEnvelopeData = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.gridCoreEnvelope = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExportCoreData = new System.Windows.Forms.Button();
            this.btnImportCoreData = new System.Windows.Forms.Button();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStripCoreImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCoreImage = new System.Windows.Forms.ToolStripMenuItem();
            this.zedGraphControlCore = new ZedGraph.ZedGraphControl();
            this.gruOperSel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gruDesignResult.SuspendLayout();
            this.gruCoreEnvelope.SuspendLayout();
            this.gruTypeList.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gruEnvelopeData.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCoreEnvelope)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.contextMenuStripCoreImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruOperSel
            // 
            this.gruOperSel.Controls.Add(this.tableLayoutPanel2);
            this.gruOperSel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruOperSel.Location = new System.Drawing.Point(3, 465);
            this.gruOperSel.Name = "gruOperSel";
            this.gruOperSel.Size = new System.Drawing.Size(192, 150);
            this.gruOperSel.TabIndex = 28;
            this.gruOperSel.TabStop = false;
            this.gruOperSel.Text = "操作选择";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnRefresh, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnExport, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnNew, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnEdit, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnJYNew, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnDelete, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnImport, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(186, 130);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(96, 67);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "刷新(&R)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(3, 99);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(85, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "导出(&O)";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(85, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "新建(&N)";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(96, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "编辑(&E)";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnJYNew
            // 
            this.btnJYNew.Location = new System.Drawing.Point(3, 35);
            this.btnJYNew.Name = "btnJYNew";
            this.btnJYNew.Size = new System.Drawing.Size(85, 23);
            this.btnJYNew.TabIndex = 2;
            this.btnJYNew.Text = "基于新建(&A)";
            this.btnJYNew.UseVisualStyleBackColor = true;
            this.btnJYNew.Click += new System.EventHandler(this.btnJYNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(96, 35);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(3, 67);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(85, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "导入(&I)";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtHelicopterName
            // 
            this.txtHelicopterName.Location = new System.Drawing.Point(144, 34);
            this.txtHelicopterName.MaxLength = 50;
            this.txtHelicopterName.Name = "txtHelicopterName";
            this.txtHelicopterName.Size = new System.Drawing.Size(177, 21);
            this.txtHelicopterName.TabIndex = 22;
            // 
            // labHelicopterName
            // 
            this.labHelicopterName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labHelicopterName.AutoSize = true;
            this.labHelicopterName.ForeColor = System.Drawing.Color.Red;
            this.labHelicopterName.Location = new System.Drawing.Point(3, 40);
            this.labHelicopterName.Name = "labHelicopterName";
            this.labHelicopterName.Size = new System.Drawing.Size(77, 12);
            this.labHelicopterName.TabIndex = 21;
            this.labHelicopterName.Text = "直升机名称：";
            // 
            // labRemark
            // 
            this.labRemark.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labRemark.AutoSize = true;
            this.labRemark.ForeColor = System.Drawing.Color.Black;
            this.labRemark.Location = new System.Drawing.Point(3, 71);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(41, 12);
            this.labRemark.TabIndex = 12;
            this.labRemark.Text = "备注：";
            // 
            // txtDesignTakingWeight
            // 
            this.txtDesignTakingWeight.Location = new System.Drawing.Point(478, 65);
            this.txtDesignTakingWeight.MaxLength = 50;
            this.txtDesignTakingWeight.Name = "txtDesignTakingWeight";
            this.txtDesignTakingWeight.Size = new System.Drawing.Size(177, 21);
            this.txtDesignTakingWeight.TabIndex = 9;
            // 
            // labDesignTakingWeight
            // 
            this.labDesignTakingWeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDesignTakingWeight.AutoSize = true;
            this.labDesignTakingWeight.ForeColor = System.Drawing.Color.Black;
            this.labDesignTakingWeight.Location = new System.Drawing.Point(327, 71);
            this.labDesignTakingWeight.Name = "labDesignTakingWeight";
            this.labDesignTakingWeight.Size = new System.Drawing.Size(89, 12);
            this.labDesignTakingWeight.TabIndex = 8;
            this.labDesignTakingWeight.Text = "设计起飞重量：";
            // 
            // gruWeightData
            // 
            this.gruWeightData.Location = new System.Drawing.Point(533, 12);
            this.gruWeightData.Name = "gruWeightData";
            this.gruWeightData.Size = new System.Drawing.Size(663, 580);
            this.gruWeightData.TabIndex = 27;
            this.gruWeightData.TabStop = false;
            this.gruWeightData.Text = "重心包线设计数据";
            // 
            // btnCancleModify
            // 
            this.btnCancleModify.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancleModify.Location = new System.Drawing.Point(570, 4);
            this.btnCancleModify.Name = "btnCancleModify";
            this.btnCancleModify.Size = new System.Drawing.Size(84, 23);
            this.btnCancleModify.TabIndex = 25;
            this.btnCancleModify.Text = "取消修改(&B)";
            this.btnCancleModify.UseVisualStyleBackColor = true;
            this.btnCancleModify.Click += new System.EventHandler(this.btnCancleModify_Click);
            // 
            // btnConfimModify
            // 
            this.btnConfimModify.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfimModify.Location = new System.Drawing.Point(481, 4);
            this.btnConfimModify.Name = "btnConfimModify";
            this.btnConfimModify.Size = new System.Drawing.Size(83, 23);
            this.btnConfimModify.TabIndex = 24;
            this.btnConfimModify.Text = "确认修改(&C)";
            this.btnConfimModify.UseVisualStyleBackColor = true;
            this.btnConfimModify.Click += new System.EventHandler(this.btnConfimModify_Click);
            // 
            // btnEditCoreEnvelope
            // 
            this.btnEditCoreEnvelope.Location = new System.Drawing.Point(69, 3);
            this.btnEditCoreEnvelope.Name = "btnEditCoreEnvelope";
            this.btnEditCoreEnvelope.Size = new System.Drawing.Size(104, 23);
            this.btnEditCoreEnvelope.TabIndex = 23;
            this.btnEditCoreEnvelope.Text = "编辑重心包线(&M)";
            this.btnEditCoreEnvelope.UseVisualStyleBackColor = true;
            this.btnEditCoreEnvelope.Click += new System.EventHandler(this.btnEditCoreEnvelope_Click);
            // 
            // gruDesignResult
            // 
            this.gruDesignResult.Controls.Add(this.zedGraphControlCore);
            this.gruDesignResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruDesignResult.Location = new System.Drawing.Point(3, 3);
            this.gruDesignResult.Name = "gruDesignResult";
            this.gruDesignResult.Size = new System.Drawing.Size(469, 300);
            this.gruDesignResult.TabIndex = 20;
            this.gruDesignResult.TabStop = false;
            this.gruDesignResult.Text = "设计结果";
            // 
            // gruCoreEnvelope
            // 
            this.gruCoreEnvelope.Controls.Add(this.treeCoreEnvelope);
            this.gruCoreEnvelope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruCoreEnvelope.Location = new System.Drawing.Point(3, 3);
            this.gruCoreEnvelope.Name = "gruCoreEnvelope";
            this.gruCoreEnvelope.Size = new System.Drawing.Size(176, 257);
            this.gruCoreEnvelope.TabIndex = 19;
            this.gruCoreEnvelope.TabStop = false;
            this.gruCoreEnvelope.Text = "重心包线列表";
            // 
            // treeCoreEnvelope
            // 
            this.treeCoreEnvelope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCoreEnvelope.Location = new System.Drawing.Point(3, 17);
            this.treeCoreEnvelope.Name = "treeCoreEnvelope";
            this.treeCoreEnvelope.Size = new System.Drawing.Size(170, 237);
            this.treeCoreEnvelope.TabIndex = 0;
            // 
            // txtRemark
            // 
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Location = new System.Drawing.Point(144, 65);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.tableLayoutPanel5.SetRowSpan(this.txtRemark, 2);
            this.txtRemark.Size = new System.Drawing.Size(177, 58);
            this.txtRemark.TabIndex = 13;
            // 
            // txtSubmitter
            // 
            this.txtSubmitter.Location = new System.Drawing.Point(478, 34);
            this.txtSubmitter.MaxLength = 50;
            this.txtSubmitter.Name = "txtSubmitter";
            this.txtSubmitter.Size = new System.Drawing.Size(177, 21);
            this.txtSubmitter.TabIndex = 7;
            // 
            // labSubmitter
            // 
            this.labSubmitter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labSubmitter.AutoSize = true;
            this.labSubmitter.ForeColor = System.Drawing.Color.Red;
            this.labSubmitter.Location = new System.Drawing.Point(327, 40);
            this.labSubmitter.Name = "labSubmitter";
            this.labSubmitter.Size = new System.Drawing.Size(101, 12);
            this.labSubmitter.TabIndex = 6;
            this.labSubmitter.Text = "设计数据提交者：";
            // 
            // txtLastModifyTime
            // 
            this.txtLastModifyTime.Location = new System.Drawing.Point(478, 3);
            this.txtLastModifyTime.Name = "txtLastModifyTime";
            this.txtLastModifyTime.ReadOnly = true;
            this.txtLastModifyTime.Size = new System.Drawing.Size(177, 21);
            this.txtLastModifyTime.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(327, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "最后修改时间：";
            // 
            // txtDesignDataName
            // 
            this.txtDesignDataName.Location = new System.Drawing.Point(144, 3);
            this.txtDesignDataName.MaxLength = 50;
            this.txtDesignDataName.Name = "txtDesignDataName";
            this.txtDesignDataName.Size = new System.Drawing.Size(177, 21);
            this.txtDesignDataName.TabIndex = 1;
            // 
            // labDesignDataName
            // 
            this.labDesignDataName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDesignDataName.AutoSize = true;
            this.labDesignDataName.ForeColor = System.Drawing.Color.Red;
            this.labDesignDataName.Location = new System.Drawing.Point(3, 9);
            this.labDesignDataName.Name = "labDesignDataName";
            this.labDesignDataName.Size = new System.Drawing.Size(89, 12);
            this.labDesignDataName.TabIndex = 0;
            this.labDesignDataName.Text = "设计数据名称：";
            // 
            // gruTypeList
            // 
            this.gruTypeList.Controls.Add(this.treeViewList);
            this.gruTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruTypeList.Location = new System.Drawing.Point(3, 3);
            this.gruTypeList.Name = "gruTypeList";
            this.gruTypeList.Size = new System.Drawing.Size(192, 456);
            this.gruTypeList.TabIndex = 26;
            this.gruTypeList.TabStop = false;
            this.gruTypeList.Text = "重心包线设计数据列表";
            // 
            // treeViewList
            // 
            this.treeViewList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewList.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewList.HideSelection = false;
            this.treeViewList.Location = new System.Drawing.Point(3, 17);
            this.treeViewList.Name = "treeViewList";
            this.treeViewList.Size = new System.Drawing.Size(186, 436);
            this.treeViewList.TabIndex = 0;
            this.treeViewList.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeViewList_DrawNode);
            this.treeViewList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewList_AfterSelect);
            this.treeViewList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewList_NodeMouseClick);
            // 
            // btnConfirmModify
            // 
            this.btnConfirmModify.Location = new System.Drawing.Point(730, 556);
            this.btnConfirmModify.Name = "btnConfirmModify";
            this.btnConfirmModify.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmModify.TabIndex = 30;
            this.btnConfirmModify.Text = "确定修改";
            this.btnConfirmModify.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnConfirm.Location = new System.Drawing.Point(590, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 29;
            this.btnConfirm.Text = "确定(&Y)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(883, 618);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.TabIndex = 31;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.gruTypeList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gruOperSel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.91909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.08091F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(198, 618);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.gruEnvelopeData, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.17476F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.825243F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(681, 618);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // gruEnvelopeData
            // 
            this.gruEnvelopeData.Controls.Add(this.tableLayoutPanel4);
            this.gruEnvelopeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruEnvelopeData.Location = new System.Drawing.Point(3, 3);
            this.gruEnvelopeData.Name = "gruEnvelopeData";
            this.gruEnvelopeData.Size = new System.Drawing.Size(675, 576);
            this.gruEnvelopeData.TabIndex = 30;
            this.gruEnvelopeData.TabStop = false;
            this.gruEnvelopeData.Text = "重心包线设计数据";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.85321F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.14679F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(669, 556);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.4178F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.75264F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.92609F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.20513F));
            this.tableLayoutPanel5.Controls.Add(this.labDesignDataName, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtDesignDataName, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.labHelicopterName, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtHelicopterName, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.labSubmitter, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtRemark, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtSubmitter, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.labRemark, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.labDesignTakingWeight, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtLastModifyTime, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtDesignTakingWeight, 3, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(663, 126);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.79487F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.20513F));
            this.tableLayoutPanel6.Controls.Add(this.gruDesignResult, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.gridCoreEnvelope, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel9, 0, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 135);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.41463F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.80488F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.02439F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(663, 418);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // gridCoreEnvelope
            // 
            this.gridCoreEnvelope.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCoreEnvelope.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCoreEnvelope.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel6.SetColumnSpan(this.gridCoreEnvelope, 2);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCoreEnvelope.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridCoreEnvelope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCoreEnvelope.Location = new System.Drawing.Point(3, 309);
            this.gridCoreEnvelope.Name = "gridCoreEnvelope";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCoreEnvelope.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridCoreEnvelope.RowTemplate.Height = 23;
            this.gridCoreEnvelope.Size = new System.Drawing.Size(657, 68);
            this.gridCoreEnvelope.TabIndex = 16;
            this.gridCoreEnvelope.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCoreEnvelope_CellEndEdit);
            this.gridCoreEnvelope.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridCoreEnvelope_DataError);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.gruCoreEnvelope, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(478, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.79661F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.20339F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(182, 300);
            this.tableLayoutPanel7.TabIndex = 26;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel8.Controls.Add(this.btnEditCoreEnvelope, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 266);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(176, 31);
            this.tableLayoutPanel8.TabIndex = 20;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 5;
            this.tableLayoutPanel6.SetColumnSpan(this.tableLayoutPanel9, 2);
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel9.Controls.Add(this.btnConfimModify, 3, 0);
            this.tableLayoutPanel9.Controls.Add(this.btnCancleModify, 4, 0);
            this.tableLayoutPanel9.Controls.Add(this.btnExportCoreData, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.btnImportCoreData, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 383);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(657, 32);
            this.tableLayoutPanel9.TabIndex = 27;
            // 
            // btnExportCoreData
            // 
            this.btnExportCoreData.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExportCoreData.Location = new System.Drawing.Point(366, 4);
            this.btnExportCoreData.Name = "btnExportCoreData";
            this.btnExportCoreData.Size = new System.Drawing.Size(109, 23);
            this.btnExportCoreData.TabIndex = 28;
            this.btnExportCoreData.Text = "重心数据导出(&X)";
            this.btnExportCoreData.UseVisualStyleBackColor = true;
            this.btnExportCoreData.Click += new System.EventHandler(this.btnExportCoreData_Click);
            // 
            // btnImportCoreData
            // 
            this.btnImportCoreData.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnImportCoreData.Location = new System.Drawing.Point(257, 4);
            this.btnImportCoreData.Name = "btnImportCoreData";
            this.btnImportCoreData.Size = new System.Drawing.Size(103, 23);
            this.btnImportCoreData.TabIndex = 29;
            this.btnImportCoreData.Text = "重心数据导入(&W)";
            this.btnImportCoreData.UseVisualStyleBackColor = true;
            this.btnImportCoreData.Click += new System.EventHandler(this.btnImportCoreData_Click);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel10.Controls.Add(this.btnConfirm, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 585);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(675, 30);
            this.tableLayoutPanel10.TabIndex = 31;
            // 
            // contextMenuStripCoreImage
            // 
            this.contextMenuStripCoreImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCoreImage});
            this.contextMenuStripCoreImage.Name = "contextMenuStripCoreImage";
            this.contextMenuStripCoreImage.Size = new System.Drawing.Size(125, 26);
            // 
            // toolStripMenuItemCoreImage
            // 
            this.toolStripMenuItemCoreImage.Name = "toolStripMenuItemCoreImage";
            this.toolStripMenuItemCoreImage.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItemCoreImage.Text = "图片保存";
            // 
            // zedGraphControlCore
            // 
            this.zedGraphControlCore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControlCore.Location = new System.Drawing.Point(3, 17);
            this.zedGraphControlCore.Name = "zedGraphControlCore";
            this.zedGraphControlCore.ScrollGrace = 0;
            this.zedGraphControlCore.ScrollMaxX = 0;
            this.zedGraphControlCore.ScrollMaxY = 0;
            this.zedGraphControlCore.ScrollMaxY2 = 0;
            this.zedGraphControlCore.ScrollMinX = 0;
            this.zedGraphControlCore.ScrollMinY = 0;
            this.zedGraphControlCore.ScrollMinY2 = 0;
            this.zedGraphControlCore.Size = new System.Drawing.Size(463, 280);
            this.zedGraphControlCore.TabIndex = 0;
            // 
            // CoreEnvelopeDesignManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 618);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.gruWeightData);
            this.Controls.Add(this.btnConfirmModify);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "CoreEnvelopeDesignManageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重心包线设计数据管理对话框";
            this.gruOperSel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.gruDesignResult.ResumeLayout(false);
            this.gruCoreEnvelope.ResumeLayout(false);
            this.gruTypeList.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.gruEnvelopeData.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCoreEnvelope)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.contextMenuStripCoreImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gruOperSel;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnJYNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtHelicopterName;
        private System.Windows.Forms.Label labHelicopterName;
        private System.Windows.Forms.Label labRemark;
        private System.Windows.Forms.TextBox txtDesignTakingWeight;
        private System.Windows.Forms.Label labDesignTakingWeight;
        private System.Windows.Forms.GroupBox gruWeightData;
        private System.Windows.Forms.GroupBox gruDesignResult;
        private System.Windows.Forms.GroupBox gruCoreEnvelope;
        private System.Windows.Forms.TreeView treeCoreEnvelope;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtSubmitter;
        private System.Windows.Forms.Label labSubmitter;
        private System.Windows.Forms.TextBox txtLastModifyTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDesignDataName;
        private System.Windows.Forms.Label labDesignDataName;
        private System.Windows.Forms.GroupBox gruTypeList;
        private System.Windows.Forms.TreeView treeViewList;
        private System.Windows.Forms.Button btnConfirmModify;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnEditCoreEnvelope;
        private System.Windows.Forms.Button btnConfimModify;
        private System.Windows.Forms.Button btnCancleModify;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox gruEnvelopeData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.DataGridView gridCoreEnvelope;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Button btnExportCoreData;
        private System.Windows.Forms.Button btnImportCoreData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCoreImage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCoreImage;
        private ZedGraph.ZedGraphControl zedGraphControlCore;
    }
}
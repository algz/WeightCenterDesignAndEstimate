namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class ParameterManageForm
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
            this.gruParameterList = new System.Windows.Forms.GroupBox();
            this.treeViewParameterList = new System.Windows.Forms.TreeView();
            this.gruParameterInfo = new System.Windows.Forms.GroupBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnParameterConfirm = new System.Windows.Forms.Button();
            this.txtParameterRemark = new System.Windows.Forms.TextBox();
            this.labParameterRemark = new System.Windows.Forms.Label();
            this.cmbParameterType = new System.Windows.Forms.ComboBox();
            this.labParameterType = new System.Windows.Forms.Label();
            this.txtParameterName = new System.Windows.Forms.TextBox();
            this.labParameterName = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.labUnit = new System.Windows.Forms.Label();
            this.gruManageOprer = new System.Windows.Forms.GroupBox();
            this.btnJYNew = new System.Windows.Forms.Button();
            this.btnAllExport = new System.Windows.Forms.Button();
            this.btnAllImort = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.cmbFilterSel = new System.Windows.Forms.ComboBox();
            this.labFilterSel = new System.Windows.Forms.Label();
            this.txtSelect = new System.Windows.Forms.TextBox();
            this.labSelect = new System.Windows.Forms.Label();
            this.btnComfirm = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.gruSel = new System.Windows.Forms.GroupBox();
            this.btnPuslishToTde = new System.Windows.Forms.Button();
            this.gruParameterList.SuspendLayout();
            this.gruParameterInfo.SuspendLayout();
            this.gruManageOprer.SuspendLayout();
            this.gruSel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruParameterList
            // 
            this.gruParameterList.Controls.Add(this.treeViewParameterList);
            this.gruParameterList.Location = new System.Drawing.Point(6, 12);
            this.gruParameterList.Name = "gruParameterList";
            this.gruParameterList.Size = new System.Drawing.Size(285, 666);
            this.gruParameterList.TabIndex = 0;
            this.gruParameterList.TabStop = false;
            this.gruParameterList.Text = "参数列表";
            // 
            // treeViewParameterList
            // 
            this.treeViewParameterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewParameterList.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewParameterList.HideSelection = false;
            this.treeViewParameterList.Location = new System.Drawing.Point(3, 17);
            this.treeViewParameterList.Name = "treeViewParameterList";
            this.treeViewParameterList.Size = new System.Drawing.Size(279, 646);
            this.treeViewParameterList.TabIndex = 0;
            this.treeViewParameterList.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeViewParameterList_DrawNode);
            this.treeViewParameterList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewParameterList_AfterSelect);
            this.treeViewParameterList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewParameterList_NodeMouseClick);
            // 
            // gruParameterInfo
            // 
            this.gruParameterInfo.Controls.Add(this.btnCancle);
            this.gruParameterInfo.Controls.Add(this.btnParameterConfirm);
            this.gruParameterInfo.Controls.Add(this.txtParameterRemark);
            this.gruParameterInfo.Controls.Add(this.labParameterRemark);
            this.gruParameterInfo.Controls.Add(this.cmbParameterType);
            this.gruParameterInfo.Controls.Add(this.labParameterType);
            this.gruParameterInfo.Controls.Add(this.txtParameterName);
            this.gruParameterInfo.Controls.Add(this.labParameterName);
            this.gruParameterInfo.Controls.Add(this.txtUnit);
            this.gruParameterInfo.Controls.Add(this.labUnit);
            this.gruParameterInfo.Location = new System.Drawing.Point(297, 12);
            this.gruParameterInfo.Name = "gruParameterInfo";
            this.gruParameterInfo.Size = new System.Drawing.Size(319, 409);
            this.gruParameterInfo.TabIndex = 1;
            this.gruParameterInfo.TabStop = false;
            this.gruParameterInfo.Text = "参数信息";
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(236, 378);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(70, 23);
            this.btnCancle.TabIndex = 9;
            this.btnCancle.Text = "取消(&Q)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnParameterConfirm
            // 
            this.btnParameterConfirm.Location = new System.Drawing.Point(160, 378);
            this.btnParameterConfirm.Name = "btnParameterConfirm";
            this.btnParameterConfirm.Size = new System.Drawing.Size(70, 23);
            this.btnParameterConfirm.TabIndex = 8;
            this.btnParameterConfirm.Text = "保存(&V)";
            this.btnParameterConfirm.UseVisualStyleBackColor = true;
            this.btnParameterConfirm.Click += new System.EventHandler(this.btnParameterConfirm_Click);
            // 
            // txtParameterRemark
            // 
            this.txtParameterRemark.Location = new System.Drawing.Point(106, 131);
            this.txtParameterRemark.Multiline = true;
            this.txtParameterRemark.Name = "txtParameterRemark";
            this.txtParameterRemark.Size = new System.Drawing.Size(200, 237);
            this.txtParameterRemark.TabIndex = 7;
            // 
            // labParameterRemark
            // 
            this.labParameterRemark.AutoSize = true;
            this.labParameterRemark.Location = new System.Drawing.Point(6, 134);
            this.labParameterRemark.Name = "labParameterRemark";
            this.labParameterRemark.Size = new System.Drawing.Size(65, 12);
            this.labParameterRemark.TabIndex = 6;
            this.labParameterRemark.Text = "参数备注：";
            // 
            // cmbParameterType
            // 
            this.cmbParameterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParameterType.FormattingEnabled = true;
            this.cmbParameterType.Location = new System.Drawing.Point(106, 95);
            this.cmbParameterType.Name = "cmbParameterType";
            this.cmbParameterType.Size = new System.Drawing.Size(200, 20);
            this.cmbParameterType.TabIndex = 5;
            // 
            // labParameterType
            // 
            this.labParameterType.AutoSize = true;
            this.labParameterType.Location = new System.Drawing.Point(6, 98);
            this.labParameterType.Name = "labParameterType";
            this.labParameterType.Size = new System.Drawing.Size(65, 12);
            this.labParameterType.TabIndex = 4;
            this.labParameterType.Text = "参数类型：";
            // 
            // txtParameterName
            // 
            this.txtParameterName.Location = new System.Drawing.Point(106, 26);
            this.txtParameterName.MaxLength = 50;
            this.txtParameterName.Name = "txtParameterName";
            this.txtParameterName.Size = new System.Drawing.Size(200, 21);
            this.txtParameterName.TabIndex = 3;
            // 
            // labParameterName
            // 
            this.labParameterName.AutoSize = true;
            this.labParameterName.Location = new System.Drawing.Point(7, 30);
            this.labParameterName.Name = "labParameterName";
            this.labParameterName.Size = new System.Drawing.Size(65, 12);
            this.labParameterName.TabIndex = 2;
            this.labParameterName.Text = "参数名称：";
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(106, 60);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(200, 21);
            this.txtUnit.TabIndex = 1;
            // 
            // labUnit
            // 
            this.labUnit.AutoSize = true;
            this.labUnit.Location = new System.Drawing.Point(7, 64);
            this.labUnit.Name = "labUnit";
            this.labUnit.Size = new System.Drawing.Size(65, 12);
            this.labUnit.TabIndex = 0;
            this.labUnit.Text = "参数单位：";
            // 
            // gruManageOprer
            // 
            this.gruManageOprer.Controls.Add(this.btnJYNew);
            this.gruManageOprer.Controls.Add(this.btnAllExport);
            this.gruManageOprer.Controls.Add(this.btnAllImort);
            this.gruManageOprer.Controls.Add(this.btnExport);
            this.gruManageOprer.Controls.Add(this.btnImport);
            this.gruManageOprer.Controls.Add(this.btnDelete);
            this.gruManageOprer.Controls.Add(this.btnEdit);
            this.gruManageOprer.Controls.Add(this.btnNew);
            this.gruManageOprer.Location = new System.Drawing.Point(297, 435);
            this.gruManageOprer.Name = "gruManageOprer";
            this.gruManageOprer.Size = new System.Drawing.Size(319, 101);
            this.gruManageOprer.TabIndex = 2;
            this.gruManageOprer.TabStop = false;
            this.gruManageOprer.Text = "管理操作";
            // 
            // btnJYNew
            // 
            this.btnJYNew.Location = new System.Drawing.Point(76, 25);
            this.btnJYNew.Name = "btnJYNew";
            this.btnJYNew.Size = new System.Drawing.Size(79, 23);
            this.btnJYNew.TabIndex = 8;
            this.btnJYNew.Text = "基于新建(&A)";
            this.btnJYNew.UseVisualStyleBackColor = true;
            this.btnJYNew.Click += new System.EventHandler(this.btnJYNew_Click);
            // 
            // btnAllExport
            // 
            this.btnAllExport.Location = new System.Drawing.Point(237, 59);
            this.btnAllExport.Name = "btnAllExport";
            this.btnAllExport.Size = new System.Drawing.Size(79, 23);
            this.btnAllExport.TabIndex = 7;
            this.btnAllExport.Text = "全局导出(&X)";
            this.btnAllExport.UseVisualStyleBackColor = true;
            this.btnAllExport.Click += new System.EventHandler(this.btnAllExport_Click);
            // 
            // btnAllImort
            // 
            this.btnAllImort.Location = new System.Drawing.Point(157, 59);
            this.btnAllImort.Name = "btnAllImort";
            this.btnAllImort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAllImort.Size = new System.Drawing.Size(79, 23);
            this.btnAllImort.TabIndex = 6;
            this.btnAllImort.Text = "全局导入(&W)";
            this.btnAllImort.UseVisualStyleBackColor = true;
            this.btnAllImort.Click += new System.EventHandler(this.btnAllImort_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(76, 59);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(79, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "列表导出(&O)";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(4, 59);
            this.btnImport.Name = "btnImport";
            this.btnImport.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnImport.Size = new System.Drawing.Size(70, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "导入(&I)";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(237, 25);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(79, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(157, 25);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(79, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "编辑(&E)";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(4, 25);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(70, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "新建(&N)";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // cmbFilterSel
            // 
            this.cmbFilterSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterSel.FormattingEnabled = true;
            this.cmbFilterSel.Location = new System.Drawing.Point(81, 21);
            this.cmbFilterSel.Name = "cmbFilterSel";
            this.cmbFilterSel.Size = new System.Drawing.Size(140, 20);
            this.cmbFilterSel.TabIndex = 9;
            // 
            // labFilterSel
            // 
            this.labFilterSel.AutoSize = true;
            this.labFilterSel.Location = new System.Drawing.Point(10, 29);
            this.labFilterSel.Name = "labFilterSel";
            this.labFilterSel.Size = new System.Drawing.Size(65, 12);
            this.labFilterSel.TabIndex = 8;
            this.labFilterSel.Text = "过滤选择：";
            // 
            // txtSelect
            // 
            this.txtSelect.Location = new System.Drawing.Point(81, 52);
            this.txtSelect.MaxLength = 50;
            this.txtSelect.Name = "txtSelect";
            this.txtSelect.Size = new System.Drawing.Size(140, 21);
            this.txtSelect.TabIndex = 7;
            // 
            // labSelect
            // 
            this.labSelect.AutoSize = true;
            this.labSelect.Location = new System.Drawing.Point(10, 55);
            this.labSelect.Name = "labSelect";
            this.labSelect.Size = new System.Drawing.Size(65, 12);
            this.labSelect.TabIndex = 6;
            this.labSelect.Text = "参数名称：";
            // 
            // btnComfirm
            // 
            this.btnComfirm.Location = new System.Drawing.Point(541, 647);
            this.btnComfirm.Name = "btnComfirm";
            this.btnComfirm.Size = new System.Drawing.Size(75, 23);
            this.btnComfirm.TabIndex = 11;
            this.btnComfirm.Text = "确定(&C)";
            this.btnComfirm.UseVisualStyleBackColor = true;
            this.btnComfirm.Click += new System.EventHandler(this.btnComfirm_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(227, 19);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(85, 23);
            this.btnSelect.TabIndex = 10;
            this.btnSelect.Text = "过滤/查找(&F)";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // gruSel
            // 
            this.gruSel.Controls.Add(this.labFilterSel);
            this.gruSel.Controls.Add(this.btnSelect);
            this.gruSel.Controls.Add(this.labSelect);
            this.gruSel.Controls.Add(this.txtSelect);
            this.gruSel.Controls.Add(this.cmbFilterSel);
            this.gruSel.Location = new System.Drawing.Point(298, 549);
            this.gruSel.Name = "gruSel";
            this.gruSel.Size = new System.Drawing.Size(318, 82);
            this.gruSel.TabIndex = 12;
            this.gruSel.TabStop = false;
            this.gruSel.Text = "过滤选择";
            // 
            // btnPuslishToTde
            // 
            this.btnPuslishToTde.Location = new System.Drawing.Point(428, 647);
            this.btnPuslishToTde.Name = "btnPuslishToTde";
            this.btnPuslishToTde.Size = new System.Drawing.Size(92, 23);
            this.btnPuslishToTde.TabIndex = 13;
            this.btnPuslishToTde.Text = "同步参数表(&S)";
            this.btnPuslishToTde.UseVisualStyleBackColor = true;
            this.btnPuslishToTde.Click += new System.EventHandler(this.btnPuslishToTde_Click);
            // 
            // ParameterManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 682);
            this.Controls.Add(this.btnPuslishToTde);
            this.Controls.Add(this.gruSel);
            this.Controls.Add(this.btnComfirm);
            this.Controls.Add(this.gruManageOprer);
            this.Controls.Add(this.gruParameterInfo);
            this.Controls.Add(this.gruParameterList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParameterManageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数管理对话框";
            this.gruParameterList.ResumeLayout(false);
            this.gruParameterInfo.ResumeLayout(false);
            this.gruParameterInfo.PerformLayout();
            this.gruManageOprer.ResumeLayout(false);
            this.gruSel.ResumeLayout(false);
            this.gruSel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gruParameterList;
        private System.Windows.Forms.TreeView treeViewParameterList;
        private System.Windows.Forms.GroupBox gruParameterInfo;
        private System.Windows.Forms.Label labUnit;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtParameterName;
        private System.Windows.Forms.Label labParameterName;
        private System.Windows.Forms.Label labParameterType;
        private System.Windows.Forms.ComboBox cmbParameterType;
        private System.Windows.Forms.TextBox txtParameterRemark;
        private System.Windows.Forms.Label labParameterRemark;
        private System.Windows.Forms.Button btnParameterConfirm;
        private System.Windows.Forms.GroupBox gruManageOprer;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ComboBox cmbFilterSel;
        private System.Windows.Forms.Label labFilterSel;
        private System.Windows.Forms.TextBox txtSelect;
        private System.Windows.Forms.Label labSelect;
        private System.Windows.Forms.Button btnComfirm;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnAllExport;
        private System.Windows.Forms.Button btnAllImort;
        private System.Windows.Forms.Button btnJYNew;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.GroupBox gruSel;
        private System.Windows.Forms.Button btnPuslishToTde;
    }
}
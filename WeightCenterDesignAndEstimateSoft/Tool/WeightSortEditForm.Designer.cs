namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class WeightSortEditForm
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
            this.gruWeightStructureTree = new System.Windows.Forms.GroupBox();
            this.btnModifyNode = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.treeViewWeightStructure = new System.Windows.Forms.TreeView();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.txtWeightSortName = new System.Windows.Forms.TextBox();
            this.labWeightSortName = new System.Windows.Forms.Label();
            this.gruNoeInfo = new System.Windows.Forms.GroupBox();
            this.btnSaveNode = new System.Windows.Forms.Button();
            this.btnNoldeCancle = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.labRemark = new System.Windows.Forms.Label();
            this.txtNodeName = new System.Windows.Forms.TextBox();
            this.labNodeName = new System.Windows.Forms.Label();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.gruWeightStructureTree.SuspendLayout();
            this.gruNoeInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruWeightStructureTree
            // 
            this.gruWeightStructureTree.Controls.Add(this.btnModifyNode);
            this.gruWeightStructureTree.Controls.Add(this.btnDelete);
            this.gruWeightStructureTree.Controls.Add(this.btnAddNode);
            this.gruWeightStructureTree.Controls.Add(this.treeViewWeightStructure);
            this.gruWeightStructureTree.Controls.Add(this.btnDown);
            this.gruWeightStructureTree.Controls.Add(this.btnUp);
            this.gruWeightStructureTree.Location = new System.Drawing.Point(10, 12);
            this.gruWeightStructureTree.Name = "gruWeightStructureTree";
            this.gruWeightStructureTree.Size = new System.Drawing.Size(258, 444);
            this.gruWeightStructureTree.TabIndex = 0;
            this.gruWeightStructureTree.TabStop = false;
            this.gruWeightStructureTree.Text = "重量结构树";
            // 
            // btnModifyNode
            // 
            this.btnModifyNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModifyNode.Location = new System.Drawing.Point(137, 360);
            this.btnModifyNode.Name = "btnModifyNode";
            this.btnModifyNode.Size = new System.Drawing.Size(115, 23);
            this.btnModifyNode.TabIndex = 4;
            this.btnModifyNode.Text = "编辑节点(&E)";
            this.btnModifyNode.UseVisualStyleBackColor = true;
            this.btnModifyNode.Click += new System.EventHandler(this.btnModifyNode_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(8, 387);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(115, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除节点(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNode.Location = new System.Drawing.Point(8, 360);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(115, 23);
            this.btnAddNode.TabIndex = 2;
            this.btnAddNode.Text = "添加根节点(&N)";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // treeViewWeightStructure
            // 
            this.treeViewWeightStructure.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewWeightStructure.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeViewWeightStructure.HideSelection = false;
            this.treeViewWeightStructure.Location = new System.Drawing.Point(6, 17);
            this.treeViewWeightStructure.Name = "treeViewWeightStructure";
            this.treeViewWeightStructure.ShowNodeToolTips = true;
            this.treeViewWeightStructure.Size = new System.Drawing.Size(246, 337);
            this.treeViewWeightStructure.TabIndex = 0;
            this.treeViewWeightStructure.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeViewWeightStructure_DrawNode);
            this.treeViewWeightStructure.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewWeightStructure_AfterSelect);
            this.treeViewWeightStructure.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewWeightStructure_NodeMouseClick);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.Location = new System.Drawing.Point(8, 414);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(115, 23);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "下移节点(&L)";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.Location = new System.Drawing.Point(137, 387);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(115, 23);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "上移节点(&U)";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // txtWeightSortName
            // 
            this.txtWeightSortName.Location = new System.Drawing.Point(283, 27);
            this.txtWeightSortName.MaxLength = 50;
            this.txtWeightSortName.Name = "txtWeightSortName";
            this.txtWeightSortName.Size = new System.Drawing.Size(213, 21);
            this.txtWeightSortName.TabIndex = 1;
            // 
            // labWeightSortName
            // 
            this.labWeightSortName.AutoSize = true;
            this.labWeightSortName.Location = new System.Drawing.Point(283, 12);
            this.labWeightSortName.Name = "labWeightSortName";
            this.labWeightSortName.Size = new System.Drawing.Size(89, 12);
            this.labWeightSortName.TabIndex = 0;
            this.labWeightSortName.Text = "重量分类名称：";
            // 
            // gruNoeInfo
            // 
            this.gruNoeInfo.Controls.Add(this.btnSaveNode);
            this.gruNoeInfo.Controls.Add(this.btnNoldeCancle);
            this.gruNoeInfo.Controls.Add(this.txtRemark);
            this.gruNoeInfo.Controls.Add(this.labRemark);
            this.gruNoeInfo.Controls.Add(this.txtNodeName);
            this.gruNoeInfo.Controls.Add(this.labNodeName);
            this.gruNoeInfo.Location = new System.Drawing.Point(277, 68);
            this.gruNoeInfo.Name = "gruNoeInfo";
            this.gruNoeInfo.Size = new System.Drawing.Size(219, 327);
            this.gruNoeInfo.TabIndex = 5;
            this.gruNoeInfo.TabStop = false;
            this.gruNoeInfo.Text = "节点添加";
            // 
            // btnSaveNode
            // 
            this.btnSaveNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveNode.Location = new System.Drawing.Point(59, 298);
            this.btnSaveNode.Name = "btnSaveNode";
            this.btnSaveNode.Size = new System.Drawing.Size(75, 23);
            this.btnSaveNode.TabIndex = 7;
            this.btnSaveNode.Text = "保存(&S)";
            this.btnSaveNode.UseVisualStyleBackColor = true;
            this.btnSaveNode.Click += new System.EventHandler(this.btnSaveNode_Click);
            // 
            // btnNoldeCancle
            // 
            this.btnNoldeCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNoldeCancle.Enabled = false;
            this.btnNoldeCancle.Location = new System.Drawing.Point(140, 298);
            this.btnNoldeCancle.Name = "btnNoldeCancle";
            this.btnNoldeCancle.Size = new System.Drawing.Size(75, 23);
            this.btnNoldeCancle.TabIndex = 6;
            this.btnNoldeCancle.Text = " 取消(&C)";
            this.btnNoldeCancle.UseVisualStyleBackColor = true;
            this.btnNoldeCancle.Click += new System.EventHandler(this.btnNoldeCancle_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(6, 96);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(207, 195);
            this.txtRemark.TabIndex = 5;
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Location = new System.Drawing.Point(6, 81);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(65, 12);
            this.labRemark.TabIndex = 4;
            this.labRemark.Text = "节点备注：";
            // 
            // txtNodeName
            // 
            this.txtNodeName.Location = new System.Drawing.Point(6, 43);
            this.txtNodeName.MaxLength = 50;
            this.txtNodeName.Name = "txtNodeName";
            this.txtNodeName.Size = new System.Drawing.Size(207, 21);
            this.txtNodeName.TabIndex = 3;
            // 
            // labNodeName
            // 
            this.labNodeName.AutoSize = true;
            this.labNodeName.Location = new System.Drawing.Point(6, 28);
            this.labNodeName.Name = "labNodeName";
            this.labNodeName.Size = new System.Drawing.Size(65, 12);
            this.labNodeName.TabIndex = 2;
            this.labNodeName.Text = "节点名称：";
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(417, 459);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 8;
            this.btnCancle.Text = "取消(&B)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(336, 459);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "确定(&A)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // WeightSortEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 493);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtWeightSortName);
            this.Controls.Add(this.labWeightSortName);
            this.Controls.Add(this.gruNoeInfo);
            this.Controls.Add(this.gruWeightStructureTree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WeightSortEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重量分类新建对话框";
            this.gruWeightStructureTree.ResumeLayout(false);
            this.gruNoeInfo.ResumeLayout(false);
            this.gruNoeInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gruWeightStructureTree;
        private System.Windows.Forms.TreeView treeViewWeightStructure;
        private System.Windows.Forms.TextBox txtWeightSortName;
        private System.Windows.Forms.Label labWeightSortName;
        private System.Windows.Forms.GroupBox gruNoeInfo;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label labRemark;
        private System.Windows.Forms.TextBox txtNodeName;
        private System.Windows.Forms.Label labNodeName;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnModifyNode;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Button btnNoldeCancle;
        private System.Windows.Forms.Button btnSaveNode;
    }
}
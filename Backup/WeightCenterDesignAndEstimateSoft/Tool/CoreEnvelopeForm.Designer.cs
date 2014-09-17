namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class CoreEnvelopeForm
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
            this.gruCoreEnvelope = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeViewList = new System.Windows.Forms.TreeView();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDeleteNode = new System.Windows.Forms.Button();
            this.btnUpNode = new System.Windows.Forms.Button();
            this.btnDownNode = new System.Windows.Forms.Button();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.txtPtName = new System.Windows.Forms.TextBox();
            this.labPtName = new System.Windows.Forms.Label();
            this.gruCoreEnvelope.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruCoreEnvelope
            // 
            this.gruCoreEnvelope.Controls.Add(this.tableLayoutPanel1);
            this.gruCoreEnvelope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruCoreEnvelope.Location = new System.Drawing.Point(0, 0);
            this.gruCoreEnvelope.Name = "gruCoreEnvelope";
            this.gruCoreEnvelope.Size = new System.Drawing.Size(388, 396);
            this.gruCoreEnvelope.TabIndex = 0;
            this.gruCoreEnvelope.TabStop = false;
            this.gruCoreEnvelope.Text = "重心包线列表";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.Controls.Add(this.treeViewList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConfirm, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnCancle, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteNode, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnUpNode, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnDownNode, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnAddNode, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtPtName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labPtName, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.986607F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.58295F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0948F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.675842F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.28058F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.34356F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.7362F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(382, 376);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeViewList
            // 
            this.treeViewList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewList.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewList.HideSelection = false;
            this.treeViewList.Location = new System.Drawing.Point(3, 3);
            this.treeViewList.Name = "treeViewList";
            this.tableLayoutPanel1.SetRowSpan(this.treeViewList, 8);
            this.treeViewList.Size = new System.Drawing.Size(215, 370);
            this.treeViewList.TabIndex = 0;
            this.treeViewList.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeViewList_DrawNode);
            this.treeViewList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewList_NodeMouseClick);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirm.Location = new System.Drawing.Point(224, 350);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(80, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "确定(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancle.Location = new System.Drawing.Point(311, 350);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(68, 23);
            this.btnCancle.TabIndex = 9;
            this.btnCancle.Text = "取消(&B)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnEdit.Location = new System.Drawing.Point(224, 188);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "编辑(&E)";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDeleteNode.Location = new System.Drawing.Point(311, 188);
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(68, 23);
            this.btnDeleteNode.TabIndex = 6;
            this.btnDeleteNode.Text = "删除(&D)";
            this.btnDeleteNode.UseVisualStyleBackColor = true;
            this.btnDeleteNode.Click += new System.EventHandler(this.btnDeleteNode_Click);
            // 
            // btnUpNode
            // 
            this.btnUpNode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnUpNode.Location = new System.Drawing.Point(224, 156);
            this.btnUpNode.Name = "btnUpNode";
            this.btnUpNode.Size = new System.Drawing.Size(80, 23);
            this.btnUpNode.TabIndex = 4;
            this.btnUpNode.Text = "上移(&U)";
            this.btnUpNode.UseVisualStyleBackColor = true;
            this.btnUpNode.Click += new System.EventHandler(this.btnUpNode_Click);
            // 
            // btnDownNode
            // 
            this.btnDownNode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDownNode.Location = new System.Drawing.Point(311, 156);
            this.btnDownNode.Name = "btnDownNode";
            this.btnDownNode.Size = new System.Drawing.Size(68, 23);
            this.btnDownNode.TabIndex = 5;
            this.btnDownNode.Text = "下移(&L)";
            this.btnDownNode.UseVisualStyleBackColor = true;
            this.btnDownNode.Click += new System.EventHandler(this.btnDownNode_Click);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAddNode.Location = new System.Drawing.Point(224, 121);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(80, 23);
            this.btnAddNode.TabIndex = 1;
            this.btnAddNode.Text = "添加节点(&A)";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // txtPtName
            // 
            this.txtPtName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.txtPtName, 2);
            this.txtPtName.Location = new System.Drawing.Point(224, 85);
            this.txtPtName.MaxLength = 50;
            this.txtPtName.Name = "txtPtName";
            this.txtPtName.Size = new System.Drawing.Size(145, 21);
            this.txtPtName.TabIndex = 3;
            // 
            // labPtName
            // 
            this.labPtName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labPtName.AutoSize = true;
            this.labPtName.Location = new System.Drawing.Point(224, 58);
            this.labPtName.Name = "labPtName";
            this.labPtName.Size = new System.Drawing.Size(71, 12);
            this.labPtName.TabIndex = 2;
            this.labPtName.Text = "坐标点名称:";
            // 
            // CoreEnvelopeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 396);
            this.Controls.Add(this.gruCoreEnvelope);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CoreEnvelopeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重心包线节点编辑";
            this.gruCoreEnvelope.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gruCoreEnvelope;
        private System.Windows.Forms.TreeView treeViewList;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Label labPtName;
        private System.Windows.Forms.TextBox txtPtName;
        private System.Windows.Forms.Button btnUpNode;
        private System.Windows.Forms.Button btnDownNode;
        private System.Windows.Forms.Button btnDeleteNode;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class WeightSortManageForm
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
            this.gruWeightSortList = new System.Windows.Forms.GroupBox();
            this.treeViewWeightSort = new System.Windows.Forms.TreeView();
            this.btnJYNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.gruWeightSortList.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruWeightSortList
            // 
            this.gruWeightSortList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gruWeightSortList.Controls.Add(this.treeViewWeightSort);
            this.gruWeightSortList.Location = new System.Drawing.Point(3, 1);
            this.gruWeightSortList.Name = "gruWeightSortList";
            this.gruWeightSortList.Size = new System.Drawing.Size(426, 372);
            this.gruWeightSortList.TabIndex = 0;
            this.gruWeightSortList.TabStop = false;
            this.gruWeightSortList.Text = "重量分类列表";
            // 
            // treeViewWeightSort
            // 
            this.treeViewWeightSort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewWeightSort.HideSelection = false;
            this.treeViewWeightSort.Location = new System.Drawing.Point(3, 17);
            this.treeViewWeightSort.Name = "treeViewWeightSort";
            this.treeViewWeightSort.Size = new System.Drawing.Size(420, 352);
            this.treeViewWeightSort.TabIndex = 0;
            this.treeViewWeightSort.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewWeightSort_MouseDoubleClick);
            this.treeViewWeightSort.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewWeightSort_AfterSelect);
            // 
            // btnJYNew
            // 
            this.btnJYNew.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnJYNew.Enabled = false;
            this.btnJYNew.Location = new System.Drawing.Point(32, 409);
            this.btnJYNew.Name = "btnJYNew";
            this.btnJYNew.Size = new System.Drawing.Size(154, 23);
            this.btnJYNew.TabIndex = 3;
            this.btnJYNew.Text = "基于...新建(&A)";
            this.btnJYNew.UseVisualStyleBackColor = true;
            this.btnJYNew.Click += new System.EventHandler(this.btnJYNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(242, 380);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(154, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "编辑(&E)";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(242, 409);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(154, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnImport.Location = new System.Drawing.Point(32, 438);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(154, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "导入(&I)";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(242, 438);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(154, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "导出(&O)";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnNew.Location = new System.Drawing.Point(32, 380);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(154, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "新建(&N)";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // WeightSortManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 470);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.gruWeightSortList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnJYNew);
            this.Controls.Add(this.btnEdit);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WeightSortManageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重量分类管理对话框";
            this.Load += new System.EventHandler(this.WeightSortManageForm_Load);
            this.gruWeightSortList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gruWeightSortList;
        private System.Windows.Forms.TreeView treeViewWeightSort;
        private System.Windows.Forms.Button btnJYNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnNew;


    }
}
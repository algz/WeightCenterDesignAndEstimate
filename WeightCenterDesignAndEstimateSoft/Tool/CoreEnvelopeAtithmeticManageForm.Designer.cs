namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class CoreEnvelopeAtithmeticManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoreEnvelopeAtithmeticManageForm));
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnJYNew = new System.Windows.Forms.Button();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.treeViewArithmeticList = new System.Windows.Forms.TreeView();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.gruArithmeticList = new System.Windows.Forms.GroupBox();
            this.gruArithmeticList.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(214, 418);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(152, 23);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "导出(&O)";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(7, 418);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(152, 23);
            this.btnImport.TabIndex = 14;
            this.btnImport.Text = "导入(&I)";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(214, 387);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(152, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnJYNew
            // 
            this.btnJYNew.Location = new System.Drawing.Point(7, 387);
            this.btnJYNew.Name = "btnJYNew";
            this.btnJYNew.Size = new System.Drawing.Size(152, 23);
            this.btnJYNew.TabIndex = 12;
            this.btnJYNew.Text = "基于...新建(&A)";
            this.btnJYNew.UseVisualStyleBackColor = true;
            this.btnJYNew.Click += new System.EventHandler(this.btnJYNew_Click);
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageListTreeView.Images.SetKeyName(0, "MainStruct.bmp");
            this.imageListTreeView.Images.SetKeyName(1, "MainStruct_sel.bmp");
            this.imageListTreeView.Images.SetKeyName(2, "folder.bmp");
            this.imageListTreeView.Images.SetKeyName(3, "folder_sel.bmp");
            this.imageListTreeView.Images.SetKeyName(4, "file_wem.bmp");
            this.imageListTreeView.Images.SetKeyName(5, "file_wem_sel.bmp");
            // 
            // treeViewArithmeticList
            // 
            this.treeViewArithmeticList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewArithmeticList.Location = new System.Drawing.Point(3, 17);
            this.treeViewArithmeticList.Name = "treeViewArithmeticList";
            this.treeViewArithmeticList.Size = new System.Drawing.Size(360, 324);
            this.treeViewArithmeticList.TabIndex = 0;
            this.treeViewArithmeticList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewArithmeticList_AfterSelect);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(214, 356);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(152, 23);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "编辑(&E)";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(7, 356);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(152, 23);
            this.btnNew.TabIndex = 10;
            this.btnNew.Text = "新建(&N)";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // gruArithmeticList
            // 
            this.gruArithmeticList.Controls.Add(this.treeViewArithmeticList);
            this.gruArithmeticList.Location = new System.Drawing.Point(3, 3);
            this.gruArithmeticList.Name = "gruArithmeticList";
            this.gruArithmeticList.Size = new System.Drawing.Size(366, 344);
            this.gruArithmeticList.TabIndex = 9;
            this.gruArithmeticList.TabStop = false;
            this.gruArithmeticList.Text = "重量包线算法列表";
            // 
            // CoreEnvelopeAtithmeticManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 445);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnJYNew);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.gruArithmeticList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CoreEnvelopeAtithmeticManageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重心包线算法管理";
            this.gruArithmeticList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnJYNew;
        private System.Windows.Forms.ImageList imageListTreeView;
        private System.Windows.Forms.TreeView treeViewArithmeticList;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox gruArithmeticList;

    }
}
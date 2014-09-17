namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class ImportWeightDataListForm
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
            this.gruWeightDataList = new System.Windows.Forms.GroupBox();
            this.treeViewWeightData = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnRefush = new System.Windows.Forms.Button();
            this.gruWeightDataList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruWeightDataList
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gruWeightDataList, 4);
            this.gruWeightDataList.Controls.Add(this.treeViewWeightData);
            this.gruWeightDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gruWeightDataList.Location = new System.Drawing.Point(3, 3);
            this.gruWeightDataList.Name = "gruWeightDataList";
            this.gruWeightDataList.Size = new System.Drawing.Size(298, 362);
            this.gruWeightDataList.TabIndex = 0;
            this.gruWeightDataList.TabStop = false;
            this.gruWeightDataList.Text = "重量数据列表";
            // 
            // treeViewWeightData
            // 
            this.treeViewWeightData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewWeightData.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewWeightData.HideSelection = false;
            this.treeViewWeightData.Location = new System.Drawing.Point(3, 17);
            this.treeViewWeightData.Name = "treeViewWeightData";
            this.treeViewWeightData.Size = new System.Drawing.Size(292, 342);
            this.treeViewWeightData.TabIndex = 0;
            this.treeViewWeightData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewWeightData_MouseDoubleClick);
            this.treeViewWeightData.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeViewWeightData_DrawNode);
            this.treeViewWeightData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewWeightData_AfterSelect);
            this.treeViewWeightData.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewWeightData_NodeMouseClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.Controls.Add(this.gruWeightDataList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancle, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnConfirm, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnRefush, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(304, 403);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancle.Location = new System.Drawing.Point(235, 374);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(66, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消(&B)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfirm.Location = new System.Drawing.Point(157, 374);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(72, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnRefush
            // 
            this.btnRefush.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRefush.Location = new System.Drawing.Point(79, 374);
            this.btnRefush.Name = "btnRefush";
            this.btnRefush.Size = new System.Drawing.Size(72, 23);
            this.btnRefush.TabIndex = 3;
            this.btnRefush.Text = "刷新(&R)";
            this.btnRefush.UseVisualStyleBackColor = true;
            this.btnRefush.Click += new System.EventHandler(this.btnRefush_Click);
            // 
            // ImportWeightDataListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 403);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportWeightDataListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportWeightDataListForm";
            this.Load += new System.EventHandler(this.ImportWeightDataListForm_Load);
            this.gruWeightDataList.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gruWeightDataList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.TreeView treeViewWeightData;
        private System.Windows.Forms.Button btnRefush;
    }
}
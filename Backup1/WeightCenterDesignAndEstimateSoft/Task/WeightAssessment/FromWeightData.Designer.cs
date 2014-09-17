namespace WeightCenterDesignAndEstimateSoft.Task.WeightAssessment
{
    partial class FromWeightData
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
            this.weightResultTree = new System.Windows.Forms.TreeView();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.grpBoxHeader = new System.Windows.Forms.GroupBox();
            this.refreshDB = new System.Windows.Forms.Button();
            this.grpBoxHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // weightResultTree
            // 
            this.weightResultTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weightResultTree.Location = new System.Drawing.Point(3, 17);
            this.weightResultTree.Name = "weightResultTree";
            this.weightResultTree.Size = new System.Drawing.Size(348, 267);
            this.weightResultTree.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(213, 305);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(291, 305);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消(&B)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // grpBoxHeader
            // 
            this.grpBoxHeader.Controls.Add(this.weightResultTree);
            this.grpBoxHeader.Location = new System.Drawing.Point(12, 12);
            this.grpBoxHeader.Name = "grpBoxHeader";
            this.grpBoxHeader.Size = new System.Drawing.Size(354, 287);
            this.grpBoxHeader.TabIndex = 4;
            this.grpBoxHeader.TabStop = false;
            this.grpBoxHeader.Text = "重量数据";
            // 
            // refreshDB
            // 
            this.refreshDB.Location = new System.Drawing.Point(15, 305);
            this.refreshDB.Name = "refreshDB";
            this.refreshDB.Size = new System.Drawing.Size(75, 23);
            this.refreshDB.TabIndex = 5;
            this.refreshDB.Text = "刷新(&R)";
            this.refreshDB.UseVisualStyleBackColor = true;
            this.refreshDB.Visible = false;
            // 
            // FromWeightData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 340);
            this.Controls.Add(this.refreshDB);
            this.Controls.Add(this.grpBoxHeader);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfirm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FromWeightData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "基准重量设计数据  当前重量设计数据";
            this.Load += new System.EventHandler(this.FromCurrentWeightData_Load);
            this.grpBoxHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView weightResultTree;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.GroupBox grpBoxHeader;
        private System.Windows.Forms.Button refreshDB;
    }
}
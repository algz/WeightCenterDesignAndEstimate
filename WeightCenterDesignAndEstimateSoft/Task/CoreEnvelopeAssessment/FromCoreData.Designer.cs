namespace WeightCenterDesignAndEstimateSoft.Task.CoreEnvelopeAssessment
{
    partial class FromCoreData
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeCoreEnvelope = new System.Windows.Forms.TreeView();
            this.refreshDB = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeCoreEnvelope);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 281);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重心包线数据列表";
            // 
            // treeCoreEnvelope
            // 
            this.treeCoreEnvelope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCoreEnvelope.Location = new System.Drawing.Point(3, 17);
            this.treeCoreEnvelope.Name = "treeCoreEnvelope";
            this.treeCoreEnvelope.Size = new System.Drawing.Size(358, 261);
            this.treeCoreEnvelope.TabIndex = 0;
            this.treeCoreEnvelope.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeCoreEnvelope_NodeMouseDoubleClick);
            // 
            // refreshDB
            // 
            this.refreshDB.Location = new System.Drawing.Point(16, 300);
            this.refreshDB.Name = "refreshDB";
            this.refreshDB.Size = new System.Drawing.Size(75, 23);
            this.refreshDB.TabIndex = 1;
            this.refreshDB.Text = "刷新(&R)";
            this.refreshDB.UseVisualStyleBackColor = true;
            this.refreshDB.Visible = false;
            this.refreshDB.Click += new System.EventHandler(this.refreshDB_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(216, 300);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确认(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(297, 300);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消(&B)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // FromCoreData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 335);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.refreshDB);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FromCoreData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "基准重心包线数据";
            this.Load += new System.EventHandler(this.FromCoreData_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeCoreEnvelope;
        private System.Windows.Forms.Button refreshDB;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
    }
}
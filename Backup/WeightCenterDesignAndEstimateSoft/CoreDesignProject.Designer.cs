namespace WeightCenterDesignAndEstimateSoft
{
    partial class CoreDesignProject
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
            this.labProjectName = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.txtCreator = new System.Windows.Forms.TextBox();
            this.labCreator = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.labRemark = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labProjectName
            // 
            this.labProjectName.AutoSize = true;
            this.labProjectName.ForeColor = System.Drawing.Color.Red;
            this.labProjectName.Location = new System.Drawing.Point(20, 24);
            this.labProjectName.Name = "labProjectName";
            this.labProjectName.Size = new System.Drawing.Size(59, 12);
            this.labProjectName.TabIndex = 0;
            this.labProjectName.Text = "工程名称:";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(88, 21);
            this.txtProjectName.MaxLength = 50;
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(192, 21);
            this.txtProjectName.TabIndex = 1;
            // 
            // txtCreator
            // 
            this.txtCreator.Location = new System.Drawing.Point(88, 57);
            this.txtCreator.MaxLength = 50;
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Size = new System.Drawing.Size(192, 21);
            this.txtCreator.TabIndex = 3;
            // 
            // labCreator
            // 
            this.labCreator.AutoSize = true;
            this.labCreator.ForeColor = System.Drawing.Color.Red;
            this.labCreator.Location = new System.Drawing.Point(20, 60);
            this.labCreator.Name = "labCreator";
            this.labCreator.Size = new System.Drawing.Size(47, 12);
            this.labCreator.TabIndex = 2;
            this.labCreator.Text = "创建者:";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(88, 90);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(192, 122);
            this.txtRemark.TabIndex = 5;
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Location = new System.Drawing.Point(20, 93);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(35, 12);
            this.labRemark.TabIndex = 4;
            this.labRemark.Text = "备注:";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(124, 231);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "确定(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(205, 231);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "取消(&A)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // CoreDesignProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(298, 266);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.txtCreator);
            this.Controls.Add(this.labCreator);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.labProjectName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CoreDesignProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 新建工程对话框";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labProjectName;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtCreator;
        private System.Windows.Forms.Label labCreator;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label labRemark;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
    }
}
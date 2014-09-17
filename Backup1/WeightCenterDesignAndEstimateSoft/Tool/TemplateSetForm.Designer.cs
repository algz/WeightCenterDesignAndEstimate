namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class TemplateSetForm
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
            this.labPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labPath
            // 
            this.labPath.AutoSize = true;
            this.labPath.Location = new System.Drawing.Point(5, 24);
            this.labPath.Name = "labPath";
            this.labPath.Size = new System.Drawing.Size(59, 12);
            this.labPath.TabIndex = 13;
            this.labPath.Text = "模板路径:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(68, 21);
            this.txtPath.MaxLength = 50;
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(298, 21);
            this.txtPath.TabIndex = 12;
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(236, 56);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 11;
            this.btnCancle.Text = "取消(&A)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(144, 56);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "确定(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(372, 19);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(63, 23);
            this.btnSelect.TabIndex = 14;
            this.btnSelect.Text = "浏览(&B)";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // TemplateSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(440, 89);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.labPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfirm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemplateSetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基础库设置";
            this.Load += new System.EventHandler(this.TemplateSetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnSelect;
    }
}
namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class BasicDBSetForm
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
            this.labUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.labPwd = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labFileFolder = new System.Windows.Forms.Label();
            this.txtFileFolder = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.labServer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labUrl
            // 
            this.labUrl.AutoSize = true;
            this.labUrl.Location = new System.Drawing.Point(19, 90);
            this.labUrl.Name = "labUrl";
            this.labUrl.Size = new System.Drawing.Size(53, 12);
            this.labUrl.TabIndex = 0;
            this.labUrl.Text = "Url地址:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(106, 90);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(287, 21);
            this.txtUrl.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(237, 151);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(318, 151);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // labPwd
            // 
            this.labPwd.AutoSize = true;
            this.labPwd.Location = new System.Drawing.Point(19, 66);
            this.labPwd.Name = "labPwd";
            this.labPwd.Size = new System.Drawing.Size(35, 12);
            this.labPwd.TabIndex = 7;
            this.labPwd.Text = "密码:";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(106, 63);
            this.txtPwd.MaxLength = 50;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(287, 21);
            this.txtPwd.TabIndex = 6;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(106, 34);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(287, 21);
            this.txtUserName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "用户名:";
            // 
            // labFileFolder
            // 
            this.labFileFolder.AutoSize = true;
            this.labFileFolder.Location = new System.Drawing.Point(19, 122);
            this.labFileFolder.Name = "labFileFolder";
            this.labFileFolder.Size = new System.Drawing.Size(83, 12);
            this.labFileFolder.TabIndex = 9;
            this.labFileFolder.Text = "共享文件目录:";
            // 
            // txtFileFolder
            // 
            this.txtFileFolder.Location = new System.Drawing.Point(106, 119);
            this.txtFileFolder.MaxLength = 50;
            this.txtFileFolder.Name = "txtFileFolder";
            this.txtFileFolder.Size = new System.Drawing.Size(287, 21);
            this.txtFileFolder.TabIndex = 8;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(106, 7);
            this.txtServer.MaxLength = 50;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(287, 21);
            this.txtServer.TabIndex = 11;
            // 
            // labServer
            // 
            this.labServer.AutoSize = true;
            this.labServer.Location = new System.Drawing.Point(12, 10);
            this.labServer.Name = "labServer";
            this.labServer.Size = new System.Drawing.Size(53, 12);
            this.labServer.TabIndex = 10;
            this.labServer.Text = " 服务器:";
            // 
            // BasicDBSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(405, 183);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.labServer);
            this.Controls.Add(this.labFileFolder);
            this.Controls.Add(this.txtFileFolder);
            this.Controls.Add(this.labPwd);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.labUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BasicDBSetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基础库配置";
            this.Load += new System.EventHandler(this.BasicDBSetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label labPwd;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labFileFolder;
        private System.Windows.Forms.TextBox txtFileFolder;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label labServer;
    }
}
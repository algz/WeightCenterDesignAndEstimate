namespace WeightCenterDesignAndEstimateSoft.Setting
{
    partial class DBServerSettingFormcs
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.labPwd = new System.Windows.Forms.Label();
            this.btnTestConnect = new System.Windows.Forms.Button();
            this.chkRemindPwd = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(88, 19);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(158, 21);
            this.txtUserName.TabIndex = 1;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(88, 48);
            this.txtPwd.MaxLength = 50;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(158, 21);
            this.txtPwd.TabIndex = 2;
            // 
            // labPwd
            // 
            this.labPwd.AutoSize = true;
            this.labPwd.Location = new System.Drawing.Point(13, 51);
            this.labPwd.Name = "labPwd";
            this.labPwd.Size = new System.Drawing.Size(35, 12);
            this.labPwd.TabIndex = 3;
            this.labPwd.Text = "密码:";
            // 
            // btnTestConnect
            // 
            this.btnTestConnect.Location = new System.Drawing.Point(91, 108);
            this.btnTestConnect.Name = "btnTestConnect";
            this.btnTestConnect.Size = new System.Drawing.Size(75, 23);
            this.btnTestConnect.TabIndex = 5;
            this.btnTestConnect.Text = "测试";
            this.btnTestConnect.UseVisualStyleBackColor = true;
            this.btnTestConnect.Click += new System.EventHandler(this.btnTestConnect_Click);
            // 
            // chkRemindPwd
            // 
            this.chkRemindPwd.AutoSize = true;
            this.chkRemindPwd.Location = new System.Drawing.Point(91, 81);
            this.chkRemindPwd.Name = "chkRemindPwd";
            this.chkRemindPwd.Size = new System.Drawing.Size(72, 16);
            this.chkRemindPwd.TabIndex = 6;
            this.chkRemindPwd.Text = "记住密码";
            this.chkRemindPwd.UseVisualStyleBackColor = true;
            // 
            // DBServerSettingFormcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(268, 143);
            this.Controls.Add(this.chkRemindPwd);
            this.Controls.Add(this.btnTestConnect);
            this.Controls.Add(this.labPwd);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBServerSettingFormcs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库服务器登录设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label labPwd;
        private System.Windows.Forms.Button btnTestConnect;
        private System.Windows.Forms.CheckBox chkRemindPwd;

    }
}
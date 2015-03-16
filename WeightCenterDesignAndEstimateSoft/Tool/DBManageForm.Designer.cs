namespace WeightCenterDesignAndEstimateSoft.Tool
{
    partial class DBManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBManageForm));
            this.gruManageData = new System.Windows.Forms.GroupBox();
            this.radCoreEnvelopeDesignDagta = new System.Windows.Forms.RadioButton();
            this.radBtnWeightDesignData = new System.Windows.Forms.RadioButton();
            this.radBtnTypeWeightData = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.gruManageData.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruManageData
            // 
            this.gruManageData.Controls.Add(this.radCoreEnvelopeDesignDagta);
            this.gruManageData.Controls.Add(this.radBtnWeightDesignData);
            this.gruManageData.Controls.Add(this.radBtnTypeWeightData);
            this.gruManageData.Location = new System.Drawing.Point(13, 13);
            this.gruManageData.Name = "gruManageData";
            this.gruManageData.Size = new System.Drawing.Size(197, 100);
            this.gruManageData.TabIndex = 0;
            this.gruManageData.TabStop = false;
            this.gruManageData.Text = "选择需要管理的数据";
            // 
            // radCoreEnvelopeDesignDagta
            // 
            this.radCoreEnvelopeDesignDagta.AutoSize = true;
            this.radCoreEnvelopeDesignDagta.Location = new System.Drawing.Point(35, 65);
            this.radCoreEnvelopeDesignDagta.Name = "radCoreEnvelopeDesignDagta";
            this.radCoreEnvelopeDesignDagta.Size = new System.Drawing.Size(119, 16);
            this.radCoreEnvelopeDesignDagta.TabIndex = 2;
            this.radCoreEnvelopeDesignDagta.TabStop = true;
            this.radCoreEnvelopeDesignDagta.Text = "重心包线设计数据";
            this.radCoreEnvelopeDesignDagta.UseVisualStyleBackColor = true;
            // 
            // radBtnWeightDesignData
            // 
            this.radBtnWeightDesignData.AutoSize = true;
            this.radBtnWeightDesignData.Location = new System.Drawing.Point(35, 43);
            this.radBtnWeightDesignData.Name = "radBtnWeightDesignData";
            this.radBtnWeightDesignData.Size = new System.Drawing.Size(95, 16);
            this.radBtnWeightDesignData.TabIndex = 1;
            this.radBtnWeightDesignData.TabStop = true;
            this.radBtnWeightDesignData.Text = "重量设计数据";
            this.radBtnWeightDesignData.UseVisualStyleBackColor = true;
            // 
            // radBtnTypeWeightData
            // 
            this.radBtnTypeWeightData.AutoSize = true;
            this.radBtnTypeWeightData.Location = new System.Drawing.Point(35, 21);
            this.radBtnTypeWeightData.Name = "radBtnTypeWeightData";
            this.radBtnTypeWeightData.Size = new System.Drawing.Size(95, 16);
            this.radBtnTypeWeightData.TabIndex = 0;
            this.radBtnTypeWeightData.TabStop = true;
            this.radBtnTypeWeightData.Text = "型号重量数据";
            this.radBtnTypeWeightData.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(24, 128);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(124, 128);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消(&A)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // DBManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 163);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.gruManageData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBManageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库管理对话框";
            this.gruManageData.ResumeLayout(false);
            this.gruManageData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gruManageData;
        private System.Windows.Forms.RadioButton radBtnTypeWeightData;
        private System.Windows.Forms.RadioButton radCoreEnvelopeDesignDagta;
        private System.Windows.Forms.RadioButton radBtnWeightDesignData;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
    }
}
namespace WeightCenterDesignAndEstimateSoft.Task.WeightAssessment.AssessmentWeightData
{
    partial class FromWDMCore
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
            this.WDMTree = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.useTestData = new System.Windows.Forms.CheckBox();
            this.testDataRadio = new System.Windows.Forms.RadioButton();
            this.actualDataRadio = new System.Windows.Forms.RadioButton();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WDMTree);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 206);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重量数据列表";
            // 
            // WDMTree
            // 
            this.WDMTree.Location = new System.Drawing.Point(6, 20);
            this.WDMTree.Name = "WDMTree";
            this.WDMTree.Size = new System.Drawing.Size(309, 180);
            this.WDMTree.TabIndex = 0;
            this.WDMTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.WDMTree_NodeMouseDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.useTestData);
            this.groupBox2.Controls.Add(this.testDataRadio);
            this.groupBox2.Controls.Add(this.actualDataRadio);
            this.groupBox2.Location = new System.Drawing.Point(13, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 94);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "评估数据设置";
            // 
            // useTestData
            // 
            this.useTestData.AutoSize = true;
            this.useTestData.Location = new System.Drawing.Point(135, 68);
            this.useTestData.Name = "useTestData";
            this.useTestData.Size = new System.Drawing.Size(180, 16);
            this.useTestData.TabIndex = 2;
            this.useTestData.Text = "实测数据为0时,使用测试数据";
            this.useTestData.UseVisualStyleBackColor = true;
            // 
            // testDataRadio
            // 
            this.testDataRadio.AutoSize = true;
            this.testDataRadio.Location = new System.Drawing.Point(135, 30);
            this.testDataRadio.Name = "testDataRadio";
            this.testDataRadio.Size = new System.Drawing.Size(71, 16);
            this.testDataRadio.TabIndex = 1;
            this.testDataRadio.Text = "测试数据";
            this.testDataRadio.UseVisualStyleBackColor = true;
            // 
            // actualDataRadio
            // 
            this.actualDataRadio.AutoSize = true;
            this.actualDataRadio.Checked = true;
            this.actualDataRadio.Location = new System.Drawing.Point(17, 31);
            this.actualDataRadio.Name = "actualDataRadio";
            this.actualDataRadio.Size = new System.Drawing.Size(71, 16);
            this.actualDataRadio.TabIndex = 0;
            this.actualDataRadio.TabStop = true;
            this.actualDataRadio.Text = "实测数据";
            this.actualDataRadio.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(13, 336);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(78, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新(&R)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(172, 336);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(78, 23);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确认(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(256, 336);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(78, 23);
            this.btnCancle.TabIndex = 4;
            this.btnCancle.Text = "取消(&B)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // FromWDMCore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 371);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FromWDMCore";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "评估重量数据  WDM系统数据";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FromWDMCore_FormClosed);
            this.Load += new System.EventHandler(this.FromWDM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton actualDataRadio;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.TreeView WDMTree;
        private System.Windows.Forms.CheckBox useTestData;
        private System.Windows.Forms.RadioButton testDataRadio;
    }
}
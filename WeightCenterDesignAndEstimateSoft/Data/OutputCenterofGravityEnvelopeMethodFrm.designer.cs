namespace WeightCenterDesignAndEstimateSoft.Data
{
    partial class OutputCenterofGravityEnvelopeMethodFrm
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
            this.treeViewCenterofGravityEnvelopeMethod = new System.Windows.Forms.TreeView();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeViewCenterofGravityEnvelopeMethod);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 264);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重心包线算法列表";
            // 
            // treeViewCenterofGravityEnvelopeMethod
            // 
            this.treeViewCenterofGravityEnvelopeMethod.Location = new System.Drawing.Point(7, 16);
            this.treeViewCenterofGravityEnvelopeMethod.Name = "treeViewCenterofGravityEnvelopeMethod";
            this.treeViewCenterofGravityEnvelopeMethod.Size = new System.Drawing.Size(264, 242);
            this.treeViewCenterofGravityEnvelopeMethod.TabIndex = 0;
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(120, 273);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(75, 23);
            this.btnOutput.TabIndex = 2;
            this.btnOutput.Text = "导出";
            this.btnOutput.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(201, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // OutputCenterofGravityEnvelopeMethodFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 302);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutputCenterofGravityEnvelopeMethodFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重心包线算法选择对话框";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeViewCenterofGravityEnvelopeMethod;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnCancel;
    }
}
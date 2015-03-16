namespace WeightCenterDesignAndEstimateSoft.Task.CoreEnvelopeAssessment
{
    partial class CoreEnvelopeAssessConfigForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.coreAssesslistView = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.weightedGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnWeightedRest = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDownMarginMax = new System.Windows.Forms.TextBox();
            this.txtDownMarginMin = new System.Windows.Forms.TextBox();
            this.txtTopMarginMax = new System.Windows.Forms.TextBox();
            this.txtTopMarginMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weightedGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.63768F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.36232F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(475, 354);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(469, 229);
            this.splitContainer1.SplitterDistance = 204;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.coreAssesslistView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 229);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待评估重心选择";
            // 
            // coreAssesslistView
            // 
            this.coreAssesslistView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.coreAssesslistView.CheckBoxes = true;
            this.coreAssesslistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coreAssesslistView.Location = new System.Drawing.Point(3, 17);
            this.coreAssesslistView.Name = "coreAssesslistView";
            this.coreAssesslistView.Size = new System.Drawing.Size(198, 209);
            this.coreAssesslistView.TabIndex = 0;
            this.coreAssesslistView.UseCompatibleStateImageBehavior = false;
            this.coreAssesslistView.View = System.Windows.Forms.View.List;
            this.coreAssesslistView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.coreAssesslistView_ItemChecked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.weightedGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 229);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "权重设置";
            // 
            // weightedGridView
            // 
            this.weightedGridView.AllowUserToAddRows = false;
            this.weightedGridView.AllowUserToDeleteRows = false;
            this.weightedGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.weightedGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.weightedGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weightedGridView.Location = new System.Drawing.Point(3, 17);
            this.weightedGridView.Name = "weightedGridView";
            this.weightedGridView.RowHeadersVisible = false;
            this.weightedGridView.RowTemplate.Height = 23;
            this.weightedGridView.Size = new System.Drawing.Size(255, 209);
            this.weightedGridView.TabIndex = 0;
            this.weightedGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.weightedGridView_CellValidating);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "重心名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "权重";
            this.Column2.Name = "Column2";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.97436F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.02564F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel2.Controls.Add(this.btnWeightedRest, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnConfirm, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancle, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 318);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(469, 33);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btnWeightedRest
            // 
            this.btnWeightedRest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWeightedRest.Location = new System.Drawing.Point(208, 3);
            this.btnWeightedRest.Name = "btnWeightedRest";
            this.btnWeightedRest.Size = new System.Drawing.Size(93, 23);
            this.btnWeightedRest.TabIndex = 0;
            this.btnWeightedRest.Text = "权重归一化(&N)";
            this.btnWeightedRest.UseVisualStyleBackColor = true;
            this.btnWeightedRest.Click += new System.EventHandler(this.btnWeightedRest_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(307, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确认(&C)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(388, 3);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(73, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消(&B)";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDownMarginMax);
            this.panel1.Controls.Add(this.txtDownMarginMin);
            this.panel1.Controls.Add(this.txtTopMarginMax);
            this.panel1.Controls.Add(this.txtTopMarginMin);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 238);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 74);
            this.panel1.TabIndex = 2;
            // 
            // txtDownMarginMax
            // 
            this.txtDownMarginMax.Location = new System.Drawing.Point(351, 45);
            this.txtDownMarginMax.Name = "txtDownMarginMax";
            this.txtDownMarginMax.Size = new System.Drawing.Size(100, 21);
            this.txtDownMarginMax.TabIndex = 7;
            // 
            // txtDownMarginMin
            // 
            this.txtDownMarginMin.Location = new System.Drawing.Point(351, 15);
            this.txtDownMarginMin.Name = "txtDownMarginMin";
            this.txtDownMarginMin.Size = new System.Drawing.Size(100, 21);
            this.txtDownMarginMin.TabIndex = 6;
            // 
            // txtTopMarginMax
            // 
            this.txtTopMarginMax.Location = new System.Drawing.Point(90, 45);
            this.txtTopMarginMax.Name = "txtTopMarginMax";
            this.txtTopMarginMax.Size = new System.Drawing.Size(100, 21);
            this.txtTopMarginMax.TabIndex = 5;
            // 
            // txtTopMarginMin
            // 
            this.txtTopMarginMin.Location = new System.Drawing.Point(88, 15);
            this.txtTopMarginMin.Name = "txtTopMarginMin";
            this.txtTopMarginMin.Size = new System.Drawing.Size(100, 21);
            this.txtTopMarginMin.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(273, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "后限后裕度:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "后限前裕度:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "前限后裕度:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "前限前裕度:";
            // 
            // CoreEnvelopeAssessConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 354);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CoreEnvelopeAssessConfigForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "评估参数设置对话框";
            this.Load += new System.EventHandler(this.CoreEnvelopeAssessConfigForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.weightedGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView weightedGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnWeightedRest;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.ListView coreAssesslistView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDownMarginMax;
        private System.Windows.Forms.TextBox txtDownMarginMin;
        private System.Windows.Forms.TextBox txtTopMarginMax;
        private System.Windows.Forms.TextBox txtTopMarginMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}
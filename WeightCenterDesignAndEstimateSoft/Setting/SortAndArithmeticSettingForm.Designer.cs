namespace WeightCenterDesignAndEstimateSoft.Setting
{
    partial class SortAndArithmeticSettingForm
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
            this.gruSortList = new System.Windows.Forms.GroupBox();
            this.gruArithmeticList = new System.Windows.Forms.GroupBox();
            this.gruRelationList = new System.Windows.Forms.GroupBox();
            this.treeViewSortList = new System.Windows.Forms.TreeView();
            this.treeViewArithmeticList = new System.Windows.Forms.TreeView();
            this.treeViewRelationList = new System.Windows.Forms.TreeView();
            this.btnAutoModify = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.labAutoModify = new System.Windows.Forms.Label();
            this.btnNewSort = new System.Windows.Forms.Button();
            this.labNewSort = new System.Windows.Forms.Label();
            this.gruSortList.SuspendLayout();
            this.gruArithmeticList.SuspendLayout();
            this.gruRelationList.SuspendLayout();
            this.SuspendLayout();
            // 
            // gruSortList
            // 
            this.gruSortList.Controls.Add(this.treeViewSortList);
            this.gruSortList.Location = new System.Drawing.Point(9, 7);
            this.gruSortList.Name = "gruSortList";
            this.gruSortList.Size = new System.Drawing.Size(200, 347);
            this.gruSortList.TabIndex = 0;
            this.gruSortList.TabStop = false;
            this.gruSortList.Text = "重量分类列表";
            // 
            // gruArithmeticList
            // 
            this.gruArithmeticList.Controls.Add(this.treeViewArithmeticList);
            this.gruArithmeticList.Location = new System.Drawing.Point(215, 7);
            this.gruArithmeticList.Name = "gruArithmeticList";
            this.gruArithmeticList.Size = new System.Drawing.Size(200, 347);
            this.gruArithmeticList.TabIndex = 1;
            this.gruArithmeticList.TabStop = false;
            this.gruArithmeticList.Text = "重量算法列表";
            // 
            // gruRelationList
            // 
            this.gruRelationList.Controls.Add(this.treeViewRelationList);
            this.gruRelationList.Location = new System.Drawing.Point(421, 7);
            this.gruRelationList.Name = "gruRelationList";
            this.gruRelationList.Size = new System.Drawing.Size(200, 347);
            this.gruRelationList.TabIndex = 2;
            this.gruRelationList.TabStop = false;
            this.gruRelationList.Text = "关系列表";
            // 
            // treeViewSortList
            // 
            this.treeViewSortList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSortList.Location = new System.Drawing.Point(3, 17);
            this.treeViewSortList.Name = "treeViewSortList";
            this.treeViewSortList.Size = new System.Drawing.Size(194, 327);
            this.treeViewSortList.TabIndex = 0;
            // 
            // treeViewArithmeticList
            // 
            this.treeViewArithmeticList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewArithmeticList.Location = new System.Drawing.Point(3, 17);
            this.treeViewArithmeticList.Name = "treeViewArithmeticList";
            this.treeViewArithmeticList.Size = new System.Drawing.Size(194, 327);
            this.treeViewArithmeticList.TabIndex = 0;
            // 
            // treeViewRelationList
            // 
            this.treeViewRelationList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewRelationList.Location = new System.Drawing.Point(3, 17);
            this.treeViewRelationList.Name = "treeViewRelationList";
            this.treeViewRelationList.Size = new System.Drawing.Size(194, 327);
            this.treeViewRelationList.TabIndex = 0;
            // 
            // btnAutoModify
            // 
            this.btnAutoModify.Location = new System.Drawing.Point(543, 359);
            this.btnAutoModify.Name = "btnAutoModify";
            this.btnAutoModify.Size = new System.Drawing.Size(75, 23);
            this.btnAutoModify.TabIndex = 3;
            this.btnAutoModify.Text = "自动维护";
            this.btnAutoModify.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(543, 386);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // labAutoModify
            // 
            this.labAutoModify.AutoSize = true;
            this.labAutoModify.Location = new System.Drawing.Point(448, 364);
            this.labAutoModify.Name = "labAutoModify";
            this.labAutoModify.Size = new System.Drawing.Size(89, 12);
            this.labAutoModify.TabIndex = 5;
            this.labAutoModify.Text = "关系自动维护：";
            // 
            // btnNewSort
            // 
            this.btnNewSort.Location = new System.Drawing.Point(337, 359);
            this.btnNewSort.Name = "btnNewSort";
            this.btnNewSort.Size = new System.Drawing.Size(75, 23);
            this.btnNewSort.TabIndex = 6;
            this.btnNewSort.Text = "新建分类";
            this.btnNewSort.UseVisualStyleBackColor = true;
            // 
            // labNewSort
            // 
            this.labNewSort.AutoSize = true;
            this.labNewSort.Location = new System.Drawing.Point(168, 364);
            this.labNewSort.Name = "labNewSort";
            this.labNewSort.Size = new System.Drawing.Size(161, 12);
            this.labNewSort.TabIndex = 7;
            this.labNewSort.Text = "根据重量算法新建重量分类：";
            // 
            // SortAndArithmeticSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 413);
            this.Controls.Add(this.labNewSort);
            this.Controls.Add(this.btnNewSort);
            this.Controls.Add(this.labAutoModify);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnAutoModify);
            this.Controls.Add(this.gruRelationList);
            this.Controls.Add(this.gruArithmeticList);
            this.Controls.Add(this.gruSortList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SortAndArithmeticSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分类与算法关系设置";
            this.gruSortList.ResumeLayout(false);
            this.gruArithmeticList.ResumeLayout(false);
            this.gruRelationList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gruSortList;
        private System.Windows.Forms.GroupBox gruArithmeticList;
        private System.Windows.Forms.GroupBox gruRelationList;
        private System.Windows.Forms.TreeView treeViewSortList;
        private System.Windows.Forms.TreeView treeViewArithmeticList;
        private System.Windows.Forms.TreeView treeViewRelationList;
        private System.Windows.Forms.Button btnAutoModify;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label labAutoModify;
        private System.Windows.Forms.Button btnNewSort;
        private System.Windows.Forms.Label labNewSort;
    }
}
﻿namespace WeightCenterDesignAndEstimateSoft.Task.WeightAdjustmentSubforms
{
    partial class SelectWeightDataForm
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
            this.grpBx_WeightDataList = new System.Windows.Forms.GroupBox();
            this.treeViewWeightData = new System.Windows.Forms.TreeView();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_ImptCrtWghtDsgnRst = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpBx_WeightDataList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBx_WeightDataList
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.grpBx_WeightDataList, 4);
            this.grpBx_WeightDataList.Controls.Add(this.treeViewWeightData);
            this.grpBx_WeightDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBx_WeightDataList.Location = new System.Drawing.Point(3, 3);
            this.grpBx_WeightDataList.Name = "grpBx_WeightDataList";
            this.grpBx_WeightDataList.Size = new System.Drawing.Size(282, 338);
            this.grpBx_WeightDataList.TabIndex = 1;
            this.grpBx_WeightDataList.TabStop = false;
            this.grpBx_WeightDataList.Text = "重量数据列表";
            // 
            // treeViewWeightData
            // 
            this.treeViewWeightData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewWeightData.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeViewWeightData.HideSelection = false;
            this.treeViewWeightData.Location = new System.Drawing.Point(3, 17);
            this.treeViewWeightData.Name = "treeViewWeightData";
            this.treeViewWeightData.Size = new System.Drawing.Size(276, 318);
            this.treeViewWeightData.TabIndex = 0;
            this.treeViewWeightData.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeViewWeightData_DrawNode);
            this.treeViewWeightData.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewWeightData_NodeMouseClick);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Refresh.Location = new System.Drawing.Point(57, 349);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(72, 23);
            this.btn_Refresh.TabIndex = 5;
            this.btn_Refresh.Text = "刷新(&R)";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Cancel.Location = new System.Drawing.Point(213, 349);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(72, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "取消(&B)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_ImptCrtWghtDsgnRst
            // 
            this.btn_ImptCrtWghtDsgnRst.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_ImptCrtWghtDsgnRst.Location = new System.Drawing.Point(135, 349);
            this.btn_ImptCrtWghtDsgnRst.Name = "btn_ImptCrtWghtDsgnRst";
            this.btn_ImptCrtWghtDsgnRst.Size = new System.Drawing.Size(72, 23);
            this.btn_ImptCrtWghtDsgnRst.TabIndex = 3;
            this.btn_ImptCrtWghtDsgnRst.Text = "确定(&C)";
            this.btn_ImptCrtWghtDsgnRst.UseVisualStyleBackColor = true;
            this.btn_ImptCrtWghtDsgnRst.Click += new System.EventHandler(this.btn_ImptCrtWghtDsgnRst_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel1.Controls.Add(this.btn_Refresh, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpBx_WeightDataList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Cancel, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_ImptCrtWghtDsgnRst, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(288, 377);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // SelectWeightDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 377);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectWeightDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择重量数据";
            this.Load += new System.EventHandler(this.SelectWeightDataForm_Load);
            this.grpBx_WeightDataList.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBx_WeightDataList;
        private System.Windows.Forms.TreeView treeViewWeightData;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_ImptCrtWghtDsgnRst;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
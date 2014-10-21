namespace WeightCenterDesignAndEstimateSoft.Task.WeightAssessment
{
    partial class WeightAssessmentConfigForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.重量比值最小值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportMinMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMinMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重量比值最大值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportMaxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMaxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.权重分配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportWeightedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportWeightedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ConfigurationGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.paramLineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.paramPieChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigurationGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paramLineChart)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paramPieChart)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重量比值最小值ToolStripMenuItem,
            this.重量比值最大值ToolStripMenuItem,
            this.权重分配ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 重量比值最小值ToolStripMenuItem
            // 
            this.重量比值最小值ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportMinMenuItem,
            this.ExportMinMenuItem});
            this.重量比值最小值ToolStripMenuItem.Name = "重量比值最小值ToolStripMenuItem";
            this.重量比值最小值ToolStripMenuItem.Size = new System.Drawing.Size(124, 21);
            this.重量比值最小值ToolStripMenuItem.Text = "重量比值最小值(&M)";
            // 
            // ImportMinMenuItem
            // 
            this.ImportMinMenuItem.Name = "ImportMinMenuItem";
            this.ImportMinMenuItem.Size = new System.Drawing.Size(151, 22);
            this.ImportMinMenuItem.Tag = "1";
            this.ImportMinMenuItem.Text = "导入最小值(&I)";
            this.ImportMinMenuItem.Click += new System.EventHandler(this.ImportMinMenuItem_Click);
            // 
            // ExportMinMenuItem
            // 
            this.ExportMinMenuItem.Name = "ExportMinMenuItem";
            this.ExportMinMenuItem.Size = new System.Drawing.Size(151, 22);
            this.ExportMinMenuItem.Tag = "1";
            this.ExportMinMenuItem.Text = "导出最小值(&E)";
            this.ExportMinMenuItem.Click += new System.EventHandler(this.ExportMinMenuItem_Click);
            // 
            // 重量比值最大值ToolStripMenuItem
            // 
            this.重量比值最大值ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportMaxMenuItem,
            this.ExportMaxMenuItem});
            this.重量比值最大值ToolStripMenuItem.Name = "重量比值最大值ToolStripMenuItem";
            this.重量比值最大值ToolStripMenuItem.Size = new System.Drawing.Size(120, 21);
            this.重量比值最大值ToolStripMenuItem.Text = "重量比值最大值(&X)";
            // 
            // ImportMaxMenuItem
            // 
            this.ImportMaxMenuItem.Name = "ImportMaxMenuItem";
            this.ImportMaxMenuItem.Size = new System.Drawing.Size(151, 22);
            this.ImportMaxMenuItem.Tag = "2";
            this.ImportMaxMenuItem.Text = "导入最大值(I)";
            this.ImportMaxMenuItem.Click += new System.EventHandler(this.ImportMinMenuItem_Click);
            // 
            // ExportMaxMenuItem
            // 
            this.ExportMaxMenuItem.Name = "ExportMaxMenuItem";
            this.ExportMaxMenuItem.Size = new System.Drawing.Size(151, 22);
            this.ExportMaxMenuItem.Tag = "2";
            this.ExportMaxMenuItem.Text = "导出最大值(&E)";
            this.ExportMaxMenuItem.Click += new System.EventHandler(this.ExportMinMenuItem_Click);
            // 
            // 权重分配ToolStripMenuItem
            // 
            this.权重分配ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportWeightedMenuItem,
            this.ExportWeightedMenuItem});
            this.权重分配ToolStripMenuItem.Name = "权重分配ToolStripMenuItem";
            this.权重分配ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.权重分配ToolStripMenuItem.Text = "权重分配(&A)";
            // 
            // ImportWeightedMenuItem
            // 
            this.ImportWeightedMenuItem.Name = "ImportWeightedMenuItem";
            this.ImportWeightedMenuItem.Size = new System.Drawing.Size(139, 22);
            this.ImportWeightedMenuItem.Tag = "3";
            this.ImportWeightedMenuItem.Text = "导入权重(&I)";
            this.ImportWeightedMenuItem.Click += new System.EventHandler(this.ImportMinMenuItem_Click);
            // 
            // ExportWeightedMenuItem
            // 
            this.ExportWeightedMenuItem.Name = "ExportWeightedMenuItem";
            this.ExportWeightedMenuItem.Size = new System.Drawing.Size(139, 22);
            this.ExportWeightedMenuItem.Tag = "3";
            this.ExportWeightedMenuItem.Text = "导出权重(&E)";
            this.ExportWeightedMenuItem.Click += new System.EventHandler(this.ExportMinMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(960, 473);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ConfigurationGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 473);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数表";
            // 
            // ConfigurationGridView
            // 
            this.ConfigurationGridView.AllowUserToAddRows = false;
            this.ConfigurationGridView.AllowUserToDeleteRows = false;
            this.ConfigurationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigurationGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.ConfigurationGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigurationGridView.Location = new System.Drawing.Point(3, 17);
            this.ConfigurationGridView.Name = "ConfigurationGridView";
            this.ConfigurationGridView.RowHeadersVisible = false;
            this.ConfigurationGridView.RowTemplate.Height = 23;
            this.ConfigurationGridView.Size = new System.Drawing.Size(314, 453);
            this.ConfigurationGridView.TabIndex = 0;
            this.ConfigurationGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.ConfigurationGridView_CellValidating);
            this.ConfigurationGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConfigurationGridView_KeyDown);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "节点名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "最小值";
            this.Column2.Name = "Column2";
            this.Column2.Width = 70;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "最大值";
            this.Column3.Name = "Column3";
            this.Column3.Width = 70;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "权重";
            this.Column4.Name = "Column4";
            this.Column4.Width = 60;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.81184F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.188161F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(636, 473);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(630, 432);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数图";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 412);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.paramLineChart);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 386);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "折线图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // paramLineChart
            // 
            chartArea1.Name = "ChartArea1";
            this.paramLineChart.ChartAreas.Add(chartArea1);
            this.paramLineChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.paramLineChart.Legends.Add(legend1);
            this.paramLineChart.Location = new System.Drawing.Point(3, 3);
            this.paramLineChart.Name = "paramLineChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.paramLineChart.Series.Add(series1);
            this.paramLineChart.Size = new System.Drawing.Size(610, 380);
            this.paramLineChart.TabIndex = 1;
            this.paramLineChart.Text = "chart1";
            title1.Name = "Title1";
            this.paramLineChart.Titles.Add(title1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.paramPieChart);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(616, 386);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "圆饼图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // paramPieChart
            // 
            chartArea2.Name = "ChartArea1";
            this.paramPieChart.ChartAreas.Add(chartArea2);
            this.paramPieChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            this.paramPieChart.Legends.Add(legend2);
            this.paramPieChart.Location = new System.Drawing.Point(3, 3);
            this.paramPieChart.Name = "paramPieChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.paramPieChart.Series.Add(series2);
            this.paramPieChart.Size = new System.Drawing.Size(610, 380);
            this.paramPieChart.TabIndex = 1;
            this.paramPieChart.Text = "chart1";
            title2.Name = "Title1";
            this.paramPieChart.Titles.Add(title2);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.19838F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.80162F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel2.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 441);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(630, 29);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "取消(&B)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(463, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "确认(&C)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "权重归一化(&N)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnWeightRest_Click);
            // 
            // WeightAssessmentConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 498);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WeightAssessmentConfigForm";
            this.ShowInTaskbar = false;
            this.Text = "重量评估参数设置对话框";
            this.Load += new System.EventHandler(this.WeightAssessmentConfigForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConfigurationGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paramLineChart)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paramPieChart)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 重量比值最小值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportMinMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportMinMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重量比值最大值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportMaxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportMaxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 权重分配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportWeightedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportWeightedMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView ConfigurationGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataVisualization.Charting.Chart paramLineChart;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart paramPieChart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeightCenterDesignAndEstimateSoft.Task.WeightAssessment;
using Model;
using System.Xml;
using WeightCenterDesignAndEstimateSoft.Task.WeightAdjustmentSubforms;
using XCommon;
using System.IO;
using Model.assessData;
using WeightCenterDesignAndEstimateSoft.Task.WeightAssessment.AssessmentWeightData;

namespace WeightCenterDesignAndEstimateSoft.Task
{
    public partial class WeightAssessmentForm : Form
    {
        public MainForm mainForm;//主窗口的引用
        //public DataTable weightDataDT;//存储重量分类数据
        private WeightSortData weightSortData;//用于临时存储重量数据,方便导出.
        public WeightAssessResult weightAssessResult = new WeightAssessResult();
        bool IsEdited = false;

        public WeightAssessmentForm()
        {
            InitializeComponent();
        }

        public WeightAssessmentForm(WeightAssessResult war)
        {
            InitializeComponent();
            IsEdited = true;

            this.weightAssessResult = war;
            this.loadWeightAssessResult(war);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //验证评估结果名称是否输入
            if (this.resultNameTxt.Text == "")
            {
                MessageBox.Show("请输入评估结果名称");
                return;
            }
            else
            {
                this.weightAssessResult.resultName = this.resultNameTxt.Text;
            }

            if (this.weightAssessResult.assessWeightDataList == null||this.weightAssessResult.assessWeightDataList.Count==0)
            {
                MessageBox.Show("请加载评估重量");
                return;
            }


            if (this.weightAssessResult.datumWeightDataList == null||this.weightAssessResult.datumWeightDataList.Count==0)
            {
                MessageBox.Show("请加载基准重量");
                return;
            }

            //验证基准/评估重量是否已加载
            if (!this.ExportDatumFileMenuItem.Enabled || !this.ExprotAssessFileMenuItem.Enabled)
            {
                MessageBox.Show("请加载" + (!this.ExportDatumFileMenuItem.Enabled == true ? "基准" : "评估") + "重量");
                return;
            }

            if (!this.weightAssessResult.isEvaluateWeight && MessageBox.Show("没有配置重重评估参数,是否按默认值匹配", "确认", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }


            if (mainForm.designProjectData.weightAssessResultList == null)
            {
                mainForm.designProjectData.weightAssessResultList = new List<WeightAssessResult>();
            }

            this.weightAssessResult.advancedInflationTotal = 0;
            this.weightAssessResult.rationalityInflationTotal = 0;
            foreach (WeightAssessParameter wap in this.weightAssessResult.weightAssessParamList)
            {
                wap.advancedInflation = this.AssessWeightArithmetic(wap.datumWeight, wap.assessWeight, wap.maxValue, wap.minValue, wap.weightedValue, 2);
                this.weightAssessResult.advancedInflationTotal += wap.advancedInflation;
                wap.rationalityInflation = this.AssessWeightArithmetic(wap.datumWeight, wap.assessWeight, wap.maxValue, wap.minValue, wap.weightedValue, 1);
                this.weightAssessResult.rationalityInflationTotal += wap.rationalityInflation;
            }

            //int i=0;
            //List<WeightAssessResult> warList=mainForm.designProjectData.weightAssessResultList;
            //for (; i < warList.Count; i++)
            //{
            //    if (warList[i].resultID == this.weightAssessResult.resultID)
            //    {
            //        break;
            //    }
            //}

            //if (i >= warList.Count)
            //{
            //    mainForm.designProjectData.weightAssessResultList.Add(this.weightAssessResult);
            //}

            //mainForm.BindProjectTreeData(mainForm.designProjectData);//用于绑定主窗口的工程树结点

            //新增
            if (IsEdited == false)
            {
                List<WeightAssessResult> warList = mainForm.designProjectData.weightAssessResultList;
                mainForm.designProjectData.weightAssessResultList.Add(this.weightAssessResult);

                mainForm.BindProjectTreeData(mainForm.designProjectData);//用于绑定主窗口的工程树结点
                mainForm.SetWeightEstimatedTab(this.weightAssessResult, mainForm.designProjectData.weightAssessResultList.Count - 1);
            }
            else
            {
                //for (int i=0;i<mainForm.designProjectData.weightAssessResultList.Count;i++)
                //{
                //    WeightAssessResult temp = mainForm.designProjectData.weightAssessResultList[i];
                //    if (temp.resultID == this.weightAssessResult.resultID)
                //    {
                //        temp = weightAssessResult;
                //        break;
                //    }
                //}
                mainForm.SetWeightEstimatedTab(this.weightAssessResult);
            }

            this.Close();
        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            //if (this.weightAssessResult.datumWeightDataList==null||
            //    this.weightAssessResult.assessWeightDataList==null||
            //    this.weightAssessResult.datumWeightDataList.Count == 0 ||
            //    this.weightAssessResult.assessWeightDataList.Count==0)
            //{
            //    MessageBox.Show("没有重量基准/评估重量数据");
            //    return;
            //}
            this.weightAssessResult.isEvaluateWeight = true;//是否评估重量,true评估
            new WeightAssessmentConfigForm().ShowDialog(this);
        }

        private void FromCurrentWeightDataMenuItem_Click(object sender, EventArgs e)
        {
            if (mainForm.designProjectData == null || mainForm.designProjectData.lstWeightArithmetic == null || mainForm.designProjectData.lstWeightArithmetic.Count == 0)
            {
                MessageBox.Show("重量设计结果没有数据");
                return;
            }
            new FromWeightData(0).ShowDialog(this);
        }

        private void FromCurrentAdjustmentDataMenuItem_Click(object sender, EventArgs e)
        {
            if (mainForm.designProjectData == null || mainForm.designProjectData.lstAdjustmentResultData == null || mainForm.designProjectData.lstAdjustmentResultData.Count == 0)
            {
                MessageBox.Show("重量调整结果没有数据");
                return;
            }
            new FromWeightData(1).ShowDialog(this);
        }

        private void FromModelWeightDBMenuItem_Click(object sender, EventArgs e)
        {
            new FromWeightData(2).ShowDialog(this);
        }

        private void FromDesignWeightDBMenuItem_Click(object sender, EventArgs e)
        {
            new FromWeightData(3).ShowDialog(this);
        }

        private void WeightAssessmentForm_Load(object sender, EventArgs e)
        {
            this.mainForm = (MainForm)this.Owner;
        }

        /// <summary>
        /// 导入基准/评估重量数据文件,根据ToolStripMenuItem.Tag值判断类型(1:基准重量;2评估重量)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportWeightDataFileMenuItem_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(((ToolStripMenuItem)sender).Tag);
            if (weightDataGridView.Rows.Count == 0 && i == 2)
            {
                MessageBox.Show("请先加载基准重量");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c://"; //对话框的初始目录   
            //要在对话框中显示的文件筛选器，例如，"文本文件(*.txt)|*.txt|所有文件(*.*)||*.*"     
            openFileDialog.Filter = "XML文件|*.xml|Excel文件|*.xls";
            //在对话框中选择的文件筛选器的索引，如果选第一项就设为1
            openFileDialog.FilterIndex = 1;
            //控制对话框在关闭之前是否恢复当前目录     
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FilterIndex == 1)
                {
                    this.weightSortData = CommonUtil.GetXmlImporSortData(openFileDialog.FileName);
                }
                else
                {
                    this.weightSortData = CommonUtil.GetXlsImportSortData(openFileDialog.FileName);
                }

                //if (i==1||(i==2&&InspectWeightName(weightSortData.lstWeightData))||MessageBox.Show("评估重量分类名称与基准重量分类名称不匹配,确认是否继续加载评估重量,不匹配值以0代替？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                this.saveWeightDataGridView(weightSortData, i);
                //}
            }
        }

        /// <summary>
        /// 导出基准重量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportDatumFileMenuItem_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(((ToolStripMenuItem)sender).Tag);
            //WeightSortData wsd = new WeightSortData();
            //wsd.sortName = i == 1 ? "基准重量" : "评估重量";
            //wsd.lstWeightData = new List<WeightData>();
            //foreach (DataGridViewRow row in this.weightDataGridView.Rows)
            //{

            //    WeightData wd = new WeightData();
            //    wd.weightName = row.Cells[0].Value.ToString();
            //    wd.weightValue=Convert.ToDouble(row.Cells[i].Value);
            //    wsd.lstWeightData.Add(wd);
            //}
            if (this.weightSortData == null)
            {
                this.weightSortData = new WeightSortData();
                this.weightSortData.lstWeightData = i == 1 ? this.weightAssessResult.datumWeightDataList :
                    this.weightAssessResult.assessWeightDataList;
            }
            this.weightSortData.sortName = i == 1 ? "基准重量" : "评估重量";
            CommonUtil.ExportDataToDataFile(this.weightSortData);
        }

        #region 自定义方法

        /// <summary>
        /// 加载重量评估结果集
        /// </summary>
        /// <param name="WeightAssessResult"></param>
        private void loadWeightAssessResult(WeightAssessResult war)
        {
            this.resultNameTxt.Text = war.resultName;

            foreach (WeightAssessParameter wap in war.weightAssessParamList)
            {
                int i = this.weightDataGridView.Rows.Add();
                this.weightDataGridView.Rows[i].Cells[0].Value = wap.weightName;
                this.weightDataGridView.Rows[i].Cells[1].Value = wap.datumWeight;
                this.weightDataGridView.Rows[i].Cells[2].Value = wap.assessWeight;

                //开启基准重量导出菜单
                this.ExportDatumFileMenuItem.Enabled = true;

                //开启评估重量菜单
                this.AssessWeightMenuItem.Enabled = true;

                //开启评估重量导出菜单
                this.ExprotAssessFileMenuItem.Enabled = true;

                //开启重量评估参数配置按钮菜单
                this.btnPreferences.Enabled = true;
            }

        }

        /// <summary>
        /// 保存基准/评估重量数据
        /// </summary>
        /// <param name="wsd"></param>
        /// <param name="i">1:基准重量;2:评估重量</param>
        public void saveWeightDataGridView(WeightSortData wsd, int i)
        {
            this.weightSortData = wsd;//临时存储,方便数据导出.

            if (i == 1)
            {
                for (int k = 0; k < wsd.lstWeightData.Count; k++)
                {
                    if (wsd.lstWeightData[k].weightValue == 0)
                    {
                        MessageBox.Show("基准数据中不能存在为0的重量值!");
                        return;
                    }
                }
            }

            if (!InspectWeightName(wsd.lstWeightData))
            {
                if (i == 1 && this.weightDataGridView.Rows.Count != 0)
                {
                    //对已生成的评估结果上重新导入基准重量
                    if (MessageBox.Show("待导入的基准重量分类名称与已导入表格的名称不匹配,确认是否继续,并清空评估重量及相关参数?", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.weightDataGridView.Rows.Clear();
                        if (this.weightAssessResult == null)
                        {
                            this.weightAssessResult = new WeightAssessResult();
                        }
                        this.btnPreferences.Enabled = false;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (i == 2)
                {
                    MessageBox.Show("待导入的评估重量不匹配");
                    return;
                }

            }

           

            //判断是否首次加载重量数据.(判断重量分类"节点名称"是否加载完成,否则进行加载节点名称.)
            if (weightDataGridView.Rows.Count == 0)
            {
                this.weightAssessResult.weightAssessParamList = new List<WeightAssessParameter>();
                foreach (WeightData wd in wsd.lstWeightData)
                {
                    if (wd.nParentID == 0 && wd.weightName != null)
                    {

                        int index = this.weightDataGridView.Rows.Add();
                        this.weightDataGridView.Rows[index].Cells[0].Value = wd.weightName;

                        WeightAssessParameter wa = new WeightAssessParameter();
                        this.weightAssessResult.weightAssessParamList.Add(wa);
                        wa.weightName = wd.weightName;
                        wa.minValue = 0.8;//初始化最小值
                        wa.maxValue = 1.2;//初始化最大值
                    }
                }
            }

            //加载基准重量数据或评估重量数据
            for (int j = 0; j < this.weightDataGridView.Rows.Count; j++)
            {
                DataGridViewRow row = this.weightDataGridView.Rows[j];
                foreach (WeightData wd in wsd.lstWeightData)
                {
                    if (wd.nParentID == 0 && wd.weightName != null)
                    {
                        if (row.Cells[0].Value.Equals(wd.weightName))
                        {
                            row.Cells[i].Value = Math.Round(wd.weightValue, 6);

                            WeightAssessParameter wa = this.weightAssessResult.weightAssessParamList[j];
                            if (i == 1)
                            {
                                wa.datumWeight = Math.Round(wd.weightValue, 6);

                                //当i==1(赋值基准重量)时,计算初始化权重值.
                                wa.weightedValue = Math.Round(wd.weightValue / weightAssessResult.datumWeightTotal, 6);
                            }
                            else if (i == 2)
                            {
                                wa.assessWeight = Math.Round(wd.weightValue, 6);
                            }
                            break;
                        }
                        else
                        {
                            row.Cells[i].Value = 0;
                        }
                    }
                    else if (wd.nParentID == -1 && i == 1)
                    {
                        //存储基准重量数据对象
                        this.weightAssessResult.datumWeightDataList = wsd.lstWeightData;

                        //计算基准重量总值
                        this.weightAssessResult.datumWeightTotal = Math.Round(wd.weightValue, 6);

                        //开启基准重量导出菜单
                        this.ExportDatumFileMenuItem.Enabled = true;

                        //开启评估重量菜单
                        this.AssessWeightMenuItem.Enabled = true;
                    }
                    else if (wd.nParentID == -1 && i == 2)
                    {

                        ////评估数据对象进行排序
                        List<WeightData> temList = new List<WeightData>();
                        foreach (WeightData datumWD in this.weightAssessResult.datumWeightDataList)
                        {
                            for (int k = 0; k < wsd.lstWeightData.Count; k++)
                            {
                                WeightData assessWD = wsd.lstWeightData[k];
                                if (datumWD.weightName == assessWD.weightName && assessWD.nParentID != -1)
                                {
                                    temList.Add(assessWD);
                                    break;
                                }
                                else if (datumWD.nParentID == -1 && assessWD.nParentID == -1)
                                {
                                    temList.Add(assessWD);
                                    break;
                                }
                            }
                        }
                        wsd.lstWeightData = temList;

                        //存储评估数据重量对象
                        this.weightAssessResult.assessWeightDataList = wsd.lstWeightData;

                        //计算评估重量总值
                        this.weightAssessResult.assessWeightTotal = Math.Round(wd.weightValue, 6);

                        //开启评估重量导出菜单
                        this.ExprotAssessFileMenuItem.Enabled = true;

                        //开启重量评估参数配置按钮菜单
                        this.btnPreferences.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 验证将要导入的重量分类与已导入到表格的重量分类是否匹配
        /// </summary>
        /// <param name="wdList">将要导入的重要分类(基准重量或评估重量)</param>
        /// <returns>true:匹配;false:不匹配</returns>
        private bool InspectWeightName(List<WeightData> wdList)
        {
            bool bl = false;
            if (wdList == null || this.weightDataGridView.Rows.Count == 0)
            {
                return bl;
            }

            int count_tem=0;
            for (int i = 0; i < wdList.Count; i++)
            {
                if (wdList[i].nParentID == 0 && wdList[i].nID != -1)
                {
                    count_tem++;
                }
            }
            if(count_tem != this.weightDataGridView.Rows.Count)
            {
                return bl;
            }

            foreach (DataGridViewRow row in this.weightDataGridView.Rows)
            {
                bl = false;
                foreach (WeightData wd in wdList)
                {
                    if (wd.nParentID == 0 && row.Cells[0].Value.Equals(wd.weightName))
                    {
                        bl = true;
                        break;
                    }
                }
                if (!bl)
                {
                    return false;
                }
            }
            return bl;
        }

        /// <summary>
        /// 重量评估算法
        /// </summary>
        /// <param name="datumWeight">基准重量</param>
        /// <param name="assessWeight">评估重量</param>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值</param>
        /// <param name="weighted">权值</param>
        /// <param name="kind">调用算法：1:合理指标;2:先进指标</param>
        /// <returns></returns>
        private double AssessWeightArithmetic(double datumWeight, double assessWeight, double max, double min, double weighted, int kind)
        {
            double ratio = assessWeight / datumWeight;
            if (kind == 1)
            {
                return ((min - ratio) * (ratio - max) / (Math.Pow((max - min), 2)/4)) * weighted;
            }
            else
            {
                return ((min - ratio) / min) * weighted;
            }

        }

        #endregion

        private void ImportWDMMenuItem_Click(object sender, EventArgs e)
        {
            new FromWDMCore().ShowDialog(this);
        }
    }
}

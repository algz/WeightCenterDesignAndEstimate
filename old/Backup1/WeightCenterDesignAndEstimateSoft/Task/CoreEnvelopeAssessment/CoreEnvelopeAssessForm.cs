using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.assessData;
using Model;

namespace WeightCenterDesignAndEstimateSoft.Task.CoreEnvelopeAssessment
{
    public partial class CoreEnvelopeAssessForm : Form
    {
        public MainForm mainForm;
        public CoreAssessResult coreAssessResult = new CoreAssessResult();
        //public List<CorePointExt> datumCoreDataList=new List<CorePointExt>();//基准重心包线数据列表
        //public List<CorePointExt> assessCoreDataList=new List<CorePointExt>();//评估重心包线数据列表

        bool IsEdit = false;

        public CoreEnvelopeAssessForm()
        {
            InitializeComponent();
        }

        public CoreEnvelopeAssessForm(CoreAssessResult coreAssessResult)
        {
            IsEdit = true;
            InitializeComponent();
            this.coreAssessResult = coreAssessResult;
        }

        private void CoreEnvelopeAssessForm_Load(object sender, EventArgs e)
        {
            this.mainForm = (MainForm)this.Owner;
            if (this.coreAssessResult.resultName == null)
            {
                this.btnCoreAssessConfig.Enabled = false;
                this.btnConfirm.Enabled = false;
            }
            else
            {
                this.txtResultName.Text = this.coreAssessResult.resultName;
                this.saveCoreGridView(this.coreAssessResult.datumCoreDataList, "0");
                this.saveCoreGridView(this.coreAssessResult.assessCoreDataList, "1");
            }
            if (mainForm.designProjectData.lstCoreEnvelopeDesign == null ||
                mainForm.designProjectData.lstCoreEnvelopeDesign.Count == 0)
            {
                this.FromCurrentCoreDesignMenuItem.Enabled = false;
            }
            if (mainForm.designProjectData.lstCutResultData == null ||
                mainForm.designProjectData.lstCutResultData.Count == 0)
            {
                this.FromCurrentCoreCutMenuItem.Enabled = false;
            }
        }

        private void FromCurrentCoreDesignMenuItem_Click(object sender, EventArgs e)
        {
            //此处加入导入数据代码，把包线存入 data.lstBasicCoreEnvelope
            if (mainForm.designProjectData.lstCoreEnvelopeDesign == null || mainForm.designProjectData.lstCoreEnvelopeDesign.Count == 0)
            {
                MessageBox.Show("当前重心包线没有设计数据");
                return;
            }
            new FromCoreData(Convert.ToInt16(((ToolStripMenuItem)sender).Tag)).ShowDialog(this);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtResultName.Text == "")
            {
                MessageBox.Show("请输入重心包线评估结果名称");
                return;
            }
            if (this.coreAssessResult.assessCoreDataList.Count == 0 ||
                this.coreAssessResult.datumCoreDataList.Count == 0)
            {
                MessageBox.Show("请加载基准/评估重心包线数据");
                return;
            }
            //if (this.coreAssessResult == null)
            //{
            //    this.coreAssessResult = new CoreAssessResult();
            //}
            CoreAssessResult car = this.coreAssessResult;
            //重心包线评估
            evaluationCoreMethod01(car);

            //add
            if (IsEdit == false)
            {
                if (mainForm.designProjectData.CoreAssessResultList == null)
                {
                    mainForm.designProjectData.CoreAssessResultList = new List<CoreAssessResult>();
                }
                mainForm.designProjectData.CoreAssessResultList.Add(car);
                mainForm.BindProjectTreeData(mainForm.designProjectData);//用于绑定主窗口的工程树结点
                mainForm.SetCoreEstimatedTab(car, mainForm.designProjectData.CoreAssessResultList.Count - 1);
            }
            else
            {
                mainForm.SetCoreEstimatedTab(car);
            }
            this.Close();
        }

        private void btnCoreAssessConfig_Click(object sender, EventArgs e)
        {
            new CoreEnvelopeAssessConfigForm().ShowDialog(this);
        }

        /// <summary>
        /// 导入基准(0)/评估(1)重心包线数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportCoreFileMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls,*.xlsx)|*.xls;*.xlsx|所有文件 (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.EndsWith(".xls") || dialog.FileName.EndsWith(".xlsx"))
                {
                    List<CorePointExt> list = new List<CorePointExt>();
                    CommonUtil.ImportExcelToPointList(list, dialog.FileName);
                    saveCoreGridView(list, menu.Tag.ToString());
                    XCommon.XLog.Write("成功从文件导入\"" + dialog.FileName + "\"数据！");
                }
                else if (dialog.FileName.EndsWith(".xml"))
                {
                    saveCoreGridView(CommonUtil.LoadXmlToPointList(dialog.FileName), menu.Tag.ToString());
                    XCommon.XLog.Write("成功从文件导入\"" + dialog.FileName + "\"数据！");
                }
            }
        }

        /// <summary>
        /// 导出基准重心包线数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportCoreMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel文件 (*.xls,*.xlsx)|*.xls;*.xlsx|xml文件 (*.xml)|*.xml|所有文件 (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.EndsWith(".xls") || dialog.FileName.EndsWith(".xlsx"))
                {
                    if (menu.Tag.ToString() == "0")
                    {
                        CommonUtil.ExportExcelFromPointList(this.coreAssessResult.datumCoreDataList, dialog.FileName);
                    }
                    else if (menu.Tag.ToString() == "1")
                    {
                        CommonUtil.ExportExcelFromPointList(this.coreAssessResult.assessCoreDataList, dialog.FileName);
                    }
                }
                else if (dialog.FileName.EndsWith(".xml"))
                {
                    if (menu.Tag.ToString() == "0")
                    {
                        CommonUtil.ExportXmlFromPointList(this.coreAssessResult.datumCoreDataList, dialog.FileName);
                    }
                    else if (menu.Tag.ToString() == "1")
                    {
                        CommonUtil.ExportXmlFromPointList(this.coreAssessResult.assessCoreDataList, dialog.FileName);
                    }

                }
            }
        }

        #region 自定义方法

        /// <summary>
        /// 保存基准/评估表格
        /// </summary>
        /// <param name="cpdList">List<CorePointData>类型</param>
        /// <param name="flag">0基准;1评估</param>
        public void saveCoreGridView(List<CorePointExt> cpdList, string flag)
        {
            DataGridView gridView = null;
            if (flag == "0")
            {
                this.coreAssessResult.datumCoreDataList = cpdList;
                gridView = this.datumCoreGridView;
                this.ExportCoreDatumMenuItem.Enabled = true;
                this.CoreAssessMenuItem.Enabled = true;
                //this.assessCoreGridView.Rows.Clear();
                //this.assessCoreDataList = null;
            }
            else if (flag == "1")
            {
                this.coreAssessResult.assessCoreDataList = cpdList;
                gridView = this.assessCoreGridView;
                this.ExportCoreAssessMenuItem.Enabled = true;
                this.btnCoreAssessConfig.Enabled = true;
                this.btnConfirm.Enabled = true;
                this.coreAssessResult.topMarginMin = 5;
                this.coreAssessResult.topMarginMax = 10;
                this.coreAssessResult.downMarginMin = 90;
                this.coreAssessResult.downMarginMax = 95;
            }

            if (cpdList == null)
            {
                return;
            }


            gridView.Rows.Clear();

            foreach (CorePointExt cpe in cpdList)
            {
                int i = gridView.Rows.Add();
                gridView.Rows[i].Cells[0].Value = cpe.pointName;
                gridView.Rows[i].Cells[1].Value = Math.Round(cpe.pointXValue, 6);
                gridView.Rows[i].Cells[2].Value = Math.Round(cpe.pointYValue, 6);
                if (flag == "1")
                {
                    gridView.Rows[i].Cells[3].Value = cpe.isAssess ? "评估" : "不评估";
                }
            }
        }

        /// <summary>
        /// 重心包线评估
        /// </summary>
        /// <param name="resut"></param>
        public void evaluationCoreMethod01(CoreAssessResult result)
        {
            // 查看基准重心包线是否完备
            if (result.datumCoreDataList == null)
            {
                return;
            }
            else if (result.datumCoreDataList.Count == 0)
            {
                return;
            }

            // 查看待评估重心包线是否完备
            if (result.assessCoreDataList == null)
            {
                return;
            }
            else if (result.assessCoreDataList.Count == 0)
            {
                return;
            }

            foreach (CorePointExt cp in result.assessCoreDataList)
            {
                if (cp.isAssess == true)
                {
                    // 交点X Y坐标列表
                    List<double> lstPointX = new List<double>();
                    List<double> lstPointY = new List<double>();

                    // 找出该点水平线与包线所有交点
                    for (int i = 0; i < result.datumCoreDataList.Count; i = i + 1)
                    {
                        double fStartX;
                        double fStartY;
                        double fEndX;
                        double fEndY;

                        if (i != result.datumCoreDataList.Count - 1)
                        {
                            fStartX = result.datumCoreDataList[i].pointXValue;
                            fStartY = result.datumCoreDataList[i].pointYValue;
                            fEndX = result.datumCoreDataList[i + 1].pointXValue;
                            fEndY = result.datumCoreDataList[i + 1].pointYValue;
                        }
                        else
                        {
                            fStartX = result.datumCoreDataList[i].pointXValue;
                            fStartY = result.datumCoreDataList[i].pointYValue;
                            fEndX = result.datumCoreDataList[0].pointXValue;
                            fEndY = result.datumCoreDataList[0].pointYValue;
                        }
                        // 超出Y范围
                        if (cp.pointYValue > fStartY && cp.pointYValue > fEndY)
                        {
                            continue;
                        }
                        else if (cp.pointYValue < fStartY && cp.pointYValue < fEndY)
                        {
                            continue;
                        }
                        else// 处理
                        {
                            // 若直线水平，则有两点相交
                            if (fStartY == fEndY)
                            {
                                lstPointX.Add(fStartX);
                                lstPointY.Add(fStartY);

                                lstPointX.Add(fEndX);
                                lstPointY.Add(fEndY);
                            }

                            // 若直线非水平，则只有一点相交
                            if (fStartY != fEndY)
                            {
                                // 若直线垂直，获得交点
                                if (fStartX == fEndX)
                                {
                                    lstPointX.Add(fStartX);
                                    lstPointY.Add(cp.pointYValue);
                                }
                                else // 若直线不垂直，使用直线方程获得交点
                                {
                                    lstPointX.Add((fEndX - fStartX) / (fEndY - fStartY) * (cp.pointYValue - fStartY) + fStartX);
                                    lstPointY.Add(cp.pointYValue);
                                }
                            }
                        }

                    }
                    // 找完交点后，根据交点求评估结果
                    // 排除Y值超限的点
                    if (lstPointX.Count == 0)
                    {
                        MessageBox.Show("待评估重心点" + cp.pointName + "的重量坐标位于基准评估重心范围外,自动剔除并平衡权重");
                        cp.isAssess = false;
                        cp.weightedValue = 0.0;
                        continue;
                    }
                    else
                    {
                        double fMaxX = lstPointX[0];
                        double fMinX = lstPointX[0];
                        //查找最大、最小横坐标
                        for (int i = 0; i < lstPointX.Count; i++)
                        {
                            if (fMaxX < lstPointX[i])
                            {
                                fMaxX = lstPointX[i];
                            }
                            if (fMinX > lstPointX[i])
                            {
                                fMinX = lstPointX[i];
                            }
                        }
                        // 排除范围外的点
                        if (cp.pointXValue < fMaxX && cp.pointXValue < fMinX)
                        {
                            MessageBox.Show("待评估重心点" + cp.pointName + "的横坐标位于基准评估重心范围外,自动剔除并平衡权重");
                            cp.isAssess = false;
                            cp.weightedValue = 0.0;
                            continue;
                        }
                        else if (cp.pointXValue > fMaxX && cp.pointXValue > fMinX)
                        {
                            MessageBox.Show("待评估重心点" + cp.pointName + "的横坐标位于基准评估重心范围外,自动剔除并平衡权重");
                            cp.isAssess = false;
                            cp.weightedValue = 0.0;
                            continue;
                        }
                        // 判断是否为定顶点
                        if (fMaxX == fMinX)
                        {
                            // 评估点位于顶点 评估结果为0
                            cp.assessValue = 0.0;
                        }
                        else// 非顶点
                        {
                            double fLength = (fMaxX - fMinX) * ((result.downMarginMax + result.downMarginMin) * 0.5 - (result.topMarginMax + result.topMarginMin) * 0.5);
                            double fMidX = fMinX + (fMaxX - fMinX) * ((result.topMarginMax - result.topMarginMin) * 0.5) + fLength / 2.0;
                            if (fLength <= 0)
                                cp.assessValue = 0.0;
                            else
                                cp.assessValue = 1 - Math.Abs(cp.pointXValue - fMidX) / fLength;
                        }
                    }

                }
            }
            // 根据修改后的待评估重心，重新设定权重，计算最终结果
            // 求和权重
            double fSum = 0.0;
            foreach (CorePointExt cp in result.assessCoreDataList)
            {
                if (cp.isAssess == true)
                {
                    fSum = cp.weightedValue + fSum;
                }
            }
            if (fSum <= 0.0)
            {
                MessageBox.Show("权重设置有误");
                return;
            }
            // 重新分配权重
            foreach (CorePointExt cp in result.assessCoreDataList)
            {
                if (cp.isAssess == true)
                {
                    cp.weightedValue = cp.weightedValue / fSum;
                }
            }
            // 计算评估结果
            result.evaluationResult = 0.0;
            foreach (CorePointExt cp in result.assessCoreDataList)
            {
                if (cp.isAssess == true)
                {
                    result.evaluationResult = result.evaluationResult + cp.weightedValue * cp.assessValue;
                }
            }
        }

        #endregion

    }
}

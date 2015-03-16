using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using Model;
using WeightCenterDesignAndEstimateSoft.Tool;
using XCommon;

namespace WeightCenterDesignAndEstimateSoft
{
    public partial class ReusltIssueDBForm : Form
    {
        WeightDataMangeForm dataForm = null;

        private DataGridView gridResult = null;
        private string strType = string.Empty;
        private Model.WeightArithmetic weightArithmetic = null;
        private string strDataName = string.Empty;
        private WeightAdjustmentResultData adjustData = null;

        private BLLWeightDesignData bllWeightDesign = new BLLWeightDesignData();
        private BLLCoreEnvelopeDesign bllCoreEnvelopeDesign = new BLLCoreEnvelopeDesign();

        public ReusltIssueDBForm()
        {
            InitializeComponent();
        }

        public ReusltIssueDBForm(string str_Type, Model.WeightArithmetic _weightArithmetic)
        {
            InitializeComponent();
            strType = str_Type;
            weightArithmetic = _weightArithmetic;

            SetTitle();
            txtDesignDataName.Text = weightArithmetic.DataName;
        }

        public ReusltIssueDBForm(string str_Type, WeightAdjustmentResultData _adjustData)
        {
            InitializeComponent();
            strType = str_Type;
            adjustData = _adjustData;

            SetTitle();
            txtDesignDataName.Text = adjustData.WeightAdjustName;
        }

        public ReusltIssueDBForm(DataGridView grid_Result, string str_Type, string str_DataName)
        {
            InitializeComponent();
            gridResult = grid_Result;
            strDataName = str_DataName;
            strType = str_Type;

            SetTitle();
            txtDesignDataName.Text = strDataName;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        private void SetTitle()
        {
            if (strType == "weightDesign")
            {
                this.Text = "重量设计结果数据库发布对话框";
            }
            if (strType == "coreEnvelopeDesign")
            {
                this.Text = "重心包线设计结果数据库发布对话框";
            }
        }

        /// <summary>
        /// 页面验证
        /// </summary>
        /// <returns></returns>
        private string PageVerification()
        {
            string strErroInfo = string.Empty;

            if (txtDesignDataName.Text == string.Empty)
            {
                strErroInfo = "请输入设计数据名称";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtDesignDataName.Text))
                {
                    strErroInfo = "设计数据名称不能输入非法字符";
                    return strErroInfo;
                }
            }

            if (txtDesignDataSumlieter.Text == string.Empty)
            {
                strErroInfo = "请输入设计数据提交者";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtDesignDataSumlieter.Text))
                {
                    strErroInfo = "设计数据提交者不能输入非法字符";
                    return strErroInfo;
                }
            }

            if (txtHelicopterName.Text == string.Empty)
            {
                strErroInfo = "请输入直升机名称";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckSignleString(txtHelicopterName.Text))
                {
                    strErroInfo = "直升机名称不能输入非法字符";
                    return strErroInfo;
                }
            }

            if (txtDesignTakingWeight.Text != string.Empty)
            {
                if (Verification.IsDoubleNumer(txtDesignTakingWeight.Text) == false)
                {
                    strErroInfo = "设计起飞重量格式错误";
                    return strErroInfo;
                }
            }

            if (txtDagtaRemark.Text != string.Empty)
            {
                if (Verification.IsCheckRemarkString(txtDagtaRemark.Text))
                {
                    strErroInfo = "数据备注不能输入非法字符";
                    return strErroInfo;
                }
            }

            return strErroInfo;
        }

        private string GetCoreEnvelope()
        {
            string strValue = string.Empty;
            object ptX = 0;
            object ptY = 0;

            for (int j = 1; j < gridResult.Columns.Count; j++)
            {
                ptX = gridResult.Rows[0].Cells[j].Value;
                ptY = gridResult.Rows[1].Cells[j].Value;
                if (ptX == null)
                {
                    ptX = 0;
                }
                if (ptY == null)
                {
                    ptY = 0;
                }
                strValue += gridResult.Columns[j].HeaderText + ":" + "横坐标(毫米)、纵坐标(千米)、"
                    + ptX.ToString() + "、" + ptY.ToString() + "|";
            }

            if (strValue != string.Empty)
            {
                strValue = strValue.Substring(0, strValue.Length - 1);
            }

            return strValue;
        }

        private WeightDesignData GetWeightDesignData()
        {
            if (dataForm == null)
            {
                dataForm = new WeightDataMangeForm();
            }

            WeightDesignData data = new WeightDesignData();

            data.Id = bllWeightDesign.GetMaxId();
            data.DesignData_Name = txtDesignDataName.Text;
            data.DesignData_Submitter = txtDesignDataSumlieter.Text;
            data.Helicopter_Name = txtHelicopterName.Text;
            data.DataRemark = txtDagtaRemark.Text;
            data.LastModify_Time = DateTime.Now.ToString();
            data.DesignTaking_Weight = (txtDesignTakingWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtDesignTakingWeight.Text));
            data.MainSystem_Name = string.Empty;
            if (weightArithmetic != null)
            {
                WeightSortData sortData = weightArithmetic.ExportDataToWeightSort();
                data.MainSystem_Name = WeightDataMangeForm.GetMainSystemWeight(sortData);
            }
            if (adjustData != null)
            {
                WeightSortData sortData = adjustData.weightAdjustData;
                data.MainSystem_Name = WeightDataMangeForm.GetMainSystemWeight(sortData);
            }
            return data;
        }

        private CoreEnvelopeDesign GetCoreEnvelopeDesignData()
        {
            CoreEnvelopeDesign data = new CoreEnvelopeDesign();

            data.Id = bllCoreEnvelopeDesign.GetMaxId();
            data.DesignData_Name = txtDesignDataName.Text;
            data.DesignData_Submitter = txtDesignDataSumlieter.Text;
            data.Helicopter_Name = txtHelicopterName.Text;
            data.DataRemark = txtDagtaRemark.Text;
            data.LastModify_Time = DateTime.Now.ToString();
            data.DesignTaking_Weight = (txtDesignTakingWeight.Text == string.Empty ? 0 : Convert.ToDouble(txtDesignTakingWeight.Text));

            data.CoreEnvelope = GetCoreEnvelope();

            return data;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string strErroInfo = PageVerification();
            if (strErroInfo != string.Empty)
            {
                XLog.Write(strErroInfo);
                return;
            }

            if (strType == "weightDesign")
            {
                WeightDesignData weight = GetWeightDesignData();

                bool IsAdd = bllWeightDesign.Add(weight);
                if (IsAdd)
                {
                    XLog.Write("重量设计结果发布数据库成功");
                }
            }
            if (strType == "coreEnvelopeDesign" || strType == "coreEnvelopeCut")
            {
                CoreEnvelopeDesign core = GetCoreEnvelopeDesignData();
                bool IsAdd = bllCoreEnvelopeDesign.Add(core);
                if (IsAdd)
                {
                    if (strType == "coreEnvelopeDesign")
                    {
                        XLog.Write("重心包线设计结果发布数据库成功");
                    }
                    if (strType == "coreEnvelopeCut")
                    {
                        XLog.Write("重心包线剪裁结果发布数据库成功");
                    }
                }
            }
            this.Close();
        }
    }
}

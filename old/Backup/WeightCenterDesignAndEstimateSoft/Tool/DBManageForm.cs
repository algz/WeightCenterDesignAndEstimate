using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCommon;
using System.Data.SqlClient;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class DBManageForm : Form
    {
        public DBManageForm()
        {
            InitializeComponent();
        }

        private void TryDBConnect()
        {
            bool IsConnect = false;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            XLog.Write("正在连接数据库...");
            try
            {
                connection.Open(); 
                if (connection.State == ConnectionState.Open)
                {
                    IsConnect = true;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                IsConnect = false;
                connection.Close();
            }

            if (IsConnect == false)
            {
                DialogResult result = MessageBox.Show("无法连接至数据库服务器,请确认配置是否正确,网络是否畅通？", "无法连接至数据库服务器...", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Retry)
                {
                    TryDBConnect();
                }
            }
        }

        #region 事件处理

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确认事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //数据库连接
           // TryDBConnect();
            this.Close();
            //型号重量数据
            if (radBtnTypeWeightData.Checked)
            {
                WeightDataMangeForm form = new WeightDataMangeForm();
                form.ShowDialog();
            }

            //重量设计数据
            if (radBtnWeightDesignData.Checked)
            {
                WeightDesignDataForm form = new WeightDesignDataForm();
                form.ShowDialog();
            }

            if (radCoreEnvelopeDesignDagta.Checked)
            {
                CoreEnvelopeDesignManageForm form = new CoreEnvelopeDesignManageForm();
                form.ShowDialog();
            }
            
        }

        #endregion
    }
}

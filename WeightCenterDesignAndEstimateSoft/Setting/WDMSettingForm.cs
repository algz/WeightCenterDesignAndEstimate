using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Xml;

namespace WeightCenterDesignAndEstimateSoft.Setting
{
    public partial class WDMSettingForm : Form
    {
        public WDMSettingForm()
        {
            InitializeComponent();
        }

        private void browsing_Click(object sender, EventArgs e)
        {
            wdmOpenFile.ShowDialog();
            this.wdmDBtxt.Text = wdmOpenFile.FileName;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.wdmDBtxt.Text == "")
            {
                MessageBox.Show("请输入WDM数据库文件");
                return;
            }
            CommonUtil.setWDMDBFilePath(this.wdmDBtxt.Text);
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WDMSettingForm_Load(object sender, EventArgs e)
        {
            string fileName = CommonUtil.getWDMDBFilePath();
            if (fileName != null && !fileName.Equals(""))
            {
                this.wdmDBtxt.Text = fileName;
            }

        }

       

    }
}

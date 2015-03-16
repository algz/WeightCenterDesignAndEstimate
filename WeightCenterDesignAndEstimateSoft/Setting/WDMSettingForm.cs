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
using System.IO;

namespace WeightCenterDesignAndEstimateSoft.Setting
{
    public partial class WDMSettingForm : Form
    {
        public WDMSettingForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //if (this.wdmDBtxt.Text == ""||!File.Exists(this.wdmDBtxt.Text))
            //{
            //    MessageBox.Show("请输入WDM数据库文件");
            //    return;
            //}
            //WDMIntegrationModule.setWDMDBFilePath(this.wdmDBtxt.Text);

            string msg="";
            if (testWDMConnection(out msg))
            {
                WDMIntegrationModule.setWDMDBConnectionStrings(msg);
                if (testWDMQueryName(out msg,msg))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(msg);
                }
            }
            else
            {
                MessageBox.Show(msg);
            }
            
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WDMSettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                string connectString = WDMIntegrationModule.getWDMDBConnectionStrings();
                if (connectString != "")
                {
                    string[] cs = connectString.Split(';');
                    this.IPtxt.Text = cs[0].Split('=')[1];
                    this.DBName.Text = cs[1].Split('=')[1];
                    this.DBUserName.Text = cs[2].Split('=')[1];
                    this.DBPassword.Text = cs[3].Split('=').Length == 2 ? cs[3].Split('=')[1] : ""; ;
                }

                this.AircTxt.Text=WDMIntegrationModule.getWDMDBQueryName("WDM_AIRC");
                this.OperTxt.Text = WDMIntegrationModule.getWDMDBQueryName("WDM_OPER");
                this.StmcTxt.Text = WDMIntegrationModule.getWDMDBQueryName("WDM_STMC");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg;
            if (!testWDMConnection(out msg))
            {
                MessageBox.Show(msg);
            }
            else
            {
                MessageBox.Show("数据库连接成功");
            }
        }

        private bool testWDMConnection(out string msg)
        {
            msg = "";
            if (this.IPtxt.Text == "")
            {
                msg= "请输入IP地址";
                return false;
            }
            else if (this.DBName.Text == "")
            {
                msg= "请输入数据库名称";
                return false;
            }
            else if (this.DBUserName.Text == "")
            {
                msg= "请输入用户名称";
                return false;
            }
            msg = "Data Source=" + this.IPtxt.Text + ";Initial Catalog=" + this.DBName.Text
                + ";User ID=" + this.DBUserName.Text + ";Password=" + this.DBPassword.Text;

            
            if (!WDMIntegrationModule.testWDMDBConnection(msg,null))
            {
                msg= "数据库连接失败";
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool testWDMQueryName(out string msg,string connectString)
        {
            msg = "";
            if (this.AircTxt.Text == "" || !WDMIntegrationModule.testWDMDBConnection(connectString, this.AircTxt.Text))
            {
                msg = "WDM的型号特征不存在";
                return false;
            }
            else
            {
                WDMIntegrationModule.setWDMDBQueryName("WDM_AIRC", this.AircTxt.Text);
            }
            if (this.OperTxt.Text == "" || !WDMIntegrationModule.testWDMDBConnection(connectString, this.OperTxt.Text))
            {
                msg = "WDM的质量特性不存在";
                return false;
            }
            else 
            {
                WDMIntegrationModule.setWDMDBQueryName("WDM_OPER", this.OperTxt.Text);
            }
            if (this.StmcTxt.Text == "" || !WDMIntegrationModule.testWDMDBConnection(connectString, this.StmcTxt.Text))
            {
                msg = "WDM的加载状态不存在";
                return false;
            }
            else
            {
                WDMIntegrationModule.setWDMDBQueryName("WDM_STMC", this.StmcTxt.Text);
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg;
            string connectionString = msg = "Data Source=" + this.IPtxt.Text + ";Initial Catalog=" + this.DBName.Text
                + ";User ID=" + this.DBUserName.Text + ";Password=" + this.DBPassword.Text;
            if (testWDMQueryName(out msg, connectionString))
            {
                MessageBox.Show("测试成功");
            }
            else 
            {
                MessageBox.Show(msg);
            };
        }
    }
}

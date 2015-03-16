using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCommon;
using System.Threading;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;

namespace WeightCenterDesignAndEstimateSoft.Setting
{
    public partial class DBServerSettingFormcs : Form
    {
        public DBServerSettingFormcs()
        {
            InitializeComponent();
            SetPageData();
        }

        private void SetPageData()
        {
            string strFileName = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileName);

            bool IsDisplay = false;
            string strDisplayPwd = string.Empty;
            string strPwd = string.Empty;
            string strUserName = string.Empty;

            string strKeyUserName = "username";
            string strKeyPwd = "password";
            string strKeyRemberpwd = "remberpwd";

            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null)
                {
                    if (att.Value == strKeyUserName)
                    {
                        //对目标元素中的属性赋值
                        att = nodes[i].Attributes["value"];
                        strUserName = att.Value;
                    }
                    if (att.Value == strKeyRemberpwd)
                    {
                        att = nodes[i].Attributes["value"];
                        strDisplayPwd= att.Value;
                    }
                    if (att.Value == strKeyPwd)
                    {
                        att = nodes[i].Attributes["value"];
                        strPwd = att.Value;
                    }
                   
                }
            }

            txtUserName.Text = strUserName;
            IsDisplay = (strDisplayPwd == string.Empty ? false : Convert.ToBoolean(strDisplayPwd));

            if (IsDisplay)
            {
                txtPwd.Text = strPwd;

            }
            chkRemindPwd.Checked = IsDisplay;
        }

        //保存服务器信息
        private void SaveSerVerInfo()
        {
            //写入Config.config
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(strFileName);

            //找出名称为“add”的所有元素

            string strKeyUserName = "username";
            string strKeyPwd = "password";
            string strKeyRemberpwd = "remberpwd";

            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null)
                {
                    if (att.Value == strKeyUserName)
                    {
                        //对目标元素中的属性赋值
                        att = nodes[i].Attributes["value"];
                        att.Value = txtUserName.Text;
                    }
                    if (att.Value == strKeyPwd)
                    {
                        att = nodes[i].Attributes["value"];
                        att.Value = txtPwd.Text;
                    }
                    if (att.Value == strKeyRemberpwd)
                    {
                        att = nodes[i].Attributes["value"];
                        att.Value = chkRemindPwd.Checked.ToString();
                    }
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            string strUserName = txtUserName.Text;
            string strPwd = txtPwd.Text;

            if (strUserName == string.Empty)
            {
                XLog.Write("请输入用户名");
                return;
            }
            if (strPwd == string.Empty)
            {
                XLog.Write("请输入密码");
                return;
            }

            bool IsConnect = false;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                IsConnect = true;
                XLog.Write("正在连接...");
            }
            catch (Exception ex)
            {
                XLog.Write("连接失败");
            }
            //连接成功
            if (IsConnect)
            {
                SaveSerVerInfo();

                XLog.Write("测试连接成功");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using XCommon;
using System.Net;
using System.Web.Services.Protocols;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class BasicDBSetForm : Form
    {
        private WeightDataMangeForm form = null;
        private WebResponse myWebResponse = null;

        public BasicDBSetForm()
        {
            InitializeComponent();
        }

        public BasicDBSetForm(WeightDataMangeForm _form)
        {
            InitializeComponent();
            form = _form;
        }

        private void SetPageData()
        {
            string strFileName = System.AppDomain.CurrentDomain.BaseDirectory + "WES.exe.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileName);

            string strServer = string.Empty;
            string strUserName = string.Empty;
            string strPwd = string.Empty;
            string strUrl = string.Empty;
            string strFolder = string.Empty;

            string strKeySever = "Server";
            string strKeyUserName = "username";
            string strKeyPwd = "password";
            string strKeyUrl = "url";
            string strkeyFolder = "folder";
            string strkeyWDMFileName = "WDMFileName";

            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null)
                {
                    if (att.Value == strKeySever)
                    {
                        //对目标元素中的属性赋值
                        att = nodes[i].Attributes["value"];
                        strServer = att.Value;
                    }
                    if (att.Value == strKeyUserName)
                    {
                        //对目标元素中的属性赋值
                        att = nodes[i].Attributes["value"];
                        strUserName = att.Value;
                    }
                    if (att.Value == strKeyPwd)
                    {
                        att = nodes[i].Attributes["value"];
                        strPwd = att.Value;
                    }
                    if (att.Value == strKeyUrl)
                    {
                        att = nodes[i].Attributes["value"];
                        strUrl = att.Value;
                    }
                    if (att.Value == strkeyFolder)
                    {
                        att = nodes[i].Attributes["value"];
                        strFolder = att.Value;
                    }
                    if (att.Value == strkeyWDMFileName)
                    {
                        att = nodes[i].Attributes["value"];
                        strFolder = att.Value;
                    }
                }
            }

            txtServer.Text = strServer;
            txtUserName.Text = strUserName;
            txtPwd.Text = strPwd;
            txtUrl.Text = strUrl;
            txtFileFolder.Text = strFolder;
        }

        //保存服务器信息
        private void SaveSerVerInfo()
        {
            //写入配置.config
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = System.AppDomain.CurrentDomain.BaseDirectory + "WES.exe.config";
            doc.Load(strFileName);

            //找出名称为“add”的所有元素

            string strKeySever = "Server";
            string strKeyUserName = "username";
            string strKeyPwd = "password";
            string strKeyUrl = "url";
            string strkeyFolder = "folder";


            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null)
                {
                    if (att.Value == strKeySever)
                    {
                        att = nodes[i].Attributes["value"];
                        att.Value = txtServer.Text.Trim();
                    }
                    if (att.Value == strKeyUserName)
                    {
                        att = nodes[i].Attributes["value"];
                        att.Value = txtUserName.Text;
                    }
                    if (att.Value == strKeyPwd)
                    {
                        att = nodes[i].Attributes["value"];
                        att.Value = txtPwd.Text;
                    }
                    if (att.Value == strKeyUrl)
                    {
                        att = nodes[i].Attributes["value"];
                        att.Value = txtUrl.Text.Trim();
                    }
                    if (att.Value == strkeyFolder)
                    {
                        att = nodes[i].Attributes["value"];
                        att.Value = txtFileFolder.Text.Trim();
                    }
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }

        /// <summary>
        /// 网络凭证
        /// </summary>
        /// <returns></returns>
        public NetworkCredential MyCred()
        {
            string loginUser = txtUserName.Text;
            string loginPSW = txtPwd.Text;//密码
            string loginHost = txtServer.Text;//主机名，可以是IP地址，也可以服务器名称
            NetworkCredential myCred = new NetworkCredential(loginUser, loginPSW, loginHost);
            return myCred;
        }

        /// <summary>
        /// 测试服务器连接
        /// </summary>
        private bool TestServerConnect()
        {
            try
            {
                //判断输入内容
                string strServer = txtServer.Text.Trim();
                string strUserName = txtUserName.Text;
                string strPwd = txtPwd.Text;
                string strUrl = txtUrl.Text.Trim();

                if (strServer == string.Empty)
                {
                    MessageBox.Show("请输入服务器");
                    return false;
                }
                if (strUserName == string.Empty)
                {
                    MessageBox.Show("请输入用户名");
                    return false;
                }
                if (strPwd == string.Empty)
                {
                    MessageBox.Show("请输入密码");
                    return false;
                }
                if (strUrl == string.Empty)
                {
                    MessageBox.Show("请输入Url地址");
                    return false;
                }
                else
                {
                    if (Verification.IsCheckUrl(strUrl) == false)
                    {
                        MessageBox.Show("Url地址格式错误");
                        return false;
                    }
                }
                if (txtFileFolder.Text == string.Empty)
                {
                    MessageBox.Show("请输入共享文件目录");
                    return false;
                }

                //身份验证
                WebRequest myWebRequest = WebRequest.Create(strUrl);//根据URL创建一个连接请求
                myWebRequest.Credentials = MyCred();
                myWebRequest.Timeout = 2000;//单位为毫秒
                myWebResponse = myWebRequest.GetResponse();

                MessageBox.Show("配置成功");
                return true;
            }
            catch (WebException wex)//无法连接到服务器，可能是因为服务器错误或用户名与密码错误
            {
                if (myWebResponse != null)//毁销
                {
                    myWebResponse.Close();
                    myWebResponse = null;
                    MessageBox.Show(wex.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (myWebResponse != null)
                {
                    myWebResponse.Close();
                    myWebResponse = null;
                    MessageBox.Show("服务器连接失败:"+ex.Message);
                    return false;

                }
            }

            return true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //从型号重量数据库进入
            if (form != null)
            {
                Model.BasicDBSetting dbSetting = new Model.BasicDBSetting();
                dbSetting.strServer = txtServer.Text.Trim();
                dbSetting.strUserName = txtUserName.Text;
                dbSetting.strUserName = txtPwd.Text;
                dbSetting.strUrl = txtUrl.Text.Trim();
                dbSetting.strFolder = txtFileFolder.Text.Trim();

                form.BasicDBGetData(string.Empty);
            }

            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BasicDBSetForm_Load(object sender, EventArgs e)
        {
            SetPageData();
        }

        private void txtFileFolder_TextChanged(object sender, EventArgs e)
        {

        }

        private void labFileFolder_Click(object sender, EventArgs e)
        {

        }
    }
}

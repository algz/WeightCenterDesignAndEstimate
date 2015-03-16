using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class TemplateSetForm : Form
    {
        private WeightDataMangeForm form = null;

        public TemplateSetForm()
        {
            InitializeComponent();
        }

        public TemplateSetForm(WeightDataMangeForm _form)
        {
            InitializeComponent();
            form = _form;
        }

        private void TemplateSetForm_Load(object sender, EventArgs e)
        {
            string strFileName = System.AppDomain.CurrentDomain.BaseDirectory + "WES.exe.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(strFileName);

            string strKeyTemplate = "Template";

            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null)
                {
                    if (att.Value == strKeyTemplate)
                    {
                        //对目标元素中的属性赋值
                        att = nodes[i].Attributes["value"];
                        txtPath.Text = att.Value;
                    }
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == string.Empty)
            {
                MessageBox.Show("请输入模板地址");
                return;
            }
            else
            {
                if (File.Exists(txtPath.Text) == false)
                {
                    MessageBox.Show("模板不存在,请重新输入模板地址");
                    return;
                }
            }
            //保存模板地址
            //写入配置.config
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = System.AppDomain.CurrentDomain.BaseDirectory + "WES.exe.config";
            doc.Load(strFileName);

            //找出名称为“add”的所有元素
            string strKeyTemplate = "Template";

            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null)
                {
                    if (att.Value == strKeyTemplate)
                    {
                        att = nodes[i].Attributes["value"];
                        att.Value = txtPath.Text.Trim();
                    }
                }
            }
            //保存上面的修改
            doc.Save(strFileName);

            if (form != null)
            {
                form.BasicDBGetData(txtPath.Text.Trim());
            }
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //打开文件
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "All Types (*.ideprj,*.ide)|*.ideprj;*.ide";
            fileDialog.RestoreDirectory = true;
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = fileDialog.FileName;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

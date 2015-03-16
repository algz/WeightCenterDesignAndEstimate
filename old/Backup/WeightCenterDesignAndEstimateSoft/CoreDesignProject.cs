using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCommon;
using Model;

namespace WeightCenterDesignAndEstimateSoft
{
    public partial class CoreDesignProject : Form
    {
        private MainForm mainForm = null;
        private string strOperType = string.Empty;

        public CoreDesignProject()
        {
            InitializeComponent();
        }
        public CoreDesignProject(MainForm main_Form, string str_OperType)
        {
            InitializeComponent();
            mainForm = main_Form;

            strOperType = str_OperType;

            if (mainForm.designProjectData != null)
            {
                if (strOperType != "new")
                {
                    SetPageData();
                }
            }
        }

        /// <summary>
        /// 页面验证
        /// </summary>
        /// <returns></returns>
        private string PageVerification()
        {
            string strErroInfo = string.Empty;

            if (txtProjectName.Text == string.Empty)
            {
                strErroInfo = "请输入工程名称";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtProjectName.Text))
                {
                    strErroInfo = "工程名称不能输入非法字符";
                    return strErroInfo;
                }
            }

            if (txtCreator.Text == string.Empty)
            {
                strErroInfo = "请输入创建者";
                return strErroInfo;
            }
            else
            {
                if (Verification.IsCheckString(txtCreator.Text))
                {
                    strErroInfo = "创建者不能输入非法字符";
                    return strErroInfo;
                }
            }

            if (txtRemark.Text != string.Empty)
            {
                if (Verification.IsCheckRemarkString(txtRemark.Text))
                {
                    strErroInfo = "备注不能输入非法字符";
                    return strErroInfo;
                }
            }

            return strErroInfo;
        }

        private void SetPageData()
        {
            txtProjectName.Text = mainForm.designProjectData.projectName;
            txtCreator.Text = mainForm.designProjectData.projectCreator; ;
            txtRemark.Text = mainForm.designProjectData.strRemark;
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

            if (strOperType == "new")
            {
                DesignProjectData projectData = new DesignProjectData();

                projectData.projectName = txtProjectName.Text;
                projectData.projectCreator = txtCreator.Text;
                projectData.strRemark = txtRemark.Text;

                mainForm.BindProjectTreeData(projectData);

                XLog.Write("新建工程成功");
                mainForm.InitializePage("new");
            }
            if (strOperType == "edit")
            {
                mainForm.designProjectData.projectName = txtProjectName.Text;
                mainForm.designProjectData.projectCreator = txtCreator.Text;
                mainForm.designProjectData.strRemark = txtRemark.Text;

                mainForm.BindProjectTreeData(mainForm.designProjectData);

                XLog.Write("编辑工程成功");
                mainForm.InitializePage("edit");
            }
            this.Close();
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

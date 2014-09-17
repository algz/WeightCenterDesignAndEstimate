using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeightCenterDesignAndEstimateSoft
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            label1.Text = "直升机重量重心设计与评估软件";
            label2.Text = "Version 1.0";
            label3.Text = "版权所有 (C) 2013";
            label4.Text = "金航数码科技有限责任公司";
            label5.Text = "http://www.avicit.com";

            string strText = "警告:本计算机程序受版权法和国际条约保护。如果未经授权而擅自复制或传播本程序(或其中任何部分),";
            strText += "将受到严厉的民事和刑事制裁,并将在法律许可的最大限度内受到起诉。";
            labWarm.Text = strText;
        }

    }
}

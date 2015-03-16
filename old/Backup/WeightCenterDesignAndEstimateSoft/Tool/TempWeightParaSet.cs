using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Model;

namespace WeightCenterDesignAndEstimateSoft.Tool
{
    public partial class TempWeightParaSet : Form
    {
        private List<WeightParameter> TempParaList = null;

        public TempWeightParaSet(List<WeightParameter> paraList)
        {
            InitializeComponent();
            TempParaList = paraList;
            foreach(WeightParameter wp in TempParaList)
            {
                listViewPara.Items.Add(wp.ParaName);
            }
            listViewPara.Items[0].Selected = true;
        }

        private void listViewPara_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPara.SelectedItems.Count != 0)
            {
                WeightParameter wp = TempParaList[listViewPara.SelectedItems[0].Index];
                textBoxParaUnit.Text = wp.ParaUnit;
                textBoxParaRemark.Text = wp.ParaRemark;
            }
            else
            {
                textBoxParaUnit.Text = "";
                textBoxParaRemark.Text = "";
            }
            textBoxParaUnit.Modified = false;
            textBoxParaRemark.Modified = false;        
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (textBoxParaUnit.Modified || textBoxParaRemark.Modified)
            {
                WeightParameter wp = TempParaList[listViewPara.SelectedItems[0].Index];
                
                if(wp.ParaUnit == textBoxParaUnit.Text && wp.ParaRemark == textBoxParaRemark.Text)
                {
                    return;
                }
                
                wp.ParaUnit = textBoxParaUnit.Text;
                wp.ParaRemark = textBoxParaRemark.Text;

                listViewPara.SelectedItems[0].SubItems[0].Text = wp.ParaName + " *";

                textBoxParaUnit.Modified = false;
                textBoxParaRemark.Modified = false;
            }
        }
    }
}

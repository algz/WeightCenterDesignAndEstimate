using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace WeightCenterDesignAndEstimateSoft.Task.WeightAssessment.AssessmentWeightData
{
    public partial class FromWDMCore : Form
    {
        private WDMIntegrationModule.Air[] airs;
        private string wdmFile;
        public FromWDMCore()
        {
            InitializeComponent();
        }

        private void FromWDM_Load(object sender, EventArgs e)
        {
            this.wdmFile = CommonUtil.getWDMDBFilePath();
            if (wdmFile == "")
            {
                return;
            }
            this.airs = WDMIntegrationModule.getAircs(wdmFile);

            TreeNode parentNode = new TreeNode("WDM机型列表");
            this.WDMTree.Nodes.Add(parentNode);
            for (int i = 0; i < airs.Length; i++)
            {
                TreeNode childNode = new TreeNode();
                childNode.Name = airs[i].ID;
                childNode.Text = airs[i].MC;
                childNode.Tag = i;
                parentNode.Nodes.Add(childNode);
            }
            this.WDMTree.ExpandAll();

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            TreeNode node = this.WDMTree.SelectedNode;
            if (node.Level != 1)
            {
                MessageBox.Show("请选择机型");
                return;
            }

            WDMIntegrationModule.Air air = airs[Convert.ToInt32(node.Tag)];
            WDMIntegrationModule.TOper[] topers = WDMIntegrationModule.GetOpers(node.Name, wdmFile);
            //Random r = new Random();
            
            List<WeightData> lstWeightData = new List<WeightData>();
            WeightData parentWD = new WeightData();
            parentWD.weightName = air.MC;
            parentWD.nID = 0;
            
            parentWD.nParentID = -1;
            lstWeightData.Add(parentWD);
            
            //int i = r.Next(10);
            double weightValueSum = 0;
            for(int i=0;i<topers.Length;i++)
            {
                WDMIntegrationModule.TOper toper = topers[i];
                if (node.Name == air.ID)
                {
                    WeightData wd = new WeightData();
                    wd.weightName = toper.MC;
                    wd.nID = i+1;// Convert.ToInt32(toper.ID);
                    wd.weightValue = this.actualDataRadio.Checked ?
                        (toper.SCW == 0 && this.useTestData.Checked ?
                        toper.ZTW : toper.SCW) : toper.ZTW;
                    wd.nParentID = 0;

                    // 添加重量数据至重量数据列表
                    lstWeightData.Add(wd);

                    weightValueSum += wd.weightValue;
                }
            }
            parentWD.weightValue = weightValueSum;
            WeightSortData wsd = new WeightSortData();
            wsd.lstWeightData = lstWeightData;
            ((WeightAssessmentForm)this.Owner).saveWeightDataGridView(wsd, 2);

            this.Close();
        }
    }
}

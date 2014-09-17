using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using Model.assessData;

namespace WeightCenterDesignAndEstimateSoft.Task.CoreEnvelopeAssessment
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
            
            if (node == null||node.Level!=1)
            {
                MessageBox.Show("请选择机型");
                return;
            }

            WDMIntegrationModule.Air air = airs[Convert.ToInt32(node.Tag)];
            WDMIntegrationModule.TStmc[] tstmcs = WDMIntegrationModule.GetStmcs(node.Name, wdmFile);
            Random r = new Random();

            
             List <CorePointExt> cpdList=new List<CorePointExt>();


            //List<WeightData> lstWeightData = new List<WeightData>();
            //CorePointExt parentWD = new CorePointExt();
            //parentWD.pointName = air.MC;
            //parentWD. = 0;
            
            //parentWD.nParentID = -1;
            //lstWeightData.Add(parentWD);
            
            //int i = r.Next(10);
            //double weightValueSum = 0;
            foreach (WDMIntegrationModule.TStmc tstmc in tstmcs)
            {
                if (node.Name == air.ID)
                {
                   CorePointExt cpe = new CorePointExt();
                //            gridView.Rows[i].Cells[0].Value = cpe.pointName;
                //gridView.Rows[i].Cells[1].Value = Math.Round(cpe.pointXValue, 6);
                //gridView.Rows[i].Cells[2].Value = Math.Round(cpe.pointYValue, 6);

                    //WeightData wd = new WeightData();
                    cpe.pointName = tstmc.MC;
                    
                    cpe.pointYValue = tstmc.ZTW;
                    cpe.pointXValue =(this.actualDataRadio.Checked?tstmc.XCG:tstmc.YCG)*1000;
                    

                    //wd.nID = r.Next(10);// Convert.ToInt32(toper.ID);
                    //wd.weightValue = this.actualDataRadio.Checked ?
                    //    (tstmc.SCW == 0 && this.useTestData.Checked ?
                    //    tstmc.ZTW : tstmc.SCW) : tstmc.ZTW;
                    //wd.nParentID = 0;

                    // 添加重量数据至重量数据列表
                    cpdList.Add(cpe);

                   // weightValueSum += wd.weightValue;
                }
            }
            //parentWD.weightValue = weightValueSum;
            ((CoreEnvelopeAssessForm)this.Owner).saveCoreGridView(cpdList, "1");

            this.Close();
        }

        private void WDMTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnConfirm_Click(sender, e);
        }
    }
}

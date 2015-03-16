using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using WeightCenterDesignAndEstimateSoft.WDM;
using System.Threading;

namespace WeightCenterDesignAndEstimateSoft.Task.WeightAssessment.AssessmentWeightData
{
    public partial class FromWDMCore : Form
    {
        private WDMIntegrationModule.Air[] airs;
        private OpaqueCommand cmd = new OpaqueCommand();
        public delegate void CallBackDelegate(string msg);
        Thread thread;
        public FromWDMCore()
        {
            InitializeComponent();
        }

        private void FromWDM_Load(object sender, EventArgs e)
        {
            //this.wdmFile = WDMIntegrationModule.getWDMDBFilePath();
            //if (wdmFile == "")
            //{
            //    this.Close();
            //    return;
            //}
            //把回调的方法给委托变量
            
            //CallBackDelegate cbd = callBack;
            this.thread = new Thread(loadWDMDB);
            thread.IsBackground = true;
            thread.Start();// (new CallBackDelegate(callBack));
            cmd.ShowOpaqueLayer(this, 123, true);

        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            TreeNode node = this.WDMTree.SelectedNode;
            if (node==null||node.Level != 1)
            {
                MessageBox.Show("请选择机型");
                return;
            }

            WDMIntegrationModule.Air air = airs[Convert.ToInt32(node.Tag)];
            WDMIntegrationModule.TOper[] topers = WDMIntegrationModule.GetOpers(node.Name);
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

        private void WDMTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            btnConfirm_Click(sender, e);
        }

        private void loadWDMDB()
        {
            this.airs = WDMIntegrationModule.getAircs();//取数据源动作
            string msg = "";
            if (this.airs == null)
            {
                msg="机型没有读取成功,请检查参数配置.";
            }
            //else
            //{
            //    TreeNode parentNode = new TreeNode("WDM机型列表");
            //    this.WDMTree.Nodes.Add(parentNode);
            //    for (int i = 0; i < airs.Length; i++)
            //    {
            //        TreeNode childNode = new TreeNode();
            //        childNode.Name = airs[i].ID;
            //        childNode.Text = airs[i].MC;
            //        childNode.Tag = i;
            //        parentNode.Nodes.Add(childNode);
            //    }
            //    this.WDMTree.ExpandAll();
            //}

            //执行回调.
            if (this.InvokeRequired)
            {
                CallBackDelegate cbd = new CallBackDelegate(callBack);
                this.BeginInvoke(cbd, msg); //异步调用回调
                //this.Invoke(cbd, msg); //同步调用回调
            }
            else
            {
                this.Close();
            }
        }

        private void callBack(string msg)
        {
            cmd.HideOpaqueLayer();
            if (msg != "")
            {
                MessageBox.Show(msg);
                this.Close();
            }
            else
            {
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
            
        }

        private void FromWDMCore_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.thread.Abort();
        }
    }
}

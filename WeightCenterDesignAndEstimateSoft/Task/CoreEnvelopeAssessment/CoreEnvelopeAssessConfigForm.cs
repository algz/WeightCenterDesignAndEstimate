using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.assessData;

namespace WeightCenterDesignAndEstimateSoft.Task.CoreEnvelopeAssessment
{
    public partial class CoreEnvelopeAssessConfigForm : Form
    {

        private CoreEnvelopeAssessForm ownerForm;
        //private double assessSum;
        private List<CorePointExt> assessCoreDatas;

        public CoreEnvelopeAssessConfigForm()
        {
            InitializeComponent();
        }

        private void CoreEnvelopeAssessConfigForm_Load(object sender, EventArgs e)
        {
            this.ownerForm = (CoreEnvelopeAssessForm)this.Owner;
            assessCoreDatas=ownerForm.coreAssessResult.assessCoreDataList.Select(item => (CorePointExt)item.Clone()).ToList();   
            foreach (CorePointExt cpe in assessCoreDatas)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = cpe.id;
                item.Checked = cpe.isAssess;
                item.Text = cpe.pointName;
                item.Name = cpe.pointName;
                this.coreAssesslistView.Items.Add(item);
            }

                this.txtTopMarginMin.Text = (Convert.ToDouble(ownerForm.coreAssessResult.topMarginMin) * 100).ToString();
            this.txtTopMarginMax.Text = (Convert.ToDouble(ownerForm.coreAssessResult.topMarginMax) * 100).ToString();
            this.txtDownMarginMin.Text = (Convert.ToDouble(ownerForm.coreAssessResult.downMarginMin) * 100).ToString();
            this.txtDownMarginMax.Text = (Convert.ToDouble(ownerForm.coreAssessResult.downMarginMax) * 100).ToString();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int[] i = {Convert.ToInt32(this.txtTopMarginMin.Text),Convert.ToInt32(this.txtTopMarginMax.Text),
                   Convert.ToInt32(this.txtDownMarginMin.Text),Convert.ToInt32(this.txtDownMarginMax.Text)};
            if (!(i[0] < i[1] && i[1] < i[2] && i[2] < i[3]))
            {
                MessageBox.Show("请按前限前裕度<前限后裕度<后限前裕度<后限后裕度");
                return;
            }

            ownerForm.saveCoreGridView(this.assessCoreDatas, "1");
            CoreAssessResult car=this.ownerForm.coreAssessResult;
            car.topMarginMin = Convert.ToDouble(this.txtTopMarginMin.Text)/100;
            car.topMarginMax = Convert.ToDouble(this.txtTopMarginMax.Text)/100;
            car.downMarginMin = Convert.ToDouble(this.txtDownMarginMin.Text)/100;
            car.downMarginMax = Convert.ToDouble(this.txtDownMarginMax.Text)/100;
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void weightedGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            double NewVal = 0;
            if (e.ColumnIndex == 1)
            {
                if (!double.TryParse(e.FormattedValue.ToString(), out NewVal) || NewVal < 0 || NewVal > 1)
                {
                    e.Cancel = true;
                    MessageBox.Show("只能输入大于0小于1的数字");
                    return;
                }
                else
                {
                    string pointID = this.weightedGridView.Rows[e.RowIndex].Cells[0].Tag.ToString();
                    CorePointExt cpe = this.assessCoreDatas.Find(item => item.pointName == pointID);
                    //this.assessSum -= (cpe.weightedValue - Convert.ToDouble(e.FormattedValue));
                    cpe.weightedValue = Convert.ToDouble(e.FormattedValue);
                    
                }
            }
        }

        private void coreAssesslistView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CorePointExt cpe = this.assessCoreDatas.Find(item => item.id.Equals(e.Item.Tag.ToString()));
            if (e.Item.Checked)
            {
                cpe.weightedValue = cpe.isAssess?cpe.weightedValue:1;
                cpe.isAssess = true;
                //this.assessSum += cpe.weightedValue;

                int i=this.weightedGridView.Rows.Add();
                this.weightedGridView.Rows[i].Cells[0].Value = cpe.pointName;
                this.weightedGridView.Rows[i].Cells[1].Value = cpe.weightedValue;
                this.weightedGridView.Rows[i].Tag = cpe.id;
            }
            else
            {
                foreach(DataGridViewRow row in  this.weightedGridView.Rows)
                {
                    if (row.Tag.ToString() == e.Item.Tag.ToString())
                    {
                        //this.assessSum -= cpe.weightedValue;
                        cpe.isAssess = false;
                        cpe.weightedValue = 0;
                        this.weightedGridView.Rows.Remove(row);
                        
                        return;
                    }
                }
            }
            
            
        }

        private void btnWeightedRest_Click(object sender, EventArgs e)
        {
            this.weightedGridView.Rows.Clear();
            double _assessSum = 0;
            //权重值检验
            foreach (CorePointExt cpe in this.assessCoreDatas)
            {
                if (cpe.isAssess)
                {
                    _assessSum += cpe.weightedValue;
                }
            }
            if (_assessSum == 0)
            {
                MessageBox.Show("权重总值不能为0");
                return;
            }

            //_assessSum = 0;
            foreach (CorePointExt cpe in this.assessCoreDatas)
            {
                if (cpe.isAssess)
                {
                    
                    cpe.weightedValue = cpe.weightedValue / _assessSum;

                    int i=this.weightedGridView.Rows.Add();
                    this.weightedGridView.Rows[i].Cells[0].Value = cpe.pointName;
                    this.weightedGridView.Rows[i].Cells[1].Value = cpe.weightedValue;
                    this.weightedGridView.Rows[i].Tag = cpe.id;
                }
            }
            //this.assessSum=_assessSum;
        }

        private bool nonNumberEntered = false;//true:拒绝；false允许
        private void txtTopMarginMin_KeyDown(object sender, KeyEventArgs e)
       {
            nonNumberEntered = false;
            if ((e.KeyCode < Keys.D0) || (e.KeyCode > Keys.D9 && e.KeyCode < Keys.NumPad0) || (e.KeyCode > Keys.NumPad9))
            {
                if (e.KeyCode != Keys.Back)
                {
                    //int[] i = {Convert.ToInt32(this.txtTopMarginMin.Text),Convert.ToInt32(this.txtTopMarginMax.Text),
                    //       Convert.ToInt32(this.txtDownMarginMin.Text),Convert.ToInt32(this.txtDownMarginMax.Text)};
                    //if (i[0] < i[1] && i[1] < i[2] && i[2] < i[3])


                    nonNumberEntered = true;
                    //}

                }
            }
            else
            {
                //输入数值
                TextBox txt = (TextBox)sender;
                if (Convert.ToInt32(txt.Text) > 100)
                {
                    nonNumberEntered = true;
                }
            }
        }

        private void txtTopMarginMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered)
            {
                e.Handled = true;
            }
        }
    }
}

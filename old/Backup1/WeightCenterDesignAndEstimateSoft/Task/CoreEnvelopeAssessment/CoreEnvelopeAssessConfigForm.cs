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
        private double assessSum;
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
                item.Checked = cpe.isAssess;
                item.Text = cpe.pointName;
                item.Name = cpe.pointName;
                this.coreAssesslistView.Items.Add(item);
            }

                this.txtTopMarginMin.Text = ownerForm.coreAssessResult.topMarginMin.ToString();
                this.txtTopMarginMax.Text = ownerForm.coreAssessResult.topMarginMax.ToString();
                this.txtDownMarginMin.Text = ownerForm.coreAssessResult.downMarginMin.ToString();
                this.txtDownMarginMax.Text = ownerForm.coreAssessResult.downMarginMax.ToString();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ownerForm.saveCoreGridView(this.assessCoreDatas, "1");
            CoreAssessResult car=this.ownerForm.coreAssessResult;
            car.topMarginMin = Convert.ToDouble(this.txtTopMarginMin.Text);
            car.topMarginMax = Convert.ToDouble(this.txtTopMarginMax.Text);
            car.downMarginMin = Convert.ToDouble(this.txtDownMarginMin.Text);
            car.downMarginMax = Convert.ToDouble(this.txtDownMarginMax.Text);
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
                    string pointName = this.weightedGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    CorePointExt cpe=this.assessCoreDatas.Find(item => item.pointName == pointName);
                    this.assessSum -= (cpe.weightedValue - Convert.ToDouble(e.FormattedValue));
                    cpe.weightedValue = Convert.ToDouble(e.FormattedValue);
                    
                }
            }
        }

        private void coreAssesslistView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CorePointExt cpe=this.assessCoreDatas.Find(item => item.pointName == e.Item.Name);
            if (e.Item.Checked)
            {
                cpe.weightedValue = cpe.isAssess?cpe.weightedValue:1;
                cpe.isAssess = true;
                this.assessSum += cpe.weightedValue;

                int i=this.weightedGridView.Rows.Add();
                this.weightedGridView.Rows[i].Cells[0].Value = cpe.pointName;
                this.weightedGridView.Rows[i].Cells[1].Value = cpe.weightedValue;
            }
            else
            {
                foreach(DataGridViewRow row in  this.weightedGridView.Rows)
                {
                    if (row.Cells[0].Value.ToString() == e.Item.Name)
                    {
                        this.assessSum -= cpe.weightedValue;
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
            foreach (CorePointExt cpe in this.assessCoreDatas)
            {
                if (cpe.isAssess)
                {
                    
                    cpe.weightedValue = cpe.weightedValue / assessSum;
                    _assessSum += cpe.weightedValue;

                    int i=this.weightedGridView.Rows.Add();
                    this.weightedGridView.Rows[i].Cells[0].Value = cpe.pointName;
                    this.weightedGridView.Rows[i].Cells[1].Value = cpe.weightedValue;
                }
            }
            this.assessSum=_assessSum;
        }
    }
}

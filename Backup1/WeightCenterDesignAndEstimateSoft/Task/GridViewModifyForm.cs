using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace WeightCenterDesignAndEstimateSoft.Task
{
    public partial class GridViewModifyForm : Form
    {
        public GridViewModifyForm(object datasrc, List<string> headerlst, int nFixedColCount)
        {
            InitializeComponent();

            dataGridViewData.DataSource = datasrc;
            int nfixed = nFixedColCount<dataGridViewData.ColumnCount?nFixedColCount:0;
            for (int i = 0; i < nfixed; ++i)
            {
                dataGridViewData.Columns[i].ReadOnly = true;
            }

            if(headerlst!=null)
            {
                for (int i = 0; i < Math.Min(dataGridViewData.ColumnCount, headerlst.Count); ++i)
                {
                    dataGridViewData.Columns[i].HeaderText = headerlst[i];
                }
            }
        }

        private void GridViewModifyForm_Load(object sender, EventArgs e)
        {
            
        }

        private void GridViewModifyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeightCenterDesignAndEstimateSoft.Task
{
    public partial class DiscreteSetForm : Form
    {
        private DiscreteSet data = null;
        public DiscreteSetForm(DiscreteSet ds)
        {
            InitializeComponent();
            data = ds;
        }

        private void DiscreteSetForm_Load(object sender, EventArgs e)
        {
            numericUpDownCircularPtNum.Value = data.nCircularPtCount;
            numericUpDownRadialPtNum.Value = data.nRadialPtCount;
            numericUpDownRadialFirstLen.Value = (decimal)data.fRadialFirstLen;
            numericUpDownRadialRatio.Value = (decimal)data.fRadialRatio;
            numericUpDownfRatioWidthVsHeight.Value = (decimal)data.fRatioWidthVsHeight;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            data.nCircularPtCount = (int)numericUpDownCircularPtNum.Value;
            data.nRadialPtCount = (int)numericUpDownRadialPtNum.Value;
            data.fRadialFirstLen = (double)numericUpDownRadialFirstLen.Value;
            data.fRadialRatio = (double)numericUpDownRadialRatio.Value;
            data.fRatioWidthVsHeight = (double)numericUpDownfRatioWidthVsHeight.Value;
        }

    }

    public class DiscreteSet
    {
        public DiscreteSet()
        {
            fRadialFirstLen = 0.05;
            fRadialRatio = 1.0;
            nRadialPtCount = 0;
            fRatioWidthVsHeight = 1.0;
        }
        public int nCircularPtCount { get; set; }
        public int nRadialPtCount { get; set; }
        public double fRadialFirstLen { get; set; }
        public double fRadialRatio { get; set; }
        public double fRatioWidthVsHeight { get; set; }
    }
}

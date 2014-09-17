using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using System.Xml;
using XCommon;
using System.IO;

namespace WeightCenterDesignAndEstimateSoft.Task.WeightAssessment
{
    public partial class WeightAssessmentConfigForm : Form
    {
        //public List<WeightData> wdList=new List<WeightData>();//权重数据
        public WeightAssessmentForm parentForm;
        private List<WeightAssessParameter> waList;//重量评估对象集合

        public WeightAssessmentConfigForm()
        {
            InitializeComponent();
        }

        private void WeightAssessmentConfigForm_Load(object sender, EventArgs e)
        {
            //窗口加载完后执行绑定
            this.ConfigurationGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConfigurationGridView_CellValueChanged);
       
            parentForm = ((WeightAssessmentForm)this.Owner);
            //List<WeightData> weightDataList = parentForm.weightAssessResult.datumWeightDataList;
            waList=parentForm.weightAssessResult.weightAssessParamList;
            this.loadGridView(waList);
            
         }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            parentForm.weightAssessResult.weightAssessParamList = this.waList;
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 参数表单元格值变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigurationGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                return;
            }
            string chartType=null;
            string weightName = this.ConfigurationGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            double value = Convert.ToDouble(this.ConfigurationGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            foreach (WeightAssessParameter wa in waList)
            {
                if (wa.weightName.Equals(weightName))
                {
                    if (e.ColumnIndex == 1)
                    {
                        wa.minValue = value;
                        chartType = "line";
                    }
                    else if(e.ColumnIndex==2)
                    {
                        wa.maxValue = value;
                        chartType = "line";
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        wa.weightedValue = value;
                        chartType = "pie";
                    }
                    break;
                }
            }

            if (chartType.Equals("pie"))
            {
                CommonUtil.DisplayPiePic(this.paramPieChart, waList, "权重分配比例图");
            }
            else
            {
                CommonUtil.DisplayLinePic(this.paramLineChart, waList, "最大/小值分配比例图");
            }
            
            
            
        }

        private void btnWeightRest_Click(object sender, EventArgs e)
        {
            double fTemp = 0.0;
            for (int i = 0; i < this.ConfigurationGridView.Rows.Count; i++ )
            {
                fTemp = fTemp + Convert.ToDouble(this.ConfigurationGridView.Rows[i].Cells[3].Value);
            }
            foreach (DataGridViewRow row in this.ConfigurationGridView.Rows)
            {
                row.Cells[3].Value = Convert.ToDouble(row.Cells[3].Value)/fTemp;
            }
        }

        private void ConfigurationGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            double NewVal = 0;
            if (e.ColumnIndex == 3)
            {              
                if (!double.TryParse(e.FormattedValue.ToString(), out NewVal) || NewVal < 0 || NewVal > 1)
                {  e.Cancel=true  ;                         
                   MessageBox.Show("只能输入大于0小于1的数字");
                    return;                 
                 }
            }
            else if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                if (!double.TryParse(e.FormattedValue.ToString(), out NewVal))
                {
                    e.Cancel = true;
                    MessageBox.Show("只能输入数字");
                    return;
                }
            }
        }

        private void ImportMinMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            ofd.FilterIndex = 0;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FilterIndex == 1)
                {
                    this.waList = this.loadXMLToData(ofd.FileName, ((ToolStripMenuItem)sender).Tag.ToString(), this.waList);
                    this.loadGridView(this.waList);
                }
                else
                {
                }
            }

        }

        private void ExportMinMenuItem_Click(object sender, EventArgs e)
        {
            

            SaveFileDialog sdf = new SaveFileDialog();
            sdf.Filter = "xml文件 (*.xml)|*.xml|Excel文件 (*.xls)|*.xls";
            sdf.OverwritePrompt = true;
            sdf.FilterIndex = 0;
            sdf.RestoreDirectory = true;
            sdf.FileName = "导出最小值";

            if (sdf.ShowDialog() == DialogResult.OK)
            {
                if (sdf.FilterIndex == 1)
                {
                    this.saveDataToXMLFile(sdf.FileName);
                }
                else
                {
                }
                //CommonFunction.mWriteListStringToFile(sdf.FileName, lstContent);
            }

            //CommonUtil.ExportDataToDataFile(wsd);
        }

        #region 自定义方法

        /// <summary>
        /// 保存评估参数数据到XML文件
        /// </summary>
        /// <param name="fileName">保存的文件路径</param>
        /// <returns></returns>
        private XmlDocument saveDataToXMLFile(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建一个XML文档声明，并添加到文档  
            //XmlDeclaration declare = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            //xmlDoc.AppendChild(declare);

            XmlElement rootElement = xmlDoc.CreateElement("重量评估参数");
            xmlDoc.AppendChild(rootElement);

            XmlElement elementList = xmlDoc.CreateElement("评估参数列表");
            rootElement.AppendChild(elementList);

            foreach (DataGridViewRow row in this.ConfigurationGridView.Rows)
            {
                XmlElement elementObj = xmlDoc.CreateElement("参数");
                elementList.AppendChild(elementObj);

                XmlElement element = xmlDoc.CreateElement("重量名称");
                element.InnerText = row.Cells[0].Value.ToString();
                elementObj.AppendChild(element);

                element = xmlDoc.CreateElement("最小值");
                element.InnerText = row.Cells[1].Value.ToString();
                elementObj.AppendChild(element);

                element = xmlDoc.CreateElement("最大值");
                element.InnerText = row.Cells[2].Value.ToString();
                elementObj.AppendChild(element);

                element = xmlDoc.CreateElement("权重值");
                element.InnerText = row.Cells[3].Value.ToString();
                elementObj.AppendChild(element);
            }

            //需要指定编码格式，否则在读取时会抛：根级别上的数据无效。 第 1 行 位置 1异常
             XmlWriterSettings settings = new XmlWriterSettings();
             //settings.Encoding = new UTF8Encoding(true);
             //settings.Indent = true;
             XmlWriter xw = XmlWriter.Create(fileName,settings);
             xmlDoc.Save(xw);
             //写入文件
             xw.Flush();
             xw.Close();
            return xmlDoc;
        }

        /// <summary>
        /// 导入XML文件到重量评估参数对象集合
        /// </summary>
        /// <param name="fileName">读取的文件路径</param>
        /// <param name="operation">加载方式:1,加载最小值;2,加载最大值;3,加载权重值</param>
        /// <returns></returns>
        private List<WeightAssessParameter> loadXMLToData(string fileName, string operation,List<WeightAssessParameter> wapList)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            XmlNodeList nodeList = xmlDoc.SelectNodes("重量评估参数/评估参数列表/参数");


            foreach (WeightAssessParameter wap in waList)
            {
                int i = 0;
                for (; i <nodeList.Count;i++ )
                {
                    XmlNode node = nodeList[i];
                    string weightName = node.SelectSingleNode("重量名称").InnerText;

                    if (wap.weightName.Equals(weightName))
                    {
                        if (operation == "1")
                        {
                            wap.minValue = Convert.ToDouble(node.SelectSingleNode("最小值").InnerText);
                        }
                        if (operation == "2")
                        {
                            wap.maxValue = Convert.ToDouble(node.SelectSingleNode("最大值").InnerText);
                        }
                        if (operation == "3")
                        {
                            wap.weightedValue = Convert.ToDouble(node.SelectSingleNode("权重值").InnerText);
                        }
                    }
                }
                if (i >= nodeList.Count)
                {
                    if (operation == "1")
                    {
                        wap.minValue = 0;
                    }
                    if (operation == "2")
                    {
                        wap.maxValue = 0;
                    }
                    if (operation == "3")
                    {
                        wap.weightedValue = 0;
                    }
                }
            }
            
            
            return wapList;
        }

        /// <summary>
        /// 加载评估参数表格
        /// </summary>
        /// <param name="waList"></param>
        private void loadGridView(List<WeightAssessParameter> waList)
        {
            if (waList == null)
            {
                return;
            }
            this.ConfigurationGridView.Rows.Clear();

            for (int i = 0;i < waList.Count; i++)
            {
                WeightAssessParameter wa = waList[i];

                this.ConfigurationGridView.Rows.Add();
                this.ConfigurationGridView.Rows[i].Cells[0].Value = wa.weightName;
                this.ConfigurationGridView.Rows[i].Cells[1].Value = wa.minValue;
                this.ConfigurationGridView.Rows[i].Cells[2].Value = wa.maxValue;
                this.ConfigurationGridView.Rows[i].Cells[3].Value = wa.weightedValue;
            }
        }

        #endregion
    }
}

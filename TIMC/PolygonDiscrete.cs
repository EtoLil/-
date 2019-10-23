using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TIMC.Model;

namespace TIMC
{
    public partial class PolygonDiscrete : Form
    {
        private Discrete discrete;

        public PolygonDiscrete()
        {
            InitializeComponent();
        }

        public PolygonDiscrete(Discrete discrete)
        {
            InitializeComponent();
            this.discrete = discrete;
        }

        private void PolygonDiscrete_Load(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = discrete.MinX();
            chart1.ChartAreas[0].AxisX.Maximum = discrete.MaxX();
            chart1.ChartAreas[0].AxisY.Minimum = discrete.MinY();
            chart1.ChartAreas[0].AxisY.Maximum = discrete.MaxY();
            chart1.ChartAreas[0].AxisX.Interval = 10;
            chart1.ChartAreas[0].AxisY.Interval = 2;

            foreach (KeyValuePair < double,double> keyValuePair in discrete.Sample)
            {
                chart1.Series[0].Points.AddXY(keyValuePair.Key,keyValuePair.Value);
            }

            double[] empirical = discrete.EmpiricalFunction();


            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[0].Points.Clear();
            chart2.ChartAreas[0].AxisX.Minimum = discrete.MinX();
            chart2.ChartAreas[0].AxisX.Maximum = discrete.MaxX();
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Maximum = 1;
            chart2.ChartAreas[0].AxisX.Interval = 10;
            chart2.ChartAreas[0].AxisY.Interval =0.1;

            int i = 0;
            double key=0;

            foreach (KeyValuePair<double, double> keyValuePair in discrete.Sample)
            {
                if (i==0)
                {
                    key = keyValuePair.Key;
                    i++;
                    continue;
                }
                chart2.Series[i-1].Points.AddXY(key, empirical[i]);
                chart2.Series[i-1].Points.AddXY(keyValuePair.Key, empirical[i]);
                key = keyValuePair.Key;
                chart2.Series.Add(new Series());
                chart2.Series[i].ChartType = SeriesChartType.Line;
                chart2.Series[i].Points.Clear();
                i++;
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}

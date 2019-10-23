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
    public partial class PolygonContinuous : Form
    {
        private Сontinuous continuous;

        public PolygonContinuous()
        {
            InitializeComponent();
        }

        public PolygonContinuous(Dictionary<double,double> sample )
        {
            continuous = new Сontinuous(sample);
            InitializeComponent();
        }

        private void PolygonContinuous_Load(object sender, EventArgs e)
        {
            label1.Text = continuous.WriteIntervals();
            label2.Text = continuous.WriteAdsoluteFrequency();
            label3.Text = continuous.WriteRelativeFrequency();
            continuous.WriteEmpiricalFunction(chart1);
            continuous.WriteHistogram(chart2);
        }
    }
}

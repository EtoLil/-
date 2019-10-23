using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIMC.Model;

namespace TIMC
{
    public partial class Form1 : Form
    {
        private Discrete discrete;

        public Form1()
        {
            discrete = new Discrete();
            InitializeComponent();
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            discrete.ReadFromFile();
            MessageBox.Show("ReadFromFile");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            discrete.Sample.Add(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void discreteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PolygonDiscrete polygon = new PolygonDiscrete(discrete);
            polygon.Show();
        }

        private void characteristicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NumericalCharacteristics characteristics = new NumericalCharacteristics(this.discrete);
            characteristics.Show();
        }

        private void сontinuousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PolygonContinuous polygonContinuous = new PolygonContinuous(discrete.Sample);
            polygonContinuous.Show();
        }
    }
}

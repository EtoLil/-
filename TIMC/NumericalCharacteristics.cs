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
    public partial class NumericalCharacteristics : Form
    {
        private Discrete discrete;

        public NumericalCharacteristics()
        {
            InitializeComponent();
        }

        public NumericalCharacteristics(Discrete discrete)
        {
            this.discrete = discrete;
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void NumericalCharacteristics_Load(object sender, EventArgs e)
        {
            label2.Text = this.discrete.X().ToString();
            label4.Text = this.discrete.Me().ToString();
            label6.Text = this.discrete.Mo().ToString();
            label8.Text = this.discrete.D().ToString();
            label10.Text = this.discrete.S().ToString();
            label12.Text = this.discrete.S2().ToString();
            label14.Text = this.discrete.P().ToString();
            label16.Text = this.discrete.R().ToString();
            label18.Text = this.discrete.Vp().ToString() + "%";
            label20.Text = this.discrete.Vs().ToString() + "%";
            label22.Text = this.discrete.M(1).ToString();
            label24.Text = this.discrete.U(2).ToString();
            label26.Text = this.discrete.A().ToString();
            label28.Text = this.discrete.E().ToString();

            label29.Text = this.discrete.Table()[0];
            label30.Text = this.discrete.Table()[1];
            label31.Text = this.discrete.Table()[2];
        }


    }
}

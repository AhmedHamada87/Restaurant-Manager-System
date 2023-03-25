using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierSystem
{
    public partial class Adminsettings : Form
    {
        public Adminsettings()
        {
            InitializeComponent();
            var apa = Application.OpenForms["Form1"] as Form1;
            if (apa.gunaLabel11.Text == "no") {

                gunaAdvenceButton9.Visible = true;
                gunaAdvenceButton3.Visible = false;
                gunaAdvenceButton1.Enabled = false;
                gunaAdvenceButton2.Enabled = false;
                gunaAdvenceButton4.Enabled = false;
                gunaAdvenceButton5.Enabled = false;
                gunaAdvenceButton6.Enabled = false;
                gunaAdvenceButton8.Enabled = false;
            }
            else { gunaAdvenceButton9.Visible = false;
                gunaAdvenceButton3.Visible = true;

                gunaAdvenceButton1.Enabled = true;
                gunaAdvenceButton2.Enabled = true;
                gunaAdvenceButton4.Enabled = true;
                gunaAdvenceButton5.Enabled = true;
                gunaAdvenceButton6.Enabled = true;
                gunaAdvenceButton8.Enabled = true;

            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            addcats adc = new addcats();
            adc.ShowDialog();
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            addprods adp = new addprods();
            adp.ShowDialog();
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            tblnums tbl = new tblnums();
            tbl.ShowDialog();
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            captinsedit cpt = new captinsedit();
            cpt.ShowDialog();
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            servdes srd = new servdes();
            srd.ShowDialog();
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            var apa = Application.OpenForms["Form1"] as Form1;
            apa.gunaLabel11.Text = "no";
            Close();
        }

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            reguser rg = new reguser();
            rg.ShowDialog();
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            showsheft sho = new showsheft();
            sho.ShowDialog();
        }

        private void gunaAdvenceButton10_Click(object sender, EventArgs e)
        {
            recchoose reca = new recchoose();
            reca.ShowDialog();
        }

      

        private void Adminsettings_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton9_Click(object sender, EventArgs e)
        {
            logadmin loga = new logadmin();
            loga.ShowDialog();
        }

        private void gunaAdvenceButton11_Click(object sender, EventArgs e)
        {
            printersettings prin = new printersettings();
            prin.ShowDialog();
        }
    }
}

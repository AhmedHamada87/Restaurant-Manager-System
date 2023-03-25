using Guna.UI2.WinForms;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierSystem
{
    public partial class recchoose : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public recchoose()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            try
            {
                findrecnum fin = new findrecnum();
                fin.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

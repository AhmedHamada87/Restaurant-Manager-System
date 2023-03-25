using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace CashierSystem
{
    public partial class newprod : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public newprod()
        {
            InitializeComponent();
            var mom = Application.OpenForms["addprods"] as addprods;
            gunaLabel6.Text = mom.gunaLabel3.Text;
            gunaLabel7.Text = mom.gunaLabel4.Text;
            con = new SqliteConnection("Data Source= cafedb.db");
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newprod_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            try
            {

                qu = "INSERT INTO prods (name,cat,price) VALUES ($nam,$cat,$prc)";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$nam", gunaTextBox1.Text);
                cmd.Parameters.AddWithValue("$cat", gunaLabel7.Text);
                cmd.Parameters.AddWithValue("$prc", guna2NumericUpDown1.Value.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                var mom = Application.OpenForms["addprods"] as addprods;
                mom.updprod();
                MessageBox.Show("تمت إضافة الصنف بنجاح");
                gunaTextBox1.Clear();
                guna2NumericUpDown1.Value = 0;

            }
            catch (Exception ex) { MessageBox.Show("حدث خطا غير معروف تواصل مع البشمهندس أحمد حمادة لحله فى اقرب وقت 01011210118"); }
        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

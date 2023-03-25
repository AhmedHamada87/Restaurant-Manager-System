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
    public partial class newcaptin : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public newcaptin()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            guna2ComboBox1.SelectedIndex = 1;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            try
            {

                qu = "INSERT INTO capt (name,phone,dos) VALUES ($nam,$phn,$dos)";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$nam", gunaTextBox1.Text);
                cmd.Parameters.AddWithValue("$phn", gunaTextBox2.Text);
                cmd.Parameters.AddWithValue("$dos", guna2ComboBox1.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                var mom = Application.OpenForms["captinsedit"] as captinsedit;
                mom.loaddata();
                MessageBox.Show("تمت إضافة الكابتن بنجاح");
                gunaTextBox1.Clear();
                gunaTextBox2.Clear();
                

            }
            catch (Exception ex) { MessageBox.Show("حدث خطا غير معروف تواصل مع البشمهندس أحمد حمادة لحله فى اقرب وقت 01011210118"); }
        }

        private void newcaptin_Load(object sender, EventArgs e)
        {

        }
    }
}

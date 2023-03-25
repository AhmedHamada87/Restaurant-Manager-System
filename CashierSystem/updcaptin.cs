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
    public partial class updcaptin : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public updcaptin()
        {
            InitializeComponent();
            var mon = Application.OpenForms["captinsedit"] as captinsedit;
            gunaLabel5.Text=mon.gunaLabel2.Text;
            con = new SqliteConnection("Data Source= cafedb.db");
            loaddata();
        }

        private void loaddata()
        {
            
            con.Open();
            cmd = new SqliteCommand("Select * From capt where id=$ida", con);
            cmd.Parameters.AddWithValue("$ida",gunaLabel5.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    gunaTextBox1.Text=read.GetString(1);
                    gunaTextBox2.Text = read.GetString(2);
                    guna2ComboBox1.Text = read.GetString(3);
                }
            }
        }

        private void updcaptin_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            qu = "UPDATE capt SET name=$nam,phone=$phn,dos=$dol WHERE id=$ida";
            //cmd.Parameters.AddWithValue("$id", idlabel);
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$ida", gunaLabel5.Text) ;
            cmd.Parameters.AddWithValue("$nam", gunaTextBox1.Text);
            cmd.Parameters.AddWithValue("$phn", gunaTextBox2.Text);
            cmd.Parameters.AddWithValue("$dol", guna2ComboBox1.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("تم تغيير بيانات الكابتن  بنجاح");
            var apa = Application.OpenForms["captinsedit"] as captinsedit;
            apa.loaddata();
        }
    }
}

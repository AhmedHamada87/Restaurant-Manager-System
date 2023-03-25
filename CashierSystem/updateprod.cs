using Guna.UI2.WinForms;
using Microsoft.Data.Sqlite;
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
    public partial class updateprod : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public updateprod()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            var mom = Application.OpenForms["addprods"] as addprods;

            guna2ComboBox2.Items.Add(mom.gunaLabel5.Text);
            guna2ComboBox2.SelectedIndex = 0;
            loadcats();
            loaddata();
            //loadfastprods();
            loadfast();
            
            
        }

        private void loadfast()
        {
            string cat;
            var mom = Application.OpenForms["addprods"] as addprods;
            con.Open();
            cmd = new SqliteCommand("SELECT p.id,p.name,c.name from prods p,cats c where cat=c.id and p.cat=$cata", con);
            cmd.Parameters.AddWithValue("$cata", mom.gunaLabel4.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {

                    cat = read.GetString(0);
                    guna2ComboBox2.Items.Add(cat);

                }
            }
        }

        private void loadfastprods()
        {
            con.Open();
            string cat = "";
            cmd = new SqliteCommand("SELECT p.id,p.name,c.name from prods p,cats c where cat=c.id and p.cat=$cata", con);
            cmd.Parameters.AddWithValue("$cata",guna2ComboBox1.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    cat = read.GetString(0);
                    guna2ComboBox2.Items.Add(cat);
                }
            }
        }

        private void loadcats()
        {
            con.Open();
            string cat = "";
            cmd = new SqliteCommand("SELECT * FROM cats", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    cat = read.GetString(1);
                    guna2ComboBox1.Items.Add(cat);
                }
            }
        }

        private void loaddata()
        {
            
            con.Open();
            cmd = new SqliteCommand("SELECT * FROM cats c,prods p WHERE c.id=p.cat and p.id=$ida", con);
            cmd.Parameters.AddWithValue("$ida", guna2ComboBox2.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    gunaTextBox1.Text=read.GetString(3);
                    guna2NumericUpDown1.Value = Convert.ToInt32(read.GetDouble(5));
                    guna2ComboBox1.Text = read.GetString(1);



                }
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateprod_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            qu = "UPDATE prods SET name=$nam,price=$prc WHERE id=$ida";
            //cmd.Parameters.AddWithValue("$id", idlabel);
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$ida", guna2ComboBox2.Text);
            cmd.Parameters.AddWithValue("$nam", gunaTextBox1.Text);
            cmd.Parameters.AddWithValue("$prc", guna2NumericUpDown1.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("تم تغيير بيانات المنتج  بنجاح");
            var poi = Application.OpenForms["addprods"] as addprods;
            poi.updprod();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من حذف هذا الصنف؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                
                qu = "DELETE FROM prods WHERE id=$ida";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$ida", guna2ComboBox2.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تم حذف المنتج  بنجاح");
                var poi = Application.OpenForms["addprods"] as addprods;
                poi.updprod();
                this.Close();
            }
        }

        private void gunaLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            try {
                guna2ComboBox2.SelectedIndex += 1;
            }
            catch (Exception ex) { }
            
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox2.SelectedIndex == 0) { }
            else { guna2ComboBox2.SelectedIndex -= 1; }
            
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loaddata();
        }

        private void gunaButton4_Click_1(object sender, EventArgs e)
        {
            try {
                guna2ComboBox2.SelectedIndex += 1;
            }
            catch (Exception ex) { MessageBox.Show("لا يوجد اصناف اخري فى القسم"); }
        }

        private void gunaButton5_Click_1(object sender, EventArgs e)
        {
            try
            {
                guna2ComboBox2.SelectedIndex -= 1;
            }
            catch (Exception ex) { MessageBox.Show("لا يوجد اصناف اخري فى القسم"); }
        }
    }
}

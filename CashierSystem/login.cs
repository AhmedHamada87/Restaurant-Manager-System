using Guna.UI2.WinForms;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierSystem
{
    public partial class login : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public login()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            //MessageBox.Show(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            /*if (verifyme()==true)
            {
                System.Windows.Forms.Application.Exit();
            }
            else { /*System.Windows.Forms.Application.Exit();
                System.Windows.Forms.Application.Exit();
            }*/

            gunaLabel4.Text = "nam";
        }

        

        private void gunaButton1_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            //DateTime lic = new DateTime(2023,2,10);
            
                try
                {
                    con.Open();
                    qu = "SELECT * FROM users WHERE user=$na AND pass=$pa";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$na", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("$pa", guna2TextBox2.Text);
                    cmd.ExecuteNonQuery();
                    dr = cmd.ExecuteReader();
                    int count = 0;
                    while (dr.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {

                        Form1 frm = new Form1();
                        frm.Show();

                        var frm1 = Application.OpenForms["Form1"] as Form1;
                        frm1.gunaLabel11.Text = "no";


                        if (isadmin() == true)
                        {
                            frm1.gunaLabel11.Text = "yes";
                        }
                        Hide();
                    }
                    if (count < 1) { MessageBox.Show("خطأ في اسم المستخدم أو كلمة المرور"); }

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            
        }
        private bool isadmin()
        {
            con.Open();
            qu = "SELECT * FROM users WHERE user=$nama";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$nama", guna2TextBox1.Text);
            string st = "";
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    st = read.GetString(3);
                }

            }
            con.Close();
            if (st == "1") { return true; }
            else { return false; }
        }

        private void guna2TextBox1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void guna2TextBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "1";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "1";
            }
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "2";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "2";
            }
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "3";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "3";
            }
        }

        private void gunaButton8_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "4";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "4";
            }
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "5";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "5";
            }
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "6";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "6";
            }
        }

        private void gunaButton11_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "7";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "7";
            }
        }

        private void gunaButton10_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "8";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "8";
            }
        }

        private void gunaButton9_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "9";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "9";
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                guna2TextBox1.Text += "0";
            }
            if (gunaLabel4.Text == "pass")
            {
                guna2TextBox2.Text += "0";
            }
        }

        private void gunaButton12_Click(object sender, EventArgs e)
        {
            if (gunaLabel4.Text == "nam")
            {
                if (guna2TextBox1.TextLength > 0)
                {
                    guna2TextBox1.Text = guna2TextBox1.Text.Substring(0, (guna2TextBox1.TextLength - 1));
                }
                else { }
                
            }
            if (gunaLabel4.Text == "pass")
            {
                if (guna2TextBox2.TextLength > 0)
                {
                    guna2TextBox2.Text = guna2TextBox2.Text.Substring(0, (guna2TextBox2.TextLength - 1));
                }
                else { }
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            gunaLabel4.Text = "nam";
        }

        private void guna2TextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            gunaLabel4.Text = "pass";
        }
    }
}

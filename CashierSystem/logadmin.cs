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
    public partial class logadmin : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public logadmin()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
        }

        private void logadmin_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
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
                    
                    

                    var frm1 = Application.OpenForms["Form1"] as Form1;
                    frm1.gunaLabel11.Text = "no";


                    if (isadmin() == true)
                    {
                        frm1.gunaLabel11.Text = "yes";

                        var adm = Application.OpenForms["Adminsettings"] as Adminsettings;
                        adm.gunaAdvenceButton9.Visible = false;
                        adm.gunaAdvenceButton3.Visible = true;
                        adm.gunaAdvenceButton1.Enabled = true;
                        adm.gunaAdvenceButton2.Enabled = true;
                        adm.gunaAdvenceButton4.Enabled = true;
                        adm.gunaAdvenceButton5.Enabled = true;
                        adm.gunaAdvenceButton6.Enabled = true;
                        adm.gunaAdvenceButton8.Enabled = true;
                    }
                    else { MessageBox.Show("هذا ليس حساب مدير"); }
                    Close();
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
    }
}

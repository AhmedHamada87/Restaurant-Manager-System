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
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace CashierSystem
{
    public partial class transorder : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        int counter = 0;
        public transorder()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            var era = Application.OpenForms["Form1"] as Form1;
            gunaLabel2.Text = era.gunaLabel2.Text;
            gunaLabel1.Text = era.gunaLabel5.Text;
            loadtables();
            greenlights();
        }

        public void greenlights()
        {
            con.Open();
            cmd = new SqliteCommand("Select distinct(cust) From orders where datetime is NULL", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    string st = read.GetString(0);
                    st = System.Text.RegularExpressions.Regex.Replace(st, "[^0-9]", "");
                    foreach (Guna2Button btn in flowLayoutPanel1.Controls.OfType<Guna2Button>())
                    {

                        if (btn.Name == st)
                        {
                            btn.FillColor = Color.Brown;

                        }


                    }
                }

            }
            turnoffs();





        }

        public void turnoffs()
        {
            foreach (Guna2Button btn in flowLayoutPanel1.Controls.OfType<Guna2Button>())
            {

                if (hasorder(btn.Text) == true)
                {
                    btn.FillColor = Color.Brown;

                }
                else { btn.FillColor = Color.Teal; }


            }
            foreach (Guna2Button btn in flowLayoutPanel2.Controls.OfType<Guna2Button>())
            {

                if (hasorder(btn.Text) == true)
                {
                    btn.FillColor = Color.Brown;

                }
                else { btn.FillColor = Color.Teal; }


            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void transorder_Load(object sender, EventArgs e)
        {

        }
        public void loadtables()
        {
            //tabPage1.Controls.Clear();
            //tabPage2.Controls.Clear();
            int table = 0;
            int takes = 0;
            con.Open();
            cmd = new SqliteCommand("select * FROM tablescap", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {

                    table = Convert.ToInt32(read.GetDouble(0));
                    takes = Convert.ToInt32(read.GetDouble(1));



                }
            }
            for (int i = 1; i <= table; i++)
            {

                Guna2Button btn = new Guna2Button();
                btn.Text = "طاولة" + i;
                btn.Name = i.ToString();
                btn.Click += transtable;
                btn.Width = 105;
                btn.Height = 70;
                btn.FillColor = Color.Teal;
                btn.FillColor = Color.Teal;
                btn.Font = new Font("Cairo", 11, FontStyle.Bold);
                flowLayoutPanel1.Controls.Add(btn);


            }
            for (int i = 1; i <= takes; i++)
            {

                Guna2Button btn = new Guna2Button();
                btn.Text = "تيك اواي" + i;
                btn.Name = i.ToString();
                btn.Click += transtable;
                btn.Width = 130;
                btn.Height = 70;
                btn.FillColor = Color.Teal;
                btn.FillColor = Color.Teal;
                btn.Font = new Font("Cairo", 11, FontStyle.Bold);
                flowLayoutPanel2.Controls.Add(btn);

            }
        }

        private void transtable(object sender, EventArgs e)
        {
            var btn=sender as Guna2Button;
            gunaLabel6.Text = btn.Text;
        }

        private void gunaAdvenceTileButton10_Click(object sender, EventArgs e)
        {
            if (gunaLabel6.Text == gunaLabel2.Text)
            {
                Close();
            }
            else if (hasorder(gunaLabel6.Text)) {
                //MessageBox.Show("هذه الطاولة ممتلئة");

                int recnuma = 0;
                con.Open();
                cmd = new SqliteCommand("Select * From orders where cust=$nam and datetime is null", con);
                cmd.Parameters.AddWithValue("$nam", gunaLabel6.Text);
                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        recnuma = read.GetInt32(8);
                    }
                }
                



                qu = "UPDATE orders SET cust=$cus,recnum=$recnuma where recnum=$rec AND cust=$cusd";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$cus", gunaLabel6.Text);
                cmd.Parameters.AddWithValue("$cusd", gunaLabel2.Text);
                cmd.Parameters.AddWithValue("$rec", gunaLabel1.Text);
                cmd.Parameters.AddWithValue("$recnuma", recnuma);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                var par = Application.OpenForms["Form1"] as Form1;
                par.greenlights();
                par.gunaLabel2.Text = "الرقم";
                par.gunaLabel5.Text = "الرقم";
                par.gunaLabel3.Text = "الرقم";
                par.guna2DataGridView1.Rows.Clear();

                this.Close();
            }
            else {
                qu = "UPDATE orders SET cust=$cus where recnum=$rec AND cust=$cusd";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$cus", gunaLabel6.Text);
                cmd.Parameters.AddWithValue("$cusd", gunaLabel2.Text);
                cmd.Parameters.AddWithValue("$rec", gunaLabel1.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                var par = Application.OpenForms["Form1"] as Form1;
                par.greenlights();
                par.gunaLabel2.Text = "الرقم";
                par.gunaLabel5.Text = "الرقم";
                par.gunaLabel3.Text = "الرقم";
                par.guna2DataGridView1.Rows.Clear();

                this.Close();
            }

        }

        private bool hasorder(string text)
        {
            int cou = 0;
            con.Open();
            cmd = new SqliteCommand("Select * From orders where cust=$nam and datetime is null", con);
            cmd.Parameters.AddWithValue("$nam", text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    cou++;
                }
            }
            if (cou == 0) { return false; }
            else { return true; }
        }

        private void gunaAdvenceTileButton5_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel2.Visible = false;
        }

        private void gunaAdvenceTileButton6_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = true;
        }
    }
}

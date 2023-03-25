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
    public partial class addprods : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public addprods()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            loadcats();
        }

        public void loadcats()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();

            con.Open();
            cmd = new SqliteCommand("Select * From cats", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    Guna2Button btn = new Guna2Button();
                    btn.Text = read.GetValue(1).ToString();
                    btn.Name= read.GetValue(0).ToString();
                    btn.Click += btanan;
                    flowLayoutPanel1.Controls.Add(btn);
                    
                }
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addprods_Load(object sender, EventArgs e)
        {

        }
        public void btanan(object sender, EventArgs e) {
            flowLayoutPanel2.Controls.Clear();
            var button = sender as Guna2Button;
            gunaLabel3.Text = button.Text;
            gunaLabel4.Text = button.Name;
            con.Open();
            cmd = new SqliteCommand("SELECT * FROM cats c,prods p WHERE c.id=p.cat and c.id=$ida", con);
            cmd.Parameters.AddWithValue("$ida",button.Name);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    Guna2Button btn = new Guna2Button();
                    btn.Text = read.GetValue(3).ToString();
                    btn.Name = read.GetValue(2).ToString();
                    btn.Click += openupdat;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.FillColor = Color.Teal;
                    flowLayoutPanel2.Controls.Add(btn);
                }
                
            }
            
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            if (gunaLabel3.Text == "القسم") { MessageBox.Show("من فضلك اختر القسم الذي تريد الاضافة به"); }
            else {
                newprod nab = new newprod();
                nab.ShowDialog();
            }
            
        }
        public void updprod() {
            flowLayoutPanel2.Controls.Clear();
            con.Open();
            cmd = new SqliteCommand("SELECT * FROM cats c,prods p WHERE c.id=p.cat and c.id=$ida", con);
            cmd.Parameters.AddWithValue("$ida", gunaLabel4.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    Guna2Button btn = new Guna2Button();
                    btn.Text = read.GetValue(3).ToString();
                    btn.Name = read.GetValue(2).ToString();
                    btn.Click += openupdat;
                    btn.Width = 100;
                    btn.Height = 100;
                    btn.FillColor = Color.Teal;
                    flowLayoutPanel2.Controls.Add(btn);
                }

            }

        }

        public void openupdat(object sender, EventArgs e) {
            var button = sender as Guna2Button;
            gunaLabel5.Text= button.Name;
            gunaLabel6.Text= button.Text;
            updateprod upd = new updateprod();
            upd.ShowDialog();

        }
    }
    
}

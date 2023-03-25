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
    public partial class servdes : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public servdes()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            loadnum();
        }

        private void loadnum()
        {
            con.Open();
            cmd = new SqliteCommand("select * FROM servedis", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    guna2NumericUpDown1.Value = Convert.ToInt32(read.GetDouble(0));
                    guna2NumericUpDown2.Value = Convert.ToInt32(read.GetDouble(1));
                    guna2NumericUpDown3.Value= Convert.ToInt32(read.GetDouble(2));
                }
            }
        }
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void servdes_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            qu = "UPDATE servedis SET serve=$num,discount=$tak,tax=$pak where discount=discount";
            //cmd.Parameters.AddWithValue("$id", idlabel);
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$num", guna2NumericUpDown1.Value);
            cmd.Parameters.AddWithValue("$tak", guna2NumericUpDown2.Value);
            cmd.Parameters.AddWithValue("$pak", guna2NumericUpDown3.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("تم تغيير الاسعار بنجاح");
            
        }
    }
}

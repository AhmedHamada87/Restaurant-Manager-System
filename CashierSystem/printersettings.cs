using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace CashierSystem
{
    public partial class printersettings : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public printersettings()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            loadcurr();
            loadprinter();
        }

        private void loadprinter()
        {
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(strPrinter);
                if (strPrinter == gunaLabel3.Text)
                {
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(strPrinter);
                }
            }
        }

        private void loadcurr()
        {
            
            con.Open();
            cmd = new SqliteCommand("Select * From printer", con);
            
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                
                while (read.Read())
                {
                    gunaLabel3.Text = read.GetString(0);
                }
            }
            
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void printersettings_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            qu = "UPDATE printer SET printer=$prin where printer=printer";
            //cmd.Parameters.AddWithValue("$id", idlabel);
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$prin", comboBox1.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Close();

        }
    }
}

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
    public partial class showsheft : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public showsheft()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            getallsheft();
            



        }
        private void loadtotkasm()
        {
            double sum = 0;
            for (int i = 0; i < guna2DataGridView2.Rows.Count; i++)
            {
                sum += Convert.ToDouble(guna2DataGridView2.Rows[i].Cells[3].Value);
            }
            gunaLabel29.Text = sum.ToString();
        }


        private void showsheftinfo()
        {
            try {
                con.Open();
                qu = "Select * From shefts where sheft=$sh";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$sh", guna2ComboBox1.Text);
                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {

                        gunaLabel13.Text = read.GetString(2);
                        gunaLabel15.Text = read.GetString(3);


                    }
                }
                con.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("الشيفت دا لسة متقفلش");
            }
            
        }

        private void getallsheft()
        {
            con.Open();
            string cat = "";
            cmd = new SqliteCommand("SELECT * FROM shefts", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    cat = read.GetString(0);
                    guna2ComboBox1.Items.Add(cat);
                }
            }
        }

        public void showallord()
        {
            double totalall = 0;
            double totable = 0;
            double totataka = 0;
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("Select DISTINCT(recnum),totalallafdes,cust,datetime From orders where totalall is not NULL and sheft=$sh", con);
            cmd.Parameters.AddWithValue("$sh", guna2ComboBox1.Text);
            guna2DataGridView1.RowTemplate.Height = 60;
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    guna2DataGridView1.Rows.Add(new object[] {
                    read.GetValue(3),
                    read.GetValue(0),
                    read.GetValue(1),
                    read.GetValue(2),

                    });

                    totalall += Convert.ToDouble(read.GetDouble(1));
                    string g = read.GetString(2);
                    if (g[0] == 'ط') { totable += Convert.ToDouble(read.GetDouble(1)); }
                    else { totataka += Convert.ToDouble(read.GetDouble(1)); }
                }
            }
            gunaLabel10.Text = totalall.ToString();
            gunaLabel4.Text = totable.ToString();
            gunaLabel6.Text = totataka.ToString();
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
            guna2DataGridView1.EnableHeadersVisualStyles = false;
            guna2DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            gunaLabel2.Text = guna2DataGridView1.Rows.Count.ToString();
        }
        private void showsheft_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id;





            if (e.ColumnIndex == 4)
            {
                id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[1].Value);
                gunaLabel8.Text = id.ToString();
                showrecoldsheft shc = new showrecoldsheft();
                shc.ShowDialog();


            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Cafe", new Font("Arial", 17, FontStyle.Bold), Brushes.Black, new Point(70, 50));
            e.Graphics.DrawString("رقم الشيفت", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 90));
            e.Graphics.DrawString(guna2ComboBox1.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 90));
            e.Graphics.DrawString("عدد الطلبات ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 110));
            e.Graphics.DrawString(gunaLabel2.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 110));
            e.Graphics.DrawString("بداية الشيفت", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 130));
            e.Graphics.DrawString(gunaLabel13.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 130));
            e.Graphics.DrawString("نهاية الشيفت", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 150));
            e.Graphics.DrawString(gunaLabel15.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 150));
            e.Graphics.DrawString("اول شيك", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 170));
            e.Graphics.DrawString(gunaLabel17.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 170));
            e.Graphics.DrawString("اخر شيك", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 190));
            e.Graphics.DrawString(gunaLabel19.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 190));
            e.Graphics.DrawString("اجمالى الخدمة", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 210));
            e.Graphics.DrawString(gunaLabel21.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 210));
            e.Graphics.DrawString("اجمالى الخصومات", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 230));
            e.Graphics.DrawString(gunaLabel23.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 230));
            e.Graphics.DrawString("اجمالى الضريبة", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 260));
            e.Graphics.DrawString(gunaLabel25.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 260));
            e.Graphics.DrawString("قيم المبيعات", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 280));
            e.Graphics.DrawString(gunaLabel27.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 280));
            e.Graphics.DrawString("اجمالى مبيعات الصالة", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 300));
            e.Graphics.DrawString(gunaLabel4.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 300));
            e.Graphics.DrawString("اجمالى دليفرى/تيك اواى", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 320));
            e.Graphics.DrawString(gunaLabel6.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 320));
            e.Graphics.DrawString("اجمالى مبلغ الدرج", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(150, 340));
            e.Graphics.DrawString(gunaLabel10.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(20, 340));

        }

        private void gunaAdvenceTileButton9_Click(object sender, EventArgs e)
        {
            int y = 600;
            printPreviewDialog1.Document = printDocument1;
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, y);
            printDocument1.PrinterSettings.Copies = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void showlastandfirst()
        {
            con.Open();
            qu = "SELECT max(recnum),min(recnum) from orders where sheft=$sh";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$sh", guna2ComboBox1.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {

                    gunaLabel17.Text = read.GetString(1);
                    gunaLabel19.Text = read.GetString(0);

                }
            }
            con.Close();
        }

        private void loadthrees()
        {
            con.Open();
            qu = "SELECT DISTINCT(recnum),serv,dis,tax from orders where sheft=$sh";
            cmd = new SqliteCommand(qu, con);
            double serv = 0;
            double dis = 0;
            double tax = 0;
            cmd.Parameters.AddWithValue("$sh", guna2ComboBox1.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {

                    serv += read.GetDouble(1);
                    dis += read.GetDouble(2);
                    tax += read.GetDouble(3);


                }
            }
            con.Close();
            gunaLabel21.Text = serv.ToString();
            gunaLabel23.Text = dis.ToString();
            gunaLabel25.Text = tax.ToString();
        }

        private void calctotmapee3()
        {
            con.Open();
            qu = "SELECT sum(total) from orders where sheft=$sh";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$sh", guna2ComboBox1.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    gunaLabel27.Text = read.GetString(0);
                }
            }
            con.Close();
        }

        private void loadteka()
        {
            guna2DataGridView2.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("SELECT prodid,name,sum(qty),total FROM orders o,prods p WHERE p.id=o.prodid and sheft=$sh GROUP BY name,prodid", con);
            cmd.Parameters.AddWithValue("$sh", guna2ComboBox1.Text);
            guna2DataGridView2.RowTemplate.Height = 60;
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    guna2DataGridView2.Rows.Add(new object[] {
                    read.GetValue(0),
                    read.GetValue(1),
                    read.GetValue(2),
                    read.GetValue(3),

                    });


                }
            }
            guna2DataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
            guna2DataGridView2.EnableHeadersVisualStyles = false;
            guna2DataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showallord();
            showsheftinfo();
            showlastandfirst();
            loadthrees();
            calctotmapee3();
            loadteka();
            loadtotkasm();

        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int sanfy = 120;
            e.Graphics.DrawString("Cafe", new Font("Arial", 17, FontStyle.Bold), Brushes.Black, new Point(70, 50));
            e.Graphics.DrawString("رقم الشيفت", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, 90));
            e.Graphics.DrawString(guna2ComboBox1.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(10, 90));

            e.Graphics.DrawString("الصنف", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(170, sanfy));
            e.Graphics.DrawString("الكمية", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(80, sanfy));
            e.Graphics.DrawString("الاجمالى", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(5, sanfy));
            sanfy += 30;

            for (int i = 0; i < guna2DataGridView2.Rows.Count; i++)
            {

                e.Graphics.DrawString(guna2DataGridView2.Rows[i].Cells[1].Value.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(170, sanfy));
                e.Graphics.DrawString(guna2DataGridView2.Rows[i].Cells[2].Value.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(80, sanfy));
                e.Graphics.DrawString(guna2DataGridView2.Rows[i].Cells[3].Value.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(5, sanfy));
                sanfy += 20;
            }
            e.Graphics.DrawString("مجموع المبيعات", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(120, sanfy));
            e.Graphics.DrawString(gunaLabel29.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(10, sanfy));
        }

        private void gunaAdvenceTileButton2_Click(object sender, EventArgs e)
        {
            int y = 300;
            printPreviewDialog2.Document = printDocument2;
            if (guna2DataGridView2.Rows.Count <= 3)
            {
                printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, y);
                printDocument2.PrinterSettings.Copies = 1;
            }
            else
            {
                y += guna2DataGridView2.Rows.Count * 20;
                printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, y);
                printDocument2.PrinterSettings.Copies = 1;
            }


            printPreviewDialog2.ShowDialog();
        }
    }
}

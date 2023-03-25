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
    public partial class tableclos : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu = "";
        public tableclos()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            var ord = Application.OpenForms["Form1"] as Form1;
            gunaLabel5.Text = ord.gunaLabel5.Text;
            gunaLabel4.Text= ord.gunaLabel3.Text;
            loaddata();
            showord();
            loadcapt();
            loadtot();
            calctot();


        }

        private void calctot()
        {
            
            //double total =Convert.ToDouble(gunaLabel4.Text)+ Convert.ToDouble(gunaLabel8.Text);
            double serv = Convert.ToDouble(gunaLabel4.Text) * (Convert.ToDouble(gunaLabel8.Text) / 100);
            gunaLabel21.Text = serv.ToString();
            double total = Convert.ToDouble(gunaLabel4.Text) + serv;
            double tax = Convert.ToDouble(gunaLabel4.Text) * (Convert.ToDouble(gunaLabel16.Text) / 100);
            gunaLabel19.Text = tax.ToString();
            total = total + tax;
            gunaLabel14.Text = total.ToString();
            double bill = total;
            double discount = Convert.ToDouble(gunaLabel12.Text);
            gunaLabel20.Text = (bill * (discount / 100)).ToString();
            double totalafter = bill - (bill * (discount / 100));
            gunaLabel10.Text = totalafter.ToString();
            gunaLabel8.Text += " %";
            gunaLabel12.Text += " %";
            gunaLabel16.Text += " %";

        }

        private void loadtot()
        {
            con.Open();
            cmd = new SqliteCommand("select * FROM servedis", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    gunaLabel8.Text = read.GetString(0);
                    gunaLabel12.Text = read.GetString(1);
                    gunaLabel16.Text = read.GetString(2);
                }
            }
        }

        private void showord()
        {
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("select qty,total,name,ordid,prc from orders o,prods p where o.prodid=p.id and recnum=$num", con);
            cmd.Parameters.AddWithValue("$num", gunaLabel5.Text);
            guna2DataGridView1.RowTemplate.Height = 60;
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    guna2DataGridView1.Rows.Add(new object[] {
                    read.GetValue(3),
                    read.GetValue(4),
                    read.GetValue(2),
                    read.GetValue(0),
                    read.GetValue(1)

                    });
                }
            }
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
            guna2DataGridView1.EnableHeadersVisualStyles = false;
            guna2DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
        }
        private void loaddata()
        {
            con.Open();
            cmd = new SqliteCommand("Select * From orders where recnum=$rec LIMIT 1", con);
            cmd.Parameters.AddWithValue("$rec",gunaLabel5.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    gunaLabel2.Text = read.GetString(1);


                }
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tableclos_Load(object sender, EventArgs e)
        {
            

        }
        public void loadcapt() {

            flowLayoutPanel2.Controls.Clear();

            con.Open();
            cmd = new SqliteCommand("Select * From capt where dos=$do", con);
            cmd.Parameters.AddWithValue("$do","صالة");
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    Guna2Button btn = new Guna2Button();
                    btn.Text = read.GetValue(1).ToString();
                    btn.Name = read.GetValue(0).ToString();
                    btn.Click += closeord;
                    flowLayoutPanel2.Controls.Add(btn);
                }
            }
        }

        private void closeord(object sender, EventArgs e)
        {
            var btn = sender as Guna2Button;
            qu = "UPDATE orders SET totalall=$tot,datetime=$dat,captin=$cap,totalallafdes=$des,tax=$ta,dis=$di,serv=$se,sheft=$sh where recnum=$rec";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$rec", gunaLabel5.Text);
            cmd.Parameters.AddWithValue("$tot", gunaLabel14.Text);
            cmd.Parameters.AddWithValue("$dat", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            cmd.Parameters.AddWithValue("$cap", btn.Name);
            cmd.Parameters.AddWithValue("$des", gunaLabel10.Text);
            cmd.Parameters.AddWithValue("$ta", gunaLabel19.Text);
            cmd.Parameters.AddWithValue("$di", gunaLabel20.Text);
            cmd.Parameters.AddWithValue("$se", gunaLabel21.Text);
            var ap = Application.OpenForms["Form1"] as Form1;
            cmd.Parameters.AddWithValue("$sh", ap.gunaLabel13.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            gunaLabel90.Text = btn.Text;

            //print order

            int y = 600;
            printPreviewDialog1.Document = printDocument1;
            if (guna2DataGridView1.Rows.Count <= 3)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, y);
                printDocument1.PrinterSettings.Copies = 1;
            }
            else
            {
                y += guna2DataGridView1.Rows.Count * 30;
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, y);
                printDocument1.PrinterSettings.Copies = 1;
            }

            printDocument1.Print();
            //printDocument1.Print();
            //printPreviewDialog1.ShowDialog();

            //close



            var par = Application.OpenForms["Form1"] as Form1;
            par.greenlights();
            par.gunaLabel2.Text = "الرقم";
            par.gunaLabel5.Text = "الرقم";
            par.gunaLabel3.Text = "الرقم";
            par.guna2DataGridView1.Rows.Clear();

            





            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string rec = gunaLabel5.Text;
            string cust = gunaLabel2.Text;
            int sanfx = 130;
            int sanfy = 145;
            int qtyx = 80;
            int prcx = 5;
            string capt = gunaLabel90.Text;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            string j = "مسائي";
            TimeSpan start = new TimeSpan(8, 0, 0); //8 o'clock
            TimeSpan end = new TimeSpan(16, 0, 0); //4 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;
            if ((now > start) && (now < end))
            {
                j = "صباحي";
            }

            e.Graphics.DrawString("Cafe", new Font("Arial", 17, FontStyle.Regular),Brushes.Black,new Point(70,1) );
            e.Graphics.DrawString("رقم الشيك", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(200, 25));
            e.Graphics.DrawString(rec, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(110, 25));
            e.Graphics.DrawString("الوقت و التاريخ", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, 45));
            e.Graphics.DrawString(DateTime.Now.ToString("MM/dd/yyyy HH:mm"), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, 45));
            e.Graphics.DrawString("الكاشير", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, 65));
            e.Graphics.DrawString(j, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, 65));
            e.Graphics.DrawString("الكابتن", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, 85));
            e.Graphics.DrawString(capt, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, 85));
            e.Graphics.DrawString("العميل", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, 105));
            e.Graphics.DrawString(cust, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, 105));

            RectangleF rectF1 = new RectangleF(150, 125, 160, 20);
            e.Graphics.DrawString("الصنف", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, rectF1, stringFormat);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF1));

            RectangleF rectF2 = new RectangleF(105, 125, 45, 20);
            e.Graphics.DrawString("الكمية", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, rectF2, stringFormat);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF2));

            RectangleF rectF36 = new RectangleF(55, 125, 50, 20);
            e.Graphics.DrawString("سعر الوحدة", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, rectF36, stringFormat);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF36));

            RectangleF rectF3 = new RectangleF(0, 125, 55, 20);
            e.Graphics.DrawString("القيمة", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, rectF3, stringFormat);
            e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF3));


            for (int i=0;i<guna2DataGridView1.Rows.Count;i++) {
                RectangleF rectF5 = new RectangleF(150, sanfy, 160, 20);
                e.Graphics.DrawString(guna2DataGridView1.Rows[i].Cells[2].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, rectF5, stringFormat);
                e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF5));

                RectangleF rectF6 = new RectangleF(105, sanfy, 45, 20);
                e.Graphics.DrawString(guna2DataGridView1.Rows[i].Cells[3].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, rectF6, stringFormat);
                e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF6));

                RectangleF rectF3666 = new RectangleF(55, sanfy, 50, 20);
                e.Graphics.DrawString(guna2DataGridView1.Rows[i].Cells[1].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, rectF3666, stringFormat);
                e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF3666));

                RectangleF rectF7 = new RectangleF(0, sanfy, 55, 20);
                e.Graphics.DrawString(guna2DataGridView1.Rows[i].Cells[4].Value.ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, rectF7, stringFormat);
                e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rectF7));
                sanfy = sanfy + 20;
            }
            //sanfy = sanfy + 20;
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(0, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("قيمة الاصناف", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel4.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("الخدمة", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel21.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            e.Graphics.DrawString(gunaLabel8.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("الضريبة", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel19.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("الخصم", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel20.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("الاجمالى", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel10.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(0, sanfy));
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaLabel13_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

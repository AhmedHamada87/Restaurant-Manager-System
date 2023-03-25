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
    public partial class showrecoldsheft : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu = "";
        public showrecoldsheft()
        {
            InitializeComponent();
            var carm = Application.OpenForms["showsheft"] as showsheft;
            gunaLabel5.Text = carm.gunaLabel8.Text;
            con = new SqliteConnection("Data Source= cafedb.db");
            loaddata();
            showord();
            calctotaasnaf();
        }

        private void calctotaasnaf()
        {
            double sum = 0;
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDouble(guna2DataGridView1.Rows[i].Cells[4].Value);
            }
            gunaLabel16.Text = sum.ToString();
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
            cmd = new SqliteCommand("Select o.*,c.name From orders o,capt c where recnum=$rec and c.id=o.captin LIMIT 1", con);
            cmd.Parameters.AddWithValue("$rec", gunaLabel5.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    gunaLabel2.Text = read.GetString(1);
                    gunaLabel14.Text = read.GetString(6);
                    gunaLabel10.Text = read.GetString(10);
                    gunaLabel8.Text = read.GetString(11);
                    gunaLabel4.Text = read.GetString(12);
                    gunaLabel12.Text = read.GetString(13);
                    gunaLabel18.Text = read.GetString(7);
                    gunaLabel20.Text = read.GetString(15);


                }
            }
        }

        private void showrecoldsheft_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string rec = gunaLabel5.Text;
            string cust = gunaLabel2.Text;
            int sanfx = 130;
            int sanfy = 145;
            int qtyx = 80;
            int prcx = 5;
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


            e.Graphics.DrawString("Cafe", new Font("Arial", 17, FontStyle.Regular), Brushes.Black, new Point(70, 1));
            e.Graphics.DrawString("رقم الشيك", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(200, 25));
            e.Graphics.DrawString(rec, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(110, 25));
            e.Graphics.DrawString("الوقت و التاريخ", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, 45));
            e.Graphics.DrawString(gunaLabel18.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, 45));
            e.Graphics.DrawString("الكاشير", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, 65));
            e.Graphics.DrawString(j, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, 65));
            e.Graphics.DrawString("الكابتن", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, 85));
            e.Graphics.DrawString(gunaLabel20.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, 85));
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
            //e.Graphics.DrawString("الصنف", new Font("Arial", 14, FontStyle.Underline), Brushes.Black, new Point(200, 210));
            //e.Graphics.DrawString("الكمية", new Font("Arial", 14, FontStyle.Underline), Brushes.Black, new Point(80, 210));
            //e.Graphics.DrawString("القيمة", new Font("Arial", 14, FontStyle.Underline), Brushes.Black, new Point(5, 210));
            //e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(0, 230));

            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
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
                //e.Graphics.DrawString(guna2DataGridView1.Rows[i].Cells[2].Value.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(sanfx, sanfy));
                //e.Graphics.DrawString(guna2DataGridView1.Rows[i].Cells[3].Value.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(qtyx, sanfy));
                //e.Graphics.DrawString(guna2DataGridView1.Rows[i].Cells[4].Value.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(prcx, sanfy));


            }
            //sanfy = sanfy + 20;
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(0, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("قيمة الاصناف", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel16.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("الخدمة", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel12.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("الضريبة", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel8.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("الخصم", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel4.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("الاجمالى", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(190, sanfy));
            e.Graphics.DrawString(gunaLabel10.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, sanfy));
            sanfy = sanfy + 20;
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(0, sanfy));
            
        }

        private void gunaAdvenceTileButton9_Click(object sender, EventArgs e)
        {
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
            //printPreviewDialog1.ShowDialog();
            printDocument1.Print();
        }
    }
}

using Guna.UI.WinForms;
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
    public partial class Form1 : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        int counter = 0;
        public Form1()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            getsheft();
            loadcats();
            guna2VScrollBar1.Value = flowLayoutPanel1.VerticalScroll.Value;
            guna2VScrollBar1.Minimum = flowLayoutPanel1.VerticalScroll.Minimum;
            guna2VScrollBar1.Maximum = flowLayoutPanel1.VerticalScroll.Maximum;
            flowLayoutPanel1.ControlAdded += FlowLayoutPanel1_ControlAdded;
            flowLayoutPanel1.ControlRemoved += FlowLayoutPanel1_ControlRemoved;
            loadtables();
            getmaxorder();
            greenlights();


        }

        private void getsheft()
        {
            try
            {
                con.Open();
                qu = "Select Max(sheft),state From shefts";
                cmd = new SqliteCommand(qu, con);
                double max = 0;
                double stat = 0;
                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {

                        max = Convert.ToDouble(read.GetValue(0));
                        stat= Convert.ToDouble(read.GetValue(1));
                        

                    }
                }
                con.Close();
                if (stat == 0) { gunaLabel13.Text = max.ToString(); }
                else {

                    qu = "INSERT INTO shefts (sheft,state,start) VALUES ($sh,$st,$sta)";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$sh", max+1);
                    cmd.Parameters.AddWithValue("$st", 0);
                    cmd.Parameters.AddWithValue("$sta", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    gunaLabel13.Text = (max+ 1).ToString();
                }
            }
            catch (Exception ex)
            {
                gunaLabel13.Text = "1";
                qu = "INSERT INTO shefts (sheft,state,start) VALUES ($sh,$st,$sta)";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$sh", 1);
                cmd.Parameters.AddWithValue("$st", 0);
                cmd.Parameters.AddWithValue("$sta", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                

            }
        }

        private void getmaxorder()
        {

            try
            {
                con.Open();
                qu = "Select Max(recnum) From orders";
                cmd = new SqliteCommand(qu, con);
                string max = "0";
                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {

                        max = read.GetString(0);


                    }
                    gunaLabel5.Text = (Convert.ToDouble(max) + 1).ToString();


                }
                con.Close();
            }
            catch (Exception ex)
            {
                gunaLabel5.Text = "1";

            }
        }

        public void loadtables()
        {
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();
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
            for (int i = 1; i <= table; i++) {

                Guna2Button btn = new Guna2Button();
                btn.Text = "طاولة" + i;
                btn.Name = i.ToString();
                btn.Click += opentable;
                btn.Width = 105;
                btn.Height = 50;
                btn.FillColor = Color.Teal;
                btn.FillColor = Color.Teal;
                btn.Font = new Font("Arial", 15, FontStyle.Bold);
                flowLayoutPanel3.Controls.Add(btn);


            }
            for (int i = 1; i <= takes; i++)
            {

                Guna2Button btn = new Guna2Button();
                btn.Text = "تيك اواي" + i;
                btn.Name = i.ToString();
                btn.Click += opentable2;
                btn.Width = 105;
                btn.Height = 50;
                btn.FillColor = Color.Teal;
                btn.FillColor = Color.Teal;
                btn.Font = new Font("Arial", 15, FontStyle.Bold);
                flowLayoutPanel4.Controls.Add(btn);

            }
        }

        private void opentable(object sender, EventArgs e)
        {
            gunaLabel10.Text = "ط";
            var button = sender as Guna2Button;
            if (hasorder(button.Text) == true) { gunaLabel5.Text = getordnum(button.Text); }
            else { getmaxorder(); }
            gunaLabel2.Text = button.Text;
            showord();
            gunaLabel7.Text = "غير";
            gunaLabel8.Text = "غير";
            greenlights();
            gettot();
            try {
                int id = Convert.ToInt32(guna2DataGridView1.Rows[0].Cells[0].Value);
                gunaLabel7.Text = id.ToString();
                gunaLabel8.Text = guna2DataGridView1.Rows[0].Cells[3].Value.ToString();
                gunaLabel9.Text = guna2DataGridView1.Rows[0].Cells[1].Value.ToString();
                gettot();
            }
            catch (Exception ex) {   
            }
            
        }

        private void opentable2(object sender, EventArgs e)
        {
            gunaLabel10.Text = "ت";
            var button = sender as Guna2Button;
            if (hasorder(button.Text) == true) { gunaLabel5.Text = getordnum(button.Text); }
            else { getmaxorder(); }
            gunaLabel2.Text = button.Text;
            showord();
            gunaLabel7.Text = "غير";
            gunaLabel8.Text = "غير";
            greenlights();
            gettot();
            try
            {
                int id = Convert.ToInt32(guna2DataGridView1.Rows[0].Cells[0].Value);
                gunaLabel7.Text = id.ToString();
                gunaLabel8.Text = guna2DataGridView1.Rows[0].Cells[3].Value.ToString();
                gunaLabel9.Text = guna2DataGridView1.Rows[0].Cells[1].Value.ToString();
                gettot();
            }
            catch (Exception ex)
            {
            }

        }

        public void showord()
        {
            guna2DataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("select qty,total,name,ordid,prc,prodid from orders o,prods p where o.prodid=p.id and recnum=$num", con);
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
                    read.GetValue(1),
                    read.GetValue(5)

                    });
                }
            }
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
            guna2DataGridView1.EnableHeadersVisualStyles = false;
            guna2DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            gettot();
        }

        private string getordnum(string text)
        {
            string kal = "";
            con.Open();
            cmd = new SqliteCommand("Select distinct(recnum) From orders where cust=$nam and datetime is null", con);
            cmd.Parameters.AddWithValue("$nam", text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    kal = read.GetString(0);
                }
            }
            return kal;
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

        public void loadcats()
        {
            flowLayoutPanel1.Controls.Clear();

            con.Open();
            cmd = new SqliteCommand("Select * From cats", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    Guna2Button btn = new Guna2Button();
                    btn.Text = read.GetValue(1).ToString();
                    btn.Name = read.GetValue(0).ToString();
                    btn.Click += btanan;
                    btn.FillColor = Color.Teal;
                    btn.Width = 130;
                    btn.FillColor = Color.Teal;
                    btn.Font = new Font("Arial", 15, FontStyle.Bold);
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }

        }

        private void FlowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            guna2VScrollBar1.Maximum = flowLayoutPanel1.VerticalScroll.Maximum;
        }

        private void FlowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            guna2VScrollBar1.Minimum = flowLayoutPanel1.VerticalScroll.Minimum;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            flowLayoutPanel1.VerticalScroll.Value = guna2VScrollBar1.Value;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaCircleButton1_Click(object sender, EventArgs e)
        {
            loadcats();
        }

        private void gunaAdvenceTileButton1_Click(object sender, EventArgs e)
        {
            /*if (gunaLabel11.Text == "yes")
            {
                Adminsettings adm = new Adminsettings();
                adm.ShowDialog();
            }
            else {
                logadmin log = new logadmin();
                log.Show();
            }*/
            Adminsettings adm = new Adminsettings();
            adm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void btanan(object sender, EventArgs e)
        {
            flowLayoutPanel2.Controls.Clear();
            var button = sender as Guna2Button;
            //gunaLabel3.Text = button.Text;
            //gunaLabel4.Text = button.Name;
            con.Open();
            cmd = new SqliteCommand("SELECT * FROM cats c,prods p WHERE c.id=p.cat and c.id=$ida", con);
            cmd.Parameters.AddWithValue("$ida", button.Name);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    Guna2Button btn = new Guna2Button();
                    btn.Text = read.GetValue(3).ToString() + "\n" + read.GetValue(5).ToString();
                    btn.Name = read.GetValue(2).ToString();
                    btn.Click += selectprod;
                    btn.Width = 100;
                    btn.Height = 55;
                    btn.FillColor = Color.Teal;
                    btn.Font = new Font("Arial", 13,FontStyle.Bold);
                    
                    flowLayoutPanel2.Controls.Add(btn);
                }

            }
        }

        private void gunaAdvenceTileButton2_Click(object sender, EventArgs e)
        {
            loadcats();
        }
        public void selectprod(object sender, EventArgs e) {
            var button = sender as Guna2Button;
            if (gunaLabel2.Text == "الرقم") { MessageBox.Show("قم بتحديد نوع الطلب اولا"); }
            else {
                if (prodexsit(button.Name) == true) { //SystemSounds.Exclamation.Play();
                    //guna2DataGridView1.ClearSelection();
                    int id=0;
                    
                    for (int i=0;i<guna2DataGridView1.Rows.Count;i++) {
                        //MessageBox.Show($"{guna2DataGridView1.Rows[i].Cells[5].Value} == {button.Name}");
                        if (Convert.ToInt32(guna2DataGridView1.Rows[i].Cells[5].Value) == Convert.ToInt32(button.Name)) {

                            id = Convert.ToInt32(guna2DataGridView1.Rows[i].Cells[0].Value);
                            gunaLabel7.Text = id.ToString();
                            gunaLabel8.Text = guna2DataGridView1.Rows[i].Cells[3].Value.ToString();
                            gunaLabel9.Text = guna2DataGridView1.Rows[i].Cells[1].Value.ToString();
                            gettot();
                            guna2DataGridView1.ClearSelection();
                            guna2DataGridView1.Rows[i].Selected= true;
                            break;
                        }
                    
                    }

                    try
                    {
                        
                        qu = "UPDATE orders SET qty=$qty,total=$tot WHERE ordid=$ida";
                        cmd = new SqliteCommand(qu, con);
                        cmd.Parameters.AddWithValue("$ida", gunaLabel7.Text);
                        int k = Convert.ToInt32(gunaLabel8.Text) + 1;
                        double nprc = k * Convert.ToInt32(gunaLabel9.Text);
                        cmd.Parameters.AddWithValue("$qty", k);
                        cmd.Parameters.AddWithValue("$tot", nprc);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        gunaLabel8.Text = k.ToString();
                        //guna2DataGridView1.CurrentRow.Cells[3].Value = k;

                        //*****gunaLabel9.Text = nprc.ToString();
                        //guna2DataGridView1.CurrentRow.Cells[4].Value = nprc;
                        int index = guna2DataGridView1.SelectedRows[0].Index;
                        showord();
                        guna2DataGridView1.ClearSelection();
                        guna2DataGridView1.Rows[index].Selected = true;
                        gettot();


                    }
                    catch (Exception ex) { MessageBox.Show("قم بتحديد منتج من الفاتورة بالضغط عليه"); }










                }
                else {
                    try
                    {
                        double prc = getprc(button.Name);
                        qu = "INSERT INTO orders (recnum,cust,prodid,qty,prc,total) VALUES ($rec,$cus,$pro,$qty,$prc,$tot)";
                        cmd = new SqliteCommand(qu, con);
                        cmd.Parameters.AddWithValue("$rec", gunaLabel5.Text);
                        cmd.Parameters.AddWithValue("$cus", gunaLabel2.Text);
                        cmd.Parameters.AddWithValue("$pro", Convert.ToInt32(button.Name));
                        cmd.Parameters.AddWithValue("$qty", 1);
                        cmd.Parameters.AddWithValue("$prc", prc);
                        cmd.Parameters.AddWithValue("$tot", prc);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        greenlights();

                        showord();
                        gettot();


                        int id = Convert.ToInt32(guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count-1].Cells[0].Value);
                        gunaLabel7.Text = id.ToString();
                        gunaLabel8.Text = guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 1].Cells[3].Value.ToString();
                        gunaLabel9.Text = guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 1].Cells[1].Value.ToString();
                        gettot();

                        guna2DataGridView1.ClearSelection();
                        guna2DataGridView1.Rows.OfType<DataGridViewRow>().Last().Selected = true;




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("حدث خطا غير معروف تواصل مع البشمهندس أحمد حمادة لحله فى اقرب وقت 01011210118");

                    }

                }

            }
        }

        

        private bool prodexsit(string name)
        {
            con.Open();
            int count = 0;
            cmd = new SqliteCommand("select id from orders o,prods p where o.prodid=p.id and recnum=$num and p.id=$ida", con);
            cmd.Parameters.AddWithValue("$num", gunaLabel5.Text);
            cmd.Parameters.AddWithValue("$ida", name);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    count++;
                }
            }
            if (count == 0) { return false; }
            else { return true; }
        }

        private double getprc(string name)
        {
            double prc = 0;
            con.Open();
            cmd = new SqliteCommand("SELECT * FROM cats c,prods p WHERE c.id=p.cat and p.id=$ida", con);
            cmd.Parameters.AddWithValue("$ida", name);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    prc = read.GetDouble(5);



                }
            }
            return prc;
        }
        public void greenlights() {
            con.Open();
            cmd = new SqliteCommand("Select distinct(cust) From orders where datetime is NULL", con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    string st = read.GetString(0);
                    st = System.Text.RegularExpressions.Regex.Replace(st, "[^0-9]", "");
                    foreach (Guna2Button btn in flowLayoutPanel3.Controls.OfType<Guna2Button>())
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

        private void gunaAdvenceTileButton3_Click(object sender, EventArgs e)
        {
            greenlights();

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);
            gunaLabel7.Text = id.ToString();
            gunaLabel8.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
            gunaLabel9.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            gettot();
        }

        private void gunaAdvenceTileButton4_Click(object sender, EventArgs e)
        {
            try {
                qu = "UPDATE orders SET qty=$qty,total=$tot WHERE ordid=$ida";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$ida", gunaLabel7.Text);
                int k = Convert.ToInt32(gunaLabel8.Text) + 1;
                double nprc = k * Convert.ToInt32(gunaLabel9.Text);
                cmd.Parameters.AddWithValue("$qty", k);
                cmd.Parameters.AddWithValue("$tot", nprc);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                gunaLabel8.Text = k.ToString();
                //guna2DataGridView1.CurrentRow.Cells[3].Value = k;

                //*****gunaLabel9.Text = nprc.ToString();
                //guna2DataGridView1.CurrentRow.Cells[4].Value = nprc;
                int index = guna2DataGridView1.SelectedRows[0].Index;
                showord();
                guna2DataGridView1.ClearSelection();
                guna2DataGridView1.Rows[index].Selected = true;
                gettot();


            }
            catch (Exception ex) { MessageBox.Show("قم بتحديد منتج من الفاتورة بالضغط عليه"); }

        }

        private void gunaAdvenceTileButton3_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(guna2DataGridView1.Rows[guna2DataGridView1.SelectedRows[0].Index].Cells[3].Value) == 1) { SystemSounds.Exclamation.Play(); }
            else {
                try
                {
                    qu = "UPDATE orders SET qty=$qty,total=$tot WHERE ordid=$ida";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$ida", gunaLabel7.Text);
                    int k = Convert.ToInt32(gunaLabel8.Text) - 1;
                    double nprc = k * Convert.ToInt32(gunaLabel9.Text);
                    cmd.Parameters.AddWithValue("$qty", k);
                    cmd.Parameters.AddWithValue("$tot", nprc);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    gunaLabel8.Text = k.ToString();
                    //guna2DataGridView1.CurrentRow.Cells[3].Value = k;

                    //***gunaLabel9.Text = nprc.ToString();
                    //guna2DataGridView1.CurrentRow.Cells[4].Value = nprc;
                    int index = guna2DataGridView1.SelectedRows[0].Index;
                    showord();
                    guna2DataGridView1.ClearSelection();
                    guna2DataGridView1.Rows[index].Selected = true;
                    gettot();


                }
                catch (Exception ex) { MessageBox.Show("قم بتحديد منتج من الفاتورة بالضغط عليه"); }

            }

        }

        private void guna2DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);
            guna2DataGridView1.Rows.Clear();
            qu = "DELETE FROM orders WHERE ordid=$ida";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$ida", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            showord();
            greenlights();
            gettot();
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);
            guna2DataGridView1.Rows.Clear();
            qu = "DELETE FROM orders WHERE ordid=$ida";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$ida", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            showord();
            greenlights();
            gettot();

        }
        public void turnoffs() {
            foreach (Guna2Button btn in flowLayoutPanel3.Controls.OfType<Guna2Button>())
            {

                if (hasorder(btn.Text) == true)
                {
                    btn.FillColor = Color.Brown;

                }
                else { btn.FillColor = Color.Teal; }


            }
            foreach (Guna2Button btn in flowLayoutPanel4.Controls.OfType<Guna2Button>())
            {

                if (hasorder(btn.Text) == true)
                {
                    btn.FillColor = Color.Brown;

                }
                else { btn.FillColor = Color.Teal; }


            }
        }
        public void gettot() {
            /*double total = 0;
            for (int i=0;i<guna2DataGridView1.Rows.Count;i++) {
                total += Convert.ToDouble(guna2DataGridView1.CurrentRow.Cells[4].Value);
            }
            gunaLabel3.Text = total.ToString();*/
            try
            {
                
                double max = 0;
                con.Open();
                qu = "select sum(total) from orders where cust=$nam and datetime is null";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$nam", gunaLabel2.Text);
                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {

                        max += read.GetDouble(0);


                    }



                }
                con.Close();
                gunaLabel3.Text = max.ToString();



            }
            catch (Exception ex) { gunaLabel3.Text = "0"; }
        }

        private void gunaAdvenceTileButton6_Click(object sender, EventArgs e)
        {
            try
            {
                qu = "UPDATE orders SET qty=$qty,total=$tot WHERE ordid=$ida";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$ida", gunaLabel7.Text);
                int k = Convert.ToInt32(guna2NumericUpDown1.Value);
                double nprc = k * Convert.ToInt32(gunaLabel9.Text);
                cmd.Parameters.AddWithValue("$qty", k);
                cmd.Parameters.AddWithValue("$tot", nprc);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                gunaLabel8.Text = k.ToString();
                //guna2DataGridView1.CurrentRow.Cells[3].Value = k;

                //****gunaLabel9.Text = nprc.ToString();
                //guna2DataGridView1.CurrentRow.Cells[4].Value = nprc;
                int index = guna2DataGridView1.SelectedRows[0].Index;
                showord();
                guna2DataGridView1.ClearSelection();
                guna2DataGridView1.Rows[index].Selected = true;
                gettot();


            }
            catch (Exception ex) { MessageBox.Show("قم بتحديد منتج من الفاتورة بالضغط عليه"); }

        }

        private void gunaAdvenceTileButton5_Click(object sender, EventArgs e)
        {
            try {
                if (Convert.ToInt32(gunaLabel8.Text) == 1)
                {
                    SystemSounds.Exclamation.Play();
                }
                else if (guna2NumericUpDown1.Value > Convert.ToInt32(gunaLabel8.Text) || guna2NumericUpDown1.Value == Convert.ToInt32(gunaLabel8.Text))
                {
                    qu = "UPDATE orders SET qty=$qty,total=$tot WHERE ordid=$ida";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$ida", gunaLabel7.Text);
                    int k = 1;
                    double nprc = k * Convert.ToInt32(gunaLabel9.Text);
                    cmd.Parameters.AddWithValue("$qty", k);
                    cmd.Parameters.AddWithValue("$tot", nprc);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    gunaLabel8.Text = k.ToString();
                    //guna2DataGridView1.CurrentRow.Cells[3].Value = k;

                    //****gunaLabel9.Text = nprc.ToString();
                    //guna2DataGridView1.CurrentRow.Cells[4].Value = nprc;
                    int index = guna2DataGridView1.SelectedRows[0].Index;
                    showord();
                    guna2DataGridView1.ClearSelection();
                    guna2DataGridView1.Rows[index].Selected = true;
                    gettot();
                }
                else
                {
                    qu = "UPDATE orders SET qty=$qty,total=$tot WHERE ordid=$ida";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$ida", gunaLabel7.Text);
                    int k = Convert.ToInt32(gunaLabel8.Text) - Convert.ToInt32(guna2NumericUpDown1.Value);
                    double nprc = k * Convert.ToInt32(gunaLabel9.Text);
                    cmd.Parameters.AddWithValue("$qty", k);
                    cmd.Parameters.AddWithValue("$tot", nprc);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    gunaLabel8.Text = k.ToString();
                    //guna2DataGridView1.CurrentRow.Cells[3].Value = k;

                    //***gunaLabel9.Text = nprc.ToString();
                    //guna2DataGridView1.CurrentRow.Cells[4].Value = nprc;
                    int index = guna2DataGridView1.SelectedRows[0].Index;
                    showord();
                    guna2DataGridView1.ClearSelection();
                    guna2DataGridView1.Rows[index].Selected = true;
                    gettot();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("قم بتحديد منتج من الفاتورة بالضغط عليه");
            }
            
            
        }

        private void gunaAdvenceTileButton7_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0) { MessageBox.Show("هذه الفاتورة فارغة حاليا"); }
            else {
                if (gunaLabel10.Text == "ط")
                {
                    tableclos tcos = new tableclos();
                    tcos.ShowDialog();
                }
                else { MessageBox.Show("دا مش اوردر طرابيزة"); }
                
            }
            
        }

        private void gunaAdvenceTileButton8_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0) { MessageBox.Show("هذه الفاتورة فارغة حاليا"); }
            else
            {
                if (gunaLabel10.Text == "ت")
                {
                    takawatdel tcos = new takawatdel();
                    tcos.ShowDialog();
                }
                else { MessageBox.Show("دا مش اوردر تيك اواى"); }

            }
        }

        private void gunaAdvenceTileButton9_Click(object sender, EventArgs e)
        {

            if (gunaLabel11.Text == "yes")
            {
                clossheft clsh = new clossheft();
                clsh.ShowDialog();
            }
            
        }

        private void gunaAdvenceTileButton10_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("لا يوجد ما يمكن نقله فى هذا الطلب");
            }
            else {
                transorder tran = new transorder();
                tran.ShowDialog();
            }
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells[0].Value);
            guna2DataGridView1.Rows.Clear();
            qu = "DELETE FROM orders WHERE ordid=ordid and cust=$cust and totalallafdes is NULL";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$cust",gunaLabel2.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            showord();
            greenlights();
            gettot();
        }
    } }
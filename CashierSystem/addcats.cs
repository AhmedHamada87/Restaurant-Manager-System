using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace CashierSystem
{
    public partial class addcats : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public addcats()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            loaddata();
        }

        private void loaddata()
        {
            gunaDataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("Select * From cats", con);
            gunaDataGridView1.RowTemplate.Height = 30;
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    gunaDataGridView1.Rows.Add(new object[] {
                    read.GetValue(0),
                    read.GetValue(1),
                    });
                }
            }
            gunaDataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
            gunaDataGridView1.EnableHeadersVisualStyles = false;
            gunaDataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gunaDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

        }

        private void addcats_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (check() == true) { MessageBox.Show("هذا القسم موجود بالفعل"); }
            else {
                try
                {

                    qu = "INSERT INTO cats (name) VALUES ($nam)";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$nam", gunaTextBox1.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loaddata();
                    gunaTextBox1.Clear();
                    var erm = Application.OpenForms["Form1"] as Form1;
                    erm.loadcats();
                    gunaLabel2.Visible = false;
                    gunaTextBox2.Visible = false;
                    gunaButton3.Visible = false;
                    gunaButton4.Visible = false;

                }
                catch (Exception ex) { MessageBox.Show("حدث خطا غير معروف تواصل مع البشمهندس أحمد حمادة لحله فى اقرب وقت 01011210118"); }
            }
            
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int id;
            if (e.ColumnIndex == 3)
            {
                try
                {
                    if (MessageBox.Show("هل أنت متأكد من حذف هذا القسم؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        id = Convert.ToInt32(gunaDataGridView1.CurrentRow.Cells[0].Value);
                        gunaDataGridView1.Rows.Clear();
                        qu = "DELETE FROM cats WHERE id=$ida";
                        cmd = new SqliteCommand(qu, con);
                        cmd.Parameters.AddWithValue("$ida", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        gunaLabel2.Visible = false;
                        gunaTextBox2.Visible = false;
                        gunaButton3.Visible = false;
                        gunaButton4.Visible = false;
                        loaddata();
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("هذا القسم به العديد من الاصناف برجاء حذف هذه الاصناف و اعادة المحاولة");
                    }

                


            }

            if (e.ColumnIndex == 2)
            {
                id = Convert.ToInt32(gunaDataGridView1.Rows[e.RowIndex].Cells[0].Value);
                gunaTextBox2.Text = gunaDataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                gunaLabel2.Visible = true;
                gunaTextBox2.Visible = true;
                gunaButton4.Visible = true;
                gunaButton3.Visible = true;
                idlabel.Text = id.ToString();


            }
        }
        public bool check() {
            con.Open();
            qu = "SELECT * FROM cats WHERE name=$na";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$na",gunaTextBox1.Text);
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count++;
            }
            if (count > 0) { return true; }
            else return false;
        }

        public bool checkupdate()
        {
            con.Open();
            qu = "SELECT * FROM cats WHERE name=$na";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$na", gunaTextBox2.Text);
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count++;
            }
            if (count > 0) { return true; }
            else return false;
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if (checkupdate() == true) { MessageBox.Show("هذا القسم موجود بالفعل"); }
            else {
                qu = "UPDATE cats SET name=$nam WHERE id=$ida";
                //cmd.Parameters.AddWithValue("$id", idlabel);
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$ida", idlabel.Text);
                cmd.Parameters.AddWithValue("$nam", gunaTextBox2.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تم تغيير اسم القسم بنجاح");
                loaddata();
                gunaLabel2.Visible = false;
                gunaTextBox2.Visible = false;
                gunaButton3.Visible = false;
                gunaButton4.Visible = false;
            }
            
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            gunaLabel2.Visible = false;
            gunaTextBox2.Visible = false;
            gunaButton3.Visible = false;
            gunaButton4.Visible = false;
        }
    }
}

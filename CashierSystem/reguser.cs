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
    public partial class reguser : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public reguser()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            loaddata();
        }

        private void loaddata()
        {
            gunaDataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("Select * From users", con);
            gunaDataGridView1.RowTemplate.Height = 30;
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    gunaDataGridView1.Rows.Add(new object[] {
                    read.GetValue(0),
                    read.GetValue(1),
                    read.GetValue(2),
                    read.GetValue(3),
                    });
                }
            }
            gunaDataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
            gunaDataGridView1.EnableHeadersVisualStyles = false;
            gunaDataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gunaDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            if (check() == true) { MessageBox.Show("هذا القسم موجود بالفعل"); }
            else
            {
                try
                {

                    qu = "INSERT INTO users (user,pass,byte) VALUES ($nam,$pas,$byt)";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$nam", gunaTextBox1.Text);
                    cmd.Parameters.AddWithValue("$pas", gunaTextBox2.Text);
                    int mod = 0;
                    if (guna2CheckBox1.Checked == true) { mod = 1; }
                    cmd.Parameters.AddWithValue("$byt", mod.ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loaddata();
                    gunaTextBox1.Clear();
                    gunaTextBox2.Clear();
                    guna2CheckBox1.Checked = false;
                    panel3.Visible = false;

                }
                catch (Exception ex) { MessageBox.Show("حدث خطا غير معروف تواصل مع البشمهندس أحمد حمادة لحله فى اقرب وقت 01011210118"); }
            }

        }

        public bool check()
        {
            con.Open();
            qu = "SELECT * FROM users WHERE user=$na";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$na", gunaTextBox1.Text);
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

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id;
            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("هل أنت متأكد من حذف هذا المستخدم؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    id = Convert.ToInt32(gunaDataGridView1.CurrentRow.Cells[0].Value);
                    gunaDataGridView1.Rows.Clear();
                    qu = "DELETE FROM users WHERE id=$ida";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$ida", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    panel3.Visible = false;
                    loaddata();
                }


            }

            if (e.ColumnIndex == 4)
            {
                id = Convert.ToInt32(gunaDataGridView1.Rows[e.RowIndex].Cells[0].Value);
                gunaTextBox3.Text = gunaDataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                gunaTextBox4.Text = gunaDataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (gunaDataGridView1.CurrentRow.Cells[3].Value.ToString() == "1") { guna2CheckBox2.Checked = true; }
                else { guna2CheckBox2.Checked = false; }
                panel3.Visible = true;
                idlabel.Text = id.ToString();


            }
        }
        

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            
           
                int mod = 0;
                qu = "UPDATE users SET user=$nam,pass=$pas,byte=$byt WHERE id=$ida";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$ida", idlabel.Text);
                cmd.Parameters.AddWithValue("$nam", gunaTextBox3.Text);
                cmd.Parameters.AddWithValue("$pas", gunaTextBox4.Text);
            if (guna2CheckBox2.Checked == true) { mod = 1; }
            else { mod = 0; }
                cmd.Parameters.AddWithValue("$byt", mod);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تم تغيير بيانات المستخدم بنجاح");
                loaddata();
                panel3.Visible = false;
            
        }
    }
}

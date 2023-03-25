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
    public partial class captinsedit : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public captinsedit()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= cafedb.db");
            loaddata();
        }

        public void loaddata()
        {
            gunaDataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("Select * From capt", con);
            gunaDataGridView1.RowTemplate.Height = 50;
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
            this.Close();
        }

        private void captinsedit_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            newcaptin ncpt = new newcaptin();
            ncpt.ShowDialog();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id;
            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show("هل أنت متأكد من حذف الكابتن ؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    id = Convert.ToInt32(gunaDataGridView1.CurrentRow.Cells[0].Value);
                    gunaDataGridView1.Rows.Clear();
                    qu = "DELETE FROM capt WHERE id=$ida";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$ida", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loaddata();
                }


            }

            if (e.ColumnIndex == 4)
            {
                id = Convert.ToInt32(gunaDataGridView1.Rows[e.RowIndex].Cells[0].Value);
                gunaLabel2.Text = id.ToString();
                updcaptin ppin = new updcaptin();
                ppin.ShowDialog();

            }
        }
    }
}

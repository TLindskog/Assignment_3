using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3
{
    public partial class Threads : Form
    {
        public Threads()
        {
            InitializeComponent();
        }

       

        SqlConnection con = new SqlConnection(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Database.mdf;integrated security=True;MultipleActiveResultSets=True;");

        SqlCommand cmd;
        DataTable dtThreads, dtPosts;
        private int id;
        private bool v;

        private void Threads_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            cmd = new SqlCommand("SELECT * FROM Threads",con);
            dtThreads = new DataTable();
            dtThreads.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dtThreads;
            cmd = new SqlCommand("SELECT * FROM Posts", con);
             dtPosts = new DataTable();
            dtPosts.Load(cmd.ExecuteReader());
            dataGridView2.DataSource = dtPosts;
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            new Form1(id,true).ShowDialog();
        }

        private void newPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Posts(int.Parse(dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString()),false).ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //ForumEntities entity = new ForumEntities();
           
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            cmd = new SqlCommand("SELECT Posts.* FROM Threads Inner Join Posts on Threads.Id=Posts.ThreadId where Threads.Id='"+id+"'", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView2.DataSource = dt;
        }

        private void deleteThreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            cmd = new SqlCommand("DELETE FROM Posts Where ThreadId='" + id + "'",con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("DELETE FROM Threads Where Id='" + id + "'",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Thread Removed Success");

        }

        private void deletePostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
          
            cmd = new SqlCommand("DELETE FROM Threads Where Id='" + id + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Post Removed Success");
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
            new Posts(id, true).ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dtThreads.DefaultView;
            dv.RowFilter = string.Format("ThreadName Like '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv;
        }
    }
}

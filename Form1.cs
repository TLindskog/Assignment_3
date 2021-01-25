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
    public partial class Form1 : Form
    {
        private int id;
        private bool v;
        SqlConnection con = new SqlConnection(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Database.mdf;integrated security=True;MultipleActiveResultSets=True;");

        SqlCommand cmd;
        public Form1()
        {
            con.Open();
            InitializeComponent();
        }

        public Form1(int id, bool v)
        {
            con.Open();
            InitializeComponent();
            if (v)
            {
                ForumEntities entity = new ForumEntities();

                var thread = entity.Threads.FirstOrDefault(x => x.Id == id);
                richTextBox1.Text = thread.ThreadName;
                textBox1.Text = thread.CreatedBy;
                dateTimePicker1.Value = DateTime.Parse(thread.DateCreated.ToString());

            }
            this.id = id;
            this.v = v;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (v)
            {
                cmd = new SqlCommand("UPDATE Threads SET ThreadName='" + richTextBox1.Text + "' , CreatedBy='" + textBox1.Text + "', DateCreated='" + dateTimePicker1.Value + "' WHERE Id='"+id+"'", con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("update success");
            }
            else
            {
                ForumEntities entity = new ForumEntities();
                Thread thread = new Thread
                {
                    ThreadName = richTextBox1.Text,
                    CreatedBy = textBox1.Text,
                    DateCreated = dateTimePicker1.Value
                };
                entity.Threads.Add(thread);
                entity.SaveChanges();
                MessageBox.Show("Thread Created Go to view all threads to create posts");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Threads th = new Threads();
            th.ShowDialog();
        }
    }
}

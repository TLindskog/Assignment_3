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
    public partial class Posts : Form
    {
        int Id;
        bool IsEdit;
        SqlConnection con = new SqlConnection(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Database.mdf;integrated security=True;MultipleActiveResultSets=True;");
        SqlCommand cmd;
        public Posts(int id,bool isEdit)
        {
            con.Open();
            InitializeComponent();
            Id = id;
            IsEdit = isEdit;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           if(IsEdit)
            {
                cmd = new SqlCommand("UPDATE Posts SET PostDescription='" + richTextBox1.Text + "' , CreatedBy='" + textBox1.Text + "', DateCreated='" + dateTimePicker1.Value + "' WHERE Id='" + Id + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("update success");

            }
           else
            {
                ForumEntities entity = new ForumEntities();
                Post post = new Post
                {
                    PostDescription = richTextBox1.Text,
                    DateCreated = dateTimePicker1.Value,
                    ThreadId = Id,
                    CreatedBy = textBox1.Text


                };
                entity.Posts.Add(post);
                entity.SaveChanges();
                MessageBox.Show("Post Create");

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace stock_management_project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //checking user and pass

            SqlConnection con = new SqlConnection(@"Data Source=HICHEM\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT *  FROM [dbo].[login] where username = '"+ textBox1.Text +"' and password = '"+ textBox2.Text +"'",con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
           else
            {
                MessageBox.Show("The username and password doesn't match","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                button2_Click(sender, e);
            }

        }
    }
}

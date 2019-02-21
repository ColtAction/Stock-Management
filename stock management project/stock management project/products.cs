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
    public partial class products : Form
    {
        public products()
        {
            InitializeComponent();
        }

        private void products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //insert or Update logic
            SqlConnection con = new SqlConnection(@"Data Source=HICHEM\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            con.Open();
            bool status = false;
            if(comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            var SqlQuery = "";
            if (IfProductExist(con, textBox1.Text))
            {
                SqlQuery = @"UPDATE [products] SET [productName] = '" + textBox2.Text + "', [productStatus] = '" + status + "' WHERE [productCode] = '" + textBox1.Text + "'";
    }
            else
            {
                SqlQuery = @"INSERT INTO [dbo].[products] ([productCode] ,[productName] ,[productStatus]) VALUES
                            ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + status + "')";
            }

            SqlCommand cmd = new SqlCommand(SqlQuery , con);
            cmd.ExecuteNonQuery();
            con.Close();
            //reading data
            LoadData();
        }

        private bool IfProductExist(SqlConnection con, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 from [products] where [productCode]='"+ productCode +"' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=HICHEM\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("Select * from [dbo].[products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["productCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["productName"].ToString();
                if ((bool)item["productStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Inactive";
                }
                //dataGridView1.Rows[n].Cells[2].Value = item["productStatus"].ToString();
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            //comboBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(@"Data Source=HICHEM\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            
            var SqlQuery = "";
            if (IfProductExist(con, textBox1.Text))
            {
                con.Open();
                SqlQuery = SqlQuery = @"DELETE FROM[products]  WHERE [productCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                MessageBox.Show("The item code doesn't match with any product..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            LoadData();



        }
    }

}

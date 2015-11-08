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

namespace TelephoneDiaryApp
{
    public partial class Phone : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=MILOS-PC\SQLEXPRESS;Initial Catalog=PhoneDB;Integrated Security=True");

        public Phone()
        {
            InitializeComponent();
            Display();
        }

        private void Phone_Load(object sender, EventArgs e)
        {
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();

            string komanda = @"UPDATE MobileTable SET First='"+ textBox1.Text +"', Last='" + textBox2.Text + "', Mobile='" + textBox3.Text + "', Email='" + textBox4.Text + "', Category='" + comboBox1.Text + "' WHERE (Mobile='" + textBox3.Text + "')";

            SqlCommand cmd = new SqlCommand(komanda, con);
            cmd.ExecuteNonQuery();

            Display();

            con.Close();
            MessageBox.Show("Update succeded", "Success");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;

            textBox1.Focus();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            con.Open();

            string komanda = @"INSERT INTO MobileTable (First, Last, Mobile, Email, Category)
                             VALUES ('"+ textBox1.Text +"','"+ textBox2.Text +"','"+ textBox3.Text +"','" + textBox4.Text + "','" + comboBox1.Text + "')";
            SqlCommand cmd = new SqlCommand(komanda, con);
            cmd.ExecuteNonQuery();

            Display();

            con.Close();
            MessageBox.Show("Contact saved!","Success");
        }

        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM MobileTable", con);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();

            }
        
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString() ;
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString() ;
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();

            string komanda = @"DELETE FROM MobileTable WHERE (Mobile='" + textBox3.Text + "')";
                             
            SqlCommand cmd = new SqlCommand(komanda, con);
            cmd.ExecuteNonQuery();

            Display();

            con.Close();
            MessageBox.Show("Delete succeded", "Success");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM MobileTable WHERE (First like '%" + textBox5.Text + "%') or (Last like '%" + textBox5.Text + "%')", con);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["First"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();

            }
        }



    }
}

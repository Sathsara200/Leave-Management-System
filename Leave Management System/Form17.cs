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

namespace Leave_Management_System
{
    public partial class Form17 : Form
    {
        public Form17()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        private DataTable dataTable;
        public void LData(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoGenerateColumns = false;
                }
            }
        }
        private void Form17_Load(object sender, EventArgs e)
        {
            LData("Select * from Roaster");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            Form6 frm1 = new Form6();
            frm1.Show();
            this.Close();
        }
    }
}

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
    public partial class Form7 : Form
    {
        public static Form7 Instance;

        public Form7()
        {
            InitializeComponent();
            Instance = this;
        }

        string connectionString = @"Data Source=DESKTOP-J1972OJ\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        internal Action<DataTable> DataLoaded;

        

        public void LoadData(string query)
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

       

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form6 frm1 = new Form6();
            frm1.Show();
            this.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            LoadData("select Employe_Id,Salary from Employe where Employe_Id = '" + Form1.instance.tb1.Text + "'");
        }
    }
}

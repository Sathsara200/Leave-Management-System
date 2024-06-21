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
using System.Xml.Linq;

namespace Leave_Management_System
{
    public partial class Form6 : Form
    {
        public static Form6 Instance;
        public Form6()
        {
            InitializeComponent();
            Instance = this;

        }
        string connectionString = @"Data Source=DESKTOP-J1972OJ\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        private void btnLeaveDetails_Click(object sender, EventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Close();
        }

        private void btnSalaryDetails_Click(object sender, EventArgs e)
        {
            Form7 frm1 = new Form7();
            frm1.Show();
            this.Close();
        }

        private void btnYourDetails_Click(object sender, EventArgs e)
        {
            Form8 frm1 = new Form8();
            frm1.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
            

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
               
                string sql = "SELECT Name FROM Employe WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read(); // Assuming you want the first row
                label3.Text = reader["Name"].ToString(); // Get the value from the "Name" column
                reader.Close();

            }


        }
    }
}

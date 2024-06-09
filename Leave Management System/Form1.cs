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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Leave_Management_System
{
    public partial class Form1 : Form
    {


        public static Form1 instance;
        public System.Windows.Forms.TextBox tb1;
        public Form1()
        {
            InitializeComponent();
            instance = this;
            tb1 = txtUserId;
        }

        string connectionString = @"Data Source=DESKTOP-J1972OJ\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";

        public Action<DataTable> DataLoaded { get; internal set; }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtUserId_Enter(object sender, EventArgs e)
        {
            if (txtUserId.Text == "User Id")
            {
                txtUserId.Text = "";
                txtUserId.ForeColor = Color.Black;

            }
        }

        private void txtUserId_Leave(object sender, EventArgs e)
        {
            if (txtUserId.Text == "")
            {
                txtUserId.Text = "User Id";
                txtUserId.ForeColor = Color.Silver;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.PasswordChar = '*';
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Silver;
                txtPassword.PasswordChar = '\0';
            }
        }

       

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userId = txtUserId.Text;
            string password = txtPassword.Text;

            // Validate user input (optional, but recommended for user experience)
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both User ID and Password.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM User_Login WHERE Employe_Id = @Employe_Id AND Emp_Password = @Emp_password";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Employe_Id", userId);
                command.Parameters.AddWithValue("@Emp_Password", password);

                int count = (int)command.ExecuteScalar(); // Count matching credentials

                if (count > 0)
                {
                    
                   
                    // Login successful!
                    MessageBox.Show("Login successful!");
                    Form2 frm1 = new Form2();
                    frm1.Show();
                    this.Hide();


                }
                else
                {
                    MessageBox.Show("Invalid User ID or Password.");
                }
            }
        }
    }
}

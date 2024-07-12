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
            if (txtUserId.Text == "Enter user id or user name")
            {
                txtUserId.Text = "";
                txtUserId.ForeColor = Color.Black;

            }
        }

        private void txtUserId_Leave(object sender, EventArgs e)
        {
            if (txtUserId.Text == "")
            {
                txtUserId.Text = "Enter user id or user name";
                txtUserId.ForeColor = Color.Silver;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Enter password")
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
                txtPassword.Text = "Enter password";
                txtPassword.ForeColor = Color.Silver;
                txtPassword.PasswordChar = '\0';
            }
        }

       

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userId = txtUserId.Text; Console.ReadLine();
            int number;

            string password = txtPassword.Text;
            if (int.TryParse(userId, out number)) 
            {
                // Validate user input (optional, but recommended for user experience)
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both User ID and Password.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT COUNT(*) FROM Employe WHERE Employe_Id = @Employe_Id AND Password = @Password";
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Employe_Id", userId);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar(); // Count matching credentials

                    if (count > 0)
                    {


                        // Login successful!
                        MessageBox.Show("Login successful!");
                        Form6 frm1 = new Form6();
                        frm1.Show();
                        this.Hide();


                    }
                    else
                    {
                        MessageBox.Show("Invalid User ID or Password.");
                    }
                }
            }
            else
            {
                if (txtUserId.Text == "Admin" && txtPassword.Text == "Admin123")
                {
                    MessageBox.Show("Welcome Admin", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form13 frm1 = new Form13();
                    frm1.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Login Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserId.Text = "";
                    txtPassword.Clear();
                    txtUserId.Focus();
                }
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnAreYouAdmin_Click(object sender, EventArgs e)
        {
          
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        

      
    }
}

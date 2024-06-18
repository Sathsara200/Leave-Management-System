using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Leave_Management_System
{
    public partial class Form4 : Form
    {
        public static Form4 Instance;

        public Form4()
        {
            InitializeComponent();
           
        }

        private void TxtExit_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "User name";
                txtUserName.ForeColor = Color.Silver;
            }
        }

        private void txtAdminPassword_Enter(object sender, EventArgs e)
        {
            if (txtAdminPassword.Text == "Password")
            {
                txtAdminPassword.Text = "";
                txtAdminPassword.ForeColor = Color.Black;
                txtAdminPassword.PasswordChar = '*';
            }
        }

        private void txtAdminPassword_Leave(object sender, EventArgs e)
        {
            if (txtAdminPassword.Text == "")
            {
                txtAdminPassword.Text = "Password";
                txtAdminPassword.ForeColor = Color.Silver;
                txtAdminPassword.PasswordChar = '\0';
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Admin" && txtAdminPassword.Text == "Admin123")
            {
                MessageBox.Show("Welcome Admin", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form5 frm1 = new Form5();
                frm1.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Login Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Text = "";
                txtAdminPassword.Clear();
                txtUserName.Focus();
            }
        }

        private void txtUserName_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "User name")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}

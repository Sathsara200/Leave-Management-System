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
  
    public partial class Form13 : Form
    {
        private object identityValue;

        public Form13(int identityValue)
        {
            InitializeComponent();
            int identityValue1 = identityValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-J1972OJ\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False");

            string sql = "INSERT INTO Employe (Employe_Id,Emp_Password) VALUES (@Employe_Id, @Emp_Password);";
            SqlCommand cmd = new SqlCommand(sql, con);


            cmd.Parameters.AddWithValue("@Employe_Id",  identityValue);
            cmd.CommandType = CommandType.Text;
            

            cmd.Parameters.AddWithValue("@Name", txtPassword.Text);
           

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Successfully added password", "Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}

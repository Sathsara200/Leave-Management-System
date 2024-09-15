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
    public partial class Form12 : Form
    {
        public static Form12 Lvalue;
        public Form12()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False");

            SqlCommand cmd = new SqlCommand("insert into Employe values (@Name,@Phone_Number,@Address,@Date_Of_Birth,@Gender,@City,@Password,@annual_leaves,@casual_leaves,@shorts_leaves)", con);

            

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@Employe_Id", txtEmployeId.Text);
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Phone_Number", txtPhoneNumber.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Date_Of_Birth", this.dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@Gender",txtGender.Text);
            cmd.Parameters.AddWithValue("@City", txtCity.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@annual_leaves", 4);
            cmd.Parameters.AddWithValue("@casual_leaves", 8);
            cmd.Parameters.AddWithValue("@shorts_leaves", 2);

            con.Open();
            cmd.ExecuteNonQuery();
            

            MessageBox.Show("Applied successfully", "Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Form11.Lvalue.LData("select Employe_Id, Name, Phone_Number, Address, Date_Of_Birth, Gender, City, Password from Employe");
        }

        private void Form12_Load(object sender, EventArgs e)
        {

        }
    }
}


using Microsoft.SqlServer.Server;
using System;
using System.Collections;
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
    public partial class Apply : Form
    {
       
        public static Apply value;
        public static Apply lvalue;
        public static Apply Instance;
        public Apply()
        {
            InitializeComponent();
            Instance = this;
        }

        string connectionString = @"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        internal Action<DataTable> DataLoaded;

        private void txtEmployeId_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Form2.value.DataLoaded += OnDataLoaded; // Subscribe to the event
        }

        private void OnDataLoaded(DataTable data)
        {
            // Perform actions after data is loaded in Form1
            // (e.g., update UI elements in Form2)
        }
        public int annual;
        public int casual;
        public int shorts;

        private void btnApply_Click(object sender, EventArgs e)
        {


          
            try 
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False");

                SqlCommand cmd = new SqlCommand("insert into Emp_Leave values ('"+int.Parse(Form1.instance.tb1.Text)+ "','"+comboBox1.Text+ "','"+dateTimePicker1.Text+ "','"+int.Parse(txtCountOfDate.Text)+ "','"+dateTimePicker2.Text+ "','"+dateTimePicker3.Text+ "','"+txtDescription.Text+ "','"+"Waiting for approval"+ "')", con);


             
                con.Open();
                cmd.ExecuteNonQuery();



                MessageBox.Show("Applied successfully", "Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form2.value.LoadData("select * from Emp_Leave where Employe_Id = '" + Form1.instance.tb1.Text + "'");
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred during update: " + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           // }



        }

        public void Annual()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                                           "UPDATE Employe SET annual_leaves = @annual_leaves " +

                                           "WHERE Employe_Id = @EmployeId", connection);
                annual = annual - 1;
                cmd.Parameters.AddWithValue("@EmployeId", Form1.instance.tb1.Text);
                cmd.Parameters.AddWithValue("@annual_leaves", annual);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                Form2.lvalue.loadLbl();
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
           using (SqlConnection connection = new SqlConnection(connectionString))
           {
            connection.Open();

            string sql = "SELECT annual_leaves, casual_leaves, shorts_leaves FROM Employe WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read(); // Assuming you want the first row
            int annual = Convert.ToInt32(reader["annual_leaves"].ToString()); // Get the value from the "annual" column
            int casual = Convert.ToInt32(reader["casual_leaves"].ToString());
            int shorts = Convert.ToInt32(reader["shorts_leaves"].ToString());

            reader.Close();
            connection.Close();
           }
        }
    }
}

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
    public partial class Form16 : Form
    {
        public static Form16 value;
        public static Form16 lvalue;
        public static Form16 Instance;
        public Form16()
        {
            InitializeComponent();
            Instance = this;    
        }
        string connectionString = @"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        internal Action<DataTable> DataLoaded;
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form16_Load(object sender, EventArgs e)
        {

        }


        public void Shorts()
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
                shorts = shorts - 1;
                SqlCommand cmmd = new SqlCommand("UPDATE Employe SET shorts_leaves = '" + shorts + "' WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'", connection);

                //cmd.Parameters.AddWithValue("@EmployeId", Form1.instance.tb1.Text);
                //cmd.Parameters.AddWithValue("@annual_leaves", annual);


                cmmd.ExecuteNonQuery();
                connection.Close();

                Form2.lvalue.loadLbl();

            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False");

            try
            {
                con.Open();

                string sql = "SELECT annual_leaves, casual_leaves, shorts_leaves FROM Employe WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read(); // Assuming you want the first row
                int annual = Convert.ToInt32(reader["annual_leaves"].ToString()); // Get the value from the "annual" column
                int casual = Convert.ToInt32(reader["casual_leaves"].ToString());
                int shorts = Convert.ToInt32(reader["shorts_leaves"].ToString());


                reader.Close();

                if (0 < shorts)
                {

                    SqlCommand cmmd = new SqlCommand("insert into Emp_Leave values ('" + int.Parse(Form1.instance.tb1.Text) + "','" + "Short leave" + "','" + dateTimePicker1.Text + "','" + DBNull.Value + "','" + DBNull.Value + "','" + DBNull.Value + "','" + txtDescription.Text + "','"+dateTimePicker2.Text+"','"+comboBox1.Text+"','" + "Waiting for approval" + "')", con);




                    cmmd.ExecuteNonQuery();



                    MessageBox.Show("Applied successfully", "Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2.value.LoadData("select * from Emp_Leave where Employe_Id = '" + Form1.instance.tb1.Text + "'");
                    Shorts();
                    con.Close();

                }             
                else
                {
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during update: " + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

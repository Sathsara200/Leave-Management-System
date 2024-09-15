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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Leave_Management_System
{
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
        }
        string connectionString = @"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        private void loadRoster(string query)
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
        private void Form15_Load(object sender, EventArgs e)
        {
            loadRoster("select * from Roaster");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Search = txtSearch.Text; Console.ReadLine();
            int number;
            if (int.TryParse(Search, out number))
                if (txtSearch.Text != "")
                {

                    loadRoster("select * from Roaster where Employe_Id = '" + txtSearch.Text + "'");

                }
                else
                {
                    MessageBox.Show(" Something went wrong ", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else
            {
                MessageBox.Show(" Something went wrong ", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False");

                SqlCommand cmd = new SqlCommand("insert into Roaster values ('" + txtRoaster.Text + "','" + txtEmploye.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','" + txtMeridian.Text + "')", con);



                con.Open();
                cmd.ExecuteNonQuery();



                MessageBox.Show("Applied successfully", "Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadRoster("select * from Roaster");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during update: " + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate user input (if applicable)
                if (string.IsNullOrEmpty(txtEmploye.Text) ||
                    string.IsNullOrEmpty(txtRoaster.Text) ||
                    // Add validation for other textboxes as needed
                    !DateTime.TryParse(dateTimePicker1.Text, out DateTime dob))
                {
                    MessageBox.Show("Please enter all required information.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method if validation fails
                }

                // Connection string handling
                string connectionString = "Data Source=DESKTOP-J1972OJ\\SQLEXPRESS;Initial Catalog=\"Leave Management System\";Integrated Security=True;Encrypt=False";
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Connection string is missing. Please configure it.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Improved SQL command with parameterized queries
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Roaster SET Roaster_Id = @Roaster_Id, Employe_Id = @Empoye_Id, " +
                        "Start_Time = @Start_Time, End_Time = @End_Time, Meridian = @Meridian, " +
                        "WHERE Employe_Id = @EmployeId", con);

                    // Add parameters with appropriate data types (consider using SqlParameter for more control)
                    cmd.Parameters.AddWithValue("@Roaster_Id", txtRoaster.Text);
                    cmd.Parameters.AddWithValue("@Employe_Id", txtEmploye.Text);
                    cmd.Parameters.AddWithValue("@Start_Time", DateTime.Parse(dateTimePicker1.Text));
                    cmd.Parameters.AddWithValue("@End_Time", DateTime.Parse(dateTimePicker2.Text));
                    cmd.Parameters.AddWithValue("@Meridian", txtMeridian.Text); // Ensure valid date format
                   


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record successfully updated!", "Update",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadRoster("select * from Employe"); // Assuming LoadData() refreshes form data
                    }
                    else
                    {
                        MessageBox.Show("No records were updated. Please check the provided data and try again.",
                            "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

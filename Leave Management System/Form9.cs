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
    public partial class Form9 : Form
    {
        public static Form9 rnumber;
        public System.Windows.Forms.TextBox num1;
        public Form9()
        {
            InitializeComponent();
            rnumber = this;
            num1 = txtSearch;
        }

        string connectionString = @"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        internal Action<DataTable> DataLoaded;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                // Get employee ID from user input
                string employeeId = txtSearch.Text;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Improved SQL query with parameterized date range
                        string queryString = @"
                SELECT *
                FROM Emp_Leave
                WHERE Employe_Id = @EmployeeId
                AND Admin_Remark = 'Approved'
                AND Date_Of_Commencing_Leave BETWEEN @StartDate AND @EndDate";

                        SqlCommand command = new SqlCommand(queryString, connection);

                        // Add parameters with proper data types and values
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);
                        command.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                        command.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date.AddDays(1).Subtract(new TimeSpan(0, 0, 0, 1)));  // Ensure end date includes entire day

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Create a new DataTable to hold the results
                            DataTable dataTable = new DataTable();

                            // Fill the DataTable with retrieved data
                            adapter.Fill(dataTable);

                            // Bind the DataTable to the DataGridView for display
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle database-related exceptions more specifically
                        MessageBox.Show("An error occurred while connecting to the database: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        // Handle other general exceptions
                        MessageBox.Show("An unexpected error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
                // Inform user about empty search text
                MessageBox.Show("Please enter an employee ID to search.");
            }




          

        }
        

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
            Form13 frm1 = new Form13();
            frm1.Show();
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            LoadData("select * from Emp_Leave ");
        }

        private void btnPendingLeave_Click(object sender, EventArgs e)
        {
            Form10 frm1 = new Form10();
            frm1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form14 frm1 = new Form14();
            frm1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate user input (if applicable)
                if (string.IsNullOrEmpty(txtAnnual.Text) ||
                    string.IsNullOrEmpty(txtCasual.Text) ||
                    string.IsNullOrEmpty(txtShort.Text))
                {
                    MessageBox.Show("Please enter all required information.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method if validation fails
                }

                // Connection string handling
                string connectionString = "Data Source=DESKTOP-IM081Q0\\SQLEXPRESS;Initial Catalog=\"Leave Management System\";Integrated Security=True;Encrypt=False";
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
                    SqlCommand cmd = new SqlCommand("UPDATE Employe SET annual_leaves = '" + int.Parse(txtAnnual.Text) + "', casual_leaves = '" + int.Parse(txtCasual.Text) + "', shorts_leaves = '" + int.Parse(txtShort.Text) + "'", con);

                    // Add parameters with appropriate data types (consider using SqlParameter for more control)



                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record successfully updated!", "Update",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                       // Assuming LoadData() refreshes form data
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

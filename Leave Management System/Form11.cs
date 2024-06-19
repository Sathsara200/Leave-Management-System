using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Leave_Management_System
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
            dataTable = new DataTable();
            LoadData();
        }
       
        string connectionString = @"Data Source=DESKTOP-J1972OJ\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        private DataTable dataTable;
        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                dataTable.Load(command.ExecuteReader());
                dataGridView1.DataSource = dataTable;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form12 frm1 = new Form12();
            frm1.Show();
           
        }

        private void Form11_Load(object sender, EventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the ID from the textbox
            int idToDelete;
            if (!int.TryParse(txtEmployeId.Text, out idToDelete))
            {
                MessageBox.Show("Please enter a valid integer ID.");
                return;
            }

            using (var connection = new SqlConnection("Data Source=DESKTOP-J1972OJ\\SQLEXPRESS;Initial Catalog=\"Leave Management System\";Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    connection.Open();

                    // Delete child rows first (optional, depending on foreign key constraints)
                    string deleteChildSql = "DELETE FROM Emp_Leave WHERE Employe_Id = @id";
                    using (var childCommand = new SqlCommand(deleteChildSql, connection))
                    {
                        childCommand.Parameters.AddWithValue("@id", idToDelete);
                        childCommand.ExecuteNonQuery();
                    }

                    // Delete the row with the primary key
                    string deleteMainSql = "DELETE FROM Employe WHERE Employe_Id = @id";
                    using (var mainCommand = new SqlCommand(deleteMainSql, connection))
                    {
                        mainCommand.Parameters.AddWithValue("@id", idToDelete);
                        mainCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Deletion successful!");
                    LoadData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error deleting data: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) 
            {
                dataGridView1.CurrentRow.Selected = true;
                txtEmployeId.Text = dataGridView1.Rows[e.RowIndex].Cells["Employe_Id"].FormattedValue.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                txtPhoneNumber.Text = dataGridView1.Rows[e.RowIndex].Cells["Phone_Number"].FormattedValue.ToString();
                txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].FormattedValue.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells["Date_Of_Birth"].FormattedValue.ToString();
                txtGender.Text = dataGridView1.Rows[e.RowIndex].Cells["Gender"].FormattedValue.ToString();
                txtCity.Text = dataGridView1.Rows[e.RowIndex].Cells["City"].FormattedValue.ToString();
                txtSalary.Text = dataGridView1.Rows[e.RowIndex].Cells["Salary"].FormattedValue.ToString();
                txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells["Password"].FormattedValue.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate user input (if applicable)
                if (string.IsNullOrEmpty(txtEmployeId.Text) ||
                    string.IsNullOrEmpty(txtName.Text) ||
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
                        "UPDATE Employe SET Name = @Name, Phone_Number = @Phone_Number, " +
                        "Address = @Address, Date_Of_Birth = @Date_Of_Birth, Gender = @Gender, " +
                        "City = @City, Salary = @Salary, Password = @Password " +
                        "WHERE Employe_Id = @EmployeId", con);

                    // Add parameters with appropriate data types (consider using SqlParameter for more control)
                    cmd.Parameters.AddWithValue("@EmployeId", txtEmployeId.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Phone_Number", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Date_Of_Birth", DateTime.Parse(dateTimePicker1.Text)); // Ensure valid date format
                    cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    // Convert salary to appropriate numeric type (e.g., decimal, double) if needed
                    cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record successfully updated!", "Update",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Assuming LoadData() refreshes form data
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtEmployeId.Text = "";
            txtName.Text = "";
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtGender.Text = "";
            txtCity.Text = "";
            txtSalary.Text = "";
            txtPassword.Text = "";
        }
    }
}

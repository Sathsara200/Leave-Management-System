using System;
using System.Collections;
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
        public static Form11 Lvalue { get; private set; }

      

        public Form11()
        {
            InitializeComponent();
            dataTable = new DataTable();
            Lvalue = this;
          

        }
       
        string connectionString = @"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        private DataTable dataTable;
        public void LData(string query)
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
            LData("select Employe_Id, Name, Phone_Number, Address, Date_Of_Birth, Gender, City, Password  from Employe"); // Assuming LData() refreshes form data
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

            using (var connection = new SqlConnection("Data Source=DESKTOP-IM081Q0\\SQLEXPRESS;Initial Catalog=\"Leave Management System\";Integrated Security=True;Encrypt=False"))
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
                    LData("select Employe_Id, Name, Phone_Number, Address, Date_Of_Birth, Gender, City, Password from Employe");
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
                    SqlCommand cmd = new SqlCommand("UPDATE Employe SET Name = '"+txtName.Text+"', Phone_Number = '"+int.Parse(txtPhoneNumber.Text)+"', Address = '"+txtAddress.Text+"', Date_Of_Birth = '"+this.dateTimePicker1.Text+"', Gender = '"+txtGender.Text+"', City = '"+txtCity.Text+"', Password = '"+txtPassword.Text+"' WHERE Employe_Id = '"+txtEmployeId.Text+"'", con);

                    // Add parameters with appropriate data types (consider using SqlParameter for more control)
                   
                  

                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record successfully updated!", "Update",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LData("select Employe_Id, Name, Phone_Number, Address, Date_Of_Birth, Gender, City, Password from Employe"); // Assuming LoadData() refreshes form data
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
            
            txtPassword.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Search = txtSearch.Text; Console.ReadLine();
            int number;
            if (int.TryParse(Search, out number))
                if (txtSearch.Text != "")
                {

                    LData("select  Employe_Id, Name, Phone_Number, Address, Date_Of_Birth, Gender, City, Password from Employe where Employe_Id = '" + txtSearch.Text + "'");

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

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form13 frm1 = new Form13();
            frm1.Show();
            this.Close();
        }

        private void btnRoaster_Click(object sender, EventArgs e)
        {
            Form15 frm1 = new Form15();
            frm1.Show();
            this.Close();
        }
    }
}

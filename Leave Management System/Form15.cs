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

                SqlCommand cmd = new SqlCommand("insert into Roaster values ('" + txtRoaster.Text + "','" + txtEmploye.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','" + dateTimePicker3.Text + "')", con);



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
                    SqlCommand cmd = new SqlCommand("UPDATE Roaster SET Employe_Id = '" + txtEmploye.Text + "', Start_Time = '" + dateTimePicker1.Text + "', End_Time = '" + dateTimePicker2.Text + "', Date = '" + dateTimePicker3.Text + "' where Roaster_Id = '"+txtRoaster.Text+"'", con);

                    // Add parameters with appropriate data types (consider using SqlParameter for more control)



                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record successfully updated!", "Update",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadRoster("select * from Roaster"); // Assuming LoadData() refreshes form data
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the ID from the textbox
            int idToDelete;
            if (!int.TryParse(txtRoaster.Text, out idToDelete))
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
                    string deleteChildSql = "DELETE FROM Roaster WHERE Roaster_Id = @id";
                    using (var childCommand = new SqlCommand(deleteChildSql, connection))
                    {
                        childCommand.Parameters.AddWithValue("@id", idToDelete);
                        childCommand.ExecuteNonQuery();
                    }

                   


                    MessageBox.Show("Deletion successful!");
                    loadRoster("select * from Roaster");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error deleting data: " + ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtRoaster.Text = "";
            txtEmploye.Text = "";
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";
            dateTimePicker3.Text = "";
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtRoaster.Text = dataGridView1.Rows[e.RowIndex].Cells["Roaster_Id"].FormattedValue.ToString();
                txtEmploye.Text = dataGridView1.Rows[e.RowIndex].Cells["Employe_Id"].FormattedValue.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells["Start_Time"].FormattedValue.ToString();
                dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells["End_Time"].FormattedValue.ToString();
                dateTimePicker3.Text = dataGridView1.Rows[e.RowIndex].Cells["Date"].FormattedValue.ToString();
               
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form13 frm1 = new Form13();
            frm1.Show();
            this.Close();
        }
    }
}

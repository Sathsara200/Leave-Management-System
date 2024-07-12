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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=DESKTOP-J1972OJ\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        internal Action<DataTable> DataLoaded;

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

        private void Form10_Load(object sender, EventArgs e)
        {
            LoadData("select * from Emp_Leave where Admin_Remark = 'Waiting for approval'" );
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Select entire rows

            // Check if a valid cell (excluding header) is clicked
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the value of the "Admin_Remark" cell for the clicked row
                string adminRemark = dataGridView1.Rows[e.RowIndex].Cells["Admin_Remark"].Value.ToString();

                // Check if "Admin_Remark" is "Waiting for approval"
                if (adminRemark == "Waiting for approval")
                {
                    // Prompt for confirmation and handle cancellation logic
                    if (MessageBox.Show("Are you sure to Approval?", "Approval Decision", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Get id from "Leave_Id" cell (assuming it's in the same row)
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Leave_Id"].FormattedValue.ToString());
                        SqlConnection con = new SqlConnection(connectionString);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand("UPDATE Emp_Leave SET Admin_Remark = 'Approved' WHERE Leave_Id = @id", con);
                            com.Parameters.AddWithValue("@id", id); // Use parameterized queries to prevent SQL injection
                            com.ExecuteNonQuery();
                            MessageBox.Show("Successfully Approved.");
                            int ip = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Employe_Id"].FormattedValue.ToString());
                            LoadData("select * from Emp_Leave where Admin_Remark = 'Waiting for approval'");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Approval leave: " + ex.Message);
                          
                        }
                        finally
                        {
                            con.Close();

                        }
                    }
                    else
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Leave_Id"].FormattedValue.ToString());
                        SqlConnection con = new SqlConnection(connectionString);
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand("UPDATE Emp_Leave SET Admin_Remark = 'Not Approved' WHERE Leave_Id = @id", con);
                            com.Parameters.AddWithValue("@id", id); // Use parameterized queries to prevent SQL injection
                            com.ExecuteNonQuery();
                            MessageBox.Show("Successfully Approval canceled.");
                            int ip = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Employe_Id"].FormattedValue.ToString());
                            LoadData("select * from Emp_Leave where Admin_Remark = 'Waiting for approval'");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Approval leave: " + ex.Message);
                        }
                        finally
                        {
                            con.Close();

                        }// Handle user not confirming cancellation (optional)
                    }
                }
                else
                {
                    // Optional: Inform user the selected row cannot be canceled
                    // MessageBox.Show("This leave request cannot be canceled.");
                }
            }
            else
            {
                // Optional: Inform user they clicked the header (for clarity)
                // MessageBox.Show("Please click on a cell within the data grid.");
            }
        }

    }
}


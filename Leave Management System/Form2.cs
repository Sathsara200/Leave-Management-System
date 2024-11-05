using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Leave_Management_System
{
    public partial class Form2 : Form
    {
        public static Form2 Instance;
        public static Form2 value { get; private set; }
        public static Form2 lvalue { get; private set; }
       
        public Form2()
        {
            InitializeComponent();
            Instance = this;
            value = this;
            lvalue = this;
          

        }

        
          

            
        


        string connectionString = @"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
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

        public void loadLbl()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT annual_leaves, casual_leaves, shorts_leaves FROM Employe WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read(); // Assuming you want the first row
                lblAnnual.Text = reader["annual_leaves"].ToString(); // Get the value from the "annual" column
                lblCasual.Text = reader["casual_leaves"].ToString();
                lblShorts.Text = reader["shorts_leaves"].ToString();

                reader.Close();

            }
        }

       
    private void Form1_Load(object sender, EventArgs e)
        {
            LoadData("select * from Emp_Leave where Employe_Id = '" + Form1.instance.tb1.Text + "'");
            loadLbl();
           
        }

        public void Annual()
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
                annual = annual + 1;
                SqlCommand cmmd = new SqlCommand("UPDATE Employe SET annual_leaves = '" + annual + "' WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'", connection);

                //cmd.Parameters.AddWithValue("@EmployeId", Form1.instance.tb1.Text);
                //cmd.Parameters.AddWithValue("@annual_leaves", annual);


                cmmd.ExecuteNonQuery();
                connection.Close();

                loadLbl();

            }
        }

        public void Casual()
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
                casual = casual + 1;
                SqlCommand cmmd = new SqlCommand("UPDATE Employe SET casual_leaves = '" + casual + "' WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'", connection);

                //cmd.Parameters.AddWithValue("@EmployeId", Form1.instance.tb1.Text);
                //cmd.Parameters.AddWithValue("@annual_leaves", annual);


                cmmd.ExecuteNonQuery();
                connection.Close();

                loadLbl();

            }
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
                shorts = shorts + 1;
                SqlCommand cmmd = new SqlCommand("UPDATE Employe SET shorts_leaves = '" + shorts + "' WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'", connection);

                //cmd.Parameters.AddWithValue("@EmployeId", Form1.instance.tb1.Text);
                //cmd.Parameters.AddWithValue("@annual_leaves", annual);


                cmmd.ExecuteNonQuery();
                connection.Close();

                loadLbl();

            }
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
                    if (MessageBox.Show("Are you sure to cancel?", "Cancel", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Get id from "Leave_Id" cell (assuming it's in the same row)
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Leave_Id"].FormattedValue.ToString());
                        SqlConnection con = new SqlConnection(connectionString);
                        try
                        {
                            con.Open();

                            
                            int i = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Leave_Id"].FormattedValue.ToString());

                            string sq = "SELECT  Leave_Type FROM Emp_Leave WHERE Leave_Id = '" + i + "'";
                            SqlCommand cd = new SqlCommand(sq, con);
                            SqlDataReader rader = cd.ExecuteReader();
                            rader.Read(); // Assuming you want the first row
                            String Type = (string)rader["Leave_Type"]; // Get the value from the "annual" column

                           
                            rader.Close();

                            if (Type == "Annual")
                            {
                               
                                SqlCommand com = new SqlCommand("UPDATE Emp_Leave SET Admin_Remark = 'Canceled' WHERE Leave_Id = @id", con);
                                com.Parameters.AddWithValue("@id", id); // Use parameterized queries to prevent SQL injection
                                com.ExecuteNonQuery();
                                MessageBox.Show("Successfully canceled.");
                                Annual();
                                int ip = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Employe_Id"].FormattedValue.ToString());
                                LoadData("select * from Emp_Leave where Employe_Id = '" + ip + "'");
                            }
                            else if (Type == "Casual")
                            {
                                
                                SqlCommand com = new SqlCommand("UPDATE Emp_Leave SET Admin_Remark = 'Canceled' WHERE Leave_Id = @id", con);
                                com.Parameters.AddWithValue("@id", id); // Use parameterized queries to prevent SQL injection
                                com.ExecuteNonQuery();
                                MessageBox.Show("Successfully canceled.");
                                Casual();
                                int ip = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Employe_Id"].FormattedValue.ToString());
                                LoadData("select * from Emp_Leave where Employe_Id = '" + ip + "'");
                            }
                            else 
                            {
                                
                                SqlCommand com = new SqlCommand("UPDATE Emp_Leave SET Admin_Remark = 'Canceled' WHERE Leave_Id = @id", con);
                                com.Parameters.AddWithValue("@id", id); // Use parameterized queries to prevent SQL injection
                                com.ExecuteNonQuery();
                                MessageBox.Show("Successfully canceled.");
                                Shorts();
                                int ip = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Employe_Id"].FormattedValue.ToString());
                                LoadData("select * from Emp_Leave where Employe_Id = '" + ip + "'");
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error canceling leave: " + ex.Message);
                        }
                        finally
                        {
                            con.Close();
                            
                        }
                    }
                    else
                    {
                        // Handle user not confirming cancellation (optional)
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

        private void btnLeave_Click(object sender, EventArgs e)
        {
            Apply frm1 = new Apply();
            frm1.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form6 frm1 = new Form6();
            frm1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form16 frm1 = new Form16();
            frm1.Show();
            
        }
    }
}

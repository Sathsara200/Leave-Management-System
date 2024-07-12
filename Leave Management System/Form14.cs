using Microsoft.Reporting.WinForms;
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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable(); // Single data table for joined results

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Inner join query (modify table and column names as needed)
                    string sql = @"SELECT e.Name, el.Employe_Id, el.Leave_Type, el.Applied_Date, el.Count_Of_Days, el.Date_Of_Commencing_Leave, el.Date_Of_Recumming_Duties, el.Description, el.Admin_Remark
                                   FROM Employe e
                                   INNER JOIN Emp_Leave el ON e.Employe_ID = el.Employe_ID;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Assuming reportViewer1 is a ReportViewer control
                reportViewer1.LocalReport.DataSources.Clear();

                // Add the data source with joined data
                ReportDataSource source = new ReportDataSource("DataSet3", dt); // Adapt name to your RDLC report
                reportViewer1.LocalReport.DataSources.Add(source);

                reportViewer1.LocalReport.ReportPath = @"C:\\Users\\Sathsara\\source\\repos\\Leave Management System\\Leave Management System\Report1.rdlc";
                reportViewer1.RefreshReport();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while connecting to the database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Consider logging the exception for further analysis
            }
            this.reportViewer1.RefreshReport();
        }
        private string connectionString = @"Data Source=DESKTOP-J1972OJ\SQLEXPRESS;Initial Catalog=Leave Management System;Integrated Security=True;Encrypt=False";
        
        
    }
}

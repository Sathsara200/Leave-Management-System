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
        public static Apply Instance;
        public Apply()
        {
            InitializeComponent();
            Instance = this;
        }

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


        private void btnApply_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-J1972OJ\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False");
           
            SqlCommand cmd = new SqlCommand("insert into Emp_Leave values (@Employe_Id,@Leave_Type,@Applied_Date,@Count_Of_Days,@Date_Of_Commencing_Leave,@Date_Of_Recumming_Duties,@Description,@Admin_Remark)", con);

            cmd.Parameters.AddWithValue("@Admin_Remark", "Waiting for approval");
            
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@Employe_Id", txtEmployeId.Text);
            cmd.Parameters.AddWithValue("@Employe_Id", Form1.instance.tb1.Text);
            cmd.Parameters.AddWithValue("@Leave_Type", comboBox1.Text);
            cmd.Parameters.AddWithValue("@Applied_Date", this.dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@Count_Of_Days", txtCountOfDate.Text);
            cmd.Parameters.AddWithValue("@Date_Of_Commencing_Leave", this.dateTimePicker2.Text);
            cmd.Parameters.AddWithValue("@Date_Of_Recumming_Duties", this.dateTimePicker3.Text);
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
           

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           
            MessageBox.Show("Applied successfully", "Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Form2.value.LoadData("select * from Emp_Leave where Employe_Id = '" + Form1.instance.tb1.Text + "'");

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}

﻿using System;
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
    public partial class Form6 : Form
    {
        public static Form6 Instance;
        public Form6()
        {
            InitializeComponent();
            Instance = this;

        }
        string connectionString = @"Data Source=DESKTOP-IM081Q0\SQLEXPRESS;Initial Catalog=""Leave Management System"";Integrated Security=True;Encrypt=False";
        private void btnLeaveDetails_Click(object sender, EventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Close();
        }

        private void btnSalaryDetails_Click(object sender, EventArgs e)
        {
            
        }

        private void btnYourDetails_Click(object sender, EventArgs e)
        {
            Form8 frm1 = new Form8();
            frm1.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

          

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form8 frm1 = new Form8();
            frm1.Show();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form17 frm1 = new Form17();
            frm1.Show();
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT Name FROM Employe WHERE Employe_Id = '" + Form1.instance.tb1.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read(); // Assuming you want the first row
                label3.Text = reader["Name"].ToString(); // Get the value from the "Name" column
                reader.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leave_Management_System
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void btnLeaveDetails_Click(object sender, EventArgs e)
        {
            Form9 frm1 = new Form9();
            frm1.Show();
            this.Close();
        }

        private void btnPendingLeaves_Click(object sender, EventArgs e)
        {
            Form10 frm1 = new Form10();
            frm1.Show();
            this.Close();
        }

        private void btnEmployeDetails_Click(object sender, EventArgs e)
        {
            Form11 frm1 = new Form11();
            frm1.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form13 frm1 = new Form13();
            frm1.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}

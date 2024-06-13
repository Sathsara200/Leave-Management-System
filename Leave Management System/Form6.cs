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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void btnLeaveDetails_Click(object sender, EventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Close();
        }

        private void btnSalaryDetails_Click(object sender, EventArgs e)
        {
            Form7 frm1 = new Form7();
            frm1.Show();
            this.Close();
        }

        private void btnYourDetails_Click(object sender, EventArgs e)
        {
            Form8 frm1 = new Form8();
            frm1.Show();
            this.Close();
        }
    }
}

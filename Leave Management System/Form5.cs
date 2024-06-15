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
    }
}

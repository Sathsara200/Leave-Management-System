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
using System.Xml.Linq;

namespace Leave_Management_System
{
  
    public partial class Form13 : Form
    {

        public Form13(int identityValue)
        {
            InitializeComponent();
            int identityValue1 = identityValue;
        }




            cmd.Parameters.AddWithValue("@Employe_Id",  identityValue);
            cmd.CommandType = CommandType.Text;
            

           

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Successfully added password", "Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}

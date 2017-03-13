using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
namespace RFIDNotifications
{
    public partial class AddReader : Form
    {
        public AddReader()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void AddReader_Load(object sender, EventArgs e)
        {
            txtAddress.Text=ConfigurationManager.AppSettings["fixreaderip"];
            txtAddress.Select();
            txtAddress.Focus();
        }
       
    }
}

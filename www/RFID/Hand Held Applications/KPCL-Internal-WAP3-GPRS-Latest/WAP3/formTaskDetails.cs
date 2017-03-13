using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WAP3
{
    public partial class formTaskDetails : Form
    {
        public formTaskDetails()
        {
            InitializeComponent();
        }

        private void formTaskDetails_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'localdbDataSet.TaskConfig' table. You can move, or remove it, as needed.
            this.taskConfigTableAdapter.Fill(this.localdbDataSet.TaskConfig);

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Form config = new formConfig();
            config.Show();
            this.Hide();
        }
    }
}
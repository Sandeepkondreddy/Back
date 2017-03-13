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
    public partial class formHome : Form
    {
        public formHome()
        {
            InitializeComponent();
        }

        private void imageButtonConfig_Click(object sender, EventArgs e)
        {
            formConfig f = new formConfig();
            f.ShowDialog();
        }

        private void imageButtonInternal_Click(object sender, EventArgs e)
        {
            formConnection f = new formConnection();
            f.ShowDialog();
        }
    }
}
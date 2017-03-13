/*********************************
Author Name:		Sandeep.K
Project Name:		RFID-EPMS
Purpose:	        Form HHD Track
Created Date:		12 Nov 2014
******************************************************/
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
    public partial class formHHDTrack : Form
    {
        string User = classLogin.User;
        string UserType = classLogin.UserType;
        public formHHDTrack()
        {
            InitializeComponent();
        }

        private void btnRequestConfig_Click(object sender, EventArgs e)
        {

        }

        private void formHHDTrack_Load(object sender, EventArgs e)
        {
            if (UserType == "admin")
            {
                btnIssue.Text = "Admin-Issue to SS";
                btnReturn.Text = "Admin-Receve from SS";
            }
            else if (UserType == "user")
            {
                btnIssue.Text = "SS-Issue to User";
                btnReturn.Text = "SS-Receve from User";
            }
            else if (UserType == "ss")
            {
                btnIssue.Text = "User-Receve from SS";
                btnReturn.Text = "User-Return to SS";
            }
        }
    }
}
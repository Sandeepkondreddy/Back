using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace SendingFiles
{
    public partial class Sendingmail : Form
    {
        public Sendingmail()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();

                mail.From = new MailAddress("epms_support@krishnapatnamport.com");
                mail.To.Add(new MailAddress("epms_support@krishnapatnamport.com"));
                mail.Subject = "TEST MAIL";
                mail.Body = "FYKI";

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.office365.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("epms_support@krishnapatnamport.com", "ITS@789$");

                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Send(mail);

                MessageBox.Show("mail Send");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}

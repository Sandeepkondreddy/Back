using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsApp
{
    public partial class formHome : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        private int childFormNumber = 0;

        public formHome()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

       
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void formHome_Load(object sender, EventArgs e)
        {

        }

        private void DiscrepancyToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            DiscrepancyToolStripMenuItem.BackColor.GetBrightness();
            
        }

        private void LogouttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void securityPermitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //foreach (Form childForm in MdiChildren)
            //{
            //    childForm.Close();
            //}
            //FileForm frm = new FileForm();
            //frm.MdiParent = this;
            //frm.Show();

            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            formEALDiscrepancy frm = new formEALDiscrepancy();
            frm.MdiParent = this;
            frm.Show();
        }

        private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            formAddUser frm = new formAddUser();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            formChangePassword frm = new formChangePassword();
            frm.MdiParent = this;
            frm.Show();
        }

        private void iALDiscrepancyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            formIALDiscrepancy frm = new formIALDiscrepancy();
            frm.MdiParent = this;
            frm.Show();
        }

        private void formHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void billableEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            formBillableEventsReport frm = new formBillableEventsReport();
            frm.MdiParent = this;
            frm.Show();
        }

        private void addSubStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            SubStaff frm = new SubStaff();
            frm.MdiParent = this;
            frm.Show();
        }

        private void eALToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            FileForm frm = new FileForm();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}

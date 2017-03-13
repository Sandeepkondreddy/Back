using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CS_RFID3_Host_Sample1
{
    public partial class formBoomBarrierTest : Form
    {
        public formBoomBarrierTest()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            //Connect Antena Set1 BoomBarrier
            string CStatus = classBoomBarriorOperations.BoomBarriorConnection(classGlobalVariables.AntenaSet1Loc_BoomBarrier_IP.ToString(), classBoomBarriorOperations._conSet1);
            MessageBox.Show(CStatus.ToString());

            //string CStatus1 = classBoomBarriorOperations.BoomBarriorConnection(classGlobalVariables.AntenaSet2Loc_BoomBarrier_IP.ToString());
            //MessageBox.Show(CStatus1.ToString());

        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            string CStatus1 = classBoomBarriorOperations.BoomBarriorStstus(classGlobalVariables.AntenaSet1Loc_BoomBarrier_IP.ToString(), classBoomBarriorOperations._conSet1);
            MessageBox.Show(CStatus1.ToString());
            
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            string CStatus1 = classBoomBarriorOperations.OpenBoomBarrior(classGlobalVariables.AntenaSet1Loc_BoomBarrier_IP.ToString(), classBoomBarriorOperations._conSet1);
            MessageBox.Show(CStatus1.ToString());
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            string CStatus1 = classBoomBarriorOperations.CloseBoomBarrior(classGlobalVariables.AntenaSet2Loc_BoomBarrier_IP.ToString(), classBoomBarriorOperations._conSet2);
            MessageBox.Show(CStatus1.ToString());
             
        }
    }
}

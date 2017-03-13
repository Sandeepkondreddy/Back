using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol.RFID3;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using ComponentFactory.Krypton.Toolkit;

namespace CS_RFID3_Host_Sample1
{
    public partial class formReader : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public formReader()
        {
            InitializeComponent();
        }
    }
}
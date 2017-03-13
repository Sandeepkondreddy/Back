/*********************************
Author Name:		Sandeep.K
Project Name:		RFID
Purpose:	        Login Screen
Created Date:		21 Oct 2012
Updated Date:       05 Dec 2014
******************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WAP3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new formLogin());
            //Application.Run(new formUpdateTime());
        }
    }
}
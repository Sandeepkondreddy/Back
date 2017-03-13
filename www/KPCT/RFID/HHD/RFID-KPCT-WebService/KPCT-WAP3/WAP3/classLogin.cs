using System;

using System.Collections.Generic;
using System.Text;

namespace WAP3
{
    class classLogin
    {
        private static string m_globalVar = "";

        public static string User
        {
            get { return m_globalVar; }
            set { m_globalVar = value; }
        }

        private static string n_globalVar = "";

        public static string UserType
        {
            get { return n_globalVar; }
            set { n_globalVar = value; }
        }

        private static string o_globalVar = "";

        public static string OpType
        {
            get { return o_globalVar; }
            set { o_globalVar = value; }
        }

        private static string p_globalVar = "";

        public static string ReaderIP
        {
            get { return p_globalVar; }
            set { p_globalVar = value; }
        }
        private static string q_globalVar = "";

        public static string ConnType
        {
            get { return q_globalVar; }
            set { q_globalVar = value; }
        }

        private static string r_globalVar = "";
        public static string Location
        {
            get { return r_globalVar; }
            set { r_globalVar = value; }
        }

        private static string s_globalVar = "";
        public static string ReaderNo
        {
            get { return s_globalVar; }
            set { s_globalVar = value; }
        }

        private static string t_globalVar = "WIFI";
        public static string Connectivity
        {
            get { return t_globalVar; }
            set { t_globalVar = value; }
        }
    }
}

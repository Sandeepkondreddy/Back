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
    }
}

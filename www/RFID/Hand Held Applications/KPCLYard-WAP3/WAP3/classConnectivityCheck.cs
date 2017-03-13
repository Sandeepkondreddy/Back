using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using PsionTeklogix.WWAN;
namespace WAP3
{
    class classConnectivityCheck
    {
        #region Wifi Signal Strenth
        public static string WifiSignalStrenthCheck()
        {
            string ConnectionStatus="";
            try
            {
                PsionTeklogix.WLAN.ConnectionState connection = WAP3.WLANHelper.GetCurrentConnection();
                //string signal;
                int strength = connection.SignalStrength; //classLog.writeLog("Message @:Wifi Connection:" + connection.ToString() + "-- Strength:" + strength.ToString());
                if (strength < -20 && strength > -95)
                    ConnectionStatus = "Success";
                else ConnectionStatus = "Failed";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                classLog.writeLog("Error @:Wifi Not Available");
                ConnectionStatus = "Failed";
            }
            return ConnectionStatus;
        }
        #endregion


        public static string GPRSConnectivityCheck()
        {
            string ConnectionStatus="";
            try
              {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(classServerDetails.WebSite.ToString());
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Proxy = null;
                request.Timeout = 9000;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                 {
                     if (response.StatusCode == HttpStatusCode.OK)
                         ConnectionStatus = "Success";
                     else ConnectionStatus = "Failed";
                 }
                }
                catch(Exception ex)
                    {
                        ConnectionStatus = "Failed";
                        classLog.writeLog("Exception @: " + ex.ToString());
                    }
            return ConnectionStatus;
        }
    }
}

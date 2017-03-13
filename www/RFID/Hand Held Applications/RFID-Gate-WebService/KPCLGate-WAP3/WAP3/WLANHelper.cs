/* $Revision */
/* Copyright Psion Teklogix Inc. 2007 */
/*
 * File: WLANHelper.cs
 *
 * Description:
 *  Implementation file for WLANHelper class in PtxWlan
 *
 */

using System;
using System.Collections.Generic;
using System.Text;
using PsionTeklogix.WLAN;

namespace WAP3
{
    class WLANHelper
    {
        static public PsionTeklogix.WLAN.NetworkAdapter adapter =
            new PsionTeklogix.WLAN.NetworkAdapter();
        public static DiscoveredNetwork[] GetAvailableNetworks()
        {
            return adapter.GetAvailableNetworks();
        }
        public static void Connect(byte[] ssid)
        {
            try
            {
                adapter.Connect(ssid);
            }
            catch (Exception e)
            {
                Debug(e);
            }
        }
        public static void Config_Save(DiscoveredNetwork network)
        {
            try
            {            
                Config_Save(new Configuration(network));
            }
            catch(Exception e)
            {
                Debug(e);
            }
        }
        public static void Config_Save(Configuration config)
        {
            try
            {
                adapter.SaveConfiguration(config);
            }
            catch(Exception e)
            {
                Debug(e);
            }
        }
        public static void Config_Remove(byte[] ssid)
        {
            try
            {
                adapter.RemoveConfiguration(ssid);
            }
            catch (Exception e)
            {
                Debug(e);
            }
        }
        public static ConnectionState GetCurrentConnection()
        {
            return adapter.GetCurrentConnection();
        }
        public static string GetAdapterName()
        {
            return adapter.ToString();
        }
        public static bool IsConnectToAll()
        {
            try
            {
                return adapter.GetConnectToAllNetworksEnabled();
            }
            catch (Exception e)
            {
                Debug(e);
                return false;
            }
        }
        public static void ConnectToAll(bool enable)
        {
            adapter.SetConnectToAllNetworksEnabled(enable);
        }
        public static string GetAdapterMAC()
        {
            return "Not implemented";
        }
        public static string GetAdapterDriverVersion()
        {
            return "Not implemented";
        }
        public static Configuration[] GetAllConfigurations()
        {            
            return adapter.GetAllConfigurations();
        }
        public static int[] authModes=
        {
            (int)Authentication.Open,            
            (int)Authentication.Shared,
            (int)Authentication.NetworkEAP,
            (int)Authentication.Unknown,
        };
        public static string[] authNames =
        {
            "Open",            
            "Shared",
            "NetworkEAP",
            "Unknown"
        };
        public static int [] Encryptions =
        {            
            (int)Encryption.None,            
            (int)Encryption.WEP_Manual,            
            (int)Encryption.WEP_Auto,                        
            (int)Encryption.WPA_PSK,            
            (int)Encryption.WPA_TKIP,            
            (int)Encryption.WPA2_PSK,            
            (int)Encryption.WPA2_AES,            
            (int)Encryption.CCKM_TKIP,            
            (int)Encryption.CKIP_Manual,
            (int)Encryption.CKIP_Auto,
            (int)Encryption.Unknown
        };
        public static string[] encryptionNames =
        {            
            "None",            
            "WEP_Manual",            
            "WEP_Auto",                        
            "WPA_PSK",            
            "WPA_TKIP",            
            "WPA2_PSK",            
            "WPA2_AES",            
            "CCKM_TKIP",            
            "CKIP_Manual",
            "CKIP_Auto",
            "Unknown"
        };
        public static int[] Infrastructures=
        {
            (int)Infrastructure.AdHoc,
            (int)Infrastructure.AccessPoint,
            (int)Infrastructure.Unknown
        };
        public static string []infrastructureNames=
        {
            "AdHoc",
            "AccessPoint",
            "Unknown"            
        };
        public static int[] EAPs=
        {
            (int)EAP.None,
            (int)EAP.MD5,        
            (int)EAP.PEAP_MCHAP,
            (int)EAP.PEAP_GTC,
            (int)EAP.EAP_TLS,
            (int)EAP.LEAP,
            (int)EAP.EAP_FAST,
            (int)EAP.Unknown
        };        
        public static string[] eapNames=
        {
            "None",
            "MD5",        
            "PEAP_MCHAP",
            "PEAP_GTC",
            "EAP_TLS",
            "LEAP",
            "EAP_FAST",
            "Unknown"
        };
        
        public enum Index
        {
            Authentication,
            Encryption,
            Infrastructure,
            EAP
        }
        public static int[][] Values = 
        {
            authModes,
            Encryptions,
            Infrastructures,
            EAPs
        };
        public static string[][] Names=
        {
            authNames,
            encryptionNames,
            infrastructureNames,
            eapNames
        };
        public static string GetName(Index index, int mode)
        {            
            for (int i = 0; i < Values[(int)index].Length; i++)
            {
                if ((int)Values[(int)index][i] == mode) return Names[(int)index][i];
            }
            return mode.ToString();
        }
        public static int GetValue(Index index, string name)
        {
            for (int i = 0; i < Names[(int)index].Length; i++)
            {
                if (Names[(int)index][i] == name) return Values[(int)index][i];
            }
            return -1;
        }
        public static string GetAuthenticationName(Authentication mode)
        {
            return GetName(Index.Authentication, (int)mode);
        }
        public static string GetEncryptionName(Encryption mode)
        {
            return GetName(Index.Encryption, (int)mode);
        }
        public static string GetEAPName(EAP mode)
        {
            return GetName(Index.EAP, (int)mode);
        }
        public static string GetInfrastructureName(Infrastructure mode)
        {
            return GetName(Index.Infrastructure, (int)mode);
        }
        public static void Debug(object obj)
        {
            System.Windows.Forms.MessageBox.Show(obj.ToString());
        }
        public static int GetWepKeyLength(Configuration config, int index)
        {
            switch (config.GetWEPKeyLength(index))
            {                
                case WEPKeyLength.WEPKeyLength_40Bit: return 5;
                case WEPKeyLength.WEPKeyLength_128Bit: return 13;
                default: return 0;
            }
        }
        public static void SetRadioMode(PsionTeklogix.WLAN.RadioMode radioMode)
        {
            try
            {
                adapter.RadioMode = radioMode;
            }
            catch (Exception e)
            {
                Debug(e);
            }
        }
        public static void SetBitRate(PsionTeklogix.WLAN.BitRate bitRate)
        {
            try
            {
                adapter.BitRate= bitRate;
            }
            catch (Exception e)
            {
                Debug(e);
            }
        }
        public static PsionTeklogix.WLAN.BitRate GetBitRate()
        {
            try
            {
                return adapter.BitRate;
            }
            catch (Exception e)
            {
                Debug(e);
            }
            return BitRate.Auto;
        }
        public static PsionTeklogix.WLAN.RadioMode GetRadioMode()
        {
            try
            {
                return adapter.RadioMode;
            }
            catch (Exception e)
            {
                Debug(e);
            }
            return RadioMode.RadioMode_80211b;
        }
        public static void SetTxPower(int power)
        {
            try
            {
                adapter.TransmitPower = power;
            }
            catch (Exception e)
            {
                Debug(e);
            }
        }
        public static int GetTxPower()
        {
            try
            {
                return adapter.TransmitPower;
            }
            catch (Exception e)
            {
                Debug(e);
            }
            return 0;
        }
        public static string NullString = "<null>";

        public static string GetSSID(byte[] bytes)
        {
            string ssid = "";
            if (bytes == null)
            {
                ssid = NullString;
            }
            else
            {
                const int upperASCII = 0x7f;
                const int lowerASCII = 0x20;
                if (bytes.Length == 1 && bytes[0] > upperASCII && bytes[0] < lowerASCII )
                {
                    ssid +="<0x>"+bytes[0].ToString();
                }
                else
                {
                    ssid = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                }
            }

            return ssid;
        }
    }   
}

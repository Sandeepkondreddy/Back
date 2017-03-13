using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

using com.caen.RFIDLibrary;

namespace WAP3
{
    public partial class formVisitor
    {
        #region CAEN Reader
        public void OpenReader()
        {
            try
            {
                Reader.Connect(com.caen.RFIDLibrary.CAENRFIDPort.CAENRFID_RS232, "COM" + this.ComPort.ToString() + ":115200");

                bOpen = true;
            }
            catch (Exception ex)
            {
                Display("Error", Images.Fail);
                throw ex;
            }
        }

        public void CloseReader()
        {
            try
            {
                if (bOpen)
                {
                    Reader.Disconnect();

                    bOpen = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArrayList Inventory()
        {
            try
            {
                if (bOpen)
                {
                    ArrayList al = new ArrayList();


                    if (bActiveEPCFilter)
                        m_RFIDTags = m_Source0.InventoryTag(Mask, MaskLenght, Position);
                    else
                        m_RFIDTags = m_Source0.InventoryTag();


                    if (m_RFIDTags == null)
                        throw new Exception("No tag in the field");

                    if (m_RFIDTags.Length == 0)
                        throw new Exception("No tag in the field");

                    for (int cpt = 0; cpt < m_RFIDTags.Length; cpt++)
                    {
                        string IDTag = "";
                        if (bHexadecimalDisplay)
                            IDTag = System.BitConverter.ToString(m_RFIDTags[cpt].GetId()).Replace("-", "");
                        if (bASCIIDisplay)
                            foreach (byte b in m_RFIDTags[cpt].GetId())
                                IDTag += ((char)b).ToString();

                        al.Add(IDTag);

                    }
                    return al;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}

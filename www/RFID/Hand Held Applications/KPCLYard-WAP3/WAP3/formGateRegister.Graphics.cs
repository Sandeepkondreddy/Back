using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using System.Drawing;

namespace WAP3
{
    public partial class formGateRegister
    {
        #region Display
        enum Images
        {
            Point,
            Arrow,
            Success,
            Fail
        }

        private void Display(string Message, Images i)
        {
            if (Message != null)
                if (Message != "")
                {
                    ListViewItem lvi = new ListViewItem(new string[] { Message });
                    lvi.ImageIndex = (int)i;
                    // Add ListViewItem to the listview
                    _lv.Items.Add(lvi);
                }

            _lv.Refresh();
        }

        private void Display(string Message)
        {
            Display(Message, Images.Point);
        }

        private void Clear()
        {
            _lv.Items.Clear();
            _lv.Refresh();
        }
        #endregion

        #region Input panel / Sort column
        Microsoft.WindowsCE.Forms.InputPanel ip = new Microsoft.WindowsCE.Forms.InputPanel();
        private void _txtMask_GotFocus(object sender, EventArgs e)
        {
            ip.Enabled = true;
        }

        private void _txtMask_LostFocus(object sender, EventArgs e)
        {
            ip.Enabled = false;
        }

        private void _txtWriteReadWrite_GotFocus(object sender, EventArgs e)
        {
            ip.Enabled = true;
        }

        private void _txtWriteReadWrite_LostFocus(object sender, EventArgs e)
        {
            ip.Enabled = false;
        }

        private void _lvReadID_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            // Create an instance of the ColHeader class.
            ColHeader clickedCol = (ColHeader)this._lvReadID.Columns[e.Column];

            // Set the ascending property to sort in the opposite order.
            clickedCol.ascending = !clickedCol.ascending;

            // Get the number of items in the list.
            int numItems = this._lvReadID.Items.Count;

            // Turn off display while data is repoplulated.
            this._lvReadID.BeginUpdate();

            // Populate an ArrayList with a SortWrapper of each list item.
            ArrayList SortArray = new ArrayList();
            for (int i = 0; i < numItems; i++)
            {
                SortArray.Add(new SortWrapper(this._lvReadID.Items[i], e.Column));
            }

            // Sort the elements in the ArrayList using a new instance of the SortComparer
            // class. The parameters are the starting index, the length of the range to sort,
            // and the IComparer implementation to use for comparing elements. Note that
            // the IComparer implementation (SortComparer) requires the sort
            // direction for its constructor; true if ascending, othwise false.
            SortArray.Sort(0, SortArray.Count, new SortWrapper.SortComparer(clickedCol.ascending));

            // Clear the list, and repopulate with the sorted items.
            this._lvReadID.Items.Clear();
            for (int i = 0; i < numItems; i++)
                this._lvReadID.Items.Add(((SortWrapper)SortArray[i]).sortItem);

            // Turn display back on.
            this._lvReadID.EndUpdate();
        }

        // An instance of the SortWrapper class is created for
        // each item and added to the ArrayList for sorting.
        public class SortWrapper
        {
            internal ListViewItem sortItem;
            internal int sortColumn;


            // A SortWrapper requires the item and the index of the clicked column.
            public SortWrapper(ListViewItem Item, int iColumn)
            {
                sortItem = Item;
                sortColumn = iColumn;
            }

            // Text property for getting the text of an item.
            public string Text
            {
                get
                {
                    return sortItem.SubItems[sortColumn].Text;
                }
            }

            // Implementation of the IComparer
            // interface for sorting ArrayList items.
            public class SortComparer : IComparer
            {
                bool ascending;

                // Constructor requires the sort order;
                // true if ascending, otherwise descending.
                public SortComparer(bool asc)
                {
                    this.ascending = asc;
                }

                // Implemnentation of the IComparer:Compare
                // method for comparing two objects.
                public int Compare(object x, object y)
                {
                    SortWrapper xItem = (SortWrapper)x;
                    SortWrapper yItem = (SortWrapper)y;

                    string xText = xItem.sortItem.SubItems[xItem.sortColumn].Text;
                    string yText = yItem.sortItem.SubItems[yItem.sortColumn].Text;
                    return xText.CompareTo(yText) * (this.ascending ? 1 : -1);
                }
            }
        }
        // The ColHeader class is a ColumnHeader object with an
        // added property for determining an ascending or descending sort.
        // True specifies an ascending order, false specifies a descending order.
        public class ColHeader : ColumnHeader
        {
            public bool ascending;
            public ColHeader(string text, int width, HorizontalAlignment align, bool asc)
            {
                this.Text = text;
                this.Width = width;
                this.TextAlign = align;
                this.ascending = asc;
            }
        }
        #endregion

        #region Graphics
        private void InitGraphics()
        {
            _plConfiguration.Location = new Point(0, _plConfiguration.Location.Y);
            _plReadID.Location = new Point(0, _plReadID.Location.Y);
            _plReadWrite.Location = new Point(0, _plReadWrite.Location.Y);
            // Add specific columns for sort data
            this._lvReadID.Columns.Add(new ColHeader("TagID", 169, System.Windows.Forms.HorizontalAlignment.Left, true));
            this._lvReadID.Columns.Add(new ColHeader("#", 39, System.Windows.Forms.HorizontalAlignment.Left, true));
            this._lvReadID.Refresh();
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Refresh();


        }

        private void _btConfigurationPanel_Click(object sender, EventArgs e)
        {
            _plConfiguration.BringToFront();
        }

        private void _btReadIDPanel_Click(object sender, EventArgs e)
        {
            ClearReadID();
            _plReadID.BringToFront();
        }

        private void _btReadeWritePanel_Click(object sender, EventArgs e)
        {
            ClearReadWrite();

            if (CurrentBankMemory == BankMemory.EPC)
            {
                _txtReadReadWrite.MaxLength = nByteEPC *
                   ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
                _txtWriteReadWrite.MaxLength = nByteEPC *
                    ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
            }
            if (CurrentBankMemory == BankMemory.USER)
            {
                _txtReadReadWrite.MaxLength = nByteUSER *
                   ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
                _txtWriteReadWrite.MaxLength = nByteUSER *
                    ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
            }


            _lbUsePassword.Text = "";
            if (PasswordUsed) _lbUsePassword.Text = "Use PWD= " + _txtPassword.Text;

            if (Reader.GetReaderInfo().GetModel() == "A528")
                _lbProtocolReadWrite.Text = "EPC C1 G2";
            else
                _lbProtocolReadWrite.Text = _cbProtocol.SelectedItem.ToString();
            _plReadWrite.BringToFront();
        }
        #endregion
        
        public void loadreadtab()
        {
            ClearReadWrite();
            
            if (CurrentBankMemory == BankMemory.EPC)
            {
                _txtReadReadWrite.MaxLength = nByteEPC *
                   ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
                _txtWriteReadWrite.MaxLength = nByteEPC *
                    ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
            }
            if (CurrentBankMemory == BankMemory.USER)
            {
                _txtReadReadWrite.MaxLength = nByteUSER *
                   ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
                _txtWriteReadWrite.MaxLength = nByteUSER *
                    ((opReadWriteOptions.HexadecimalDisplay) ? 2 : 1);
            }


            _lbUsePassword.Text = "";
            if (PasswordUsed) _lbUsePassword.Text = "Use PWD= " + _txtPassword.Text;

            if (Reader.GetReaderInfo().GetModel() == "A528")
                _lbProtocolReadWrite.Text = "EPC C1 G2";
            else
                _lbProtocolReadWrite.Text = _cbProtocol.SelectedItem.ToString();
            _plReadWrite.BringToFront();
            //localdbrecordcount();
        }

    
    }
}

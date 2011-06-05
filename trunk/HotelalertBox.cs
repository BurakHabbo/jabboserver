using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JabboServer
{
    public partial class HotelalertBox : Form
    {
        public HotelalertBox()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (inputBox.Text.Length > 0)
            {
                if (Engine.GetWebSocket().ConnectionCount > 0)
                {
                    Engine.GetWebSocket().GetFactory().BroadcastHotelAlert(inputBox.Text);
                }
                else
                {
                    MessageBox.Show("No users connected at the moment..", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter some values in the input box..", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

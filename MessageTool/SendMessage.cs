using FoneDynamics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageTool
{
    public partial class SendMessage : Form
    {


        public SendMessage()
        {
            InitializeComponent();
        }

        private void butSend_Click(object sender, EventArgs e)
        {
            string from = txtFrom.Text;
            if (string.IsNullOrWhiteSpace(from))
            {
                from = null;
            }

            var message = new FoneDynamics.Rest.V2.MessageResource(txtTo.Text.Trim(), txtMessage.Text, from);
            FoneDynamicsClient.Initialize(txtAccountSid.Text.Trim(), txtToken.Text.Trim(), txtPropertySid.Text.Trim());


            try
            {
                var response = FoneDynamics.Rest.V2.MessageResource.Send(message);
                txtResponse.Text = response.Status.ToString();
                txtMessageSid.Text = response.MessageSid;
            }
            catch (Exception ex)
            {
                txtResponse.Text = ex.Message;
            }

        }

        private void SendMessage_Load(object sender, EventArgs e)
        {
            txtAccountSid.Text = Properties.Settings.Default.AccountSid;
            txtToken.Text = Properties.Settings.Default.Token;
            txtPropertySid.Text = Properties.Settings.Default.PropertySid;
        }

        private void SendMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.AccountSid = txtAccountSid.Text;
            Properties.Settings.Default.Token = txtToken.Text;
            Properties.Settings.Default.PropertySid = txtPropertySid.Text;
            Properties.Settings.Default.Save();
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            lblCharacters.Text = txtMessage.Text.Length.ToString();
        }

        private void butCheckStatus_Click(object sender, EventArgs e)
        {
            try
            {
                var response = FoneDynamics.Rest.V2.MessageResource.Get(txtMessageSid.Text);
                txtResponse.Text = response.Status.ToString();
            }
            catch (Exception ex)
            {
                txtResponse.Text = ex.Message;
            }

        }
    }
}

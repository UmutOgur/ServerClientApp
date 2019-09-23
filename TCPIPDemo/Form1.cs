using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPIPDemo
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;
        private void Server_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0 * 13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataRecievered;
        }

        private void Server_DataRecievered(object sender, SimpleTCP.Message e)
        {
            txtstatus.Invoke((MethodInvoker)delegate ()
            {
                txtstatus.Text += e.MessageString;
                e.ReplyLine(string.Format("You Said:{0}", e.MessageString));

            });
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            txtstatus.Text += "Server Starting...";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txthost.Text);
            server.Start(ip, Convert.ToInt32(txtport.Text));
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }
    }
}

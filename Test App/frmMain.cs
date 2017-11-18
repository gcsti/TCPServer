/****************************************************************
 * This work is original work authored by Craig Baird, released *
 * under the Code Project Open Licence (CPOL) 1.02;             *
 * http://www.codeproject.com/info/cpol10.aspx                  *
 * This work is provided as is, no guarentees are made as to    *
 * suitability of this work for any specific purpose, use it at *
 * your own risk.                                               *
 * This product is not intended for use in any form except      *
 * learning. The author recommends only using small sections of *
 * code from this project when integrating the attacked         *
 * TcpServer project into your own project.                     *
 * This product is not intended for use for any comercial       *
 * purposes, however it may be used for such purposes.          *
 ****************************************************************/

using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace testerApp
{
    public partial class frmMain : Form
    {
        public delegate void invokeDelegate();

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnChangePort_Click(object sender, EventArgs e)
        {
            try
            {
                openTcpPort(tcpServer1, txtPort);
            }
            catch (FormatException)
            {
                MessageBox.Show("Port must be an integer", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Port is too large", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnChangePort2_Click(object sender, EventArgs e)
        {
            try
            {
                openTcpPort(tcpServer2, txtPort2);
            }
            catch (FormatException)
            {
                MessageBox.Show("Port must be an integer", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Port is too large", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private void btnChangePort3_Click(object sender, EventArgs e)
        {
            try
            {
                openTcpPort(tcpServer3, txtPort3);
            }
            catch (FormatException)
            {
                MessageBox.Show("Port must be an integer", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Port is too large", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnChangePort4_Click(object sender, EventArgs e)
        {
            try
            {
                openTcpPort(tcpServer4, txtPort4);
            }
            catch (FormatException)
            {
                MessageBox.Show("Port must be an integer", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Port is too large", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void openTcpPort(tcpServer.TcpServer tcp, TextBox port)
        {
            tcp.Close();
            tcp.Port = Convert.ToInt32(port.Text);
            port.Text = tcp.Port.ToString();
            tcp.Open();



            displayTcpServerStatus();
        }




        private void displayTcpServerStatus()
        {
            if (tcpServer1.IsOpen)
            {
                lblStatus.Text = "PORT OPEN";
                lblStatus.BackColor = Color.Lime;
            }
            else
            {
                lblStatus.Text = "PORT NOT OPEN";
                lblStatus.BackColor = Color.Red;
            }


            if (tcpServer2.IsOpen)
            {
                lblStatus2.Text = "PORT OPEN";
                lblStatus2.BackColor = Color.Lime;
            }
            else
            {
                lblStatus2.Text = "PORT NOT OPEN";
                lblStatus2.BackColor = Color.Red;
            }

            if (tcpServer3.IsOpen)
            {
                lblStatus3.Text = "PORT OPEN";
                lblStatus3.BackColor = Color.Lime;
            }
            else
            {
                lblStatus3.Text = "PORT NOT OPEN";
                lblStatus3.BackColor = Color.Red;
            }

            if (tcpServer4.IsOpen)
            {
                lblStatus4.Text = "PORT OPEN";
                lblStatus4.BackColor = Color.Lime;
            }
            else
            {
                lblStatus4.Text = "PORT NOT OPEN";
                lblStatus4.BackColor = Color.Red;
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            send();
        }

        private void send()
        {
            string data = "";
            string data1 = "";
            string data2 = "";

            if (txtText.Text.Length > 0)
            {
                foreach (string line in txtText.Lines)
                {
                    data = data + line.Replace("\r", "").Replace("\n", "") + "\r\n";
                }
                data = data.Substring(0, data.Length - 2);

                tcpServer1.Send(data);

                logData(true, data);
            }

            if (txtText2.Text.Length > 0)
            {
                foreach (string line in txtText2.Lines)
                {
                    data1 = data1 + line.Replace("\r", "").Replace("\n", "") + "\r\n";
                }
                data1 = data1.Substring(0, data1.Length - 2);
                tcpServer2.Send(data1);
                logData(true, data1);
            }

            if (txtText3.Text.Length > 0)
            {
                foreach (string line in txtText3.Lines)
                {
                    data2 = data2 + line.Replace("\r", "").Replace("\n", "") + "\r\n";
                }
                data2 = data2.Substring(0, data2.Length - 2);

                tcpServer3.Send(data2);
                logData(true, data2);
            }

           

         
            
        }

        public void logData(bool sent, string text)
        {
            txtLog.Text += "\r\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt") + (sent ? " SENT:\r\n" : " RECEIVED:\r\n");
            txtLog.Text += text;
            txtLog.Text += "\r\n";
            if (txtLog.Lines.Length > 500)
            {
                string[] temp = new string[500];
                Array.Copy(txtLog.Lines, txtLog.Lines.Length - 500, temp, 0, 500);
                txtLog.Lines = temp;
            }
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcpServer1.Close();
            tcpServer2.Close();
            tcpServer3.Close();
            tcpServer4.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //btnChangePort_Click(null, null);

            timer1.Enabled = true;
        }

        private void tcpServer1_OnDataAvailable(tcpServer.TcpServerConnection connection)
        {
            byte[] data = readStream(connection.Socket);

            if (data != null)
            {
                string dataStr = Encoding.ASCII.GetString(data);

                invokeDelegate del = () =>
                {
                    logData(false, dataStr);
                };
                Invoke(del);

                data = null;
            }
        }

        private void tcpServer2_OnDataAvailable(tcpServer.TcpServerConnection connection)
        {
            byte[] data = readStream(connection.Socket);

            if (data != null)
            {
                string dataStr = Encoding.ASCII.GetString(data);

                invokeDelegate del = () =>
                {
                    logData(false, dataStr);
                };
                Invoke(del);

                data = null;
            }
        }

        private void tcpServer3_OnDataAvailable(tcpServer.TcpServerConnection connection)
        {
            byte[] data = readStream(connection.Socket);

            if (data != null)
            {
                string dataStr = Encoding.ASCII.GetString(data);

                invokeDelegate del = () =>
                {
                    logData(false, dataStr);
                };
                Invoke(del);

                data = null;
            }
        }

        private void tcpServer4_OnDataAvailable(tcpServer.TcpServerConnection connection)
        {
            byte[] data = readStream(connection.Socket);

            if (data != null)
            {
                string dataStr = Encoding.ASCII.GetString(data);

                invokeDelegate del = () =>
                {
                    logData(false, dataStr);
                    lblPLC.Text = dataStr;
                };
                Invoke(del);

                data = null;
            }
        }

        protected byte[] readStream(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            if (stream.DataAvailable)
            {
                byte[] data = new byte[client.Available];

                int bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(data, 0, data.Length);
                }
                catch (IOException)
                {
                }

                if (bytesRead < data.Length)
                {
                    byte[] lastData = data;
                    data = new byte[bytesRead];
                    Array.ConstrainedCopy(lastData, 0, data, 0, bytesRead);
                }
                return data;
            }
            return null;
        }

        private void tcpServer1_OnConnect(tcpServer.TcpServerConnection connection)
        {
            invokeDelegate setText = () => lblConnected.Text = tcpServer1.Connections.Count.ToString();

            Invoke(setText);
        }

        private void tcpServer2_OnConnect(tcpServer.TcpServerConnection connection)
        {
            invokeDelegate setText = () => lblConnected2.Text = tcpServer2.Connections.Count.ToString();

            Invoke(setText);
        }


        private void tcpServer3_OnConnect(tcpServer.TcpServerConnection connection)
        {
            invokeDelegate setText = () => lblConnected3.Text = tcpServer3.Connections.Count.ToString();

            Invoke(setText);
        }

        private void tcpServer4_OnConnect(tcpServer.TcpServerConnection connection)
        {
            invokeDelegate setText = () => lblConnected4.Text = tcpServer4.Connections.Count.ToString();

            Invoke(setText);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            displayTcpServerStatus();
            lblConnected.Text = tcpServer1.Connections.Count.ToString();
            lblConnected2.Text = tcpServer2.Connections.Count.ToString();
            lblConnected3.Text = tcpServer3.Connections.Count.ToString();
            lblConnected4.Text = tcpServer4.Connections.Count.ToString();
        }

        private void txtIdleTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int time = Convert.ToInt32(txtIdleTime.Text);
                tcpServer1.IdleTime = time;

                time = Convert.ToInt32(txtIdleTime2.Text);
                tcpServer2.IdleTime = time;

                time = Convert.ToInt32(txtIdleTime3.Text);
                tcpServer3.IdleTime = time;

                time = Convert.ToInt32(txtIdleTime4.Text);
                tcpServer4.IdleTime = time;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void txtMaxThreads_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int threads = Convert.ToInt32(txtMaxThreads.Text);
                tcpServer1.MaxCallbackThreads = threads;

                threads = Convert.ToInt32(txtMaxThreads2.Text);
                tcpServer2.MaxCallbackThreads = threads;

                threads = Convert.ToInt32(txtMaxThreads3.Text);
                tcpServer3.MaxCallbackThreads = threads;

                threads = Convert.ToInt32(txtMaxThreads4.Text);
                tcpServer4.MaxCallbackThreads = threads;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void txtAttempts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int attempts = Convert.ToInt32(txtAttempts.Text);
                tcpServer1.MaxSendAttempts = attempts;

                attempts = Convert.ToInt32(txtAttempts2.Text);
                tcpServer2.MaxSendAttempts = attempts;

                attempts = Convert.ToInt32(txtAttempts3.Text);
                tcpServer3.MaxSendAttempts = attempts;

                attempts = Convert.ToInt32(txtAttempts4.Text);
                tcpServer4.MaxSendAttempts = attempts;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void txtValidateInterval_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int interval = Convert.ToInt32(txtValidateInterval.Text);
                tcpServer1.VerifyConnectionInterval = interval;

                interval = Convert.ToInt32(txtValidateInterval2.Text);
                tcpServer2.VerifyConnectionInterval = interval;

                interval = Convert.ToInt32(txtValidateInterval3.Text);
                tcpServer3.VerifyConnectionInterval = interval;

                interval = Convert.ToInt32(txtValidateInterval4.Text);
                tcpServer4.VerifyConnectionInterval = interval;
            }
            catch (FormatException) { }
            catch (OverflowException) { }
        }


    }
}

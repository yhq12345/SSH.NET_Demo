using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Renci.SshNet;
using System.IO;
using Routrek.SSHC;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Routrek.SSHCTest;

namespace SSH.NET_DEMO
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Renci.SshNet.SshClient sshC = null;

        private void btn_C1_Click(object sender, EventArgs e)
        {
            sshC = new SshClient(txt_ip1.Text, txt_user1.Text, txt_pwd1.Text);

            sshC.Connect();


            if (sshC.IsConnected)
            {
                lab_ContentState1.Text = "连接状态：成功";
            }
            else
            {
                lab_ContentState1.Text = "连接状态：失败";
            }

        }

        private void btn_Q1_Click(object sender, EventArgs e)
        {
            Renci.SshNet.SshClient _session = new Renci.SshNet.SshClient(txt_ip1.Text, txt_user1.Text, txt_pwd1.Text);

            SshCommand x = sshC.RunCommand(txt_cmd1.Text);
            //SshCommand z2 = sshC.RunCommand(txt_cmd1.Text);
            //string z1 = z2.Execute(txt_cmd1.Text);

            txt_Show1.Text = "结果信息\n" + x.Result + "错误信息:" + x.Error;

            // txt_Show1.Text += "错误信息\n" + x.Error;
            //txt_Show1.Text += z1;

        }

        private void btn_C2_Click(object sender, EventArgs e)
        {
            /*
             * Resources.HOST 192.168.10.131
             * Resources.PORT 22
             * Resources.USERNAME root
             * proxyHost 
             * Resources.PORT
             * Resources.USERNAME
             * Resources.PASSWORD
             * Resources.USERNAME
             */
            var connectionInfo = new ConnectionInfo("10.69.66.67", 22, "toor4nsn",
                ProxyTypes.None, "192.168.253.18", 22, "toor4nsn", "oZPS0POrRieRtu",
                new KeyboardInteractiveAuthenticationMethod("10.69.66.67"));


            Renci.SshNet.SshClient _session2 = new Renci.SshNet.SshClient(connectionInfo);
            _session2.Connect();
            _session2.RunCommand("lst");

            //Renci.SshNet.Session zz = new Renci.SshNet.Session(,);
            //xx.Start();

        }

        private void btn_Q2_Click(object sender, EventArgs e)
        {
            sshC = new SshClient("192.168.10.131", "root", "123123");

            sshC.KeepAliveInterval = new TimeSpan(0, 0, 30);
            sshC.ConnectionInfo.Timeout = new TimeSpan(0, 0, 20);
            sshC.Connect();

            // 动态链接 
            ForwardedPortDynamic port = new ForwardedPortDynamic("127.0.0.1", 22);
            sshC.AddForwardedPort(port);
            port.Start();

            ////var fport = new ForwardedPortRemote("192.168.10.131", 22, "127.0.0.1", 22);
            ////sshC.AddForwardedPort(fport);
            ////fport.Start();

            ////string x = sshC.RunCommand("pwd").Result;
            ;
            string x = sshC.RunCommand("pwd").Result;
            ;

        }
        public void Stop()
        {
            sshC.Disconnect();
        }

        private void btn_C3_Click(object sender, EventArgs e)
        {

        }

        private void btn_Q3_Click(object sender, EventArgs e)
        {

        }

        private void T_1_Click(object sender, EventArgs e)
        {
            Renci.SshNet.SftpClient sftp1 = new SftpClient(txt_ip4.Text, txt_user4.Text, txt_pwd4.Text);
            sftp1.Connect();

            byte[] bytes = { 12, 2, 3, 4, };
            Stream stream = new MemoryStream(bytes);
            sftp1.UploadFile(stream, "/home/administrator/Documents/yhq.tar", null);


        }
        public SSHConnection _conn;

        private void button2_Click(object sender, EventArgs e)
        {

            SSHConnectionParameter f = new SSHConnectionParameter();
            f.UserName = "root";

            f.Password = "123123";
            f.Protocol = SSHProtocol.SSH2;
            f.AuthenticationType = AuthenticationType.Password;

            Reader reader = new Reader();
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(new IPEndPoint(IPAddress.Parse("192.168.10.131"), 22));
            _conn = SSHConnection.Connect(f, reader, s);
            reader._conn = _conn;
            SSHChannel ch = _conn.OpenShell(reader);
            reader._pf = ch;
            SSHConnectionInfo ci = _conn.ConnectionInfo;

            Thread.Sleep(1000);

            byte[] b = new byte[1];

            string cmd = "ls";
            byte[] data = (new UnicodeEncoding()).GetBytes(cmd);
            reader._pf.Transmit(data);


            Thread.Sleep(5000);
            string x = reader.SessionLog;
            //reader.OnData +=


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


    }
}

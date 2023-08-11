using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Server
{
    public partial class Form1 : Form
    {
        public static NetworkStream stream { get; set; }

        UserControl1 uc1 = new UserControl1();
        UserControl2 uc2 = new UserControl2();
        UserControl3 uc3 = new UserControl3();
        public static Form1 f;

        public Form1()
        {
            InitializeComponent();
            f = this;

            Controls.Add(uc2);
            uc2.Location = new Point(richTextBox1.Location.X, richTextBox1.Location.Y);

        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            int port = 13001;
            TcpListener server = new TcpListener(localAddr, port);
            server.Start();
            

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                Task task = Task.Run(() => HandleClient(client));
            }
        }

        private async void HandleClient(TcpClient client)
        {
            AppendText("발주 품목 주문!!!\n");
            using (stream = client.GetStream())
            {
                byte[] data = new byte[1024];
                int bytes;

                while ((bytes = await stream.ReadAsync(data, 0, data.Length)) != 0)
                {
                    AppendText(Encoding.UTF8.GetString(data, 0, bytes) + "\n");
                }

                /*
                byte[] senddata = Encoding.UTF8.GetBytes("완료");
                UserControl2.ucc2.button1.Click += (sender, e) => { richTextBox1.Text="" };//stream.Write(senddata, 0, senddata.Length); };
                */
            }
            client.Close();
        }
        private void AppendText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action<string>(AppendText), text);
            }
            else
            {
                if(text == "발주 품목 주문!!!\n")
                {
                    richTextBox1.AppendText(text);
                    UserControl1.ucc1.label1.Text += "\n";
                    UserControl2.ucc2.label2.Text += "\n";
                    UserControl3.ucc3.label1.Text += "\n";
                    UserControl2.ucc2.label1.Text += "\n";

                }
                else
                {
                    richTextBox1.AppendText(text);
                    UserControl1.ucc1.label1.Text += text;
                    UserControl2.ucc2.label2.Text += text;
                    UserControl3.ucc3.label1.Text += text;
                    UserControl2.ucc2.label1.Text += text;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            panel1.Visible = true;
            panel1.Controls.Clear();
            panel1.Controls.Add(uc1);

            UserControl1.ucc1.progressBar1.Minimum = 0;
            UserControl1.ucc1.progressBar1.Maximum = 100;
            UserControl1.ucc1.progressBar1.Value = 0;


            Thread thread = new Thread(new ThreadStart(UserControl1.ucc1.UpdateProgressBar));
            thread.Start();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            uc2.Location = new Point(0,0);
            panel1.Controls.Clear();
            panel1.Controls.Add(uc2);

            UserControl2.ucc2.progressBar1.Minimum = 0;
            UserControl2.ucc2.progressBar1.Maximum = 100;
            UserControl2.ucc2.progressBar1.Value = 0;


            Thread thread = new Thread(new ThreadStart(UserControl2.ucc2.UpdateProgressBar));
            thread.Start();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(uc3);

            UserControl3.ucc3.progressBar1.Minimum = 0;
            UserControl3.ucc3.progressBar1.Maximum = 100;

            UserControl3.ucc3.progressBar1.Value = 45;

            Thread thread = new Thread(new ThreadStart(UserControl3.ucc3.UpdateProgressBar));
            thread.Start();
        }
        
    }
}

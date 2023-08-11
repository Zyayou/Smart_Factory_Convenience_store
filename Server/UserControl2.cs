using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Server
{
    public partial class UserControl2 : UserControl
    {
        public static UserControl2 ucc2;
        public UserControl2()
        {
            InitializeComponent();

            ucc2 = this;
            Controls.Add(Form1.f);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("발송하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                byte[] senddata = Encoding.UTF8.GetBytes("완료");
                Form1.stream.Write(senddata, 0, senddata.Length);

                MessageBox.Show("발송하였습니다!\n초기화면으로 돌아갑니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1.f.panel1.Visible = false;
                Form1.f.richTextBox1.Text = "";
                UserControl1.ucc1.label1.Text = "품목";
                label2.Text = "발주 목록";
                UserControl3.ucc3.label1.Text = "품목";


            }
            else if (result == DialogResult.Cancel)
            {
                MessageBox.Show("발송을 취소합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void UserControl2_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        public void UpdateProgressBar()
        {
            progressBar1.Value = 90;
            progressBar1.Value = 89;

            for (int i = 90; i <= 100; i++)
            {
                // ProgressBar의 값을 변경하기 위해 Invoke 사용
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = i;
                }));
                

                Thread.Sleep(333);

            }
        }
    }
}

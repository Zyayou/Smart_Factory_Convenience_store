using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Server
{
    public partial class UserControl1 : UserControl
    {
        public static UserControl1 ucc1;
        public UserControl1()
        {
            InitializeComponent();
            ucc1=this;

            Controls.Add(Form1.f);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            pictureBox3.Enabled = false;
            pictureBox2.Enabled = false;
            pictureBox4.Enabled = false;
            pictureBox5.Enabled = false;

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }
        public void UpdateProgressBar()
        {
            for (int i = 0; i <= 45; i++)
            {
                // ProgressBar의 값을 변경하기 위해 Invoke 사용
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = i;
                }));
                if (i == 9)
                {
                    pictureBox3.Enabled = true;
                }
                else if (i == 18)
                {
                    pictureBox2.Enabled = true;
                }
                else if (i == 27)
                {
                    pictureBox4.Enabled = true;
                }
                else if (i == 36)
                {
                    pictureBox5.Enabled = true;
                }

                Thread.Sleep(333);

            }
        }
    }
}

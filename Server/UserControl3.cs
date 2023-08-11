using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class UserControl3 : UserControl
    {
        public static UserControl3 ucc3;
        public UserControl3()
        {
            InitializeComponent();

            ucc3 = this;
            Controls.Add(Form1.f);
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {
            pictureBox3.Enabled = false;
            pictureBox2.Enabled = false;
            pictureBox4.Enabled = false;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        public void UpdateProgressBar()
        {
            progressBar1.Value = 45;
            progressBar1.Value = 44;
            
            for (int i = 45; i <= 90; i++)
            {
                // ProgressBar의 값을 변경하기 위해 Invoke 사용
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = i;
                }));
                if (i == 56)
                {
                    pictureBox3.Enabled = true;
                }
                else if (i == 68)
                {
                    pictureBox2.Enabled = true;
                }
                else if (i == 79)
                {
                    pictureBox4.Enabled = true;
                }
               

                Thread.Sleep(333);

            }
        }
    }
}

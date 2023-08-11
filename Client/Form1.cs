using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joeun_Convenience_store
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inventory_control ic = new Inventory_control();
            ic.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Store_management sm = new Store_management();
            sm.Show();
        }
    }
}

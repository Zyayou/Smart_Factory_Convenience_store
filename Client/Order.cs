using Oracle.ManagedDataAccess.Client;
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
    public partial class Order : Form
    {
        private string strCon = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))
                                (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe))); User Id = hr; Password = hr;";
        public Order()
        {
            InitializeComponent();
            LoadProductNames();
        }

        private void LoadProductNames()
        {
            using (OracleConnection connection = new OracleConnection(strCon))
            {
                connection.Open();
                string query = "SELECT DISTINCT 제품명 FROM Inventory_Status";
                OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    string productName = row["제품명"].ToString();
                    CheckBox checkBox = new CheckBox();
                    checkBox.Text = productName;
                    flowLayoutPanel1.Controls.Add(checkBox);
                }
            }
        }
        private void Order_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(strCon))
            {
                connection.Open();

                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is CheckBox checkBox && checkBox.Checked)
                    {
                        string productName = checkBox.Text;
                        int quantity = Convert.ToInt32(numericUpDown1.Value);

                        // Update the current stock
                        string updateQuery = $"UPDATE Inventory_Status SET \"현재 재고량\" = \"현재 재고량\" + {quantity} WHERE \"제품명\" = '{productName}'";
                        OracleCommand updateCommand = new OracleCommand(updateQuery, connection);
                        updateCommand.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("발주가 완료되었습니다.");
                this.Close();
            }
        }
    }
}

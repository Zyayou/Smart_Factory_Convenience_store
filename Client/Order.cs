using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joeun_Convenience_store
{
    public partial class Order : Form
    {
        private NetworkStream stream;
        private TcpClient client;

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
            string serverip = "127.0.0.1";
            int port = 13001;
            client = new TcpClient(serverip, port);
            stream = client.GetStream();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(strCon))
            {
                connection.Open();

                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is CheckBox checkBox && checkBox.Checked)
                    {
                        string productName = checkBox.Text;
                        string updateQuery;
                        int quantity = Convert.ToInt32(numericUpDown1.Value);
                        DateTime now = DateTime.Now;
                        string current = now.ToString("yyyy-MM-dd");


                        byte[] data = Encoding.UTF8.GetBytes(productName + quantity + "\n");
                        stream.Write(data, 0, data.Length);

                        // Update the current stock
                        //string updateQuery = $"UPDATE Inventory_Status SET \"현재 재고량\" = \"현재 재고량\" + {quantity} WHERE \"제품명\" = '{productName}'";

                        if (productName == "도시락" || productName == "삼각김밥" || productName == "빵")
                        {
                            updateQuery = $@"
                     BEGIN
                            INSERT INTO PRODUCT_ORDER VALUES ('{productName}', {quantity}, to_date('2023-08-05','yyyy-mm-dd'), 2, to_date('2023-08-05','yyyy-mm-dd')+2);
                            UPDATE Inventory_Status SET ""현재 재고량"" = ""현재 재고량"" + {quantity} WHERE ""제품명"" = '{productName}';
                                                      
                     END;";
                        }

                        else if (productName == "라면" || productName == "음료수" || productName == "과자")
                        {
                            updateQuery = $@"
                     BEGIN
                            INSERT INTO PRODUCT_ORDER VALUES ('{productName}', {quantity}, to_date('{current}','yyyy-mm-dd'), 50, to_date('{current}','yyyy-mm-dd')+2);
                            UPDATE Inventory_Status SET ""현재 재고량"" = ""현재 재고량"" + {quantity} WHERE ""제품명"" = '{productName}';
                                                      
                     END;";
                        }

                        else
                        {
                            updateQuery = $@"
                     BEGIN
                            INSERT INTO PRODUCT_ORDER VALUES ('{productName}', {quantity}, to_date('{current}','yyyy-mm-dd'), 30, to_date('{current}','yyyy-mm-dd')+30);
                            UPDATE Inventory_Status SET ""현재 재고량"" = ""현재 재고량"" + {quantity} WHERE ""제품명"" = '{productName}';
                                                      
                     END;";
                        }

                        OracleCommand updateCommand = new OracleCommand(updateQuery, connection);
                        updateCommand.ExecuteNonQuery();

                    }
                }

                MessageBox.Show("발주 신청이 완료되었습니다. 잠시만 기다려주세요.");

                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data2 = new byte[1024];
                    int bytes;

                    while ((bytes = await stream.ReadAsync(data2, 0, data2.Length)) != 0)
                    {

                        string responseData = Encoding.UTF8.GetString(data2, 0, bytes);
                        if (responseData == "완료")
                        {
                            MessageBox.Show("발주 완료.");
                        }
                    }
                }
                client.Close();
            }
        }
    }
}

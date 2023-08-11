using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joeun_Convenience_store
{

    public partial class Inventory_control : Form
    {
        DateTime expiration_date;
        DateTime now;
      

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 현재 재고량 속성 열의 인덱스를 가져옴
            int currentStockColumnIndex = 2;
            //int currentStockColumnIndex2 = 4; //데이터 그리드 뷰2에 가져갈 거임

            if (e.RowIndex >= 0 && e.ColumnIndex == currentStockColumnIndex)
            {
                // 현재 재고량 값 가져오기
                object currentValue = dataGridView1.Rows[e.RowIndex].Cells[currentStockColumnIndex].Value;

                if (currentValue != null && !Convert.IsDBNull(currentValue))
                {
                    int currentStock = Convert.ToInt32(currentValue);
                    string productName = dataGridView1.Rows[e.RowIndex].Cells["제품명"].Value.ToString();

                    // 속성별로 현재 재고량이 권고량보다 이하인 경우 배경색을 빨간색으로 변경
                    if (((productName == "라면" && currentStock <= 20) || (productName == "도시락" && currentStock <= 2)
                        || (productName == "음료수" && currentStock <= 20) || (productName == "과자" && currentStock <= 20) || (productName == "삼각김밥" && currentStock <= 2)
                        || (productName == "빵" && currentStock <= 2) || (productName == "술" && currentStock <= 10)
                        || (productName == "담배" && currentStock <= 10) || (productName == "상비약" && currentStock <= 10)
                        || (productName == "생필품" && currentStock <= 10)))
                    {
                        e.CellStyle.BackColor = Color.Red;
                    }
                }
            }
            
        }
        private static string strCon = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))
                                    (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe))); User Id = hr; Password = hr;";

        public Inventory_control()
        {
            InitializeComponent();
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView2.CellFormatting += dataGridView2_CellFormatting;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trash();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sell sell = new Sell();
            sell.Show();
        }

        private void Inventory_control_Load(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 15);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 15);

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void LoadDataToDataGridView()
        {
            using (OracleConnection connection = new OracleConnection(strCon))
            {

                connection.Open();
                string query = "SELECT * FROM Inventory_Status"; // EMP 테이블 데이터 가져오기
                OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
                //dataGridView1.Columns["유통기한날짜"].DefaultCellStyle.Format = "dd-MM-yyyy";

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
            LoadDataToDataGridView2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChartForm chartForm = new ChartForm();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string productName = row.Cells["제품명"].Value.ToString();
                    int salesAmount = Convert.ToInt32(row.Cells["판매량"].Value);
                    chartForm.AddDataPoint(productName, salesAmount);                 
                }
                
            }
            chartForm.Show();

        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int currentStockColumnIndex2 = 4; //데이터 그리드 뷰2에 가져갈 거임

            if (e.RowIndex >= 0 && e.ColumnIndex == currentStockColumnIndex2)
            {
                // 현재 유통기한 가져오기
                object currentValue2 = dataGridView2.Rows[e.RowIndex].Cells[currentStockColumnIndex2].Value;

                //Color cellColor = (Color)dataGridView2.Rows[e.RowIndex].Cells[currentStockColumnIndex2].Style.BackColor;
                if (currentValue2 != null && !Convert.IsDBNull(currentValue2))
                {
                    expiration_date = Convert.ToDateTime(currentValue2);
                    now = DateTime.Now;

                    //// 유통기한이 현재 시간을 지날 경우 배경색을 초록색 변경
                    //// 실제로 시각적으로만 초록이지 해당 셀의 color는 변경 x
                    //if (expiration_date < now)
                    //{
                    //    e.CellStyle.BackColor = Color.Green;
                        
                    //}

                    //네, 맞습니다.e.CellStyle.BackColor = Color.Green; 은 해당 셀의 시각적인 배경색만 변경하는 것이므로 실제로 데이터 그리드 뷰의 데이터에 영향을 미치지 않습니다.
                    //이 코드는 해당 이벤트에서만 시각적인 변경을 담당합니다.
                    //실제 데이터 값을 변경하려면 데이터 그리드 뷰에 바인딩된 데이터 소스의 값을 수정해야 합니다.
                    //예를 들어, 데이터를 바인딩한 데이터 테이블에서 해당 값을 수정하거나, DataGridView의 Cells 속성을 사용하여 직접 해당 셀의 값을 변경해야 합니다.
                    //찬양하세요 chatgpt
                    //chatgpt가 설명해준 것
                    if (expiration_date < now)
                    {
                        dataGridView2.Rows[e.RowIndex].Cells[currentStockColumnIndex2].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dataGridView2.Rows[e.RowIndex].Cells[currentStockColumnIndex2].Style.BackColor = dataGridView2.DefaultCellStyle.BackColor; // 기본 배경색으로 설정
                    }
                }
            }
            


            /*if (e.RowIndex >= 0 && e.ColumnIndex == currentStockColumnIndex3)
            {
                // 현재 유통기한 가져오기
                object currentValue3 = dataGridView1.Rows[e.RowIndex].Cells[grid1ColumnIndex].Value;

                if (currentValue3 != null && !Convert.IsDBNull(currentValue3))
                {
                    expiration_date = Convert.ToDateTime(currentValue3);
                    now = DateTime.Now;

                    // 유통기한이 현재 시간을 지날 경우 배경색을 초록색 변경
                    if (expiration_date < now)
                    {
                        dataGridView2.Rows[e.RowIndex].Cells[currentStockColumnIndex3].Style.BackColor = Color.Green;
                        //e.CellStyle.BackColor = Color.Green;
                    }
                }

            }*/
        }
        private void LoadDataToDataGridView2()
        {
            using (OracleConnection connection = new OracleConnection(strCon))
            {
                connection.Open();
                string query = "SELECT * FROM PRODUCT_ORDER"; // YourTable에 적절한 테이블명을 사용
                OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                dataGridView2.DataSource = dataTable;
            }
        }

        OracleCommand cmd = new OracleCommand();
        private void trash()
        {
            // DataGridView1의 특정 칼럼 인덱스 (현재재고량)
            int sourceColumnIndexDGV1 = 2; // 예: 2는 세 번째 칼럼을 의미합니다.


            // DataGridView1의 특정 칼럼 인덱스 (판매량)
            int soldThings = 4;

            // DataGridView2의 특정 칼럼 인덱스 (유통기한 날짜)
            int sourceColumnIndexDGV2 = 4; // 예: 2는 세 번째 칼럼을 의미합니다.

            // DataGridView2의 초록색을 나타내는 Color 객체
            Color greenColor = Color.Green;

            // 삭제 대상 항목을 저장할 리스트
            List<string> itemsToDelete = new List<string>(); 

            // DataGridView2의 행을 반복하며 셀의 색상을 확인하고, 값 빼기 및 DataGridView1 업데이트
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                // DataGridView1의 특정 셀의 색상 확인
               
                Color cellColor = (Color)row.Cells[sourceColumnIndexDGV2].Style.BackColor;             

                // DataGridView2의 초록색인 경우에만 처리
                if (cellColor == greenColor)
                {
                    // DataGridView2의 특정 칼럼 값 가져오기 (발주량)

                    int subtractValue = Convert.ToInt32(row.Cells[1].Value);
                    itemsToDelete.Add(Convert.ToString(row.Cells[0].Value)); // 상품명 추가


                    // DataGridView2의 특정 항목을 가지고 있는 행의 값 빼기 및 업데이트 (DataGridView1)
                    string productName = Convert.ToString(row.Cells[0].Value); // 상품명은 DataGridView2의 첫 번째 칼럼에 위치
                    DataGridViewRow correspondingRowInDGV1 = FindRowInDataGridView1(productName);
                    if (correspondingRowInDGV1 != null)
                    {
                        int currentValueDGV1 = Convert.ToInt32(correspondingRowInDGV1.Cells[sourceColumnIndexDGV1].Value);
                        int currentValueSoldDGV1 = Convert.ToInt32(correspondingRowInDGV1.Cells[soldThings].Value);
                        int updatedValueDGV1 = currentValueDGV1 - (subtractValue - currentValueSoldDGV1);
                        

                        // 값이 음수가 되지 않도록 조정
                        if (updatedValueDGV1 < 0)
                        {
                            MessageBox.Show($"{productName} 폐기할 수 없습니다.");
                            continue;
                        }
                        correspondingRowInDGV1.Cells[sourceColumnIndexDGV1].Value = updatedValueDGV1;

                        using (OracleConnection connection = new OracleConnection(strCon))
                        {
                            //2.데이터베이스 접속을 위한 연결
                            connection.Open();

                          
                                cmd.Connection = connection; //연결객체와 연동

                                //업데이트
                                cmd.CommandText = $"UPDATE Inventory_Status SET \"현재 재고량\" = {updatedValueDGV1} WHERE \"제품명\" = '{productName}'";
                                cmd.ExecuteNonQuery();                                              
                        }
                        MessageBox.Show("재고량 업데이트가 완료되었습니다.");
                    }

                }
            }
            using (OracleConnection connection = new OracleConnection(strCon))
            {
                connection.Open();

                foreach (string productName2 in itemsToDelete)
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        command.Connection = connection;
                        
                        // 삭제
                        command.CommandText = $"DELETE FROM PRODUCT_ORDER WHERE \"제품명\" = '{productName2}'";
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("행 삭제가 완료되었습니다.");
            }
            // 데이터 업데이트 후 DataGridView1을 갱신
            LoadDataToDataGridView();
        }

        // DataGridView1에서 상품명으로 특정 행을 찾는 함수
        private DataGridViewRow FindRowInDataGridView1(string productName)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //Convert.ToString(row.Cells[0].Value) == productName
                if (row.Cells["제품명"].Value != null && row.Cells["제품명"].Value.ToString() == productName)
                {
                    return row;
                }

            }
            return null;
        }
    }
}

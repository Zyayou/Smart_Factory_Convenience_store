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
using System.Windows.Forms.DataVisualization.Charting;

namespace Joeun_Convenience_store
{
    public partial class Sales_volume : Form
    {
        private static string strCon = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))
                                    (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe))); User Id = hr; Password = hr;";

        public Sales_volume()
        {
            InitializeComponent();
            InitializeChart();
            UpdateChart();

        }

        private void InitializeChart()
        {
            // 차트 초기 설정
            chartSales.Series.Clear();
            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            chartSales.Series.Add(series);
        }
        private void UpdateChart()
        {
            using (OracleConnection connection = new OracleConnection(strCon))
            {
                connection.Open();

                // 판매 정보를 가져올 SQL 쿼리 작성
                string query = @"
                    SELECT 제품명, SUM(수량) AS 총_판매량
                    FROM 판매_테이블
                    GROUP BY 제품명";

                OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // 차트 데이터 소스 설정
                chartSales.DataSource = dataTable;

                // X축 설정 (제품명)
                chartSales.Series[0].XValueMember = "제품명";

                // Y축 설정 (총 판매량)
                chartSales.Series[0].YValueMembers = "총_판매량";

                // 데이터 바인딩 및 표시
                chartSales.DataBind();
            }
        }
        private void Sales_volume_Load(object sender, EventArgs e)
        {
           
        }
    }
}

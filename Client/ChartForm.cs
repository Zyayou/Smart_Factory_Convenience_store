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
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();           
            chart1.ChartAreas.Add(new ChartArea("SalesChart"));
            Series series = new Series("판매량");
            series.ChartType = SeriesChartType.Column;
            chart1.Series.Add(series);

            chart1.ChartAreas[0].AxisX.Interval = 1; // X 축 간격을 1로 설정
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // 레이블을 45도로 회전
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // 메이저 그리드 비활성화

            // 차트 크기 조절
            chart1.Size = new System.Drawing.Size(955, 1200); // 원하는 크기로 조절
        }

        public void AddDataPoint(string productName, int salesAmount)
        {
            //Series series = chart1.Series["Sales"];
            //series.Points.AddXY(productName, salesAmount);
            Series series = chart1.Series["판매량"];
            DataPoint dataPoint = new DataPoint();
            dataPoint.AxisLabel = productName; // 행 이름을 레이블로 설정
            dataPoint.YValues = new double[] { salesAmount };
            series.Points.Add(dataPoint);
        }

       
    }
}

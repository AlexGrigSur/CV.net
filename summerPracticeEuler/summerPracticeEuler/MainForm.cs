using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using summerPracticeEuler.EulerCalc;


namespace summerPracticeEuler
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();            
        private void Calc(int stepsCount)
        {
            chart1.Series.Clear();

            double leftBorder = 0;
            double rightBorder = 1;
            double step = (rightBorder-leftBorder)/ stepsCount;

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("X", "Y\\X");
            dataGridView1.Rows.Add("Y_Approximate");
            dataGridView1.Rows.Add("Y_Accurate");

            List<List<double>> resultApprox = new EulerCalcApprox(0,1).Calc(leftBorder,rightBorder,step);
            List<List<double>> resultAccur = new EulerCalcAccurate(0, 1).Calc(leftBorder, rightBorder, step);
            Series s1 = new Series("Approximate");
            Series s2 = new Series("Accurate");
            s1.ChartType = SeriesChartType.Line;
            s2.ChartType = SeriesChartType.Line;
            
            double maxDifference = 0;
            
            for (int i = 0; i < resultApprox.Count; ++i)
            {
                dataGridView1.Columns.Add($"X[{i}]", $"{Math.Round(resultApprox[i][0],5)}");
                dataGridView1.Rows[0].Cells[dataGridView1.Columns.Count-1].Value = Math.Round(resultApprox[i][1],5);
                dataGridView1.Rows[1].Cells[dataGridView1.Columns.Count - 1].Value = Math.Round(resultAccur[i][1],5);

                s1.Points.AddXY(resultApprox[i][0], resultApprox[i][1]);
                s2.Points.AddXY(resultAccur[i][0], resultAccur[i][1]);
                
                if (Math.Abs(resultApprox[i][1] - resultAccur[i][1]) > maxDifference)
                    maxDifference = Math.Abs(resultApprox[i][1] - resultAccur[i][1]);
            }
            chart1.Series.Add(s1);
            chart1.Series.Add(s2);
            textBoxDifference.Text = Math.Round(maxDifference, 5).ToString();
        }
        private void textBoxStep_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStep.TextLength > 0)
            {
                if (Convert.ToInt32(textBoxStep.Text) > 653) 
                    textBoxStep.Text = "653";
                else
                    Calc(Convert.ToInt32(textBoxStep.Text));
            }
            else
                chart1.Series.Clear();
        }

        private void textBoxStep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}

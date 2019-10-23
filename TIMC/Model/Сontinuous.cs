using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TIMC.Model
{
    public class Сontinuous
    {
        public Dictionary<double, double> Sample { get; set; }

        public Сontinuous(Dictionary<double, double> sample)
        {
            this.Sample = sample;
        }

        public double Amount()
        {
            double amount = 0;

            foreach (KeyValuePair<double, double> keyValuePair in Sample)
            {
                amount += keyValuePair.Value;
            }

            return amount;
        }

        public List<double> Intervals()
        {
            List<double> intervals = new List<double>();

            int n = (int)(5 * Math.Log10(Amount()));
            double item = Sample.Keys.Min();

            double k = (Sample.Keys.Max() - Sample.Keys.Min()) / n;
            intervals.Add(Sample.Keys.Min());

            for (int i = 0; i < n; ++i)
            {
                item += k;
                intervals.Add(item);
            }

            return intervals;
        }

        public List<double> AbsoluteFrequency()
        {
            List<double> absoluteFrequency = new List<double>();
            List<double> intervals = Intervals();

            double sum = 0;


            for (int i = 0; i < intervals.Count - 1; ++i) {
                foreach (KeyValuePair<double, double> keyValuePair in Sample)
                {
                    if (intervals[i] <= keyValuePair.Key && intervals[i + 1] >= keyValuePair.Key)
                    {
                        sum += keyValuePair.Value;
                    }
                }
                absoluteFrequency.Add(sum);
                sum = 0;
            }

            return absoluteFrequency;
        }

        public List<double> RelativeFrequency()
        {
            List<double> relativeFrequency = new List<double>();
            double amount = Amount();
            List<double> absoluteFrequnecy = AbsoluteFrequency();

            for (int i=0; i<absoluteFrequnecy.Count;++i)
            {
                relativeFrequency.Add(absoluteFrequnecy[i] / amount);
            }
            return relativeFrequency;
        }

        public string WriteIntervals()
        {
            string strIntervals = "";
            List<double> intervals = Intervals();

            for (int i = 0; i < intervals.Count - 1; ++i)
            {
                strIntervals += $"[{intervals[i]},{intervals[i + 1]})  ";
            }
            return strIntervals;
        }
        
        public string WriteAdsoluteFrequency()
        {
            string strAbsoluteFrequency = "m  ";
            List<double> absoluteFrequency = AbsoluteFrequency();

            for (int i = 0; i < absoluteFrequency.Count; ++i)
            {
                strAbsoluteFrequency += absoluteFrequency[i] + "  ";
            }

            return strAbsoluteFrequency;

        }

        public string WriteRelativeFrequency()
        {
            string strRelativeFrequency = "w  ";
            List<double> relativeFrequency = RelativeFrequency();

            for (int i = 0; i < relativeFrequency.Count; ++i)
            {
                strRelativeFrequency += relativeFrequency[i] + "  ";
            }

            return strRelativeFrequency;
        }

        public List<double> EmpiricalFunction()
        {
            List<double> relativeFrequency = RelativeFrequency();
            List<double> empiricalFunction = new List<double>();
            double sum = 0;
            empiricalFunction.Add(sum);

            for (int i = 0; i < relativeFrequency.Count; ++i)
            {
                sum += relativeFrequency[i];
                empiricalFunction.Add(sum);
            }
            return empiricalFunction;
        }

        public void WriteEmpiricalFunction(Chart chart )
        {
            List<double> intervals = Intervals();
            List<double> empiricalFunction = EmpiricalFunction();

            chart.Series[0].ChartType = SeriesChartType.Line;
            chart.Series[0].Points.Clear();
            chart.ChartAreas[0].AxisX.Minimum = Sample.Keys.Min();
            chart.ChartAreas[0].AxisX.Maximum = Sample.Keys.Max();
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 1;
            chart.ChartAreas[0].AxisX.Interval = 10;
            chart.ChartAreas[0].AxisY.Interval = 0.1;

            for (int i=0;i<intervals.Count;++i)
            {
                chart.Series[0].Points.AddXY(intervals[i],empiricalFunction[i]);
            }
        }

        public void WriteHistogram(Chart chart)
        {
            List<double> relativeFrequency = RelativeFrequency();
            List<double> intervals = Intervals();

            chart.Series[0].ChartType = SeriesChartType.Column;
            chart.Series[0].Points.Clear();
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 0.05;
            chart.ChartAreas[0].AxisY.Interval = 0.005;
            chart.ChartAreas[0].AxisX.Interval = 1;
            double h = 0;
            string strInterval = "";

            for (int i=0; i<intervals.Count-1;++i)
            {
                strInterval = $"[{intervals[i]},{intervals[i+1]})";
                h = relativeFrequency[i] / (intervals[i + 1] - intervals[i]);
                chart.Series[0].Points.AddXY(strInterval, h);
            }
        }


    }
}

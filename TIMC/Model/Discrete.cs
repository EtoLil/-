using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIMC.Model
{
    public class Discrete
    {
        public Dictionary<double, double> Sample { get; set; }

        public Discrete()
        {
            Sample = new Dictionary<double, double>();
        }


        public List<double> RelativeFrequency()
        {
            List<double> relativeFrequency = new List<double>();
            double[] absoluteFrequency = AbsoluteFrequency();

            double countOfSample = Sum(absoluteFrequency);


            foreach (KeyValuePair<double, double> keyValue in Sample)
            {
                relativeFrequency.Add(keyValue.Value / countOfSample);
            }

            return relativeFrequency;
        }

        public double Sum(double[] absoluteFrequency)
        {
            double sum = 0;

            for (int i = 0; i < absoluteFrequency.Length; ++i)
            {
                sum += absoluteFrequency[i];
            }

            return sum;
        }

        public List<double> Elements()
        {
            List<double> elements = new List<double>();

            foreach (var element in Sample.Keys)
            {
                elements.Add(element);
            }

            return elements;
        }

        public double[] AbsoluteFrequency()
        {
            int count = Sample.Count;

            int i = 0;

            double[] absoluteFrequency = new double[count];

            foreach (var element in Sample.Values)
            {
                absoluteFrequency[i] = element;
                i++;
            }

            return absoluteFrequency;
        }

        public void ReadFromFile()
        {
            using (StreamReader sr = new StreamReader(@"C: \Users\admin\source\repos\TIMC\TIMC\Data\Sample.txt"))
            {
                string line1, line2;

                line1 = sr.ReadLine();
                line2 = sr.ReadLine();
                string[] stringVariable= line1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] stringAbsoluteFrequency = line2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i=0;i<stringVariable.Length;++i)
                {
                    Sample.Add(Convert.ToDouble(stringVariable[i]),Convert.ToDouble(stringAbsoluteFrequency[i]));
                }

            }
        }

        public double MinX()
        {
            return Sample.Keys.Min();
        }

        public double MaxX()
        {
            return Sample.Keys.Max();
        }

        public double MinY()
        {
            return Sample.Values.Min();
        }

        public double MaxY()
        {
            return Sample.Values.Max();
        }

        public double[] EmpiricalFunction()
        {
            int count = Sample.Count;

            double[] empiricalFunction = new double[count+1];
            List<double> relativeFrequency = RelativeFrequency();
            double empiricalElement = 0;


            for (int i=0; i<count;++i)
            {
                empiricalFunction[i] = empiricalElement;
                empiricalElement += relativeFrequency[i];
            }
            empiricalFunction[count] = empiricalElement;

            return empiricalFunction;
        }

        public double R()
        {
            return MaxX() - MinX();
        }

        public double X()
        {
            double arithmeticMean = 0;
            int count = Sample.Count;
            List<double> relativeFrequncy = RelativeFrequency();
            int i = 0;

            foreach (KeyValuePair<double,double> keyValuePair in Sample)
            {
                arithmeticMean += keyValuePair.Key * relativeFrequncy[i];
                i++;
            }

            return arithmeticMean;
        }

        public double Me()
        {
            int count = Sample.Count;
            List<double> elements = Elements();

            if (count % 2 == 1)
                return elements[(count / 2)];
            else
            {
                return (elements[(count / 2)-1] + elements[(count / 2)]) / 2;
            }
        }

        public double Mo()
        {
            int indexMax=0;
            double maxAbsoluteFrequency=0;
            int i = 0;
            List<double> elements = Elements();
            foreach (KeyValuePair<double,double> keyValuePair in Sample)
            {
                if (keyValuePair.Value > maxAbsoluteFrequency)
                {
                    maxAbsoluteFrequency = keyValuePair.Value;
                    indexMax = i;
                }
                i++;
            }
            return elements[indexMax];

        }

        public double D()
        {
            double x = this.X();
            double D = 0;
            int count = Sample.Count;
            List<double> relativeFrequency = RelativeFrequency();
            List<double> elements = Elements();

            for (int i=0;i<count;++i)
            {
                D += Math.Pow(elements[i] - x, 2) * relativeFrequency[i];
            }

            return D;
        }

        public double S()
        {
            return Math.Sqrt(D());
        }

        public double S2()
        {
            double S2;
            double[] absoluteFrequency = AbsoluteFrequency();
            double n = Sum(absoluteFrequency);
            double s = S();

            S2 = n * Math.Pow(s, 2)/(n-1);

            return S2;
        }

        public double P()
        {
            double p=0;
            double[] absoluteFrequency = AbsoluteFrequency();
            double n = Sum(absoluteFrequency);
            double x = X();

            foreach (KeyValuePair<double,double> keyValuePair in Sample)
            {
                p += Math.Abs(keyValuePair.Key - x) * keyValuePair.Value;
            }

            p *= 1 / n;

            return p;
        }


        public double Vp()
        {
            double V;
            double p = P();
            double x = X();
            V = (p * 100) / x;
            double two = V - (int)V;
            two *= 1000;
            two = (int)two;
            two /= 1000;
            V = (int)V + two;

            return V;
        }

        public double Vs()
        {
            double V;
            double s = S();
            double x = X();
            V = (s * 100) / x;
            double two = V - (int)V;
            two *= 1000;
            two = (int)two;
            two /= 1000;
            V = (int)V + two;

            return V;
        }

        public double M(int s)
        {
            double m = 0;
            int count = Sample.Count;
            List<double> relativeFrequency = RelativeFrequency();
            List<double> elements = Elements();

            for (int i=0;i<count;++i)
            {
                m += Math.Pow(elements[i], s) * relativeFrequency[i];
            }

            return m;
        }

        public double U(int s)
        {
            double u = 0;
            double x = X();
            int count = Sample.Count;
            List<double> relativeFrequency = RelativeFrequency();
            List<double> elements = Elements();

            for (int i=0; i<count;++i)
            {
                u += Math.Pow((elements[i] - x), s) * relativeFrequency[i];
            }

            return u;
        }

        public double A()
        {
            double a;
            double u = U(3);
            double s = Math.Pow(S(), 3);
            a = u / s;

            return a;
        }

        public double E()
        {
            double e = -3;
            double sum = 0;
            double s = Math.Pow(S(), 4);
            double x = X();

            double n =Sum(AbsoluteFrequency());

            foreach (KeyValuePair<double,double> keyValuePair in Sample)
            {
                sum += Math.Pow(keyValuePair.Key - x, 4) * keyValuePair.Value;
            }

            e += sum / (n * s);

            return e;
        }

        public string[] Table()
        {
            string[] table = new string[3];
            string x = "x     ";
            string m = "m     ";
            string w = "w    ";
            int i = 0;

            List<double> relativeFrequency = RelativeFrequency();

            foreach (KeyValuePair<double,double> keyValuePair in Sample)
            {
                x += keyValuePair.Key + "       ";
                m += keyValuePair.Value + "       ";
                w += relativeFrequency[i] + "      ";
                i++;
            }
            table[0] = x;
            table[1] = m;
            table[2] = w;


            return table;
        } 
    }
}

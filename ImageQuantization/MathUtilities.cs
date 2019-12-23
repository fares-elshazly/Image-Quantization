using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageQuantization
{
    public class MathUtilities
    {
        static double Eps = .00000001;
        public static double mean { set; get; }
        public static double stdDev { set; get; }
        public double prevStdDev = 0;
        public double WeightLeadToMax = 0;
        public int K = 0;
        public Edge edgeLeadToMax;
        public static List<Edge> currentEdges;

        public static int FastPower(int Base, int Power)
        {
            if (Power == 0)
                return 1;

            int Result = FastPower(Base, Power / 2);

            if (Power % 2 == 0)
                return Result * Result;
            else
                return Base * Result * Result;
        }

        public static double GetDistance(int i, int j)
        {
            RGBPixel Cluster1 = ImageUtilities.DistinctColours[i];
            RGBPixel Cluster2 = ImageUtilities.DistinctColours[j];
            double Result = (double)Math.Sqrt((Cluster1.red - Cluster2.red) * (Cluster1.red - Cluster2.red) + (Cluster1.green - Cluster2.green) * (Cluster1.green - Cluster2.green) + (Cluster1.blue - Cluster2.blue) * (Cluster1.blue - Cluster2.blue));
            return Result;

        }

        public static double RoundUp(double Input, int Places)
        {
            double Multiplier = Math.Pow(10, Convert.ToDouble(Places));
            return Math.Ceiling(Input * Multiplier) / Multiplier;
        }

        private void CalculateMean()
        {
            double sum = 0;
            foreach (Edge edge in currentEdges)
                sum += edge.Weight;

            mean = sum / currentEdges.Count();
        }
        private void CalcuateStdDev()
        {
            double sum = 0;
            foreach (Edge edge in currentEdges)
            {
                if ((edge.Weight - mean) * (edge.Weight - mean) > WeightLeadToMax)
                {
                    WeightLeadToMax = (edge.Weight - mean) * (edge.Weight - mean);
                    edgeLeadToMax = edge;
                }
                sum += ((edge.Weight - mean) * (edge.Weight - mean));
            }
            WeightLeadToMax = 0;
            stdDev = sum / (currentEdges.Count() - 1);
            stdDev = Math.Sqrt(stdDev);
        }
    }
}

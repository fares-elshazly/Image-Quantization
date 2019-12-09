using System;

namespace ImageQuantization
{
    public class MathUtilities
    {
        static double Eps = .00000001;

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

        public static double GetDistance(RGBPixel Cluster1, RGBPixel Cluster2)
        {
            double Result = FastPower(Cluster1.red - Cluster2.red, 2) + FastPower(Cluster1.green - Cluster2.green, 2) + FastPower(Cluster1.blue - Cluster2.blue, 2);
            return Math.Sqrt(Result);
        }

        public static double RoundUp(double Input, int Places)
        {
            double Multiplier = Math.Pow(10, Convert.ToDouble(Places));
            return Math.Ceiling(Input * Multiplier) / Multiplier;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageQuantization;

namespace ImageQuantization
{
    public class Utilities
    {
        public int FastPower(int Base, int Power)
        {

            int Result = FastPower(Base, Power / 2);
            if (Power == 1)
                return Base;
            if (Power == 0)
                return 1;
            if (Power % 2 == 0)
                return FastPower(Base, Power / 2) * Result;
            else
                return Base * Result * FastPower(Base, Power / 2);

        }
        public double SquareRoot(double Start, double End, double Number)
        {
            Start = 1;
            End = Number;
            if (Start > End)
                return -1;
            double Mid = (Start + End) / 2;
            if (Mid * Mid == Number || (Math.Abs(Mid * Mid - Number) < .00000001))
            {
                int Res = (int)Mid;
                if (Mid * Mid == Number)
                    return Res;
                return Mid;
            }
            if (Mid * Mid > Number)
                return SquareRoot(Start, Mid, Number);
            else
                return SquareRoot(Mid, End, Number);
        }


    }
}
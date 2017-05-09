using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesMakingSystemsLab
{
    class Function
    {
        private double A;
        private double C;
        private double B;
        private double D;

        public void SetFunctionParams(double _A, double _C, double _B, double _D)
        {
            A = _A;
            C = _C;
            B = _B;
            D = _D;
        }

        public double GetFunctionValue(double x)
        {
            return A * Math.Sin(C * x) + B * Math.Cos(D * x);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace summerPracticeEuler.EulerCalc
{
    class EulerCalcApprox : BaseEuler
    {
        public EulerCalcApprox(double X, double Y) : base(X, Y)
        {
        }
        protected override double function(double x, double y) => (Exp(2 * x) * (2 + 3 * Cos(x)) * (1 / y) - 3 * y * Cos(x)) / 2;
    }
}

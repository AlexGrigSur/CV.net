using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace summerPracticeEuler.EulerCalc
{
    class EulerCalcAccurate : BaseEuler
    {
        public EulerCalcAccurate(double X, double Y) : base(X, Y)
        {
        }
        protected override double function(double x, double y) => Sqrt(Exp(2 * x));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace summerPracticeEuler.EulerCalc
{
    abstract class BaseEuler
    {
        protected double x;
        protected double y;

        // 2y'+3ycosx=e^(2x)(2+3cosx)y^-1
        public BaseEuler(double X, double Y)
        {
            x = X;
            y = Y;
        }
        protected abstract double function(double x, double y);

        public List<List<double>> Calc(double leftBorder, double rightBorder, double step)
        {
            List<List<double>> Result = new List<List<double>>();
            for (double i = leftBorder; i <= rightBorder; i += step)
            {
                List<double> XY = new List<double>();
                XY.Clear();
                XY.Add(x);
                XY.Add(y);
                Result.Add(XY);
                y += step * function(x, y);
                x += step;
            }
            return Result;
        }
    }

}

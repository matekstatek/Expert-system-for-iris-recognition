using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    interface Functions
    {
        void randomize(ref float x1, ref float x2, ref float x3, ref float y1, ref float y2, ref float y3, ref float z1, ref float z2, ref float z3, ref float a1, ref float a2, ref float a3);

        void licz(ref int counter, ref float x, ref float y, ref float z, ref float a, float[] floatValues, ref float b);

        /**
         * funkcja radialna
         */
        float f(float b, float centre, float x);
    }
}

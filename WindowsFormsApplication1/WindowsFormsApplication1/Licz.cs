using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Licz:Functions
    {
        public Licz()
        {

        }

        public void licz(ref int counter, ref float x, ref float y, ref float z, ref float a, float[] floatValues, ref float b)
        {
            counter++;
            x = (x * (counter - 1) + floatValues[0]) / counter;
            y = (y * (counter - 1) + floatValues[1]) / counter;
            z = (z * (counter - 1) + floatValues[2]) / counter;
            a = (a * (counter - 1) + floatValues[3]) / counter;
            if (b >= 0.5) b -= (float)0.01;
        }

        public void randomize(ref float a, ref float b, ref float c, ref float d, ref float e, ref float f, ref float g, ref float h, ref float i, ref float j, ref float k, ref float l)
        {
            Random rnd = new Random();

            a = rnd.Next(0, 10);
            b = rnd.Next(0, 10);
            c = rnd.Next(0, 10);
            d = rnd.Next(0, 10);
            e = rnd.Next(0, 10);
            f = rnd.Next(0, 10);
            g = rnd.Next(0, 10);
            h = rnd.Next(0, 10);
            i = rnd.Next(0, 10);
            j = rnd.Next(0, 10);
            k = rnd.Next(0, 10);
            l = rnd.Next(0, 10);
        }

        public float f(float b, float centre, float x)
        {
            //funkcja radialna f(x) = exp(-b*r^2), gdzie:
            //b - współczynnik szerokości wykresu
            //r - odległość między centrum i daną uczącą
            return (float)(Math.Exp(-b * Math.Pow((x - centre), 2)));
        }
    }
}

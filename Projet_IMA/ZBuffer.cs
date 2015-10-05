using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    public static class ZBuffer
    {
        public static double[,] zbuffer = new double[957, 569];

        public static void init()
        {
            for (int i = 0; i < zbuffer.GetLength(0); i++)
            {
                for (int j = 0; j < zbuffer.GetLength(1); j++)
                {
                    zbuffer[i, j] = double.MaxValue;
                }
            }
        }
    }
}

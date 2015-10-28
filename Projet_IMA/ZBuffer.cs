using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    public static class ZBuffer
    {
        public static double[,] zbuffer = new double[BitmapEcran.GetWidth(), BitmapEcran.GetHeight()];

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

        public static bool test(double y, int x_ecran, int z_ecran)
        {
            if (x_ecran < zbuffer.GetLength(0) && z_ecran < zbuffer.GetLength(1))
            {
                if (y < ZBuffer.zbuffer[x_ecran, z_ecran])
                {
                    ZBuffer.zbuffer[x_ecran, z_ecran] = y;
                    return true;
                }
            }
            return false;
        }
    }
}

using System;

namespace Projet_IMA
{
    static class Draw
    {
        public static void DrawSphere(int x_center, int y_center, int z_center, int r, Couleur C_obj, Couleur C_ambiant, Couleur C_lampe, ref double[,] zbuffer, V3 L)
        {
            //Texture T1 = new Texture("brick01.jpg");




            double pas = 0.01;
            Couleur final_ambiant = C_obj * C_ambiant;
            Couleur final_diff, final_spec;
            float cosln;
            int k = 30;

            for (double u = 0; u < 2 * Math.PI; u += pas)
            {
                for (double v = -1 * Math.PI / 2; v < Math.PI / 2; v += pas)
                {

                    //C_obj = T1.LireCouleur((float)u, (float)v);
                    double x = r * Math.Cos(v) * Math.Cos(u);
                    double y = r * Math.Cos(v) * Math.Sin(u);
                    double z = r * Math.Sin(v);
                    int x_ecran = (int)x + x_center;
                    int z_ecran = (int)z + z_center;
                    if (y < zbuffer[x_ecran, z_ecran])
                    {
                        zbuffer[x_ecran, z_ecran] = y;
                        V3 N = new V3((float)x, (float)y, (float)z); //ne pas toucher 
                        L.Normalize();
                        N.Normalize();
                        cosln = L * N;
                        cosln = cosln < 0 ? 0 : cosln;
                        final_diff = C_obj * C_lampe * cosln;

                        V3 S = 2 * N * cosln - L;
                        S.Normalize();
                        V3 O = new V3((float)x, (float)y, (float)z);
                        V3 camera = new V3(200, -1000, 200);
                        O = camera - O;
                        O.Normalize();
                        double cosso = S * O;
                        final_spec = C_lampe * (float)Math.Pow(cosso, k);

                        Couleur finalColor = final_ambiant + final_diff + final_spec;
                        BitmapEcran.DrawPixel(x_ecran, z_ecran, finalColor);
                    }
                }
            }
        }
    }
}

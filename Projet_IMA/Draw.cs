using System;
using System.Diagnostics;

namespace Projet_IMA
{
    static class Draw
    {

        public static void DrawSphere(int x_center, int y_center, int z_center, int r, Texture T, Couleur C_ambiant, Couleur C_lampe, V3 L)
        {
            double pas = 0.005;
            Couleur final_diff, final_spec;
            float cosln;
            int k = 30;
            Texture T_bump = new Texture("bump38.jpg");

            for (double u = 0; u < 2 * Math.PI; u += pas)
            {
                for (double v = -1 * Math.PI / 2; v < Math.PI / 2; v += pas)
                {
                    Couleur C_obj = T.LireCouleur((float)(u / (2 * Math.PI)), (float)((v+Math.PI/2)/Math.PI));
                    Couleur final_ambiant = C_obj * C_ambiant;
                    double x = r * Math.Cos(v) * Math.Cos(u);
                    double y = r * Math.Cos(v) * Math.Sin(u);
                    double z = r * Math.Sin(v);
                    int x_ecran = (int)x + x_center;
                    int z_ecran = (int)z + z_center;
                    if (y < ZBuffer.zbuffer[x_ecran, z_ecran])
                    {
                        /* UPDATE ZBUFFER */
                        ZBuffer.zbuffer[x_ecran, z_ecran] = y;
                        V3 N = new V3((float)x, (float)y, (float)z); //ne pas toucher 

                        /* BUMP MAP */
                        V3 dmdu = new V3((float)(-r * Math.Cos(v) * Math.Sin(u)),
                                            (float)(r * Math.Cos(v) * Math.Cos(u)),
                                            0.0f);
                        V3 dmdv = new V3((float)(-r * Math.Sin(v) * Math.Cos(u)),
                                            (float)(-r * Math.Sin(v) * Math.Sin(u)),
                                            (float)(r * Math.Cos(v)));
                        N = bumpStep(T_bump, N, (float)(u / (2 * Math.PI)), (float)((v+Math.PI/2)/Math.PI), dmdu, dmdv);
                        /* FIN BUMP MAP */

                        /* DIFFUS */
                        N.Normalize();
                        L.Normalize();
                        cosln = L * N;
                        cosln = cosln < 0 ? 0 : cosln;
                        final_diff = C_obj * C_lampe * cosln;
                        /* FIN DIFFUS */

                        /* SPECULAIRE */
                        V3 S = 2 * N * cosln - L;
                        S.Normalize();
                        V3 O = new V3((float)x, (float)y, (float)z);
                        V3 camera = new V3(200, -1000, 200);
                        O = camera - O;
                        O.Normalize();
                        double cosso = S * O;
                        final_spec = C_lampe * (float)Math.Pow(cosso, k);
                        /* FIN SPECULAIRE */

                        // COULEUR FINALE
                        Couleur finalColor = final_ambiant + final_diff + final_spec;
                        // ON DESSINE A L'ECRAN
                        BitmapEcran.DrawPixel(x_ecran, z_ecran, finalColor);
                    }
                }
            }
        }

        public static V3 bumpStep(Texture T_bump, V3 N, float u, float v, V3 dmdu, V3 dmdv)
        {
            float coeff = 0.005f;
            float dhdu, dhdv;
            
            T_bump.Bump(u, v, out dhdu, out dhdv);
            V3 Np = new V3(N + coeff * ((dmdu ^ (N * dhdv)) + ((N * dhdu) ^ dmdv)));

            return Np;
        }
    }
}

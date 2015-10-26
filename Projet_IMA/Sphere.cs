using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Sphere : Objet3D
    {
        int rayon;
        int k;
        float bump_coeff;

        public Sphere(V3 center, int rayon, Texture T, Texture T_bump)
        {
            pas = 0.005;
            k = 30;
            this.origin = center;
            this.rayon = rayon;
            this.T = T;
            this.T_bump = T_bump;
            this.bump_coeff = 0.005f;
        }

        public override void Draw(Couleur C_ambiant, Lumiere L)
        { 
            float cosln;
            Couleur final_ambiant, final_diff, final_spec;
            Couleur C_lampe = L.getClampe();

            for (double u = 0; u < 2 * Math.PI; u += pas)
            {
                for (double v = -1 * Math.PI / 2; v < Math.PI / 2; v += pas)
                {
                    float dhdu, dhdv;
                    Couleur C_obj = T.LireCouleur((float)(u / (2 * Math.PI)), (float)((v + Math.PI / 2) / Math.PI));
                    final_ambiant = C_obj * C_ambiant;
                    double x = rayon * Math.Cos(v) * Math.Cos(u);
                    double y = rayon * Math.Cos(v) * Math.Sin(u);
                    double z = rayon * Math.Sin(v);
                    int x_ecran = (int)x + (int)origin.x;
                    int z_ecran = (int)z + (int)origin.z;
                    if (ZBuffer.test(y, x_ecran, z_ecran))
                    {
                        V3 N = new V3((float)x, (float)y, (float)z); //ne pas toucher 

                        // BUMP MAP */
                        if (T_bump != null)
                        {
                            V3 dmdu = new V3((float)(-rayon * Math.Cos(v) * Math.Sin(u)),
                                            (float)(rayon * Math.Cos(v) * Math.Cos(u)),
                                            0.0f);
                            V3 dmdv = new V3((float)(-rayon * Math.Sin(v) * Math.Cos(u)),
                                                (float)(-rayon * Math.Sin(v) * Math.Sin(u)),
                                                (float)(rayon * Math.Cos(v)));
                            this.T_bump.Bump((float)(u / (2 * Math.PI)), (float)((v + Math.PI / 2) / Math.PI), out dhdu, out dhdv);
                            N = N + bump_coeff * ((dmdu ^ (N * dhdv)) + ((N * dhdu) ^ dmdv));
                        }
                        
                        // FIN BUMP MAP */

                        V3 O = new V3((float)x, (float)y, (float)z);
                        V3 camera = new V3(200, -1000, 200);
                        O = camera - O;
                        Couleur finalColor = computeLights(C_obj, C_ambiant, N, O, L, k);
                        BitmapEcran.DrawPixel(x_ecran, z_ecran, finalColor);
                    }
                }
            }
        }
    }
}

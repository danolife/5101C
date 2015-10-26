using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Projet_IMA
{
    class Rect : Objet3D
    {
        V3 L1, L2, N; // vecteurs longueur, largeur, normale
        int k;
        float bump_coeff;

        public Rect(V3 origin, V3 L1, V3 L2, Texture T, Texture T_bump)
        {
            pas = 0.001;
            k = 30;
            this.origin = origin;
            this.L1 = L1;
            this.L2 = L2;
            this.N = L1 ^ L2;
            this.T = T;
            this.T_bump = T_bump;
            this.bump_coeff = 0.005f;
        }

        public override void Draw(Couleur C_ambiant, Lumiere L, V3 camera)
        {
            //float cosln;
            //Couleur final_ambiant, final_diff, final_spec;
            //Couleur C_lampe = L.getClampe();

            //L1 = (new V3(0.0f, 1.0f, 0.0f)) ^ (L1 ^ (new V3(0.0f, 1.0f, 0.0f)));
            //L2 = (new V3(0.0f, 1.0f, 0.0f)) ^ (L2 ^ (new V3(0.0f, 1.0f, 0.0f)));
            // 4 sommets : origin, origin + L1, origin + L2, origin + L1 + L2
            for (V3 u = new V3(0.0f, 0.0f, 0.0f); (u.x < Math.Abs(L1.x) || L1.x == 0) && (u.y < Math.Abs(L1.y) || L1.y == 0) && (u.z < Math.Abs(L1.z) || L1.z == 0); u += (L1 * (float)pas))
            {
                for (V3 v = new V3(0.0f, 0.0f, 0.0f); (v.x < Math.Abs(L2.x) || L2.x == 0) && (v.y < Math.Abs(L2.y) || L2.y == 0) && (v.z < Math.Abs(L2.z) || L2.z == 0); v += (L2 * (float)pas))
                {
                    float dhdu, dhdv;
                    float x = u.x + v.x + origin.x;
                    float y = u.y + v.y + origin.y;
                    float z = u.z + v.z + origin.z;

                    int x_ecran = (int)x;
                    int z_ecran = (int)z;

                    Couleur C_obj = T.LireCouleur((float)(u.Norm() / L1.Norm()), (float)(v.Norm() / L2.Norm()));
                    //final_ambiant = C_obj * C_ambiant;

                    if (ZBuffer.test(y, x_ecran, z_ecran))
                    {
                        /* BUMP MAP */
                        V3 Np = N;
                        if (T_bump != null)
                        {
                            V3 dmdu = L1;
                            V3 dmdv = L2;
                            this.T_bump.Bump((float)(u.Norm() / L1.Norm()), (float)(v.Norm() / L2.Norm()), out dhdu, out dhdv);
                            Np = N + bump_coeff * ((dmdu ^ (N * dhdv)) + ((N * dhdu) ^ dmdv));
                        }
                        /* FIN BUMP MAP */

                        V3 O = new V3(x, y, z);
                        O = camera - O;
                        Couleur finalColor = computeLights(C_obj, C_ambiant, Np, O, L, k);
                        // ON DESSINE A L'ECRAN
                        BitmapEcran.DrawPixel(x_ecran, z_ecran, finalColor);
                    }
                }
            }
        }
    }
}

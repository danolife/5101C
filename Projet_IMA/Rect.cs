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

        public Rect(V3 origin, V3 L1, V3 L2, V3 N, Texture T, Texture T_bump)
        {
            pas = 0.001;
            k = 30;
            this.origin = origin;
            this.L1 = L1;
            this.L2 = L2;
            this.N = L1 ^ L2;
            this.T = T;
            this.T_bump = T_bump;
        }

        public override void Draw(Couleur C_ambiant, Couleur C_lampe, V3 L)
        {
            float cosln;
            Couleur final_ambiant, final_diff, final_spec;

            L1 = (new V3(0.0f, 1.0f, 0.0f)) ^ (L1 ^ (new V3(0.0f, 1.0f, 0.0f)));
            L2 = (new V3(0.0f, 1.0f, 0.0f)) ^ (L2 ^ (new V3(0.0f, 1.0f, 0.0f)));
            // 4 sommets : origin, origin + L1, origin + L2, origin + L1 + L2
            for (V3 u = origin; (u.x < origin.x + Math.Abs(L1.x) || L1.x == 0) && (u.y < origin.y + Math.Abs(L1.y) || L1.y == 0) && (u.z < origin.z + Math.Abs(L1.z) || L1.z == 0); u += (L1 * (float)pas))
            {
                for (V3 v = new V3(0.0f, 0.0f, 0.0f); (v.x < Math.Abs(L2.x) || L2.x == 0) && (v.y < origin.y + Math.Abs(L2.y) || L2.y == 0) && (v.z < Math.Abs(L2.z) || L2.z == 0); v += (L2 * (float)pas))
                {
                    float x = u.x + v.x;
                    float y = u.y + v.y;
                    float z = u.z + v.z;

                    int x_ecran = (int)x;
                    int z_ecran = (int)z;

                    Couleur C_obj = T.LireCouleur((float)(u.Norm()/L1.Norm()), (float)(v.Norm() / L2.Norm()));
                    final_ambiant = C_obj * C_ambiant;

                    if (y < ZBuffer.zbuffer[x_ecran, z_ecran])
                    {
                        /* UPDATE ZBUFFER */
                        ZBuffer.zbuffer[x_ecran, z_ecran] = y;

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
                        V3 O = new V3(x, y, z);
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
    }
}

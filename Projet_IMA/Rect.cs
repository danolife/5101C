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

        public Rect(V3 origin, V3 L1, V3 L2, V3 N, Texture T, Texture T_bump)
        {
            pas = 0.005;
            this.origin = origin;
            this.L1 = L1;
            this.L2 = L2;
            this.N = N;
            this.T = T;
            this.T_bump = T_bump;
        }

        public override void Draw(Couleur C_ambiant, Couleur C_lampe, V3 L)
        {
            /*for (int i = 100; i < 200; i++)
            {
                for (int j = 100; j < 200; j++)
                {
                    BitmapEcran.DrawPixel(i, j, new Couleur(1.0f, 1.0f, 1.0f));
                }
            }*/
            L1 = (new V3(0.0f, 1.0f, 0.0f)) ^ (L1 ^ (new V3(0.0f, 1.0f, 0.0f)));
            L2 = (new V3(0.0f, 1.0f, 0.0f)) ^ (L2 ^ (new V3(0.0f, 1.0f, 0.0f)));
            // 4 sommets : origin, origin + L1, origin + L2, origin + L1 + L2
            for (V3 u = origin; (u.x <= origin.x + Math.Abs(L1.x)) && (u.y <= origin.y + Math.Abs(L1.y)) && (u.z <= origin.z + Math.Abs(L1.z)); u += (L1 * (float)pas))
            {
                for (V3 v = new V3(0.0f, 0.0f, 0.0f); (v.x <= Math.Abs(L2.x)) && (v.y <= origin.y + Math.Abs(L2.y)) && (v.z <=Math.Abs(L2.z)); v += (L2 * (float)pas))
                {
                    BitmapEcran.DrawPixel((int)(u.x + v.x), (int)(u.z + v.z), new Couleur(1.0f, 1.0f, 1.0f));
                }
            }
        }
    }
}

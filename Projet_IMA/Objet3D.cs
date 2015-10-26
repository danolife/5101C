using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Objet3D
    {
        protected V3 origin; //un des coins du rectangle, ou le centre de la sphere
        protected Texture T, T_bump;
        protected double pas;

        abstract public void Draw(Couleur C_ambiant, Lumiere L);

        public Couleur computeLights(Couleur C_obj, Couleur C_ambiant, V3 N, V3 O, Lumiere L, int k)
        {
            float cosln;
            Couleur final_ambiant, final_diff, final_spec;
            final_ambiant = C_obj * C_ambiant;

            /* DIFFUS */
            N.Normalize();
            L.getDirection().Normalize();
            cosln = L.getDirection() * N;
            cosln = cosln < 0 ? 0 : cosln;
            final_diff = C_obj * L.getClampe() * cosln;
            /* FIN DIFFUS */

            /* SPECULAIRE */
            V3 S = 2 * N * cosln - L.getDirection();
            S.Normalize();
            O.Normalize();
            double cosso = S * O;
            final_spec = L.getClampe() * (float)Math.Pow(cosso, k);
            /* FIN SPECULAIRE */

            return new Couleur(final_ambiant + final_diff + final_spec);
        }
    }
}

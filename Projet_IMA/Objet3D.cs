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

        //abstract public void Draw(Couleur C_ambiant, Lumiere L, V3 camera);
        abstract public float getIntersect(V3 R, V3 cam);
        abstract public Couleur DrawPoint(Couleur C_ambiant, List<Lampe> lampList, V3 camera, V3 R, bool[] occs);

        public Couleur computeLights(Point I, List<Lampe> lampList, bool[] occs)
        {
            Couleur C_obj = I.C_obj;
            Couleur C_ambiant = I.C_ambiant;
            V3 N = I.N;
            V3 O = I.O;
            int k = I.k;

            float cosln;
            Couleur black = new Couleur(0f, 0f, 0f);
            Couleur final_ambiant;
            Couleur final_diff = black;
            Couleur final_spec = black;
            Couleur finalColor = black;
            final_ambiant = C_obj * C_ambiant;

            
            for(int i = 0; i < lampList.Count; i++)
            {
                Lumiere L = (Lumiere)lampList.ElementAt(i);
                if (occs[i] == false)
                {
                    /* DIFFUS */
                    N.Normalize();
                    L.getDirection().Normalize();
                    cosln = L.getDirection() * N;
                    cosln = cosln < 0 ? 0 : cosln;
                    final_diff += C_obj * L.getClampe() * cosln;
                    /* FIN DIFFUS */

                    /* SPECULAIRE */
                    V3 S = 2 * N * cosln - L.getDirection();
                    S.Normalize();
                    O.Normalize();
                    double cosso = S * O;
                    final_spec += L.getClampe() * (float)Math.Pow(cosso, k);
                    /* FIN SPECULAIRE */

                    finalColor += final_diff + final_spec;
                }
            }

            finalColor += final_ambiant;
            //Console.WriteLine("Couleur 1 " + finalColor.R + " " + finalColor.V + " " + finalColor.B);
            finalColor.check();
            //Console.WriteLine("Couleur 2 " + finalColor.R + " " + finalColor.V + " " + finalColor.B);
            return finalColor;
        }

        public bool[] occultation(V3 R, List<Objet3D> objList, List<Lampe> lampList)
        {
            V3 Rd;
            float t;
            bool[] occs = new bool[lampList.Count];
            // créer un rayon de R vers chaque Lampe
            for (int i = 0; i < lampList.Count; i++ )
            {
                Lumiere L = (Lumiere)lampList.ElementAt(i);
                if (L.getType() == 0)
                { // directional : direction L.getDirection();
                    Rd = L.getDirection();
                }
                else
                {
                    Rd = L.getPosition() - R;
                }
                Rd.Normalize();

                occs[i] = false;
                foreach (Objet3D obj in objList)
                {
                    t = obj.getIntersect(Rd, R);
                    //if (t != -1f && t != 0f) // si il y a une intersection
                    if(t > 0.01f)
                    {
                        occs[i] = true; // on occulte la lampe
                    }
                }

            }

            return occs;
        }
    }
}

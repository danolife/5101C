using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Point
    {
        public Couleur C_obj, C_ambiant;
        public V3 N, O;
        public int k;

        public Point()
        {
            this.C_obj = new Couleur (1.0f, 1.0f, 1.0f);
            this.C_ambiant = new Couleur(0.2f, 0.2f, 0.2f);
            this.N = new V3(0, -1, 0);
            this.O = new V3(0, 0, 0);
            this.k = 30;
        }

        public Point(Couleur C_obj, Couleur C_ambiant, V3 N, V3 O, int k)
        {
            this.C_obj = C_obj;
            this.C_ambiant = C_ambiant;
            this.N = N;
            this.O = O;
            this.k = k;
        }

    }
}

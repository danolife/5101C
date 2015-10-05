using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    abstract class Objet3D
    {
        public int x_center, y_center, z_center;
        public Texture T, T_bump;

        abstract public void Draw(Couleur C_ambiant, Couleur C_lampe, V3 L);
    }
}

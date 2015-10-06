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

        abstract public void Draw(Couleur C_ambiant, Couleur C_lampe, V3 L);
    }
}

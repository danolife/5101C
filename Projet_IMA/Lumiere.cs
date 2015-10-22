using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Lumiere : Lampe // fille
    {
        public Lumiere(){
            type = 0;
            direction = new V3(0.0f,0.0f,0.0f);
            position = new V3(0.0f, 0.0f, 0.0f);
            C_lampe = new Couleur(1.0f, 1.0f, 1.0f);
        }

        public Lumiere(int pType, V3 pDirection, V3 pPosition, Couleur pC_lampe){
            this.type = pType;
            this.direction = pDirection;
            this.position = pPosition;
            this.C_lampe = pC_lampe;
        }
    }
}

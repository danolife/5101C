using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Lumiere : Lampe // fille
    {
        int type;
        V3 position;

        public Lumiere(){
            type = 0;
            position = new V3(0.0f,0.0f,0.0f);
        }

        public Lumiere(int pType, V3 pPosition){
            type = pType;
            position = pPosition;
        }
    }
}

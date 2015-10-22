using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    class Lampe //maman
    {
        protected int type;
        protected V3 direction;
        protected V3 position;
        protected Couleur C_lampe;

        public int getType(){
            return this.type;
        }
        public void setType(int pType)
        {
            this.type = pType;
        }
        public V3 getDirection()
        {
            return this.direction;
        }
        public void setDirection(V3 pDirection)
        {
            this.direction = pDirection;
        }
        public V3 getPosition()
        {
            return this.position;
        }
        public void setPosition(V3 pPosition)
        {
            this.position = pPosition;
        }
        public Couleur getClampe()
        {
            return this.C_lampe;
        }
        public void setClampe(Couleur pClampe)
        {
            this.C_lampe = pClampe;
        }
    }
}

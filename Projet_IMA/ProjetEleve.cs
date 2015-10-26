using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Go()
        {
            ZBuffer.init();

            Lumiere L = new Lumiere(0, new V3(1.0f, -1.0f, 1.0f), new V3(0.0f, 0.0f, 0.0f), new Couleur(0.8f, 0.8f, 0.8f));

            Couleur C_ambiant = new Couleur(0.2f, 0.2f, 0.2f);
            Couleur C_lampe = new Couleur(0.8f, 0.8f, 0.8f);

            Texture T_bump = new Texture("bump38.jpg");

            V3 camera = new V3(200, -1000, 200);
            
            Sphere s1 = new Sphere(new V3(300, 0, 300), 200, new Texture("carreau.jpg"), T_bump);
            Sphere s2 = new Sphere(new V3(450, 0, 200), 100, new Texture("lead.jpg"), null);
            s1.Draw(C_ambiant, L, camera);
            s2.Draw(C_ambiant, L, camera);

            //Rect r1 = new Rect(new V3(150, 0, 150), new V3(300, 0, 0), new V3(0, 0, 300), new Texture("carreau.jpg"), T_bump);
            //Rect r2 = new Rect(new V3(100, 0, 100), new V3(100, 0, 0), new V3(100, 0, 100), null, null);
            //r1.Draw(C_ambiant, L, camera);
            //r2.Draw(C_ambiant, L, camera);
        }
    }
}
